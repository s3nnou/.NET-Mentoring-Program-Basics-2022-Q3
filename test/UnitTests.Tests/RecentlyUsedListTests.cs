using FluentAssertions;
using UnitTesting;

namespace UnitTests.Tests
{
    public class RecentlyUsedListTests
    {
        [Test]
        public void Indexer_WhenIndexIsInListCount_ShouldReturnItem()
        {
            // Arrange
            var list = new RecentlyUsedList();
            var item = "hello";
            var index = 0;
            var expectedItem = "hello";
            list.Add(item);

            // Act 
            var recivedItem = list[index];

            // Assert
            recivedItem.Should().Be(expectedItem);
        }

        [Test]
        public void Indexer_WhenIndexIsNegative_ShouldThrowException()
        {
            // Arrange
            var list = new RecentlyUsedList();
            var index = -1;

            // Act 
            var action = () => list[index];

            // Assert
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void Indexer_WhenIndexIsLargerThenSize_ShouldThrowException()
        {
            // Arrange
            var list = new RecentlyUsedList();
            var index = 6;

            // Act 
            var action = () => list[index];

            // Assert
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void Add_WhenNoDuplicatesExistsInList_ShouldAddItemToCollection()
        {
            // Arrange
            var list = new RecentlyUsedList();
            var item = "hello";
            var expectedItem = "hello";
            var expectedSize = 1;

            // Act
            list.Add(item);

            // Assert
            list[0].Should().BeEquivalentTo(expectedItem);
            list.Count.Should().Be(expectedSize);
        }

        [Test]
        public void Add_WhenDuplicatesExistsInList_ShouldThrowExceptionAndAbortAdding()
        {
            // Arrange
            var list = new RecentlyUsedList();
            var firstItem = "hello";
            var secondItem = "hello";
            var expectedItem = "hello";

            // Act & Assert
            list.Add(firstItem);
            list[0].Should().BeEquivalentTo(expectedItem);
            list.Count.Should().Be(1);
            list.Add(secondItem);
            list.Count.Should().Be(1);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]

        public void Add_WhenDuplicatesExistsInList_ShouldThrowExceptionAndAbortAdding(string stringToAdd)
        {
            // Arrange
            var list = new RecentlyUsedList();

            // Act
            var action = () => list.Add(stringToAdd);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void RecentlyUsedList_WhenParametlessConsturctorUsed_ShouldUseDefaultSize()
        {
            // Arrange
            var expectedLimit = 5;

            // Act
            var list = new RecentlyUsedList();

            // Assert
            list.Size.Should().Be(expectedLimit);
        }

        [Test]
        public void RecentlyUsedList_WhenSizeNumberUsed_ShouldUseProvidedSize()
        {
            // Arrange
            var size = 6;
            var expectedSize = 6;

            // Act
            var list = new RecentlyUsedList(size);

            // Assert
            list.Size.Should().Be(expectedSize);
        }

        [Test]
        public void Add_WhenCountIsBiggerThenSizw_ShouldRemoveNewItems()
        {
            // Arrange
            var list = new RecentlyUsedList(3);
            var expectedList = new RecentlyUsedList(3);
            expectedList.Add("1");
            expectedList.Add("2");
            expectedList.Add("3");

            // Act
            list.Add("1");
            list.Add("2");
            list.Add("3");
            list.Add("4");
            list.Add("5");

            // Assert
            list.Should().BeEquivalentTo(expectedList);
        }
    }
}