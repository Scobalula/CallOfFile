// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System.Diagnostics;

namespace CallOfFile.CLI
{
    public static class Program
    {
        public static string? GetConvertedFilePath(string filePath)
        {
            var ext = Path.GetExtension(filePath);

            if (ext.Equals(".xmodel_export", StringComparison.InvariantCultureIgnoreCase))
                return Path.ChangeExtension(filePath, ".xmodel_bin");
            else if (ext.Equals(".xmodel_bin", StringComparison.InvariantCultureIgnoreCase))
                return Path.ChangeExtension(filePath, ".xmodel_export");
            else if (ext.Equals(".xanim_export", StringComparison.InvariantCultureIgnoreCase))
                return Path.ChangeExtension(filePath, ".xanim_bin");
            else if (ext.Equals(".xanim_bin", StringComparison.InvariantCultureIgnoreCase))
                return Path.ChangeExtension(filePath, ".xanim_export");

            return null;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("| Call of File CLI");
            Console.WriteLine("| Developed by Scobalula");

            foreach (var arg in args)
            {
                TokenReader? reader = null;
                TokenWriter? writer = null;
                var fileName = Path.GetFileName(arg);

                try
                {
                    var watch = Stopwatch.StartNew();
                    Console.WriteLine($"| Attempting to convert: {fileName}...");

                    if (!TokenReader.TryCreateReader(arg, out reader))
                        throw new Exception($"Failed to create reader for {fileName}");
                    if (!TokenWriter.TryCreateWriter(GetConvertedFilePath(arg), out writer))
                        throw new Exception($"Failed to create writer for {fileName}");

                    writer.WriteTokens(reader.ReadTokens());
                    Console.WriteLine($"| Converted: {fileName} in {watch.ElapsedMilliseconds / 1000.0f} seconds.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"| Exception: {e.Message}");
                }
                finally
                {
                    reader?.Dispose();
                    writer?.Dispose();
                }
            }

            Console.WriteLine("| Execution complete, press Enter to exit.");
            Console.ReadLine();
        }
    }
}