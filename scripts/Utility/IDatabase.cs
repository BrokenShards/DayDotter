// BinaryDatabase.cs //

#nullable enable

namespace Dotter
{
	public interface IIndexedDatabase<T> : IBinarySerializable where T : IBinarySerializable, new()
	{
		bool Empty { get; }
		int Count { get; }

		int IndexOf( T item );
		bool Contains( T item );
		T? Get( int index );
		void Set( int index, T item );
		void Add( T item );
		void Insert( int index, T item );
		void Remove( T item );
		void RemoveAt( int index );
		void Clear();
	}
	public interface IKeyedDatabase<K, T> : IBinarySerializable where T : IBinarySerializable, new()
	{
		bool Empty { get; }
		int Count { get; }

		bool ContainsKey( K key );
		bool Contains( T item );
		T? Get( K key );
		void Add( K key, T item );
		void Remove( K key );
		void Clear();
	}
	public interface IStringDatabase<T> : IKeyedDatabase<string, T> where T : IBinarySerializable, new()
	{ }
}
