////////////////////////////////////////////////////////////////////////////////
// Range.cs 
////////////////////////////////////////////////////////////////////////////////
//
// Popcon - A simple, theme-able folder viewer widget.
// Copyright (C) 2021 Michael Furlong <michaeljfurlong@outlook.com>
//
// This program is free software: you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free 
// Software Foundation, either version 3 of the License, or (at your option) 
// any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for 
// more details.
// 
// You should have received a copy of the GNU General Public License along with
// this program. If not, see <https://www.gnu.org/licenses/>.
//
////////////////////////////////////////////////////////////////////////////////

#nullable enable

using System;
using Godot;

namespace Dotter
{
	public interface IRange<T> where T : struct, IComparable, IComparable<T>
	{
		[Export]
		public T Min
		{
			get; set;
		}
		[Export]
		public T Max
		{
			get; set;
		}

		public T Apply( T val )
		{
			if( val.CompareTo( Min ) < 0 )
				return Min;
			if( val.CompareTo( Max ) > 0 )
				return Max;

			return val;
		}
		public bool WithinRange( T val )
		{
			return !( val.CompareTo( Min ) < 0 && val.CompareTo( Max ) > 0 );
		}
	}

	public struct IntRange : IRange<int>
	{
		public IntRange()
		{
			m_min = int.MinValue;
			m_max = int.MaxValue;
		}
		public IntRange( int min, int max )
		{ 
			m_min = min;
			m_max = max < min ? min : max;
		}

		[Export]
		public int Min
		{
			get => m_min;
			set
			{
				m_min = value;
				
				if( m_max.CompareTo( m_min ) < 0 )
					m_max = m_min;
			}
		}
		[Export]
		public int Max
		{
			get => m_max;
			set
			{
				m_max = value;
				
				if( m_max.CompareTo( m_min ) < 0 )
					m_min = m_max;
			}
		}

		public int Apply( int val )
		{
			if( val.CompareTo( Min ) < 0 )
				return Min;
			if( val.CompareTo( Max ) > 0 )
				return Max;

			return val;
		}
		public bool WithinRange( int val )
		{
			return !( val.CompareTo( Min ) < 0 && val.CompareTo( Max ) > 0 );
		}

		int m_min,
		    m_max;
	}
	public struct UIntRange : IRange<uint>
	{
		public UIntRange()
		{
			m_min = uint.MinValue;
			m_max = uint.MaxValue;
		}
		public UIntRange( uint min, uint max )
		{ 
			m_min = min;
			m_max = max < min ? min : max;
		}

		[Export]
		public uint Min
		{
			get => m_min;
			set
			{
				m_min = value;
				
				if( m_max.CompareTo( m_min ) < 0 )
					m_max = m_min;
			}
		}
		[Export]
		public uint Max
		{
			get => m_max;
			set
			{
				m_max = value;
				
				if( m_max.CompareTo( m_min ) < 0 )
					m_min = m_max;
			}
		}

		public uint Apply( uint val )
		{
			if( val.CompareTo( Min ) < 0 )
				return Min;
			if( val.CompareTo( Max ) > 0 )
				return Max;

			return val;
		}
		public bool WithinRange( uint val )
		{
			return !( val.CompareTo( Min ) < 0 && val.CompareTo( Max ) > 0 );
		}

		uint m_min,
		     m_max;
	}
	public struct FloatRange : IRange<float>
	{
		public FloatRange()
		{
			m_min = float.MinValue;
			m_max = float.MaxValue;
		}
		public FloatRange( float min, float max )
		{ 
			m_min = min;
			m_max = max < min ? min : max;
		}

		[Export]
		public float Min
		{
			get => m_min;
			set
			{
				m_min = value;
				
				if( m_max.CompareTo( m_min ) < 0 )
					m_max = m_min;
			}
		}
		[Export]
		public float Max
		{
			get => m_max;
			set
			{
				m_max = value;
				
				if( m_max.CompareTo( m_min ) < 0 )
					m_min = m_max;
			}
		}

		public float Apply( float val )
		{
			if( val.CompareTo( Min ) < 0 )
				return Min;
			if( val.CompareTo( Max ) > 0 )
				return Max;

			return val;
		}
		public bool WithinRange( float val )
		{
			return !( val.CompareTo( Min ) < 0 && val.CompareTo( Max ) > 0 );
		}

		float m_min,
		      m_max;
	}
}
