using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdvancedCSharp
{
    public class FileSystemVisitor
    {
        private readonly string _rootPath;
        private readonly Func<Node, bool>? _filteringPredicate;
        private FoundFileEventArgs _filteredEventArgs { get; set; } = new FoundFileEventArgs();

        private bool IsExcludeSearch { get; set; }
        private bool IsAborted { get; set; }

        public event EventHandler<FoundFileEventArgs>? StartEvent;
        public event EventHandler? Finish;
        public event EventHandler<FileEventArgs>? FileFound;
        public event EventHandler<FileEventArgs>? FolderFound;

        public event EventHandler<FoundFileEventArgs>? FoundFileFound;
        public event EventHandler<FoundFileEventArgs>? FoundFolderFound;

        public FileSystemVisitor(string rootPath)
        {
            _rootPath = rootPath;
        }

        public FileSystemVisitor(string rootPath, Func<Node, bool> filteringPredicate) : this(rootPath)
        {
            _filteringPredicate = filteringPredicate;
        }

        public Node GetNode()
        {
            try
            {
                OnStart(false, false);
                var startDir = new DirectoryInfo(_rootPath);
                var folders = TraverseDirectory(startDir, null);
                OnFinish();
                return folders;
            }
            catch (Exception ex)
            {
                throw new Exception("error", ex);
            }
        }

        public IEnumerable<Node> FilterNode(Node root, bool abortToggle, bool excludeTogge, int elementsToFind)
        {
            var backUpRoot = root;
            OnStart(abortToggle, excludeTogge);
            _filteredEventArgs.LimitCounter = elementsToFind;
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
                    yield return current;
                    if (IsAborted)
                    {
                        yield break;
                    }
                }

                foreach (var node in current.Children)
                    queue.Enqueue(node);
            }
        }

        private Node TraverseDirectory(DirectoryInfo directoryInfo, Node parent)
        {
            var directoryLeaf = new Leaf { Name = directoryInfo.Name, FileType = "Folder", IsFolder = true };
            OnElementFound(directoryLeaf);

            var directoryNode = new Node(directoryLeaf, parent);  
            var subdirectories = directoryInfo.EnumerateDirectories();

            foreach (var subdirectory in subdirectories)
                directoryNode.Children.Add(TraverseDirectory(subdirectory, directoryNode));

            var files = directoryInfo.EnumerateFiles();

            foreach (var file in files)
            {
                var leaf = new Leaf { Name = file.Name, FileType = file.Extension.ToString() };
                directoryNode.Children.Add(new Node(leaf, directoryNode));
                OnElementFound(leaf);
            }

            return directoryNode;
        }

        public void OnAbort(object source, EventArgs eventArgs)
        {
            IsAborted = true;
        }

        protected virtual void OnStart(bool enableAbort, bool enableExclude)
        {
            var foundFileEventArgs = new FoundFileEventArgs() { AbortToggle = enableAbort, ExcludeToggle = enableExclude };
            StartEvent?.Invoke(this, foundFileEventArgs);

            if (foundFileEventArgs.AbortSearch)
            {
                IsAborted = true;
            }

            if (foundFileEventArgs.Exclude)
            {
                IsExcludeSearch = true;
            }

            _filteredEventArgs = foundFileEventArgs;
        }

        protected virtual void OnFinish()
        {
            Finish?.Invoke(this, null);
            IsAborted = false;
            IsExcludeSearch = false;
        }

        protected virtual void OnFilteredElementFound(Leaf item)
        {
            if (item.IsFolder)
            {
                FoundFolderFound?.Invoke(this, _filteredEventArgs);
            }
            else
            {
                FoundFileFound?.Invoke(this, _filteredEventArgs);
            }

            if (_filteredEventArgs.AbortSearch)
            {
                IsAborted = true;
            }
        }

        protected virtual void OnElementFound(Leaf item) 
        {   
            if (item.IsFolder)
            {
                FolderFound?.Invoke(this, new FileEventArgs { Leaf = item });
            }
            else
            {
                FileFound?.Invoke(this, new FileEventArgs { Leaf = item });
            }                
        }
    }
}
