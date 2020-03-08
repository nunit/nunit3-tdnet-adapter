using System;
using System.IO;
using System.Collections.Generic;

namespace NUnitTDNet.Adapter
{
    public class BaseDirectoryGuard : IDisposable
    {
        public const string BackupExtension = "tdnet.bak";

        readonly string baseDir;
        readonly IList<string> fileNames;

        public BaseDirectoryGuard(string baseDir, IList<string> fileNames)
        {
            this.baseDir = baseDir;
            this.fileNames = fileNames;

            foreach (var fileName in fileNames)
            {
                var file = Path.Combine(baseDir, fileName);
                var bakFile = Path.ChangeExtension(file, BackupExtension);

                Move(file, bakFile);
            }
        }

        public void Dispose()
        {
            foreach (var fileName in fileNames)
            {
                var file = Path.Combine(baseDir, fileName);
                var bakFile = Path.ChangeExtension(file, BackupExtension);

                Move(bakFile, file);
            }
        }

        static void Move(string sourceFile, string destFile)
        {
            if (File.Exists(sourceFile))
            {
                if (File.Exists(destFile))
                {
                    File.Delete(destFile);
                }

                File.Move(sourceFile, destFile);
            }
        }
    }
}
