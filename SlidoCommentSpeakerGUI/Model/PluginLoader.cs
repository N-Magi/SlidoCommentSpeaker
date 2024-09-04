using SlidoCommentSpeakerPluginBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace SlidoCommentSpeakerGUI.Model
{
	internal class PluginLoader : AssemblyLoadContext
	{
		private AssemblyDependencyResolver resolver;
		public PluginLoader(string pluginDirectory = "/plugins/")
		{
			if (!Directory.Exists(pluginDirectory))
				Directory.CreateDirectory(pluginDirectory);

			var curret = Directory.GetCurrentDirectory();
			var plugins = curret + pluginDirectory;

			resolver = new AssemblyDependencyResolver(@"C:\\Users\\masay\\source\\repos\\SlidoCommentSpeaker\\SlidoCommentSpeakerGUI\\bin\\Debug\\net8.0-windows8.0\\plugins\\VoicevoxPlugin.dll".Replace('\\', Path.DirectorySeparatorChar));

		}

		protected override Assembly? Load(AssemblyName assemblyName)
		{
			string assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
			if (assemblyPath != null)
			{
				return LoadFromAssemblyPath(assemblyPath);
			}

			return null;
		}
	}
}
