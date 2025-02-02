// BinarySerializable.cs //

#nullable enable

using System;
using System.IO;

namespace Dotter
{
	/// <summary>
	///   Base class for objects that are be serialized/deserialized to/from 
	///   binary files.
	/// </summary>
	[Serializable]
	public abstract class BinarySerializable : IBinarySerializable
	{
		/// <summary>
		///   Attempts to deserialize the object from the stream.
		/// </summary>
		/// <param name="sr">
		///   Stream reader.
		/// </param>
		/// <returns>
		///   True if deserialization succeeded and false otherwise.
		/// </returns>
		public abstract bool LoadFromStream( BinaryReader sr );
		/// <summary>
		///   Attempts to serialize the object to the stream.
		/// </summary>
		/// <param name="sw">
		///   Stream writer.
		/// </param>
		/// <returns>
		///   True if serialization succeeded and false otherwise.
		/// </returns>
		public abstract bool SaveToStream( BinaryWriter sw );

		/// <summary>
		///   Constructs an object of type T and attempts to deserialize from file.
		/// </summary>
		/// <typeparam name="T">
		///   The type of object to deserialize.
		/// </typeparam>
		/// <param name="path">
		///   The file path.
		/// </param>
		/// <returns>
		///   A new object of type T deserialzed from file on success, otherwise null.
		/// </returns>
		public static T? FromFile<T>( string path ) where T : class, IBinarySerializable, new()
		{
			T? val = new();

			try
			{
				using FileStream str = File.OpenRead( path );
				using BinaryReader r = new( str );

				if( !val.LoadFromStream( r ) )
					return null;
			}
			catch
			{
				return null;
			}

			return val;
		}
		/// <summary>
		///   Constructs an object of type T and attempts to deserialize from file.
		/// </summary>
		/// <typeparam name="T">
		///   The type of object to deserialize.
		/// </typeparam>
		/// <param name="path">
		///   The file path.
		/// </param>
		/// <returns>
		///   A new object of type T deserialzed from file on success, otherwise null.
		/// </returns>
		public static bool FromFile<T>( T val, string path ) where T : IBinarySerializable
		{
			try
			{
				using FileStream str = File.OpenRead( path );
				using BinaryReader r = new( str );

				if( !val.LoadFromStream( r ) )
					return false;
			}
			catch
			{
				return false;
			}

			return true;
		}
		/// <summary>
		///   Attempts to serialize an object to file.
		/// </summary>
		/// <param name="t">
		///   The object to save to file.
		/// </param>
		/// <param name="path">
		///   The file path.
		/// </param>
		/// <param name="replace">
		///   If a file already exists at <paramref name="path"/>, should it be replaced?
		/// </param>
		/// <returns>
		///   True if <paramref name="path"/> is valid and data is successfully written to file, 
		///   otherwise false. Also returns false if a file already exists at <paramref name="path"/>
		///   and <paramref name="replace"/> is false.
		/// </returns>
		public static bool ToFile<T>( T t, string path, bool replace = false ) where T : IBinarySerializable
		{
			try
			{
				if( File.Exists( path ) )
				{
					if( !replace )
						return Logger.LogReturn( $"Unable to save file { path }: File already exists and replace is false.", false, LogType.Error );

					File.Delete( path );
				}

				using FileStream str = File.OpenWrite( path );
				using BinaryWriter r = new( str );

				if( !t.SaveToStream( r ) )
					return Logger.LogReturn( $"Unable to save object to file { path }: Saving to stream failed.", false, LogType.Error );
			}
			catch( Exception e )
			{
				return Logger.LogReturn( $"Unable to save object to file { path }: { e.Message }.", false, LogType.Error );
			}

			return true;
		}
	}

	public static class Binary
	{
		public static DateTime? ReadDateTime( BinaryReader br )
		{
			DateTime? dt;

			try
			{
				dt = new DateTime( br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32() );
			}
			catch
			{
				return null;
			}

			return dt;
		}
		public static TimeSpan? ReadTimeSpan( BinaryReader br )
		{
			TimeSpan? dt;

			try
			{
				dt = new TimeSpan( br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32(), br.ReadInt32() );
			}
			catch
			{
				return null;
			}

			return dt;
		}

		public static bool Write( DateTime dt, BinaryWriter bw )
		{
			try
			{
				bw.Write( dt.Year );
				bw.Write( dt.Month );
				bw.Write( dt.Day );
				bw.Write( dt.Hour );
				bw.Write( dt.Minute );
				bw.Write( dt.Second );
				bw.Write( dt.Millisecond );
			}
			catch
			{
				return false;
			}

			return true;
		}
		public static bool Write( TimeSpan ts, BinaryWriter bw )
		{
			try
			{
				bw.Write( ts.Days );
				bw.Write( ts.Hours );
				bw.Write( ts.Minutes );
				bw.Write( ts.Seconds );
				bw.Write( ts.Milliseconds );
			}
			catch
			{
				return false;
			}

			return true;
		}
	}

}
