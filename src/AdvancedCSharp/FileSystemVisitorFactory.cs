using System;

namespace AdvancedCSharp
{
    public static class FileSystemVisitorFactory
    {
        public static FileSystemVisitor GetFileSystemVisitor(string path, string filter = null)
        {
            var appService = new EventService();
            FileSystemVisitor fileSystemVisitor;
            if (filter != null)
            {
                fileSystemVisitor = new FileSystemVisitor(path, i => i.Data.Name.Contains(filter));
            }
            else
            {
                fileSystemVisitor = new FileSystemVisitor(path);
            }

            fileSystemVisitor.StartEvent += appService.OnStart;
            fileSystemVisitor.Finish += appService.OnFinish;
            fileSystemVisitor.FileFound += appService.OnFileFound;
            fileSystemVisitor.FolderFound += appService.OnFolderFound;
            fileSystemVisitor.FoundFileFound += appService.OnFilteredFileFound;
            fileSystemVisitor.FoundFolderFound += appService.OnFilteredFolderFound;
            
            return fileSystemVisitor;
        }
    }
}