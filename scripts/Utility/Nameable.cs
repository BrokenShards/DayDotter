// Nameable.cs //

#nullable enable

using System.Collections.Generic;
using System.Text;

namespace Dotter
{
	/// <summary>
	///   Interface for objects that have a name.
	/// </summary>
	public interface INameable
	{
		/// <summary>
		///   The name of the object.
		/// </summary>
		string Name { get; set; }
	}

	/// <summary>
	///   Contains Name related functionality.
	/// </summary>
	public static class Naming
	{
		/// <summary>
		///   If the given string is a valid name.
		/// </summary>
		/// <param name="name">
		///   The name string.
		/// </param>
		/// <returns>
		///   True if the name is valid and false otherwise.
		/// </returns>
		public static bool IsValid( string name )
		{
			if( name.Trim().Length is 0 || char.IsWhiteSpace( name[ 0 ] ) )
				return false;

			for( int i = 0; i < name.Length; i++ )
				if( !char.IsLetterOrDigit( name[ i ] ) && !char.IsPunctuation( name[ i ] ) &&
					!char.IsSymbol( name[ i ] ) && name[ i ] is not ' ' )
					return false;

			return true;
		}
		/// <summary>
		///   If the name of the given object is valid.
		/// </summary>
		/// <param name="i">
		///   The object to check.
		/// </param>
		/// <returns>
		///   True if the name of the object is valid and false otherwise.
		/// </returns>
		public static bool IsValid( INameable i )
		{
			return IsValid( i.Name );
		}

		/// <summary>
		///   Returns the given string as a valid name.
		/// </summary>
		/// <param name="name">
		///   The possibly invalid name.
		/// </param>
		/// <param name="repl">
		///   The character used to replace invalid characters.
		/// </param>
		/// <returns>
		///   The given string as a valid name.
		/// </returns>
		public static string AsValid( string name, char repl = '_' )
		{
			if( name.Trim().Length is 0 )
				return NewName();
			if( IsValid( name ) )
				return name;

			static bool valid_char( char c ) => char.IsLetterOrDigit( c ) || char.IsPunctuation( c ) || char.IsSymbol( c );

			if( !valid_char( repl ) )
				repl = '_';

			string n = name.Trim();
			StringBuilder res = new( n.Length );

			for( int i = 0; i < n.Length; i++ )
			{
				if( !valid_char( n[ i ] ) && n[ i ] is not ' ' )
					res.Append( repl );
				else
					res.Append( n[ i ] );
			}

			return res.ToString();
		}
		/// <summary>
		///   Returns the given objects' name as a valid name.
		/// </summary>
		/// <param name="i">
		///   The identifiable object.
		/// </param>
		/// <returns>
		///   The given object' invalid name as a valid ID.
		/// </returns>
		public static string AsValid( INameable i ) => AsValid( i.Name );

		/// <summary>
		///   Creates a psuedo-new, valid name with a given prefix.
		/// </summary>
		/// <param name="prefix">
		///   Prefixes the name numbers.
		/// </param>
		/// <returns>
		///   A new name.
		/// </returns>
		public static string NewName( string? prefix = null )
		{
			if( string.IsNullOrWhiteSpace( prefix ) )
				prefix = "New Name";

			if( !_counter.ContainsKey( prefix ) )
				_counter.Add( prefix, 0 );

			string str = prefix + _counter[ prefix ].ToString();
			_counter[ prefix ]++;
			return str.Trim();
		}

		private static readonly Dictionary<string, ulong> _counter = new();
	}
}
