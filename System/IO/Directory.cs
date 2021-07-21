using Sandbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sandbox.System.IO
{
    public static class Directory
    {
        public static DirectoryImplementation Mounted => new DirectoryImplementation(FileSystem.Mounted);
        public static DirectoryImplementation Data => new DirectoryImplementation(FileSystem.Data);
        public static DirectoryImplementation OrganizationData => new DirectoryImplementation(FileSystem.OrganizationData);
    }

    public class DirectoryImplementation
    {
        public BaseFileSystem FileSystem { get; private set; }
        public DirectoryImplementation(BaseFileSystem fs)
        {
            FileSystem = fs;
        }
        public void CreateDirectory(string path)
        {
            FileSystem.CreateDirectory(path);
        }
        public void Delete(string path)
        {
            FileSystem.DeleteDirectory(path);
        }
        public void Delete(string path, bool recursive)
        {
            FileSystem.DeleteDirectory(path, recursive);
        }
        public IEnumerable<string> EnumerateDirectories(string path)
            => FileSystem.FindDirectory(path, "*");
        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
            => FileSystem.FindDirectory(path, searchPattern);
        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
            => FileSystem.FindDirectory(path, searchPattern, searchOption == SearchOption.AllDirectories);
        public IEnumerable<string> EnumerateFiles(string path)
            => FileSystem.FindFile(path, "*");
        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
            => FileSystem.FindFile(path, searchPattern);
        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
            => FileSystem.FindFile(path, searchPattern, searchOption == SearchOption.AllDirectories);
        public IEnumerable<string> EnumerateFileSystemEntries(string path)
            => EnumerateDirectories(path).Concat(EnumerateFiles(path));
        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)   
            => EnumerateDirectories(path, searchPattern).Concat(EnumerateFiles(path, searchPattern));
        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
            => EnumerateDirectories(path, searchPattern, searchOption).Concat(EnumerateFiles(path, searchPattern, searchOption));
        public bool Exists(string path)
            => FileSystem.DirectoryExists(path);
        public string GetCurrentDirectory()
            => "";
            //=> return FileSystem.GetFullPath("");
        
        public IEnumerable<string> GetDirectories(string path)
            => EnumerateDirectories(path, "*").Select(d => path + "/" + d);
        public IEnumerable<string> GetDirectories(string path, string searchPattern)
            => EnumerateDirectories(path, searchPattern).Select(d => path + "/" + d);
        public IEnumerable<string> GetDirectories(string path, string searchPattern, SearchOption searchOption)
            => EnumerateDirectories(path, searchPattern, searchOption).Select(d => path + "/" + d);
        public string GetDirectoryRoot(string path)
            => "/";
        public IEnumerable<string> GetFiles(string path)
            => EnumerateFiles(path, "*").Select(f => path + "/" + f);
        public IEnumerable<string> GetFiles(string path, string searchPattern)
            => EnumerateFiles(path, searchPattern).Select(f => path + "/" + f);
        public IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
            => EnumerateFiles(path, searchPattern, searchOption).Select(f => path + "/" + f);
        public IEnumerable<string> GetFileSystemEntries(string path)
            => GetDirectories(path).Concat(GetFiles(path));
        public string[] GetLogicalDrives()
            => new string[]{"/"};

        // Hacks and workarounds for missing APIs
        public DateTime GetCreationTime(string path) => DateTime.Now;
        public DateTime GetCreationTimeUtc(string path) => DateTime.UtcNow;
        public DateTime GetLastAccessTime(string path) => DateTime.Now;
        public DateTime GetLastAccessTimeUtc(string path) => DateTime.UtcNow;
        public DateTime GetLastWriteTime(string path) => DateTime.Now;
        public DateTime GetLastWriteTimeUtc(string path) => DateTime.UtcNow;
    }
}