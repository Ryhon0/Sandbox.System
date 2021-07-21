using Sandbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.System.IO
{
    public static class Path
    {
        public static PathImplementation Mounted => new PathImplementation(FileSystem.Mounted);
        public static PathImplementation Data => new PathImplementation(FileSystem.Data);
        public static PathImplementation OrganizationData => new PathImplementation(FileSystem.OrganizationData);
    }

    public class PathImplementation
    {
        public BaseFileSystem FileSystem { get; private set; }
        public PathImplementation(BaseFileSystem fs)
        {
            FileSystem = fs;
        }

        public string ChangeExtension(string path, string extension)
            => global::System.IO.Path.ChangeExtension(path, extension);
        public string Combine(params string[] paths)
            => global::System.IO.Path.Combine(paths);
        public string GetDirectoryName(string path)
            => global::System.IO.Path.GetDirectoryName(path);
        public string GetExtension(string path)
            => global::System.IO.Path.GetExtension(path);
        public string GetFileName(string path)
            => global::System.IO.Path.GetFileName(path);
        public string GetFileNameWithoutExtension(string path)
            => global::System.IO.Path.GetFileNameWithoutExtension(path);
        public char[] GetInvalidFileNameChars()
            => global::System.IO.Path.GetInvalidFileNameChars();
        public char[] GetInvalidPathChars()
            => global::System.IO.Path.GetInvalidPathChars();
        public bool HasExtension(string path)
            => global::System.IO.Path.HasExtension(path);
        public string Join(params string[] paths)
            => global::System.IO.Path.Join(paths);
        public string TrimEndingDirectorySeparator(string path)
            => global::System.IO.Path.TrimEndingDirectorySeparator(path);
        public bool TryJoin(ReadOnlySpan<char> path, ReadOnlySpan<char> directory, Span<char> destination, out int written)
            => global::System.IO.Path.TryJoin(path, directory, destination, out written);

        public string GetFullPath(string path)
            => path;
        public string GetPathRoot(string path)
            => "";
        public string GetRandomFileName()
            => new Guid().ToString();
        public string GetTempPath()
            => "/temp/";
        public string GetTempFileName()
            => GetTempPath() + GetRandomFileName();
        public bool EndsInDirectorySeparator(string s)
            => s.EndsWith("/") || s.EndsWith("\\");
        public bool IsPathFullyQualified(string path)
            => true;
        public bool IsPathRooted(string path)
            => true;
    }
}