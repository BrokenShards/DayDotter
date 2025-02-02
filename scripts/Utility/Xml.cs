// Xml.cs //

#nullable enable

using System;
using System.Text;
using System.Xml;

using Godot;

namespace Dotter
{
	/// <summary>
	///   Contains xml helper functionality.
	/// </summary>
	public static class Xml
	{
		/// <summary>
		///   Standard xml header.
		/// </summary>
		public const string Header = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

		/// <summary>
		///   If the given ID is valid.
		/// </summary>
		/// <param name="id">
		///   The ID to check.
		/// </param>
		/// <returns>
		///   True if the ID is valid and false otherwise.
		/// </returns>
		public static bool IsValid( string id )
		{
			if( id.Trim().Length is 0 || ( !char.IsLetter( id[ 0 ] ) && id[ 0 ] is not '_' ) )
				return false;

			for( int i = 0; i < id.Length; i++ )
				if( !char.IsLetterOrDigit( id[ i ] ) && id[ i ] is not '_' )
					return false;

			return true;
		}

		/// <summary>
		///   Gets the vector xml string.
		/// </summary>
		/// <param name="vec">
		///   The vector.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <param name="indent">
		///   Indentation level.
		/// </param>
		/// <returns>
		///   The given vector as an xml string.
		/// </returns>
		public static string ToNodeString( Vector2 vec, string? name = null, uint indent = 0 )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Vector2 );

			StringBuilder sb = new();

			sb.Append( '<' ).Append( name ).Append( ' ' )
				.Append( nameof( Vector2.X )[ 0 ] ).Append( "=\"" ).Append( vec.X ).Append( "\" " )
				.Append( nameof( Vector2.Y )[ 0 ] ).Append( "=\"" ).Append( vec.Y ).Append( "\"/>" );

			return Indent( sb.ToString(), indent );
		}
		public static string ToNodeString( Vector2I vec, string? name = null, uint indent = 0 )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Vector2I );

			StringBuilder sb = new();

			sb.Append( '<' ).Append( name ).Append( ' ' )
				.Append( nameof( Vector2I.X )[ 0 ] ).Append( "=\"" ).Append( vec.X ).Append( "\" " )
				.Append( nameof( Vector2I.Y )[ 0 ] ).Append( "=\"" ).Append( vec.Y ).Append( "\"/>" );

			return Indent( sb.ToString(), indent );
		}

		public static string ToNodeString( Vector3 vec, string? name = null, uint indent = 0 )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Vector3 );

			StringBuilder sb = new();

			sb.Append( '<' ).Append( name ).Append( ' ' )
				.Append( nameof( Vector3.X )[ 0 ] ).Append( "=\"" ).Append( vec.X ).Append( "\" " )
				.Append( nameof( Vector3.Y )[ 0 ] ).Append( "=\"" ).Append( vec.Y ).Append( "\" " )
				.Append( nameof( Vector3.Z )[ 0 ] ).Append( "=\"" ).Append( vec.Z ).Append( "\"/>" );

			return Indent( sb.ToString(), indent );
		}
		public static string ToNodeString( Vector3I vec, string? name = null, uint indent = 0 )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Vector3I );

			StringBuilder sb = new();

			sb.Append( '<' ).Append( name ).Append( ' ' )
				.Append( nameof( Vector3I.X )[ 0 ] ).Append( "=\"" ).Append( vec.X ).Append( "\" " )
				.Append( nameof( Vector3I.Y )[ 0 ] ).Append( "=\"" ).Append( vec.Y ).Append( "\" " )
				.Append( nameof( Vector3I.Z )[ 0 ] ).Append( "=\"" ).Append( vec.Z ).Append( "\"/>" );

			return Indent( sb.ToString(), indent );
		}

		/// <summary>
		///   Gets the rect xml string.
		/// </summary>
		/// <param name="rect">
		///   The rect.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <param name="indent">
		///   Indentation level.
		/// </param>
		/// <returns>
		///   The given rect as an xml string.
		/// </returns>
		public static string ToNodeString( Rect2 rect, string? name = null, uint indent = 0 )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Rect2 );

			StringBuilder sb = new();
						
			for( int i = 0; i < name.Length + 2; i++ )
				sb.Append( ' ' );

			string spaces = sb.ToString();

			sb.Clear();

			sb.Append( '<' ).Append( name ).Append( ' ' )
				.Append( nameof( Rect2.Position ) ).Append( "=\"" ).Append( ToAttributeString( rect.Position ) ).AppendLine( "\"" )
				.Append( spaces )
				.Append( nameof( Rect2.Size ) ).Append( "=\"" ).Append( ToAttributeString( rect.Size ) ).Append( "\"/>" );

			return Indent( sb.ToString(), indent );
		}
		/// <summary>
		///   Gets the rect xml string.
		/// </summary>
		/// <param name="rect">
		///   The rect.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <param name="indent">
		///   Indentation level.
		/// </param>
		/// <returns>
		///   The given rect as an xml string.
		/// </returns>
		public static string ToNodeString( Rect2I rect, string? name = null, uint indent = 0 )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Rect2I );

			StringBuilder sb = new( name.Length + 2 );
						
			for( int i = 0; i < name.Length + 2; i++ )
				sb.Append( ' ' );

			string spaces = sb.ToString();

			sb.Clear();

			sb.Append( '<' ).Append( name ).Append( ' ' )
				.Append( nameof( Rect2I.Position ) ).Append( "=\"" ).Append( ToAttributeString( rect.Position ) ).AppendLine( "\"" )
				.Append( spaces )
				.Append( nameof( Rect2I.Size ) ).Append( "=\"" ).Append( ToAttributeString( rect.Size ) ).Append( "\"/>" );

			return Indent( sb.ToString(), indent );
		}

		/// <summary>
		///   Gets the rect xml string.
		/// </summary>
		/// <param name="col">
		///   The rect.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <param name="indent">
		///   Indentation level.
		/// </param>
		/// <returns>
		///   The given rect as an xml string.
		/// </returns>
		public static string ToNodeString( Color col, string? name = null, uint indent = 0 )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Color );

			StringBuilder sb = new();

			sb.Append( '<' ).Append( name ).Append( ' ' )
				.Append( nameof( Color.R )[ 0 ] ).Append( "=\"" ).Append( col.R ).Append( "\" " )
				.Append( nameof( Color.G )[ 0 ] ).Append( "=\"" ).Append( col.G ).Append( "\" " )
				.Append( nameof( Color.B )[ 0 ] ).Append( "=\"" ).Append( col.B ).Append( "\" " )
				.Append( nameof( Color.A )[ 0 ] ).Append( "=\"" ).Append( col.A ).Append( "\"/>" );

			return Indent( sb.ToString(), indent );
		}

		/// <summary>
		///   Attempts to load a vector from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid vector on success or null on failure.
		/// </returns>
		public static Vector2? ToVec2( XmlElement ele )
		{
			if( !ele.HasAttribute( nameof( Vector2.X ) ) )
				return Logger.LogReturn<Vector2?>( "Unable to load Vector2: No X attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Vector2.Y ) ) )
				return Logger.LogReturn<Vector2?>( "Unable to load Vector2: No Y attribute.", null, LogType.Error );

			Vector2 vec;

			try
			{
				vec = new Vector2( float.Parse( ele.GetAttribute( nameof( Vector2.X ) ) ), 
				                   float.Parse( ele.GetAttribute( nameof( Vector2.Y ) ) ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Vector2?>( $"Unable to load Vector2: { e.Message }", null, LogType.Error );
			}

			return vec;
		}
		/// <summary>
		///   Attempts to load a vector from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid vector on success or null on failure.
		/// </returns>
		public static Vector2I? ToVec2I( XmlElement ele )
		{
			if( !ele.HasAttribute( nameof( Vector2I.X ) ) )
				return Logger.LogReturn<Vector2I?>( "Unable to load Vector2I: No X attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Vector2I.Y ) ) )
				return Logger.LogReturn<Vector2I?>( "Unable to load Vector2I: No Y attribute.", null, LogType.Error );

			Vector2I vec;

			try
			{
				vec = new Vector2I( int.Parse( ele.GetAttribute( nameof( Vector2I.X ) ) ),
									int.Parse( ele.GetAttribute( nameof( Vector2I.Y ) ) ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Vector2I?>( $"Unable to load Vector2I: { e.Message }", null, LogType.Error );
			}

			return vec;
		}

		/// <summary>
		///   Attempts to load a vector from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid vector on success or null on failure.
		/// </returns>
		public static Vector3? ToVec3( XmlElement ele )
		{
			if( !ele.HasAttribute( nameof( Vector3.X ) ) )
				return Logger.LogReturn<Vector3?>( "Unable to load Vector3: No X attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Vector3.Y ) ) )
				return Logger.LogReturn<Vector3?>( "Unable to load Vector3: No Y attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Vector3.Z ) ) )
				return Logger.LogReturn<Vector3?>( "Unable to load Vector3: No Z attribute.", null, LogType.Error );

			Vector3 vec;

			try
			{
				vec = new Vector3( float.Parse( ele.GetAttribute( nameof( Vector3.X ) ) ),
								   float.Parse( ele.GetAttribute( nameof( Vector3.Y ) ) ),
								   float.Parse( ele.GetAttribute( nameof( Vector3.Z ) ) ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Vector3?>( $"Unable to load Vector3: {e.Message}", null, LogType.Error );
			}

			return vec;
		}
		/// <summary>
		///   Attempts to load a vector from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid vector on success or null on failure.
		/// </returns>
		public static Vector3I? ToVec3I( XmlElement ele )
		{
			if( !ele.HasAttribute( nameof( Vector3I.X ) ) )
				return Logger.LogReturn<Vector3I?>( "Unable to load Vector3I: No X attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Vector3I.Y ) ) )
				return Logger.LogReturn<Vector3I?>( "Unable to load Vector3I: No Y attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Vector3I.Z ) ) )
				return Logger.LogReturn<Vector3I?>( "Unable to load Vector3I: No Z attribute.", null, LogType.Error );

			Vector3I vec;

			try
			{
				vec = new Vector3I( int.Parse( ele.GetAttribute( nameof( Vector3I.X ) ) ),
									int.Parse( ele.GetAttribute( nameof( Vector3I.Y ) ) ),
									int.Parse( ele.GetAttribute( nameof( Vector3I.Z ) ) ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Vector3I?>( $"Unable to load Vector3I: {e.Message}", null, LogType.Error );
			}

			return vec;
		}

		/// <summary>
		///   Attempts to load a rect from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid rect on success or null on failure.
		/// </returns>
		public static Rect2? ToRect2( XmlElement ele )
		{
			XmlAttribute? posatt = ele.GetAttributeNode( "Position" ),
			              sizatt = ele.GetAttributeNode( "Size" );

			if( posatt is null )
				return Logger.LogReturn<Rect2?>( "Unable to load Rect2: No Position attribute.", null, LogType.Error );
			if( sizatt is null )
				return Logger.LogReturn<Rect2?>( "Unable to load Rect2: No Size attribute.", null, LogType.Error );

			Rect2 rect;

			try
			{
				Vector2 pos = ToVec2( posatt ) ?? throw new Exception( "Unable to parse Position attribute as Vector2." );
				Vector2 siz = ToVec2( sizatt ) ?? throw new Exception( "Unable to parse Size attribute as Vector2." ); ;

				rect = new Rect2( pos, siz );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Rect2?>( $"Unable to load Rect2: { e.Message }", null, LogType.Error );
			}

			return rect;
		}
		/// <summary>
		///   Attempts to load a rect from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid rect on success or null on failure.
		/// </returns>
		public static Rect2I? ToRect2I( XmlElement ele )
		{
			XmlAttribute? posatt = ele.GetAttributeNode( "Position" ),
			              sizatt = ele.GetAttributeNode( "Size" );

			if( posatt is null )
				return Logger.LogReturn<Rect2I?>( "Unable to load Rect2I: No Position attribute.", null, LogType.Error );
			if( sizatt is null )
				return Logger.LogReturn<Rect2I?>( "Unable to load Rect2I: No Size attribute.", null, LogType.Error );

			Rect2I rect;

			try
			{
				Vector2I pos = ToVec2I( posatt ) ?? throw new Exception( "Unable to parse Position attribute as Vector2." );
				Vector2I siz = ToVec2I( sizatt ) ?? throw new Exception( "Unable to parse Size attribute as Vector2." ); ;

				rect = new Rect2I( pos, siz );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Rect2I?>( $"Unable to load Rect2I: { e.Message }", null, LogType.Error );
			}

			return rect;
		}

		/// <summary>
		///   Attempts to load a color from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid color on success or null on failure.
		/// </returns>
		public static Color? ToColor( XmlElement ele )
		{
			if( !ele.HasAttribute( nameof( Color.R ) ) )
				return Logger.LogReturn<Color?>( "Unable to load Color: No R attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Color.G ) ) )
				return Logger.LogReturn<Color?>( "Unable to load Color: No G attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Color.B ) ) )
				return Logger.LogReturn<Color?>( "Unable to load Color: No B attribute.", null, LogType.Error );
			if( !ele.HasAttribute( nameof( Color.A ) ) )
				return Logger.LogReturn<Color?>( "Unable to load Color: No A attribute.", null, LogType.Error );

			Color col;

			try
			{
				col = new Color( byte.Parse( ele.GetAttribute( nameof( Color.R ) ) ),
								 byte.Parse( ele.GetAttribute( nameof( Color.G ) ) ),
								 byte.Parse( ele.GetAttribute( nameof( Color.B ) ) ),
								 byte.Parse( ele.GetAttribute( nameof( Color.A ) ) ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Color?>( $"Unable to load Color: { e.Message }", null, LogType.Error );
			}

			return col;
		}

		/// <summary>
		///   Gets the vector xml attribute string.
		/// </summary>
		/// <param name="vec">
		///   The vector.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <returns>
		///   The given vector as an xml attribute string.
		/// </returns>
		public static string ToAttributeString( Vector2 vec, string? name = null )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Vector2 );

			return new StringBuilder().Append( name ).Append( "=\"" ).Append( vec.X ).Append( ", " )
			                          .Append( vec.Y ).Append( '"' ).ToString();
		}
		/// <summary>
		///   Gets the vector xml attribute string.
		/// </summary>
		/// <param name="vec">
		///   The vector.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <returns>
		///   The given vector as an xml attribute string.
		/// </returns>
		public static string ToAttributeString( Vector2I vec, string? name = null )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Vector2I );

			return new StringBuilder().Append( name ).Append( "=\"" ).Append( vec.X ).Append( ", " )
									  .Append( vec.Y ).Append( '"' ).ToString();
		}

		/// <summary>
		///   Gets the vector xml attribute string.
		/// </summary>
		/// <param name="vec">
		///   The vector.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <returns>
		///   The given vector as an xml attribute string.
		/// </returns>
		public static string ToAttributeString( Vector3 vec, string? name = null )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Vector3 );

			return new StringBuilder().Append( name ).Append( "=\"" )
			                          .Append( vec.X ).Append( ", " )
									  .Append( vec.Y ).Append( ", " )
									  .Append( vec.Z ).Append( '"' ).ToString();
		}
		/// <summary>
		///   Gets the vector xml attribute string.
		/// </summary>
		/// <param name="vec">
		///   The vector.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <returns>
		///   The given vector as an xml attribute string.
		/// </returns>
		public static string ToAttributeString( Vector3I vec, string? name = null )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Vector3I );

			return new StringBuilder().Append( name ).Append( "=\"" )
									  .Append( vec.X ).Append( ", " )
									  .Append( vec.Y ).Append( ", " )
									  .Append( vec.Z ).Append( '"' ).ToString();
		}

		/// <summary>
		///   Gets the rect xml attribute string.
		/// </summary>
		/// <param name="rect">
		///   The rect.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <returns>
		///   The given rect as an xml attribute string.
		/// </returns>
		public static string ToAttributeString( Rect2 rect, string? name = null )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Rect2 );

			return new StringBuilder().Append( name ).Append( "=\"" )
			                          .Append( ToAttributeString( rect.Position ) ).Append( ", " )
									  .Append( ToAttributeString( rect.Size ) ).Append( '"' ).ToString();
		}
		/// <summary>
		///   Gets the rect xml attribute string.
		/// </summary>
		/// <param name="rect">
		///   The rect.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <returns>
		///   The given rect as an xml attribute string.
		/// </returns>
		public static string ToAttributeString( Rect2I rect, string? name = null )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Rect2I );

			return new StringBuilder().Append( name ).Append( "=\"" )
									  .Append( ToAttributeString( rect.Position ) ).Append( ", " )
									  .Append( ToAttributeString( rect.Size ) ).Append( '"' ).ToString();
		}

		/// <summary>
		///   Gets the rect xml attribute string.
		/// </summary>
		/// <param name="col">
		///   The rect.
		/// </param>
		/// <param name="name">
		///   Xml element name.
		/// </param>
		/// <returns>
		///   The given rect as an xml attribute string.
		/// </returns>
		public static string ToAttributeString( Color col, string? name = null )
		{
			if( name is null || !IsValid( name ) )
				name = nameof( Color );

			return new StringBuilder().Append( name ).Append( "=\"" )
									  .Append( col.R ).Append( ", " )
									  .Append( col.G ).Append( ", " )
									  .Append( col.B ).Append( ", " )
									  .Append( col.A ).Append( '"' ).ToString();
		}


		/// <summary>
		///   Attempts to load a vector from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid vector on success or null on failure.
		/// </returns>
		public static Vector2? ToVec2( XmlAttribute ele )
		{
			string[] args = ele.Value.Split( ", ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( args.Length != 2 )
				return Logger.LogReturn<Vector2?>( "Unable to load Vector2: Invalid amount of attribute parameters.", null, LogType.Error );

			Vector2 vec;

			try
			{
				vec = new Vector2( float.Parse( args[ 0 ] ), float.Parse( args[ 1 ] ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Vector2?>( $"Unable to load Vector2: { e.Message }", null, LogType.Error );
			}

			return vec;
		}
		/// <summary>
		///   Attempts to load a vector from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid vector on success or null on failure.
		/// </returns>
		public static Vector2I? ToVec2I( XmlAttribute ele )
		{
			string[] args = ele.Value.Split( ", ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( args.Length != 2 )
				return Logger.LogReturn<Vector2I?>( "Unable to load Vector2I: Invalid amount of attribute parameters.", null, LogType.Error );

			Vector2I vec;

			try
			{
				vec = new Vector2I( int.Parse( args[ 0 ] ), int.Parse( args[ 1 ] ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Vector2I?>( $"Unable to load Vector2I: { e.Message }", null, LogType.Error );
			}

			return vec;
		}

		/// <summary>
		///   Attempts to load a vector from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid vector on success or null on failure.
		/// </returns>
		public static Vector3? ToVec3( XmlAttribute ele )
		{
			string[] args = ele.Value.Split( ", ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( args.Length != 3 )
				return Logger.LogReturn<Vector3?>( "Unable to load Vector3: Invalid amount of attribute parameters.", null, LogType.Error );

			Vector3 vec;

			try
			{
				vec = new Vector3( float.Parse( args[ 0 ] ), float.Parse( args[ 1 ] ), float.Parse( args[ 2 ] ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Vector3?>( $"Unable to load Vector3: {e.Message}", null, LogType.Error );
			}

			return vec;
		}
		/// <summary>
		///   Attempts to load a vector from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid vector on success or null on failure.
		/// </returns>
		public static Vector3I? ToVec3I( XmlAttribute ele )
		{
			string[] args = ele.Value.Split( ", ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( args.Length != 3 )
				return Logger.LogReturn<Vector3I?>( "Unable to load Vector3I: Invalid amount of attribute parameters.", null, LogType.Error );

			Vector3I vec;

			try
			{
				vec = new Vector3I( int.Parse( args[ 0 ] ), int.Parse( args[ 1 ] ), int.Parse( args[ 2 ] ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Vector3I?>( $"Unable to load Vector3I: {e.Message}", null, LogType.Error );
			}

			return vec;
		}

		/// <summary>
		///   Attempts to load a rect from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid rect on success or null on failure.
		/// </returns>
		public static Rect2? ToRect2( XmlAttribute ele )
		{
			string[] args = ele.Value.Split( ", ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( args.Length != 4 )
				return Logger.LogReturn<Rect2?>( "Unable to load Rect2: Invalid amount of attribute parameters.", null, LogType.Error );

			Rect2 rect;

			try
			{
				rect = new Rect2( float.Parse( args[ 0 ] ), float.Parse( args[ 1 ] ),
				                  float.Parse( args[ 2 ] ), float.Parse( args[ 3 ] ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Rect2?>( $"Unable to load Rect2: { e.Message }", null, LogType.Error );
			}

			return rect;
		}
		/// <summary>
		///   Attempts to load a rect from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid rect on success or null on failure.
		/// </returns>
		public static Rect2I? ToRect2I( XmlAttribute ele )
		{
			string[] args = ele.Value.Split( ", ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( args.Length != 4 )
				return Logger.LogReturn<Rect2I?>( "Unable to load Rect2I: Invalid amount of attribute parameters.", null, LogType.Error );

			Rect2I rect;

			try
			{
				rect = new Rect2I( int.Parse( args[ 0 ] ), int.Parse( args[ 1 ] ),
								   int.Parse( args[ 2 ] ), int.Parse( args[ 3 ] ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Rect2I?>( $"Unable to load Rect2I: { e.Message }", null, LogType.Error );
			}

			return rect;
		}

		/// <summary>
		///   Attempts to load a color from an xml element.
		/// </summary>
		/// <param name="ele">
		///   The element to load from.
		/// </param>
		/// <returns>
		///   A valid color on success or null on failure.
		/// </returns>
		public static Color? ToColor( XmlAttribute ele )
		{
			string[] args = ele.Value.Split( ", ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( args.Length != 4 )
				return Logger.LogReturn<Color?>( "Unable to load Color: Invalid amount of attribute parameters.", null, LogType.Error );

			Color col;

			try
			{
				col = new Color( byte.Parse( args[ 0 ] ), byte.Parse( args[ 1 ] ),
								 byte.Parse( args[ 2 ] ), byte.Parse( args[ 3 ] ) );
			}
			catch( Exception e )
			{
				return Logger.LogReturn<Color?>( $"Unable to load Color: { e.Message }", null, LogType.Error );
			}

			return col;
		}

		/// <summary>
		///   Indents each line of the string a set amount of times.
		/// </summary>
		/// <param name="lines">
		///   The string to indent.
		/// </param>
		/// <param name="indent">
		///   The amount of tabs to use for indentation.
		/// </param>
		/// <returns>
		///   The given string indented with the given amount of tabs, or just tabs if the string is
		///   null or empty.
		/// </returns>
		public static string Indent( string lines, uint indent = 1 )
		{
			string tabs = string.Empty;

			for( uint i = 0; i < indent; i++ )
				tabs += '\t';

			if( lines.Equals( string.Empty ) )
				return tabs;

			return $"{ tabs }{ lines.Replace( "\r\n", "\n" ).Replace( "\n", $"\n{ tabs }" ).Replace( "\n", "\r\n" ) }";
		}
	}
}
