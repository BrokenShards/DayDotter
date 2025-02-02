// OpenTime.cs //

#nullable enable

using System;

namespace Dotter
{
	public class OpenTime
	{
		public OpenTime()
		{
			m_open  = new();
			m_close = new();
		}
		public OpenTime( TimeOfDay open, TimeOfDay close )
		{
			m_open  = open;
			m_close = close < open ? open : close;
		}

		public TimeOfDay Open
		{
			get => m_open;
			set
			{
				m_open = value;

				if( m_open > Close )
					Close = m_open;
			}
		}
		public TimeOfDay Close
		{
			get => m_close;
			set
			{
				m_close = value;

				if( m_close < Open )
					Open = m_close;
			}
		}

		public bool FromString( string str )
		{
			if( string.IsNullOrWhiteSpace( str ) )
				return false;
			
			string[] split = str.Split( ',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

			if( split.Length != 2 )
				return false;

			if( !Open.FromString( split[ 0 ] ) || !Close.FromString( split[ 1 ] ) )
				return false;

			return true;
		}

		public override string ToString()
		{
			return $"{ Open }, { Close }";
		}

		private TimeOfDay m_open;
		private TimeOfDay m_close;
	}
}
