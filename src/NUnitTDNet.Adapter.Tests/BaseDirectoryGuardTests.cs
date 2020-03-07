using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NUnitTDNet.Adapter.Tests
{
    [TestClass]
    public class BaseDirectoryGuardTests
    {
        [TestMethod]
        public void Constructor_MoveFile()
        {
            var newContent = "NEW_CONTENT";
            var dir = Directory.GetCurrentDirectory();
            var fileName = "file.dll";
            var file = Path.Combine(dir, fileName);
            var bakFile = Path.ChangeExtension(file, BaseDirectoryGuard.BackupExtension);
            File.WriteAllText(file, newContent);
            File.Delete(bakFile);

            try
            {
                var target = new BaseDirectoryGuard(dir, new[] { fileName });

                Assert.IsFalse(File.Exists(file));
                Assert.AreEqual(newContent, File.ReadAllText(bakFile));
            }
            finally
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void Constructor_MoveFile_OverwriteBackup()
        {
            var newContent = "NEW_CONTENT";
            var oldContent = "OLD_CONTENT";
            var dir = Directory.GetCurrentDirectory();
            var fileName = "file.dll";
            var file = Path.Combine(dir, fileName);
            var bakFile = Path.ChangeExtension(file, BaseDirectoryGuard.BackupExtension);
            File.WriteAllText(file, newContent);
            File.WriteAllText(bakFile, oldContent);

            try
            {
                var target = new BaseDirectoryGuard(dir, new[] { fileName });

                Assert.IsFalse(File.Exists(file));
                Assert.AreEqual(newContent, File.ReadAllText(bakFile));
            }
            finally
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void Dispose_RestoreFile()
        {
            var newContent = "NEW_CONTENT";
            var dir = Directory.GetCurrentDirectory();
            var fileName = "file.dll";
            var file = Path.Combine(dir, fileName);
            File.WriteAllText(file, newContent);

            try
            {
                var target = new BaseDirectoryGuard(dir, new[] { fileName });
                target.Dispose();

                Assert.AreEqual(newContent, File.ReadAllText(file));
            }
            finally
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void Dispose_RestoreFile_RestorePreviousBackup()
        {
            var oldContent = "OLD_CONTENT";
            var dir = Directory.GetCurrentDirectory();
            var fileName = "file.dll";
            var file = Path.Combine(dir, fileName);
            var bakFile = Path.ChangeExtension(file, BaseDirectoryGuard.BackupExtension);
            File.Delete(file);
            File.WriteAllText(bakFile, oldContent);

            try
            {
                var target = new BaseDirectoryGuard(dir, new[] { fileName });
                target.Dispose();

                Assert.AreEqual(oldContent, File.ReadAllText(file));
                Assert.IsFalse(File.Exists(bakFile));
            }
            finally
            {
                File.Delete(file);
            }
        }
    }
}
