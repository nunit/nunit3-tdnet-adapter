namespace NUnitTDNet.Adapter.Tests
{
    using System;
    using System.IO;
    using System.CodeDom.Compiler;

    class CompilerUtilities
    {
        public static string Compile(string assemblyFile, string[] referencedAssemblies, params string[] sources)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters cp = new CompilerParameters();

            // Generate an executable instead of 
            // a class library.
            cp.GenerateExecutable = false;

            // Specify the assembly file name to generate.
            cp.OutputAssembly = assemblyFile;

            // Save the assembly as a physical file.
            cp.GenerateInMemory = false;

            // Set whether to treat all warnings as errors.
            cp.TreatWarningsAsErrors = false;

            cp.ReferencedAssemblies.AddRange(referencedAssemblies);

            // Invoke compilation of the source file.
            CompilerResults cr = provider.CompileAssemblyFromSource(cp, sources);

            if (cr.Errors.Count > 0)
            {
                var writer = new StringWriter();
                foreach(var source in sources)
                {
                    writer.WriteLine("Errors building: " + source);
                }

                writer.WriteLine(@" into: " + cr.PathToAssembly);

                foreach (CompilerError ce in cr.Errors)
                {
                    writer.WriteLine("  {0}", ce.ToString());
                    writer.WriteLine();
                }

                throw new Exception(writer.ToString());
            }

            return cr.PathToAssembly;
        }
    }
}