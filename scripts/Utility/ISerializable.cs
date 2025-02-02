// ISerializable.cs //

#nullable enable

using System.IO;
using System.Xml;

namespace Dotter
{
	/// <summary>
	///   Interface for objects that can be serialized/deserialized to/from a file.
	/// </summary>
	/// <typeparam name="ReadT">
	///   The stream reader type used to deserialize the object from a stream.
	/// </typeparam>
	/// <typeparam name="WriteT">
	///   The stream writer type used to serialize the object to a stream.
	/// </typeparam>
	public interface ISerializable<ReadT, WriteT>
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
		bool LoadFromStream( ReadT sr );
		/// <summary>
		///   Attempts to serialize the object to the stream.
		/// </summary>
		/// <param name="sw">
		///   Stream writer.
		/// </param>
		/// <returns>
		///   True if serialization succeeded and false otherwise.
		/// </returns>
		bool SaveToStream( WriteT sw );
	}

	/// <summary>
	///   Interface for objects that are be serialized/deserialized to/from binary files.
	/// </summary>
	public interface IBinarySerializable : ISerializable<BinaryReader, BinaryWriter>
	{ }

	/// <summary>
	///   Interface for objects that can be loaded from an xml element.
	/// </summary>
	public interface IXmlLoadable
	{
		/// <summary>
		///   Attempts to load the object from the xml element.
		/// </summary>
		/// <param name="element">
		///   The xml element.
		/// </param>
		/// <returns>
		///   True if the object was successfully loaded, otherwise false.
		/// </returns>
		bool LoadFromXml( XmlElement element );
	}
}
