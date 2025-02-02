// ProductEntry.cs //

#nullable enable

using System;
using Godot;

namespace Dotter
{
	public partial class ProductEntry : Control
	{
		public ProductEntry()
		:	base()
		{
			m_mobile = false;
			Clicked  = false;
			m_index  = -1;
			m_button = null;
			m_daydot = null;
		}

		public bool MobileLayout
		{
			get => m_mobile;
			set
			{
				m_mobile = value;
				
				if( m_daydot != null )
					m_daydot.Visible = !m_mobile && ( ProductIndex >= 0 );
			}
		}
		public bool Clicked
		{
			get;
			private set;
		}
		public int ProductIndex
		{
			get => m_index;
			set
			{
				m_index = Math.Max( value, -1 );

				if( m_index >= ProductList.Instance.Count )
					m_index = -1;

				Product? prod = Product;

				if( m_button != null )
					m_button.Text = prod?.Name ?? "[Invalid Product]";
				if( m_daydot != null )
				{
					m_daydot.Visible = !m_mobile && ( ProductIndex >= 0 );

					if( m_daydot.Visible )
						m_daydot.Date = DateTime.Now.Add( prod?.Lifetime ?? TimeSpan.Zero );
				}
			}
		}
		public Product? Product
		{
			get
			{
				return m_index < 0 ? null : ProductList.Instance.Get( m_index );
			}
		}

		public void HandleClick()
		{
			Clicked = false;
		}
		public void OnProductClick()
		{
			Clicked = true;
		}

		public override void _Ready()
		{
			try
			{
				m_button = GetNode<Button>( "Button" );
				m_daydot = GetNode<DayDot>( "DayDot" );
			}
			catch( Exception e )
			{
				Utility.Abort( GetTree(), $"Failed Readying ProductEntry: { e.Message }." );
				return;
			}

			if( m_button == null )
			{
				Utility.Abort( GetTree(), "Failed Readying ProductEntry: Button is null." );
				return;
			}
			if( m_daydot == null )
			{
				Utility.Abort( GetTree(), "Failed Readying ProductEntry: DayDot is null." );
				return;
			}

			int index = ProductIndex;
			ProductIndex = index;
		}
		
		private int    m_index;
		private bool   m_mobile;
		private Button? m_button;
		private DayDot? m_daydot;
	}
}
