using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace AdvancedCSharp
{
    public class FileSystemVisitor
    {
        private const int SeachAll = 0;

        private readonly string _rootPath;
        private readonly Func<Node, bool>? _filteringPredicate;

        private FoundFileEventArgs FilteredEventArgs { get; set; } = new FoundFileEventArgs();

        private bool IsExcludeSearch { get; set; }
        private bool IsAborted { get; set; }

        public event EventHandler<FoundFileEventArgs> Start;
        public event EventHandler<FileSystemVisitorBaseEventArgs> Finish;
        public event EventHandler<FileEventArgs> FileFound;
        public event EventHandler<FileEventArgs> DirectoryFound;

        public event EventHandler<FoundFileEventArgs> FilteredFileFound;
        public event EventHandler<FoundFileEventArgs> FilteredDirectoryFound;

        private IFileSystem _fileSystem;

        public FileSystemVisitor(string rootPath)
        {
            _rootPath = rootPath;
            _fileSystem = new FileSystem();
        }

        public FileSystemVisitor(string rootPath, Func<Node, bool> filteringPredicate) : this(rootPath)
        {
            _filteringPredicate = filteringPredicate;
        }

        public FileSystemVisitor(string rootPath, IFileSystem fileSystem)
        {
            _rootPath = rootPath;
            _fileSystem = fileSystem;
        }

        public FileSystemVisitor(string rootPath, Func<Node, bool> filteringPredicate, IFileSystem fileSystem) : this(rootPath, fileSystem)
        {
            _filteringPredicate = filteringPredicate;
        }

        public Node GetNode()
        {
            try
            {
                OnStart(false, false, SeachAll);
                var startDir = _fileSystem.DirectoryInfo.FromDirectoryName(_rootPath);
                var folders = TraverseDirectory(startDir, null);
                OnFinish();
                return folders;
            }
            catch (Exception ex)
            {
                throw new FileSystemVisitorException($"There is an error: \n {ex.Message}");
            }
        }

        public IEnumerable<Node> FilterNode(Node root, bool abortToggle, bool excludeTogge, int elementsToFind = 0)
        {
            var backUpRoot = root;
            OnStart(abortToggle, excludeTogge, elementsToFind);

            var foundNodesBackup = new List<Node>(Search(backUpRoot));

            if (IsExcludeSearch)
            {
                foreach (var node in backUpRoot.ToList())
                {
                    if (foundNodesBackup.Contains(node))
                    {
                        node.Parent.Children.Remove(node);
                    }
                }
                return new List<Node> { backUpRoot };
            }
            OnFinish();

            return foundNodesBackup;
        }

        private IEnumerable<Node> Search(Node root)
        {
            if (_filteringPredicate == null) yield break;

            var queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {   
                var current = queue.Dequeue();
                if (_filteringPredicate(current))
                {
                    OnFilteredElementFound(current.Data);

                    if (IsAborted)
                    {
                        yield break;
                    }

                    yield return current;
                }

                foreach (var node in current.Children)
                    queue.Enqueue(node);
            }
        }

        private Node TraverseDirectory(IDirectoryInfo directoryInfo, Node parent)
        {
            var directoryLeaf = new Item { Name = directoryInfo.Name, FileType = FileType.Folder};
            OnElementFound(directoryLeaf);

            var directoryNode = new Node(directoryLeaf, parent);  
            var subdirectories = directoryInfo.EnumerateDirectories();

            foreach (var subdirectory in subdirectories)
                directoryNode.Children.Add(TraverseDirectory(subdirectory, directoryNode));

            var files = directoryInfo.EnumerateFiles();

            foreach (var file in files)
            {
                var leaf = new Item { Name = file.Name, FileType = FileType.File };
                directoryNode.Children.Add(new Node(leaf, directoryNode));
                OnElementFound(leaf);
            }

            return directoryNode;
        }

        public void OnAbort(object source, EventArgs eventArgs)
        {
            IsAborted = true;
        }

        protected virtual void OnStart(bool enableAbort, bool enableExclude, int elementsToSearch)
        {
            var foundFileEventArgs = new FoundFileEventArgs() { AbortToggle = enableAbort, ExcludeToggle = enableExclude, LimitCounter = elementsToSearch };
            Start?.Invoke(this, foundFileEventArgs);

            if (foundFileEventArgs.AbortSearch)
            {
                IsAborted = true;
            }

            if (foundFileEventArgs.Exclude)
            {
                IsExcludeSearch = true;
            }

            FilteredEventArgs = foundFileEventArgs;
        }

        protected virtual void OnFinish()
        {
            Finish?.Invoke(this, new FileSystemVisitorBaseEventArgs());
            IsAborted = false;
            IsExcludeSearch = false;
        }

        protected virtual void OnFilteredElementFound(Item item)
        {
            switch (item.FileType)
            {
                case FileType.File:
                    FilteredFileFound?.Invoke(this, FilteredEventArgs);
                    break;
                case FileType.Folder:
                    FilteredDirectoryFound?.Invoke(this, FilteredEventArgs);
                    break;                   
            }
           
            if (FilteredEventArgs.AbortSearch)
            {
                IsAborted = true;
            }
        }

        protected virtual void OnElementFound(Item item) 
        {
            switch (item.FileType)
            {
                case FileType.File:
                    FileFound?.Invoke(this, new FileEventArgs { Leaf = item });
                    break;
                case FileType.Folder:
                    DirectoryFound?.Invoke(this, new FileEventArgs { Leaf = item });
                    break;
            }            
        }
    }
}
