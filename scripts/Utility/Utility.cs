// Utility.cs //

#nullable enable

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dotter
{
	public static class Utility
	{
		public static void Quit( Godot.SceneTree tree )
		{
			tree.Root.PropagateNotification( (int)Godot.Node.NotificationWMCloseRequest );
			tree.Quit();
		}
		public static void Abort( Godot.SceneTree tree, string msg )
		{
			Logger.Log( msg, LogType.Error );
			Quit( tree );
		}

		public static bool DownloadFile( string url, string path )
		{
			Task<bool> task;
			
			try
			{
				task = Task.Run( async () => await DownloadFileAsync( url, path ) );
			}
			#if DEBUG
			catch( Exception e )
			{
				Logger.Log( $"Failed downloading file from \"{ url }\": { e.Message }.", LogType.Error );
				return false;
			}
			#else
			catch
			{
				return false;
			}
			#endif

			return task.Result;
		}
		private static async Task<bool> DownloadFileAsync( string url, string path )
		{
			using HttpClient httpClient = new();

			try
			{
				using var downloadStream = await httpClient.GetStreamAsync( url );
				using var fileStream = new FileStream( path, FileMode.Create, FileAccess.Write);

				await downloadStream.CopyToAsync( fileStream );
				await fileStream.FlushAsync();
				fileStream.Close();
			}
			#if DEBUG
			catch( Exception e )
			{
				Logger.Log( $"Failed downloading file from \"{ url }\": { e.Message }.", LogType.Error );
				return false;
			}
			#else
			catch
			{
				return false;
			}
			#endif

			return true;
		}
	}
}
