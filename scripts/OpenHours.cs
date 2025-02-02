// OpenHours.cs //

#nullable enable

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Dotter
{
	public class OpenHours : XmlLoadable
	{
		private OpenHours()
		{
			m_opentimes = new OpenTime[ Enum.GetValues<DayOfWeek>().Length ];
						
			for( int i = 0; i < m_opentimes.Length; i++ )
				m_opentimes[ i ] = new();
			
			if( !Load() )
				SetDefaults();
		}
		public static OpenHours Instance
		{
			get
			{
				if( _instance == null )
				{
					lock( _syncRoot )
					{
						_instance ??= new OpenHours();
					}
				}

				return _instance;
			}
		}
		
		public OpenTime Get( DayOfWeek day )
		{
			int index = (int)day;

			if( index < 0 || index >= m_opentimes.Length )
				throw new IndexOutOfRangeException();
			
			return m_opentimes[ index ];
		}

		public DateTime ClipToOpenHours( DateTime date )
		{
			OpenTime otime = Get( date.DayOfWeek );

			if( otime.Open == otime.Close )
				return date;

			if( date.Hour < otime.Open.Hours || ( date.Hour == otime.Open.Hours && date.Minute <= otime.Open.Minutes ) )
			{
				int prev = (int)date.DayOfWeek - 1;

				if( prev < 0 )
					prev = m_opentimes.Length - 1;

				OpenTime ptime = Get( (DayOfWeek)prev );
				DateTime time  = date.Subtract( TimeSpan.FromDays( 1 ) );

				return new DateTime( time.Year, time.Month, time.Day, ptime.Close.Hours, ptime.Close.Minutes, 0 );
			}
			if( date.Hour > otime.Close.Hours || ( date.Hour == otime.Close.Hours && date.Minute > otime.Close.Minutes ) )
				return new DateTime( date.Year, date.Month, date.Day, otime.Close.Hours, otime.Close.Minutes, 0 );
			
			return date;
		}

		public bool Load()
		{
			if( !File.Exists( FilePaths.OpenTimes ) )
				if( !DownloadPreset() )
					return false;

			return LoadFromFile( FilePaths.OpenTimes );
		}
		public bool Save()
		{
			return ToFile( this, FilePaths.OpenTimes, true );
		}

		public static bool DownloadPreset()
		{
			return Utility.DownloadFile( DownloadPaths.OpenTimes, FilePaths.OpenTimes );
		}

		public override bool LoadFromXml( XmlElement element )
		{
			for( int i = 0; i < m_opentimes.Length; i++ )
			{
				string? dayname = Enum.GetName( (DayOfWeek)i );

				if( dayname == null || !element.HasAttribute( dayname ) )
					return false;
				if( !m_opentimes[ i ].FromString( element.GetAttribute( dayname ) ) )
					return false;
			}

			return true;
		}
		public override string ToString()
		{
			StringBuilder sb = new();

			string name = nameof( OpenHours );

			for( int i = 0; i < name.Length + 2; i++ )
				sb.Append( ' ' );

			string space = sb.ToString();
			sb.Clear();

			sb.Append( '<' ).Append( name ).Append( ' ' );
			
			for( int i = 0; i < m_opentimes.Length; i++ )
			{
				string? dayname = Enum.GetName( (DayOfWeek)i );

				if( dayname == null )
					return string.Empty;
				
				if( i > 0 )
					sb.Append( space );
				
				sb.Append( dayname ).Append( "=\"" ).Append( m_opentimes[ i ].ToString() ).Append( '\"' );

				if( i + 1 < m_opentimes.Length )
					sb.AppendLine();
			}
			
			sb.Append( "/>" );
			return sb.ToString();
		}

		private void SetDefaults()
		{
			for( int i = 0; i < m_opentimes.Length; i++ )
			{
				DayOfWeek day = (DayOfWeek)i;

				m_opentimes[ i ] = new( day == DayOfWeek.Sunday ? new( 8 ) :
				                        day == DayOfWeek.Saturday ? new( 6 ) :
										new( 5, 30 ),
										day == DayOfWeek.Sunday || 
										day == DayOfWeek.Saturday ? new( 18 ) :
										new( 19, 30 ) );
			}
		}
		
		private readonly OpenTime[] m_opentimes;
		private static volatile OpenHours? _instance;
		private static readonly object _syncRoot = new();
	}
}
