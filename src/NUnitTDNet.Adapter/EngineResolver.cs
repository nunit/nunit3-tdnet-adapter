namespace NUnitTDNet.Adapter
{
    using System;
    using System.IO;
    using System.Reflection;

    class EngineResolver : IDisposable
    {
        string engineDir;

        public EngineResolver()
        {
            engineDir = findEngineDirectory();
            if(engineDir == null)
            {
                throw new Exception("Couldn't find NUnit engine directory");
            }

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var split = args.Name.Split(",".ToCharArray());
            string name = split[0];

            string assemblyFile = Path.Combine(engineDir, name + ".dll");
            if(File.Exists(assemblyFile))
            {
                return Assembly.LoadFile(assemblyFile);
            }

            return null;
        }

        static string findEngineDirectory()
        {
            var packagesDir = findPackagesDirectory();
            if (packagesDir == null)
            {
                return null;
            }

            var dirs = Directory.GetDirectories(packagesDir, "NUnit.ConsoleRunner.*");
            foreach (string dir in dirs)
            {
                var engineDir = Path.Combine(dir, "tools");
                var engineFile = Path.Combine(engineDir, "nunit.engine.dll");
                if (File.Exists(engineFile))
                {
                    return engineDir;
                }
            }

            return null;
        }

        static string findPackagesDirectory()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var dir = new Uri(assembly.EscapedCodeBase).LocalPath;

            while (true)
            {
                var packagesDir = Path.Combine(dir, "packages");
                if (Directory.Exists(packagesDir))
                {
                    return packagesDir;
                }

                dir = Path.GetDirectoryName(dir);
                if (dir == null)
                {
                    return null;
                }
            }
        }

        public void Dispose()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }
    }
}
