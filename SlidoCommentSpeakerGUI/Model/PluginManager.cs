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
	internal class PluginContainer
	{
		public CancellationTokenSource cancellationSource
		{
			get; set;
		}

		public IPlugin Plugin { get; set; }

	}

	internal class PluginManager
	{
		public PluginContext PluginContext { get; set; }
		public List<PluginContainer> Plugins { get; set; }

		public string PluginDirectory { get; }

		public PluginManager(string pluginDirectory)
		{
			PluginDirectory = pluginDirectory;
			Plugins = new();
			PluginContext = new();
		}

		public void LoadPlugins()
		{
			var files = Directory.GetFiles(PluginDirectory);
			foreach (var file in files)
			{
				PluginLoader loader = new(file);
				var asm = loader.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(file)));
				foreach (Type t in asm.GetTypes())
				{
					if (typeof(IPlugin).IsAssignableFrom(t))
					{
						IPlugin? plugin = Activator.CreateInstance(t) as IPlugin;
						if (plugin == null) continue;
						Plugins.Add(new PluginContainer { Plugin = plugin });
					}
				}
			}
		}
	}
}
