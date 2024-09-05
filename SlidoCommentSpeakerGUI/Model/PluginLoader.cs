using System;
using System.Collections.Generic;
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
		public PluginLoader(string path)
		{
			resolver = new(path);
		}

		protected override Assembly? Load(AssemblyName assemblyName)
		{
			string assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
			if (assemblyPath == null) return null;

			return LoadFromAssemblyPath(assemblyPath);
		}


	}
}
