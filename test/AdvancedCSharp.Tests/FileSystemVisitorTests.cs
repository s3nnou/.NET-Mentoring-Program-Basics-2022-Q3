using FluentAssertions;
using NUnit.Framework;
using System.IO.Abstractions.TestingHelpers;
using System.Windows;

namespace AdvancedCSharp.Tests
{
    public class Tests
    {
        private static readonly MockFileSystem _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {

            { @"c:\folder-test\1\4.txt", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\1\5.txt", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\1\6.txt", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\1\4\7.dat", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\2\8.dat", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\2\9.jiff", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\3\10.txt", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\1.txt", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\2.dat", new MockFileData("Testing is meh.") },
            { @"c:\folder-test\3.jiff", new MockFileData("Testing is meh.") },
        });

        private static Node BuildNode()
        {
            var rootNode = new Node(new Item { Name = "folder-test", FileType = FileType.Folder });
            var folderNode1 = new Node(new Item { Name = "1", FileType = FileType.Folder }, rootNode);
            folderNode1.Children.Add(new Node(new Item { Name = "4.txt", FileType = FileType.File }, folderNode1));
            folderNode1.Children.Add(new Node(new Item { Name = "5.txt", FileType = FileType.File }, folderNode1));
            folderNode1.Children.Add(new Node(new Item { Name = "6.txt", FileType = FileType.File }, folderNode1));
            var folderNode4 = new Node(new Item { Name = "4", FileType = FileType.Folder }, folderNode1);
            folderNode1.Children.Add(folderNode4);
            folderNode4.Children.Add(new Node(new Item { Name = "7.dat", FileType = FileType.File }, folderNode4));
            rootNode.Children.Add(folderNode1);
            var folderNode2 = new Node(new Item { Name = "2", FileType = FileType.Folder }, rootNode);
            folderNode2.Children.Add(new Node(new Item { Name = "8.dat", FileType = FileType.File }, folderNode2));
            folderNode2.Children.Add(new Node(new Item { Name = "9.jiff", FileType = FileType.File }, folderNode2));
            rootNode.Children.Add(folderNode2);
            var folderNode3 = new Node(new Item { Name = "3", FileType = FileType.Folder }, rootNode);
            folderNode3.Children.Add(new Node(new Item { Name = "10.txt", FileType = FileType.File }, folderNode3));
            rootNode.Children.Add(folderNode3);
            rootNode.Children.Add(new Node(new Item { Name = "1.txt", FileType = FileType.File }, rootNode));
            rootNode.Children.Add(new Node(new Item { Name = "2.dat", FileType = FileType.File }, rootNode));
            rootNode.Children.Add(new Node(new Item { Name = "3.jiff", FileType = FileType.File }, rootNode));

            return rootNode;
        }

        private static Node BuildNodeWithoutJiffNodes()
        {
            var rootNode = new Node(new Item { Name = "folder-test", FileType = FileType.Folder });
            var folderNode1 = new Node(new Item { Name = "1", FileType = FileType.Folder }, rootNode);
            folderNode1.Children.Add(new Node(new Item { Name = "4.txt", FileType = FileType.File }, folderNode1));
            folderNode1.Children.Add(new Node(new Item { Name = "5.txt", FileType = FileType.File }, folderNode1));
            folderNode1.Children.Add(new Node(new Item { Name = "6.txt", FileType = FileType.File }, folderNode1));
            var folderNode4 = new Node(new Item { Name = "4", FileType = FileType.Folder }, folderNode1);
            folderNode1.Children.Add(folderNode4);
            folderNode4.Children.Add(new Node(new Item { Name = "7.dat", FileType = FileType.File }, folderNode4));
            rootNode.Children.Add(folderNode1);
            var folderNode2 = new Node(new Item { Name = "2", FileType = FileType.Folder }, rootNode);
            folderNode2.Children.Add(new Node(new Item { Name = "8.dat", FileType = FileType.File }, folderNode2));
            rootNode.Children.Add(folderNode2);
            var folderNode3 = new Node(new Item { Name = "3", FileType = FileType.Folder }, rootNode);
            folderNode3.Children.Add(new Node(new Item { Name = "10.txt", FileType = FileType.File }, folderNode3));
            rootNode.Children.Add(folderNode3);
            rootNode.Children.Add(new Node(new Item { Name = "1.txt", FileType = FileType.File }, rootNode));
            rootNode.Children.Add(new Node(new Item { Name = "2.dat", FileType = FileType.File }, rootNode));

            return rootNode;
        }

        private static List<Node> BuildNodeWithOnlyJiffNodes()
        {
            var rootNode = new Node(new Item { Name = "folder-test", FileType = FileType.Folder });
            var folderNode2 = new Node(new Item { Name = "2", FileType = FileType.Folder }, rootNode);

            var node1 = new Node(new Item { Name = "3.jiff", FileType = FileType.File }, rootNode);
            var node2 = new Node(new Item { Name = "9.jiff", FileType = FileType.File }, folderNode2);

            return new List<Node> { node1, node2 };
        }

        private static List<Node> BuildOnlyTwoTxtNodes()
        {
            var rootNode = new Node(new Item { Name = "folder-test", FileType = FileType.Folder });

            var node1 = new Node(new Item { Name = "4.txt", FileType = FileType.File }, rootNode);
            var folderNode2 = new Node(new Item { Name = "2", FileType = FileType.Folder }, rootNode);
            var node2 = new Node(new Item { Name = "5.txt", FileType = FileType.File }, folderNode2);

            return new List<Node> { node1, node2 };
        }

        private static Node BuildNodeWithoutTwoTxtNodes()
        {
            var rootNode = new Node(new Item { Name = "folder-test", FileType = FileType.Folder });
            var folderNode1 = new Node(new Item { Name = "1", FileType = FileType.Folder }, rootNode);
            folderNode1.Children.Add(new Node(new Item { Name = "5.txt", FileType = FileType.File }, folderNode1));
            folderNode1.Children.Add(new Node(new Item { Name = "6.txt", FileType = FileType.File }, folderNode1));
            var folderNode4 = new Node(new Item { Name = "4", FileType = FileType.Folder }, folderNode1);
            folderNode1.Children.Add(folderNode4);
            folderNode4.Children.Add(new Node(new Item { Name = "7.dat", FileType = FileType.File }, folderNode4));
            rootNode.Children.Add(folderNode1);
            var folderNode2 = new Node(new Item { Name = "2", FileType = FileType.Folder }, rootNode);
            folderNode2.Children.Add(new Node(new Item { Name = "8.dat", FileType = FileType.File }, folderNode2));
            folderNode2.Children.Add(new Node(new Item { Name = "9.jiff", FileType = FileType.File }, folderNode2));
            rootNode.Children.Add(folderNode2);
            var folderNode3 = new Node(new Item { Name = "3", FileType = FileType.Folder }, rootNode);
            folderNode3.Children.Add(new Node(new Item { Name = "10.txt", FileType = FileType.File }, folderNode3));
            rootNode.Children.Add(folderNode3);
            rootNode.Children.Add(new Node(new Item { Name = "2.dat", FileType = FileType.File }, rootNode));
            rootNode.Children.Add(new Node(new Item { Name = "3.jiff", FileType = FileType.File }, rootNode));

            return rootNode;
        }



        //private static List<Node> BuildOnlyTwoTxtNodes()
        //{
        //    var rootNode = new Node(new Item { Name = "folder-test", IsFolder = true, FileType = "Folder" });

        //    var node1 = new Node(new Item { Name = "3.jiff", IsFolder = false, FileType = ".jiff" }, rootNode);
        //    var folderNode2 = new Node(new Item { Name = "2", IsFolder = true, FileType = "Folder" }, rootNode);
        //    var node2 = new Node(new Item { Name = "9.jiff", IsFolder = false, FileType = ".jiff" }, folderNode2);

        //    return new List<Node> { node1, node2 };
        //}

        //private static Node BuildNodeWithoutJiffNodes()
        //{
        //    var rootNode = new Node(new Item { Name = "folder-test", IsFolder = true, FileType = "Folder" });
        //    var folderNode1 = new Node(new Item { Name = "1", IsFolder = true, FileType = "Folder" }, rootNode);
        //    folderNode1.Children.Add(new Node(new Item { Name = "4.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
        //    folderNode1.Children.Add(new Node(new Item { Name = "5.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
        //    folderNode1.Children.Add(new Node(new Item { Name = "6.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
        //    var folderNode4 = new Node(new Item { Name = "4", IsFolder = true, FileType = "Folder" }, folderNode1);
        //    folderNode1.Children.Add(folderNode4);
        //    folderNode4.Children.Add(new Node(new Item { Name = "7.dat", IsFolder = false, FileType = ".dat" }, folderNode4));
        //    rootNode.Children.Add(folderNode1);
        //    var folderNode2 = new Node(new Item { Name = "2", IsFolder = true, FileType = "Folder" }, rootNode);
        //    folderNode2.Children.Add(new Node(new Item { Name = "8.dat", IsFolder = false, FileType = ".dat" }, folderNode2));
        //    rootNode.Children.Add(folderNode2);
        //    var folderNode3 = new Node(new Item { Name = "3", IsFolder = true, FileType = "Folder" }, rootNode);
        //    folderNode3.Children.Add(new Node(new Item { Name = "10.txt", IsFolder = false, FileType = ".txt" }, folderNode3));
        //    rootNode.Children.Add(folderNode3);
        //    rootNode.Children.Add(new Node(new Item { Name = "1.txt", IsFolder = false, FileType = ".txt" }, rootNode));
        //    rootNode.Children.Add(new Node(new Item { Name = "2.dat", IsFolder = false, FileType = ".dat" }, rootNode));

        //    return rootNode;
        //}

        [Test]
        public void GetNode_WhenEverythingIsOkay_ShouldReturnCorrectNode()
        {
            // Arrange
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, _fileSystem);
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var nodeList = new List<Node> { node };

            // Assert
            nodeList.Should().BeEquivalentTo(new List<Node> { expectedNode }, options => options.IgnoringCyclicReferences());
        }

        [Test]
        public void FilterNode_WhenExcludeIsOffAbortIsOff_ShouldShowAllFoundFiles()
        {
            // Arrange
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, _fileSystem, ".jiff");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, false);

            // Assert
            filteredNodes.Should().BeEquivalentTo(BuildNodeWithOnlyJiffNodes(), options => options.IgnoringCyclicReferences());
        }

        [Test]
        public void FilterNode_WhenExcludeIsOffAbortIsOn_ShouldShowFoundFile()
        {
            // Arrange
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, _fileSystem, ".jiff");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, true, false);

            // Assert
            filteredNodes.Should().BeEquivalentTo(new List<Node> { BuildNodeWithOnlyJiffNodes()[1] }, options => options.IgnoringCyclicReferences());
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FilterNode_WhenExcludeIsOnAbortIsOff_ShouldShowNodeWithoutJiffNodes()
        {
            // Arrange
            var app = new Application();
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, _fileSystem, ".jiff");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, true);

            // Assert
            filteredNodes.Should().BeEquivalentTo(new List<Node> { BuildNodeWithoutJiffNodes() }, options => options.IgnoringCyclicReferences());
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FilterNode_WhenExcludeIsOffAbortIsOffAndElementsToFindIsSet_ShouldShowFoundFiles()
        {
            // Arrange
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, _fileSystem, ".txt");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, false, 2);

            // Assert
            filteredNodes.Should().BeEquivalentTo(BuildOnlyTwoTxtNodes(), options => options.IgnoringCyclicReferences());
        }

        [Test]
        public void FilterNode_WhenExcludeIsOnAbortIsOnAndElementsToFindIsSet_ShouldShowFoundFiles()
        {
            // Arrange
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, ".txt");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, true, 2);

            // Assert
            filteredNodes.Should().BeEquivalentTo(new List<Node> { BuildNodeWithoutTwoTxtNodes() }, options => options.IgnoringCyclicReferences());
        }

        [Test]
        public void FilterNode_WhenExcludeIsOffAbortIsOffNoMatches_ShouldShowEmptyNode()
        {
            // Arrange
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, "Find Me Cool, Visitor");

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, false);

            // Assert
            filteredNodes.Should().BeEmpty();
        }

        [Test]
        public void GetNode_WhenRootFolderNotExists_ShouldThrowException()
        {
            // Arrange
            var path = "c://folder-that-not-exsists-for-sure";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path);
            var expectedNode = BuildNode();

            // Act
            var action = () => fileSystemVisitor.GetNode();

            // Assert
            action.Should().Throw<FileSystemVisitorException>();
        }
    }
}