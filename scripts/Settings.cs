// Settings.cs //

using System.IO;
using System.Text;
using System.Xml;

namespace Dotter
{
	public class Settings : XmlLoadable
	{
		private Settings()
		{
			if( !File.Exists( FilePaths.Settings ) )
			{
				SetDefaults();
				Save();
			}
			else if( !Load() )
				SetDefaults();
		}

		public static Settings Instance
		{
			get
			{
				if( _instance == null )
				{
					lock( _syncRoot )
					{
						if( _instance == null )
							_instance = new Settings();
					}
				}

				return _instance;
			}
		}

		public bool Use24HourClock
		{
			get; set;
		}

		public bool Load()
		{
			if( !File.Exists( FilePaths.Settings ) )
				return false;
			
			return LoadFromFile( FilePaths.Settings );
		}
		public bool Save()
		{
			return ToFile( this, FilePaths.Settings, true );
		}
		public void SetDefaults()
		{
			Use24HourClock = false;
		}

		public override bool LoadFromXml( XmlElement element )
		{
			if( !element.HasAttribute( nameof( Use24HourClock ) ) )
				return false;

			try
			{
				Use24HourClock = bool.Parse( element.GetAttribute( nameof( Use24HourClock ) ) );
			}
			catch
			{
				return false;
			}

			return true;
		}
		public override string ToString()
		{
			StringBuilder sb = new();

			string name = nameof( Settings );

			for( int i = 0; i < name.Length + 2; i++ )
				sb.Append( ' ' );

			string space = sb.ToString();
			sb.Clear();

			sb.Append( '<' ).Append( name ).Append( ' ' ).Append( nameof( Use24HourClock ) ).Append( "=\"" ).Append( Use24HourClock ).Append( "\"/>" );

			return sb.ToString();
		}

		private static volatile Settings _instance = null;
		private static readonly object _syncRoot = new();
	}
}