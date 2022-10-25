using FluentAssertions;
using NUnit.Framework;
using System.Windows;

namespace AdvancedCSharp.Tests
{
    public class Tests
    {
        public static Node BuildNode()
        {
            var rootNode = new Node(new Leaf { Name = "folder-test", IsFolder = true, FileType = "Folder" });
            var folderNode1 = new Node(new Leaf { Name = "1", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode1.Children.Add(new Node(new Leaf { Name = "4.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
            folderNode1.Children.Add(new Node(new Leaf { Name = "5.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
            folderNode1.Children.Add(new Node(new Leaf { Name = "6.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
            var folderNode4 = new Node(new Leaf { Name = "4", IsFolder = true, FileType = "Folder" }, folderNode1);
            folderNode1.Children.Add(folderNode4);
            folderNode4.Children.Add(new Node(new Leaf { Name = "7.dat", IsFolder = false, FileType = ".dat" }, folderNode4));
            rootNode.Children.Add(folderNode1);
            var folderNode2 = new Node(new Leaf { Name = "2", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode2.Children.Add(new Node(new Leaf { Name = "8.dat", IsFolder = false, FileType = ".dat" }, folderNode2));
            folderNode2.Children.Add(new Node(new Leaf { Name = "9.jiff", IsFolder = false, FileType = ".jiff" }, folderNode2));
            rootNode.Children.Add(folderNode2);
            var folderNode3 = new Node(new Leaf { Name = "3", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode3.Children.Add(new Node(new Leaf { Name = "10.txt", IsFolder = false, FileType = ".txt" }, folderNode3));
            rootNode.Children.Add(folderNode3);
            rootNode.Children.Add(new Node(new Leaf { Name = "1.txt", IsFolder = false, FileType = ".txt" }, rootNode));
            rootNode.Children.Add(new Node(new Leaf { Name = "2.dat", IsFolder = false, FileType = ".dat" }, rootNode));
            rootNode.Children.Add(new Node(new Leaf { Name = "3.jiff", IsFolder = false, FileType = ".jiff" }, rootNode));

            return rootNode;
        }

        public static Node BuildNodeWthoutTwoTxt()
        {
            var rootNode = new Node(new Leaf { Name = "folder-test", IsFolder = true, FileType = "Folder" });
            var folderNode1 = new Node(new Leaf { Name = "1", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode1.Children.Add(new Node(new Leaf { Name = "5.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
            folderNode1.Children.Add(new Node(new Leaf { Name = "6.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
            var folderNode4 = new Node(new Leaf { Name = "4", IsFolder = true, FileType = "Folder" }, folderNode1);
            folderNode1.Children.Add(folderNode4);
            folderNode4.Children.Add(new Node(new Leaf { Name = "7.dat", IsFolder = false, FileType = ".dat" }, folderNode4));
            rootNode.Children.Add(folderNode1);
            var folderNode2 = new Node(new Leaf { Name = "2", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode2.Children.Add(new Node(new Leaf { Name = "8.dat", IsFolder = false, FileType = ".dat" }, folderNode2));
            folderNode2.Children.Add(new Node(new Leaf { Name = "9.jiff", IsFolder = false, FileType = ".jiff" }, folderNode2));
            rootNode.Children.Add(folderNode2);
            var folderNode3 = new Node(new Leaf { Name = "3", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode3.Children.Add(new Node(new Leaf { Name = "10.txt", IsFolder = false, FileType = ".txt" }, folderNode3));
            rootNode.Children.Add(folderNode3);
            rootNode.Children.Add(new Node(new Leaf { Name = "2.dat", IsFolder = false, FileType = ".dat" }, rootNode));
            rootNode.Children.Add(new Node(new Leaf { Name = "3.jiff", IsFolder = false, FileType = ".jiff" }, rootNode));

            return rootNode;
        }

        public static List<Node> BuildOnlyJiffNodes()
        {
            var rootNode = new Node(new Leaf { Name = "folder-test", IsFolder = true, FileType = "Folder" });
            var node1 = new Node(new Leaf { Name = "1.txt", IsFolder = false, FileType = ".txt" }, rootNode);

            var folderNode1 = new Node(new Leaf { Name = "1", IsFolder = true, FileType = "Folder" }, rootNode);
            var node2 = new Node(new Leaf { Name = "4.txt", IsFolder = false, FileType = ".txt" }, folderNode1);
            
            return new List<Node> { node1, node2};
        }

        public static List<Node> BuildOnlyTwoTxtNodes()
        {
            var rootNode = new Node(new Leaf { Name = "folder-test", IsFolder = true, FileType = "Folder" });

            var node1 = new Node(new Leaf { Name = "3.jiff", IsFolder = false, FileType = ".jiff" }, rootNode);
            var folderNode2 = new Node(new Leaf { Name = "2", IsFolder = true, FileType = "Folder" }, rootNode);
            var node2 = new Node(new Leaf { Name = "9.jiff", IsFolder = false, FileType = ".jiff" }, folderNode2);

            return new List<Node> { node1, node2 };
        }

        public static Node BuildNodeWithoutJiffNodes()
        {
            var rootNode = new Node(new Leaf { Name = "folder-test", IsFolder = true, FileType = "Folder" });
            var folderNode1 = new Node(new Leaf { Name = "1", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode1.Children.Add(new Node(new Leaf { Name = "4.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
            folderNode1.Children.Add(new Node(new Leaf { Name = "5.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
            folderNode1.Children.Add(new Node(new Leaf { Name = "6.txt", IsFolder = false, FileType = ".txt" }, folderNode1));
            var folderNode4 = new Node(new Leaf { Name = "4", IsFolder = true, FileType = "Folder" }, folderNode1);
            folderNode1.Children.Add(folderNode4);
            folderNode4.Children.Add(new Node(new Leaf { Name = "7.dat", IsFolder = false, FileType = ".dat" }, folderNode4));
            rootNode.Children.Add(folderNode1);
            var folderNode2 = new Node(new Leaf { Name = "2", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode2.Children.Add(new Node(new Leaf { Name = "8.dat", IsFolder = false, FileType = ".dat" }, folderNode2));
            rootNode.Children.Add(folderNode2);
            var folderNode3 = new Node(new Leaf { Name = "3", IsFolder = true, FileType = "Folder" }, rootNode);
            folderNode3.Children.Add(new Node(new Leaf { Name = "10.txt", IsFolder = false, FileType = ".txt" }, folderNode3));
            rootNode.Children.Add(folderNode3);
            rootNode.Children.Add(new Node(new Leaf { Name = "1.txt", IsFolder = false, FileType = ".txt" }, rootNode));
            rootNode.Children.Add(new Node(new Leaf { Name = "2.dat", IsFolder = false, FileType = ".dat" }, rootNode));

            return rootNode;
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FileSystemVisitor_WhenEverythingIsOkay_ShouldReturnCorrectNode()
        {
            // Arrange
            var app = new Application();
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path);
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var nodeList = new List<Node> { node };

            // Assert
            nodeList.Should().BeEquivalentTo(new List<Node> { expectedNode }, options => options.IgnoringCyclicReferences());
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FileSystemVisitor_WhenFilterIsOkay_ShouldShowAllFoundFiles()
        {
            // Arrange
            var app = new Application();
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, ".jiff");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, false, 9999);

            // Assert
            filteredNodes.Should().BeEquivalentTo(BuildOnlyJiffNodes(), options => options.IgnoringCyclicReferences());
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FileSystemVisitor_WhenFilterIsOkayAndTakesOnlyFirst_ShouldShowAllFoundFiles()
        {
            // Arrange
            var app = new Application();
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, ".jiff");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, true, false, 9999);

            // Assert
            filteredNodes.Should().BeEquivalentTo(new List<Node> { BuildOnlyJiffNodes()[0] }, options => options.IgnoringCyclicReferences());
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FileSystemVisitor_WhenFilterIsOkayAndExcludes_ShouldShowAllFoundFiles()
        {
            // Arrange
            var app = new Application();
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, ".jiff");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, true, 9999);

            // Assert
            filteredNodes.Should().BeEquivalentTo(new List<Node> { BuildNodeWithoutJiffNodes()}, options => options.IgnoringCyclicReferences());
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FileSystemVisitor_WhenFilterIsOkayAndTakesOnly2Files_ShouldShowAllFoundFiles()
        {
            // Arrange
            var app = new Application();
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, ".txt");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, false, 2);

            // Assert
            filteredNodes.Should().BeEquivalentTo(BuildOnlyTwoTxtNodes(), options => options.IgnoringCyclicReferences());
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FileSystemVisitor_WhenFilterIsOkayAndExludesOnly2Files_ShouldShowAllFoundFiles()
        {
            // Arrange
            var app = new Application();
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, ".txt");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, true, 2);

            // Assert
            filteredNodes.Should().BeEquivalentTo(new List<Node> { BuildNodeWthoutTwoTxt() }, options => options.IgnoringCyclicReferences());
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FileSystemVisitor_WhenFilterIsWrong_ShouldBeEmpty()
        {
            // Arrange
            var app = new Application();
            var path = "c://folder-test";
            var fileSystemVisitor = FileSystemVisitorFactory.GetFileSystemVisitor(path, "Find Me Cool, Visitor");
            var expectedNode = BuildNode();

            // Act
            var node = fileSystemVisitor.GetNode();
            var filteredNodes = fileSystemVisitor.FilterNode(node, false, false, 999);

            // Assert
            filteredNodes.Should().BeEmpty();
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void FileSystemVisitor_WhenFolderNotExists_ShouldReturnException()
        {
            // Arrange
            var app = new Application();
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