// Paths.cs //

#nullable enable

using System;
using System.IO;
using System.Reflection;
using Godot;

namespace Dotter
{
	/// <summary>
	///   Contains folder paths.
	/// </summary>
	public static partial class FolderPaths
	{
		/// <summary>
		///   Directory separator character.
		/// </summary>
		public static readonly char Separator = Path.DirectorySeparatorChar;
		/// <summary>
		///   Directory separator character as a string.
		/// </summary>
		public static readonly string SeparatorString = new( Separator, 1 );

		/// <summary>
		///   The path to the directory containing the binary executable.
		/// </summary>
		public static string Executable
		{
			get { return FilePaths.Executable.Length == 0 ? string.Empty : $"{ Path.GetDirectoryName( FilePaths.Executable ) }{ Separator }"; }
		}

		/// <summary>
		///   The path to the directory containing user data for the application.
		/// </summary>
		public static string UserData
		{
			get
			{
				if( _data == null )
				{
					lock( _datasync )
					{
						_data ??= GetUserData();
					}
				}

				return _data;
			}
		}
		
		/// <summary>
		///   The path to the directory containing the background images used for day dots.
		/// </summary>
		public const string DayDots = "res://images/dots/";

		private static string GetUserData()
		{
			string path = OS.GetUserDataDir().Replace( '\\', Separator ).Replace( '/', Separator );

			if( path.Length == 0 )
				return string.Empty;

			return path.EndsWith( Separator ) ? path : $"{path}{Separator}";
		}

		private static string? _data = null;
		private static readonly object _datasync = new();
	}

	/// <summary>
	///   Contains file paths.
	/// </summary>
	public static partial class FilePaths
	{
		/// <summary>
		///   The binary executable path.
		/// </summary>
		public static string Executable
		{
			get
			{
				if( _exec == null )
				{
					lock( _execsync )
					{
						_exec ??= GetExecutable();
					}
				}

				return _exec;
			}
		}
		/// <summary>
		///   The path to the settings file.
		/// </summary>
		public static string Settings
		{
			get
			{
				return $"{FolderPaths.UserData}settings.xml";
			}
		}
		/// <summary>
		///   The path to the open times file.
		/// </summary>
		public static string OpenTimes
		{
			get
			{
				return $"{FolderPaths.UserData}times.xml";
			}
		}
		/// <summary>
		///   The path to the product list file.
		/// </summary>
		public static string Products
		{
			get
			{
				return $"{FolderPaths.UserData}products.xml";
			}
		}
		
		/// <summary>
		///   The path to the log file.
		/// </summary>
		public static string Log
		{
			get
			{
				return $"{FolderPaths.UserData}log.txt";
			}
		}

		private static string GetExecutable()
		{
			string path;

			try
			{
				path = Assembly.GetExecutingAssembly().Location
					       .Replace( '\\', FolderPaths.Separator )
						   .Replace( '/', FolderPaths.Separator );
			}
			catch
			{
				return string.Empty;
			}

			if( path.Length == 0 || path.Equals( FolderPaths.SeparatorString ) )
				return string.Empty;

			if( path.StartsWith( $"file:{ new string( FolderPaths.Separator, 3 ) }" ) )
				path = path[ 8.. ];
			else if( path.StartsWith( $"file:{ new string( FolderPaths.Separator, 2 ) }" ) )
				path = path[ 7.. ];

			return path.EndsWith( FolderPaths.Separator ) ? path : $"{path}{FolderPaths.Separator}";
		}
		
		private static string? _exec = null;
		private static readonly object _execsync = new();
	}

	public static partial class DownloadPaths
	{
		public const string OpenTimes = "https://raw.githubusercontent.com/BrokenShards/DayDotter/refs/heads/master/times.xml";
		public const string Products  = "https://raw.githubusercontent.com/BrokenShards/DayDotter/refs/heads/master/products.xml";
	}

	public static partial class ResourcePaths
	{
		public static string? DayDotTexture( DayOfWeek day )
		{
			int index = (int)day;

			string[] names = Enum.GetNames<DayOfWeek>();

			if( index < 0 || index > names.Length )
				return null;

			return $"{ FolderPaths.DayDots }{ names[ index ].ToLower() }.png"; 
		}
		
		public const string ProductEntry = "res://scenes/product_entry.tscn";
	}
}
