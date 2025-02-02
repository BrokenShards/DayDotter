// DotManager.cs //

#nullable enable

using Godot;
using System;

namespace Dotter
{
	public static class DotManager
	{
		public static Texture2D? Get( DayOfWeek day )
		{
			int index = (int)day;

			if( index < 0 || index >= _textures.Length )
				return null;

			if( _textures[ index ] == null )
			{
				try
				{
					_textures[ index ] = GD.Load<Texture2D>( ResourcePaths.DayDotTexture( day ) );
				}
				catch
				{
					return null;
				}
			}

			return _textures[ index ];
		}

		private static readonly Texture2D[] _textures = new Texture2D[ Enum.GetValues<DayOfWeek>().Length ];
	}
}
