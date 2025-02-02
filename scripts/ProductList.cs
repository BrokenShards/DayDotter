// ProductList.cs //

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Dotter
{
	public class ProductList : XmlLoadable
	{
		private ProductList()
		{ 
			m_products = new();
			Load();
		}

		public static ProductList Instance
		{
			get
			{
				if( _instance == null )
				{
					lock( _syncRoot )
					{
						_instance ??= new ProductList();
					}
				}

				return _instance;
			}
		}

		public bool Empty => Count == 0;
		public int Count => m_products.Count;
		
		public int IndexOf( Product prod )
		{
			if( !prod.IsValid )
				return -1;

			for( int i = 0; i < Count; i++ )
				if( prod == m_products[ i ] )
					return i;

			return -1;
		}
		public int IndexOf( string name, bool casesense = false )
		{
			if( name.Trim().Length == 0 )
				return -1;
			
			string n = casesense ? name : name.ToLower();

			for( int i = 0; i < Count; i++ )
			{
				string pn = casesense ? m_products[ i ].Name : m_products[ i ].Name.ToLower();
				
				if( pn == n )
					return i;
			}

			return -1;
		}
		
		public bool Contains( Product prod )
		{
			return IndexOf( prod ) >= 0;
		}
		public bool Contains( string name, bool casesense = false )
		{
			return IndexOf( name, casesense ) >= 0;
		}

		public Product Get( int index )
		{
			if( index < 0 || index >= Count )
				return null;
			
			return m_products[ index ];
		}
		public Product Get( string name )
		{
			return Get( IndexOf( name ) );
		}

		public bool Add( Product prod, bool replace = false )
		{
			if( prod == null || !prod.IsValid )
				return false;
			
			if( Contains( prod.Name ) )
			{
				if( !replace )
					return false;
				
				m_products[ IndexOf( prod.Name ) ] = prod;
			}
			else
				m_products.Add( prod );

			return true;
		}
		public bool Remove( Product prod )
		{
			return RemoveAt( IndexOf( prod ) );
		}
		public bool Remove( string name, bool casesense = false )
		{
			return RemoveAt( IndexOf( name, casesense ) );
		}
		public bool RemoveAt( int index )
		{
			if( index < 0 || index >= Count )
				return false;
			
			m_products.RemoveAt( index );
			return true;
		}
		public void Clear()
		{
			m_products.Clear();
		}

		public bool Load()
		{
			if( !File.Exists( FilePaths.Products ) )
				if( !DownloadPreset() )
					return false;

			return LoadFromFile( FilePaths.Products );
		}
		public bool Save()
		{
			return ToFile( this, FilePaths.Products, true );
		}

		public static bool DownloadPreset()
		{
			return Utility.DownloadFile( DownloadPaths.Products, FilePaths.Products );
		}

		public override bool LoadFromXml( XmlElement element )
		{
			var childs = element.ChildNodes;

			foreach( XmlElement c in childs )
			{
				if( c.Name != nameof( Product ) )
					continue;

				Product p = new();

				if( !p.LoadFromXml( c ) )
					return false;
				
				Add( p, true );
			}

			return true;
		}
		public override string ToString()
		{
			StringBuilder sb = new();

			string name = nameof( ProductList );

			for( int i = 0; i < name.Length + 2; i++ )
				sb.Append( ' ' );

			string space = sb.ToString();
			sb.Clear();

			sb.Append( '<' ).Append( name ).Append( ">\n" );
			
			for( int i = 0; i < Count; i++ )
				sb.AppendLine( StringTools.Indent( m_products[ i ].ToString() ) );

			sb.Append( "</" ).Append( name ).Append( ">\n" );
			return sb.ToString();
		}
		
		private readonly List<Product> m_products;
		private static volatile ProductList _instance = null;
		private static readonly object _syncRoot = new();
	}
}
