// DayDot.cs //

#nullable enable

using System;
using Godot;

namespace Dotter
{
	public partial class DayDot : Control
	{
		public DayDot()
		:	base()
		{ 
			m_texture  = null;
			m_day      = null;
			m_date     = null;
			m_datetime = DateTime.Now;
		}

		public DateTime Date
		{
			get => m_datetime;
			set
			{
				m_datetime = OpenHours.Instance.ClipToOpenHours( value );
				UpdateDayDot();
			}
		}

		public override void _Ready()
		{
			try
			{
				m_texture = GetNode<TextureRect>( "Image/Background" );
				m_day     = GetNode<Label>( "Text/Day" );
				m_date    = GetNode<Label>( "Text/Date" );
			}
			#if DEBUG
			catch( Exception e )
			{
				Utility.Abort( GetTree(), $"Failed Readying DayDot: { e.Message }." );
				return;
			}
			#else
			catch
			{
				return;
			}
			#endif

			if( m_texture == null )
			{
				Utility.Abort( GetTree(), "Failed Readying DayDot: texture is null." );
				return;
			}
			if( m_day == null )
			{
				Utility.Abort( GetTree(), "Failed Readying DayDot: day label is null." );
				return;
			}
			if( m_date == null )
			{
				Utility.Abort( GetTree(), "Failed Readying DayDot: date label is null." );
				return;
			}

			UpdateDayDot();
		}

		public void UpdateDayDot()
		{
			if( m_texture != null )
				m_texture.Texture = DotManager.Get( m_datetime.DayOfWeek );
			
			if( m_day != null )
				m_day.Text = m_datetime.ToString( "ddd" ).ToUpper();
			
			if( m_date != null )
			{
				m_date.Text = m_datetime.ToString( DateTime.Now.Year != m_datetime.Year ? "dd/MM/y" : "dd/MM" );

				var productlen = m_datetime.Subtract( DateTime.Now );
				
				if( Math.Abs( productlen.TotalHours ) <= 48 )
				{
					bool pm = m_datetime.Hour >= 12;

					m_date.Text += Settings.Instance.Use24HourClock ?
					               $"\n{ m_datetime.ToString( "HH:mm" ) }" :
								   $"\n{ m_datetime.ToString( "hh:mm" ) }{( pm ? "pm" : "am" )}";
				}
			}
		}

		private DateTime m_datetime;
		private TextureRect? m_texture;
		private Label? m_day;
		private Label? m_date;
	}
}
