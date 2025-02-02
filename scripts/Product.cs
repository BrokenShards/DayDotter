// Product.cs //

#nullable enable

using System;
using System.Text;
using System.Xml;

namespace Dotter
{
	public class Product : XmlLoadable, ICloneable
	{
		public Product()
		{ 
			Name     = string.Empty;
			Lifetime = TimeSpan.FromHours( 24 );
		}
		public Product( string name, TimeSpan? lifetime = null )
		{ 
			Name     = name;
			Lifetime = lifetime ?? TimeSpan.FromHours( 24 );
		}
		public Product( Product prod )
		{
			Name     = (string)prod.Name.Clone();
			Lifetime = prod.Lifetime;
		}

		public bool IsValid
		{
			get => Name.Trim().Length > 0 && Lifetime > TimeSpan.Zero;
		}
		public string Name
		{
			get; set;
		}
		public TimeSpan Lifetime
		{
			get; set;
		}

		public DateTime GetExpiration()
		{
			return OpenHours.Instance.ClipToOpenHours( DateTime.Now.Add( Lifetime ) );
		}

		public override bool LoadFromXml( XmlElement element )
		{
			if( !element.HasAttribute( nameof( Name ) ) ||
				!element.HasAttribute( nameof( Lifetime ) ) )
				return false;

			try
			{
				Name     = element.GetAttribute( nameof( Name ) );
				Lifetime = TimeSpan.Parse( element.GetAttribute( nameof( Lifetime ) ) );
			}
			catch
			{
				return false;
			}

			return true;
		}

		public override string ToString()
		{
			StringBuilder sb = new();

			string name = nameof( Product );

			for( int i = 0; i < name.Length + 2; i++ )
				sb.Append( ' ' );

			string space = sb.ToString();
			sb.Clear();

			sb.Append( '<' ).Append( name ).Append( ' ' ).Append( nameof( Name ) ).Append( "=\"" ).Append( Name ).AppendLine( "\"" )
			                .Append( space ).Append( nameof( Lifetime ) ).Append( "=\"" ).Append( Lifetime.ToString() ).Append( "\"/>" );

			return sb.ToString();
		}

		public object Clone()
		{
			return new Product( this );
		}
	}
}