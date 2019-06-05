using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

class Program
{
    public static void MethodA()
    {
        Console.Write("Enter the name of the archive to be unpacked : ");
        string zipPath = "./" + Console.ReadLine() + ".zip";

        string extractPath = @".\Temporary directory";
        extractPath = Path.GetFullPath(extractPath);

        if (!Directory.Exists(extractPath))
            Directory.CreateDirectory(extractPath);
        else
        {
            Directory.Delete(extractPath, true);
            Directory.CreateDirectory(extractPath);
        }

        if (!extractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
            extractPath += Path.DirectorySeparatorChar;

        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
        {
            List<FileInfo> unpackedFiles = new List<FileInfo>();

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));

                    if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                    {
                        entry.ExtractToFile(destinationPath);
                        unpackedFiles.Add(new FileInfo(entry.FullName));
                    }
                }
            }
            MethodB(unpackedFiles);
        }
        Directory.Delete(extractPath, true);
    }

    public static void MethodB(List<FileInfo> unpackedFiles)
    {
        foreach (FileInfo file in unpackedFiles)
        {
            Console.WriteLine("\n\n{0}", file.Name);
            Console.WriteLine("Extension: {0}", file.Extension.ToString());
            Console.WriteLine("In directory: {0}", file.Directory);
        }
    }
    static void Main(string[] args)
    {
        MethodA();
        Console.ReadKey();
    }
}