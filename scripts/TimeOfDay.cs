// TimeOfDay.cs //

#nullable enable

using System;
using System.Text;

namespace Dotter
{
	public class TimeOfDay : IEquatable<TimeOfDay>, IComparable<TimeOfDay>
	{
		public TimeOfDay( int hours = 0, int minutes = 0 )
		{
			m_hours   = Math.Clamp( hours, 0, 23 );
			m_minutes = Math.Clamp( minutes, 0, 59 );
		}

		public int Hours
		{
			get => m_hours;
			set => m_hours = Math.Clamp( value, 0, 23 );
		}
		public int Minutes
		{
			get => m_minutes;
			set => m_minutes = Math.Clamp( value, 0, 59 );
		}

		public bool FromString( string str )
		{
			if( string.IsNullOrWhiteSpace( str ) )
				return false;
			
			string[] split = str.Split( ':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( split.Length != 2 )
				return false;
			
			try
			{
				Hours   = int.Parse( split[ 0 ] );
				Minutes = int.Parse( split[ 1 ] );
			}
			catch( Exception e )
			{
				Logger.Log( $"Failed parsing TimeOfDay from string: { e.Message }.", LogType.Error );
				return false;
			}

			return true;
		}
		public override string ToString()
		{
			StringBuilder sb = new();

			if( Hours < 10 )
				sb.Append( 0 );
			
			sb.Append( Hours ).Append( ':' );

			if( Minutes < 10 )
				sb.Append( 0 );
			
			sb.Append( Minutes );
			return sb.ToString();
		}

		public bool Equals( TimeOfDay? other )
		{
			return Hours == other?.Hours && Minutes == other?.Minutes;
		}
		public override bool Equals( object? obj )
		{
			return obj is TimeOfDay day && Equals( day );
		}
		public override int GetHashCode()
		{
			return HashCode.Combine( m_hours, m_minutes );
		}

		public int CompareTo( TimeOfDay? other )
		{
			if( other is null )
				throw new ArgumentNullException( nameof( other ) );

			if( Equals( other ) )
				return 0;
			if( Hours < other.Hours || ( Hours == other.Hours && Minutes < other.Minutes ) )
				return -1;

			return 1;
		}

		public static bool operator==( TimeOfDay left, TimeOfDay right )
		{
			return left.Equals( right );
		}
		public static bool operator!=( TimeOfDay left, TimeOfDay right )
		{
			return !( left == right );
		}

		public static bool operator<( TimeOfDay left, TimeOfDay right )
		{
			return left.CompareTo( right ) < 0;
		}

		public static bool operator<=( TimeOfDay left, TimeOfDay right )
		{
			return left.CompareTo( right ) <= 0;
		}

		public static bool operator>( TimeOfDay left, TimeOfDay right )
		{
			return left.CompareTo( right ) > 0;
		}

		public static bool operator>=( TimeOfDay left, TimeOfDay right )
		{
			return left.CompareTo( right ) >= 0;
		}

		private int m_hours,
		            m_minutes;
	}
}
