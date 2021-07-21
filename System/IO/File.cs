using Sandbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.System.IO
{
    public static class File
    {
        public static FileImplementation Mounted => new FileImplementation(FileSystem.Mounted);
        public static FileImplementation Data => new FileImplementation(FileSystem.Data);
        public static FileImplementation OrganizationData => new FileImplementation(FileSystem.OrganizationData);
    }

    public class FileImplementation
    {
        public BaseFileSystem FileSystem { get; private set; }
        public FileImplementation(BaseFileSystem fs)
        {
            FileSystem = fs;
        }

        public bool Exists(string path)
            => FileSystem.FileExists(path);
        public Stream Open(string path, FileAccess access)
        {
            switch (access)
            {
                case FileAccess.Read:
                    return OpenRead(path);
                case FileAccess.Write:
                    return OpenWrite(path);

                case FileAccess.ReadWrite:
                default:
                    throw new NotImplementedException("The FileSystem API doesn't allow R/W streams");
            }
        }
        public Stream OpenRead(string path)
            => FileSystem.OpenRead(path);
        public StreamReader OpenText(string path)
            => new StreamReader(OpenRead(path));
        public Stream OpenWrite(string path)
            => FileSystem.OpenWrite(path);
        public byte[] ReadAllBytes(string path)
            => FileSystem.ReadAllBytes(path).ToArray();
        public async Task<byte[]> ReadAllBytesAsync(string path)
            => await FileSystem.ReadAllBytesAsync(path);
        public string ReadAllText(string path)
            => FileSystem.ReadAllText(path);
        string[] ReadAllLines(string path)
            => FileSystem.ReadAllText(path).Split('\n');
        public IEnumerable<string> ReadLines(string path)
            => ReadLines(path);
        public void WriteAllBytes(string path, byte[] bytes)
            => FileSystem.OpenWrite(path).Write(bytes);
        public async void WriteAllBytesAsync(string path, byte[] bytes)
            => await FileSystem.OpenWrite(path).WriteAsync(bytes);
        public void WriteAllText(string path, string text)
            => WriteAllText(path, text, Encoding.UTF8);
        public void WriteAllText(string path, string text, Encoding encoding)
            => FileSystem.OpenWrite(path).Write(encoding.GetBytes(text));

        // Hacks and workarounds for missing APIs
        public DateTime GetCreationTime(string path) => DateTime.Now;
        public DateTime GetCreationTimeUtc(string path) => DateTime.UtcNow;
        public DateTime GetLastAccessTime(string path) => DateTime.Now;
        public DateTime GetLastAccessTimeUtc(string path) => DateTime.UtcNow;
        public DateTime GetLastWriteTime(string path) => DateTime.Now;
        public DateTime GetLastWriteTimeUtc(string path) => DateTime.UtcNow;

        // These are hacky and slow!
#pragma warning disable
        [Obsolete("This method reads and writes instead of actually moving the file")]
        public void Move(string from, string to)
        {
            Copy(from, to);
            FileSystem.DeleteFile(from);
        }
        [Obsolete("This method reads and writes instead of actually moving the file")]
        public void Move(string from, string to, bool overwrite)
        {
            if (overwrite)
                Move(from, to);
            else
                if (!Exists(to))
                Move(from, to);
        }
        [Obsolete("This method reads and writes instead of actually moving the file")]
        public void Replace(string from, string to, string? backup)
        {
            if (backup != null) Copy(to, backup);

            Move(from, to);
        }
#pragma warning restore
        public void Copy(string from, string to)
        {
            FileSystem.OpenRead(from).CopyTo(FileSystem.OpenWrite(to));
        }
    }

        public class FileNotFoundException : Exception
    {
        public FileNotFoundException() : base()
        {

        }

        public FileNotFoundException(string message) : base(message)
        {

        }
    }

    public class EndOfStreamException : Exception
    {
        public EndOfStreamException() : base()
        {

        }

        public EndOfStreamException(string message) : base(message)
        {

        }
    }
}