// ProductEntryList.cs //

#nullable enable

using System;
using System.Collections.Generic;
using Godot;

namespace Dotter
{
	public partial class ProductEntryList : Control
	{
		public ProductEntryList()
		:	base()
		{
			m_product_prefab = null;
			m_products       = new();
		}

		public override void _Ready()
		{
			m_product_prefab = GD.Load<PackedScene>( ResourcePaths.ProductEntry );

			if( m_product_prefab == null )
			{
				Utility.Abort( GetTree(), $"Failed loading product entry prefab from { ResourcePaths.ProductEntry }." );
				return;
			}
			if( !LoadEntries() )
			{
				Utility.Abort( GetTree(), $"Failed loading product entries." );
				return;
			}
		}

		public void SearchTextChanged( string text )
		{
			for( int i = 0; i < m_products.Count; i++ )
			{
				if( string.IsNullOrWhiteSpace( text ) )
				{
					m_products[ i ].Visible = true;
					continue;					
				}
				
				Product? prod = m_products[ i ].Product;

				if( prod == null )
				{
					m_products[ i ].Visible = false;
					continue;					
				}

				m_products[ i ].Visible = prod.Name.Contains( text, StringComparison.CurrentCultureIgnoreCase );
			}
		}

		public bool LoadEntries()
		{
			m_products.Clear();

			for( int i = 0; i < ProductList.Instance.Count; i++ )
				if( !AddEntryToList( i ) )
					return false;
			
			return true;
		}
		private void ClearEntries()
		{
			if( m_products.Count == 0 )
				return;
			
			for( int i = 0; i < m_products.Count; i++ )
			{
				RemoveChild( m_products[ i ] );
				m_products[ i ].QueueFree();
			}

			m_products.Clear();
		}
		private bool AddEntryToList( int index )
		{
			ProductEntry? opt = m_product_prefab?.Instantiate<ProductEntry>();

			if( opt == null )
				return false;

			opt.ProductIndex = index;
			AddChild( opt );
			m_products.Add( opt );
			return true;
		}
						
		private PackedScene? m_product_prefab;
		private readonly List<ProductEntry> m_products;
	}
}
