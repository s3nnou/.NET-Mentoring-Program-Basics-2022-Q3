using System.IO.Abstractions;

namespace AdvancedCSharp
{
    public static class FileSystemVisitorFactory
    {
        public static FileSystemVisitor GetFileSystemVisitor(string path, IFileSystem fileSystem, string filter = null)
        {
            var appService = new EventService();
            FileSystemVisitor fileSystemVisitor;
            if (filter != null)
            {
                fileSystemVisitor = new FileSystemVisitor(path, i => i.Data.Name.Contains(filter), fileSystem);
            }
            else
            {
                fileSystemVisitor = new FileSystemVisitor(path, fileSystem);
            }

            return SubscribeToEvents(fileSystemVisitor);
        }

        public static FileSystemVisitor GetFileSystemVisitor(string path, string filter = null)
        {
            FileSystemVisitor fileSystemVisitor;
            if (filter != null)
            {
                fileSystemVisitor = new FileSystemVisitor(path, i => i.Data.Name.Contains(filter));
            }
            else
            {
                fileSystemVisitor = new FileSystemVisitor(path);
            }

            return SubscribeToEvents(fileSystemVisitor);
        }

        private static FileSystemVisitor SubscribeToEvents(FileSystemVisitor fileSystemVisitor)
        {
            var appService = new EventService();

            fileSystemVisitor.Start += appService.OnStart;
            fileSystemVisitor.Finish += appService.OnFinish;
            fileSystemVisitor.FileFound += appService.OnFileFound;
            fileSystemVisitor.DirectoryFound += appService.OnDirectoryFound;
            fileSystemVisitor.FilteredFileFound += appService.OnFilteredFileFound;
            fileSystemVisitor.FilteredDirectoryFound += appService.OnFilteredDirectoryFound;

            return fileSystemVisitor;
        }
    }
}