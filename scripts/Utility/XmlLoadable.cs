// XmlLoadable.cs //

#nullable enable

using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Dotter
{
	/// <summary>
	///   Base class for objects that can be loaded from an xml element.
	/// </summary>
	[Serializable]
	public abstract class XmlLoadable : IXmlLoadable
	{
		/// <summary>
		///   Attempts to load the object from the xml element.
		/// </summary>
		/// <param name="element">
		///   The xml element.
		/// </param>
		/// <returns>
		///   True if the object was successfully loaded and false otherwise.
		/// </returns>
		public abstract bool LoadFromXml( XmlElement element );
		/// <summary>
		///   Attempts to load the object from an xml file.
		/// </summary>
		/// <param name="path">
		///   The path to the xml file.
		/// </param>
		/// <param name="xpath">
		///   Optional xpath expression to select a single node to load from.
		/// </param>
		/// <param name="nsm">
		///   Optional xml namespace manager.
		/// </param>
		/// <returns>
		///   True on success and false on failure.
		/// </returns>
		public bool LoadFromFile( string path, string? xpath = null, XmlNamespaceManager? nsm = null )
		{
			XmlDocument doc = new();

			try
			{
				doc.Load( path );
			}
			catch( Exception e )
			{
				return Logger.LogReturn( $"Loading XmlLoadable from xml element failed: { e.Message }.", false, LogType.Error );
			}

			return FromXmlDoc( doc, xpath, nsm );
		}

		/// <summary>
		///   Converts the object to an xml string.
		/// </summary>
		/// <returns>
		///   Returns the object to an xml string.
		/// </returns>
		public abstract override string ToString();
		/// <summary>
		///   Converts the object to an xml string with the given indentation level.
		/// </summary>
		/// <param name="indent">
		///   Indentation level.
		/// </param>
		/// <returns>
		///   Returns the object to an xml string with the given indentation level.
		/// </returns>
		public string ToString( uint indent )
		{
			return ToString( this, indent );
		}

		private bool FromXmlDoc( XmlDocument doc, string? xpath = null, XmlNamespaceManager? nsm = null )
		{
			if( string.IsNullOrWhiteSpace( xpath ) )
				xpath = null;

			XmlElement? dele = doc.DocumentElement;

			if( dele is null )
				return Logger.LogReturn( "Loading XmlLoadable failed: Unable to get DocumentElement.", false, LogType.Error );

			try
			{
				if( xpath is null && !LoadFromXml( dele ) )
					return Logger.LogReturn( "Loading XmlLoadable from root element failed.", false, LogType.Error );
				else if( xpath is not null )
				{
					XmlElement? nele = nsm is null ? (XmlElement?)doc.SelectSingleNode( xpath ) :
													 (XmlElement?)doc.SelectSingleNode( xpath, nsm );

					if( nele is null )
						return Logger.LogReturn( "Loading XmlLoadable failed: Unable to get xpath selected node.", false, LogType.Error );
					if( !LoadFromXml( nele ) )
						return Logger.LogReturn( "Loading XmlLoadable from xpath element failed.", false, LogType.Error );
				}
			}
			catch( Exception e )
			{
				return Logger.LogReturn( $"Loading XmlLoadable from xml failed: { e.Message }.", false, LogType.Error );
			}

			return true;
		}

		/// <summary>
		///   Converts the object to an xml string with optional indentation.
		/// </summary>
		/// <param name="xl">
		///   The object.
		/// </param>
		/// <param name="indent">
		///   Indentation level.
		/// </param>
		/// <returns>
		///   Returns the object to an xml string with the given indentation level.
		/// </returns>
		public static string ToString( IXmlLoadable xl, uint indent = 0 )
		{
			string[] lines;
			{
				string data = xl.ToString()?.Replace( "\r\n", "\n" ) ?? string.Empty;
				lines = data.Split( '\n' );
			}

			StringBuilder sb = new();

			for( int i = 0; i < lines.Length; i++ )
			{
				if( i + i < lines.Length )
					sb.AppendLine( lines[ i ].TrimEnd() );
				else
					sb.Append( lines[ i ].TrimEnd() );
			}

			return Xml.Indent( sb.ToString(), indent );
		}

		/// <summary>
		///   Attempts to create a new object from the xml element.
		/// </summary>
		/// <typeparam name="T">
		///   The type of object to load.
		/// </typeparam>
		/// <param name="element">
		///   The xml element.
		/// </param>
		/// <returns>
		///   A valid object of type T loaded from the xml element on success and null on failure.
		/// </returns>
		public static T? FromElement<T>( XmlElement element ) where T : class, IXmlLoadable, new()
		{
			T val = new();

			if( !val.LoadFromXml( element ) )
				return Logger.LogReturn<T?>( "Failed loading XmlLoadable from XmlElement.", null, LogType.Error );
			
			return val;
		}
		/// <summary>
		///   Attempts to create a new object loaded from xml at the given path.
		/// </summary>
		/// <typeparam name="T">
		///   The type to load.
		/// </typeparam>
		/// <param name="path">
		///   The path of the xml document.
		/// </param>
		/// <param name="xpath">
		///   Optional xpath expression to select a single node to load from.
		/// </param>
		/// <param name="nsm">
		///   Optional xml namespace manager.
		/// </param>
		/// <returns>
		///   A valid object of type T on success and null on failure.
		/// </returns>
		public static T? FromFile<T>( string path, string? xpath = null, XmlNamespaceManager? nsm = null ) where T: class, IXmlLoadable, new()
		{
			XmlDocument doc = new();

			try
			{
				doc.Load( path );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<T?>( $"Loading XmlLoadable from xml element failed: { e.Message }.", null, LogType.Error );
			}

			return FromXmlDoc<T>( doc, xpath, nsm );
		}
		/// <summary>
		///   Attempts to create a new object from a string.
		/// </summary>
		/// <typeparam name="T">
		///   The type to load.
		/// </typeparam>
		/// <param name="xml">
		///   The xml string.
		/// </param>
		/// <param name="xpath">
		///   Optional xpath expression to select a single node to load from.
		/// </param>
		/// <param name="nsm">
		///   Optional xml namespace manager.
		/// </param>
		/// <returns>
		///   A valid object of type T on success and null on failure.
		/// </returns>
		public static T? FromXml<T>( string xml, string? xpath = null, XmlNamespaceManager? nsm = null ) where T : class, IXmlLoadable, new()
		{
			XmlDocument doc = new();

			try
			{
				doc.LoadXml( xml );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<T?>( $"Failed loading XmlDocument from xml: { e.Message }.", null, LogType.Error );
			}

			return FromXmlDoc<T>( doc, xpath, nsm );
		}

		/// <summary>
		///   Attempts to save XmlLoadable object to file.
		/// </summary>
		/// <remarks>
		///   Please note <see cref="Xml.Header"/> will be written at the beginning of the file
		///   before the file data.
		/// </remarks>
		/// <param name="x">
		///   The object to save.
		/// </param>
		/// <param name="path">
		///   The path to save the object to.
		/// </param>
		/// <param name="overwrite">
		///   If an already existing file should be overwritten.
		/// </param>
		/// <returns>
		///   True if the object was written to file successfully, otherwise false.
		/// </returns>
		public static bool ToFile( IXmlLoadable x, string path, bool overwrite = false )
		{
			if( File.Exists( path ) && !overwrite )
				return false;

			try
			{
				File.WriteAllText( path, $"{ Xml.Header }\r\n{ x }" );
			}
			catch( Exception e )
			{
				return Logger.LogReturn( $"Unable to save XmlLoadable to file: { e.Message }.", false, LogType.Error );
			}

			return true;
		}

		private static T? FromXmlDoc<T>( XmlDocument doc, string? xpath = null, XmlNamespaceManager? nsm = null ) where T : class, IXmlLoadable, new()
		{
			T val = new();

			if( string.IsNullOrWhiteSpace( xpath ) )
				xpath = null;

			XmlElement? dele = doc.DocumentElement;

			if( dele is null )
				return Logger.LogReturn<T?>( "Loading XmlLoadable failed: Unable to get DocumentElement.", null, LogType.Error );

			try
			{
				if( xpath is null && !val.LoadFromXml( dele ) )
					return Logger.LogReturn<T?>( "Loading XmlLoadable from root element failed.", null, LogType.Error );
				else if( xpath is not null )
				{
					XmlElement? nele = nsm is null ? (XmlElement?)doc.SelectSingleNode( xpath ) :
													 (XmlElement?)doc.SelectSingleNode( xpath, nsm );

					if( nele is null )
						return Logger.LogReturn<T?>( "Loading XmlLoadable failed: Unable to get xpath selected node.", null, LogType.Error );
					if( !val.LoadFromXml( nele ) )
						return Logger.LogReturn<T?>( "Loading XmlLoadable from xpath element failed.", null, LogType.Error );
				}
			}
			catch( Exception e )
			{
				return Logger.LogReturn<T?>( $"Loading XmlLoadable from xml failed: { e.Message }.", null, LogType.Error );
			}

			return val;
		}
	}
}
