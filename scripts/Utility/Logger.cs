// Logger.cs //

#nullable enable

using System;
using System.IO;
using Godot;

namespace Dotter
{
	/// <summary>
	///   Possible log message types.
	/// </summary>
	public enum LogType
	{
		/// <summary>
		///   For logging unrecoverable errors.
		/// </summary>
		Error,
		/// <summary>
		///   For logging recoverable warnings.
		/// </summary>
		Warning,
		/// <summary>
		///   For logging debug information.
		/// </summary>
		Debug,
		/// <summary>
		///   For logging standard messages.
		/// </summary>
		Info
	}

	/// <summary>
	///   Handles logging functionality.
	/// </summary>
	public static class Logger
	{
		/// <summary>
		///   If logs should be written to file.
		/// </summary>
		public static bool Enabled
		{
			get; set;
		} = true;

		/// <summary>
		///   If a file exists at <see cref="LogPath"/>.
		/// </summary>
		public static bool LogFileExists()
		{
			return File.Exists( FilePaths.Log );
		}

		/// <summary>
		///   Logs a message with the given log type.
		/// </summary>
		/// <param name="msg">
		///   The log message.
		/// </param>
		/// <param name="l">
		///   The log type.
		/// </param>
		public static void Log( string msg, LogType l = LogType.Info )
		{
		#if !DEBUG
			if( !Enabled || l == LogType.Debug )
				return;
		#endif

			if( l is not LogType.Info )
				msg = $"{ Enum.GetName( typeof( LogType ), l )?.ToUpper() }: { msg }";

			lock( _logsync )
			{
			#if DEBUG
				if( l is LogType.Error )
					GD.PrintErr( msg );
				else
					GD.Print( msg );
			#endif
				if( Enabled )
				{
					string datetime = $"{ DateTime.Now.ToLongDateString() } { DateTime.Now.ToLongTimeString() } | ";
				
					try
					{
						File.AppendAllText( FilePaths.Log, $"{ datetime }{ msg }\r\n" );
					}
					catch
					{
					#if DEBUG
						GD.PrintErr( $"Unable to log message to file: {msg}." );
					#endif
					}
				}
			}
		}
		/// <summary>
		///   Logs a message to the log stream before returning a value.
		/// </summary>
		/// <typeparam name="T">
		///   The type of value to return.
		/// </typeparam>
		/// <param name="msg">
		///   The log message.
		/// </param>
		/// <param name="val">
		///   The value to return.
		/// </param>
		/// <param name="l">
		///   The log type.
		/// </param>
		/// <returns>
		///   Returns <paramref name="val"/>.
		/// </returns>
		public static T LogReturn<T>( string msg, T val, LogType l = LogType.Info )
		{
			Log( msg, l );
			return val;
		}

		/// <summary>
		///   Deletes the log file.
		/// </summary>
		public static void DeleteLogFile()
		{
			if( !LogFileExists() )
				return;

			try
			{
				lock( _logsync )
				{
					File.Delete( FilePaths.Log );
				}
			}
		#if DEBUG
			catch( ArgumentException )
			{
				Log( "Unable to delete log file: Path contains invalid characters.", LogType.Error );
			}
			catch( NotSupportedException )
			{
				Log( "Unable to delete log file: Path is in an invalid format.", LogType.Error );
			}
			catch( UnauthorizedAccessException )
			{
				Log( "Unable to delete log file: Insufficient permissions or the file is readonly.", LogType.Error );
			}
			catch( PathTooLongException )
			{
				Log( "Unable to delete log file: Path is too long.", LogType.Error );
			}
			catch( DirectoryNotFoundException )
			{
				Log( "Unable to delete log file: Path is invalid (is it on an unmapped drive?).", LogType.Error );
			}
			catch( IOException )
			{
				Log( "Unable to delete log file: The file is in use.", LogType.Error );
			}
			catch( Exception e )
			{
				Log( $"Unable to delete log file: { e.Message }", LogType.Error );
			}
		#else
			catch
			{ }
		#endif
		}

		private static readonly object _logsync = new();
	}
}
