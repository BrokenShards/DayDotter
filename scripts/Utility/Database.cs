// BinaryDatabase.cs //

#nullable enable

using System;
using System.Collections.Generic;
using System.IO;

namespace Dotter
{
	public class IndexedDatabase<T> : BinarySerializable, IIndexedDatabase<T> where T : IBinarySerializable, new()
	{
		public IndexedDatabase()
		{
			_items = new List<T>();
		}
		public IndexedDatabase( params T[] items )
		{
			_items = new( items );
		}

		public bool Empty => Count == 0;
		public int Count => _items.Count;

		public int IndexOf( T item )
		{
			return _items.IndexOf( item );
		}
		public bool Contains( T item )
		{
			return _items.Contains( item );
		}
		public T? Get( int index )
		{
			return index < 0 || index >= Count ? default : _items[ index ];
		}
		public void Set( int index, T item )
		{
			if( index < 0 || index >= Count )
				return;

			_items[ index ] = item;
		}
		public void Add( T item )
		{
			_items.Add( item );
		}
		public void Insert( int index, T item )
		{
			_items.Insert( index < 0 ? 0 : index > Count ? Count : index, item );
		}
		public void Remove( T item )
		{
			RemoveAt( IndexOf( item ) );
		}
		public void RemoveAt( int index )
		{
			if( index < 0 || index >= Count )
				return;

			_items.RemoveAt( index );
		}
		public void Clear()
		{
			_items.Clear();
		}

		public override bool LoadFromStream( BinaryReader br )
		{
			try
			{
				int count = br.ReadInt32();

				_items.Clear();
				_items.Capacity = _items.Capacity < count ? count : _items.Capacity;

				for( int i = 0; i < count; i++ )
				{
					T item = new();

					if( !item.LoadFromStream( br ) )
						throw new Exception( "Failed loading database item." );

					_items.Add( item );
				}
			}
			catch( Exception e )
			{
				return Logger.LogReturn( $"Failed loading IndexedDatabase from stream: {e.Message}", false, LogType.Error );
			}

			return true;
		}
		public override bool SaveToStream( BinaryWriter bw )
		{
			try
			{
				bw.Write( Count );

				for( int i = 0; i < Count; i++ )
					if( !_items[ i ].SaveToStream( bw ) )
						throw new Exception( "Failed saving database item." );
			}
			catch( Exception e )
			{
				return Logger.LogReturn( $"Failed saving IndexedDatabase to stream: {e.Message}", false, LogType.Error );
			}

			return true;
		}

		private readonly List<T> _items;
	}
	public class StringDatabase<T> : BinarySerializable, IStringDatabase<T> where T : IBinarySerializable, new()
	{
		public StringDatabase()
		{
			_items = new();
		}
		public StringDatabase( params KeyValuePair<string, T>[] items )
		{
			_items = new( items );
		}

		public bool Empty => Count == 0;
		public int Count => _items.Count;

		public bool ContainsKey( string key )
		{
			return _items.ContainsKey( key );
		}
		public bool Contains( T item )
		{
			return _items.ContainsValue( item );
		}
		public T? Get( string key )
		{
			return !ContainsKey( key ) ? default : _items[ key ];
		}
		public void Add( string key, T item )
		{
			_items.Add( key, item );
		}
		public void Remove( string key )
		{
			if( !ContainsKey( key ) )
				return;

			_items.Remove( key );
		}
		public void Clear()
		{
			_items.Clear();
		}

		public override bool LoadFromStream( BinaryReader br )
		{
			try
			{
				int count = br.ReadInt32();

				_items.Clear();

				for( int i = 0; i < count; i++ )
				{
					string id = br.ReadString();
					T item = new();

					if( !item.LoadFromStream( br ) )
						throw new Exception( "Failed loading database item." );

					_items.Add( id, item );
				}
			}
			catch( Exception e )
			{
				return Logger.LogReturn( $"Failed loading StringDatabase from stream: {e.Message}", false, LogType.Error );
			}

			return true;
		}
		public override bool SaveToStream( BinaryWriter bw )
		{
			try
			{
				bw.Write( Count );

				foreach( var item in _items )
				{
					bw.Write( item.Key );

					if( !item.Value.SaveToStream( bw ) )
						throw new Exception( "Failed saving database item." );
				}
			}
			catch( Exception e )
			{
				return Logger.LogReturn( $"Failed saving StringDatabase to stream: {e.Message}", false, LogType.Error );
			}

			return true;
		}

		private readonly Dictionary<string, T> _items;
	}
}
