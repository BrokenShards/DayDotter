// StringTools.cs //

#nullable enable

using System.Text;

namespace Dotter
{
	public static class StringTools
	{
		public static string Indent( string text, int indent = 1 )
		{
			if( indent <= 0 )
				return text;

			StringBuilder sb = new();

			for( int i = 0; i < indent; i++ )
				sb.Append( '\t' );

			string tabs = sb.ToString();

			return $"{tabs}{text.Replace( "\n", $"\n{tabs}" )}";
		}
	}
}
