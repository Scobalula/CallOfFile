// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CallOfFile.UnitTests
{
    [TestClass]
    public class BinaryFileTests
    {
        public static string TestFiles { get; } = Path.Combine(Environment.GetEnvironmentVariable("COF_TEST_FILES_DIR") ?? string.Empty, "BinaryTests");

        public static string OutputFiles { get; } = Path.Combine(Environment.GetEnvironmentVariable("COF_TEST_FILES_DIR") ?? string.Empty, "BinaryTestsResults");

        [TestMethod]
        public void TestBinaryFiles()
        {
            Directory.CreateDirectory(OutputFiles);

            foreach (var file in Directory.EnumerateFiles(TestFiles, "*.*_bin"))
            {
                var newFile = Path.Combine(OutputFiles, Path.GetFileName(file).Replace("_bin", "_export"));
                using var reader = new BinaryTokenReader(file);
                using var writer = new ExportTokenWriter(newFile);

                writer.WriteTokens(reader.ReadTokens());
            }
            foreach (var file in Directory.EnumerateFiles(OutputFiles, "*.*_export"))
            {
                var newFile = Path.Combine(OutputFiles, Path.GetFileName(file).Replace("_export", "_bin"));
                using var reader = new ExportTokenReader(file);
                using var writer = new BinaryTokenWriter(newFile);

                writer.WriteTokens(reader.ReadTokens());
            }
        }
    }
}