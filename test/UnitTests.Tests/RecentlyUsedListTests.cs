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
            var item1 = "hello";
            var item2 = "evening";
            var item3 = "morning";
            var expectedSize = 3;
            var expectedList = new RecentlyUsedList(3);
            expectedList.Add("hello");
            expectedList.Add("evening");
            expectedList.Add("morning");

            // Act
            list.Add(item1);
            list.Add(item2);
            list.Add(item3);

            // Assert
            list.Should().BeEquivalentTo(expectedList);
            list.Count.Should().Be(expectedSize);
        }

        [Test]
        public void Add_WhenDuplicatesExistsInList_ShouldRemoveDuplicateAndInsertItOnTop()
        {
            // Arrange
            var list = new RecentlyUsedList();
            var item1 = "hello";
            var item2 = "evening";
            var item3 = "morning";
            var duplicateItem = "hello";
            var expectedSize = 3;
            var expectedList = new RecentlyUsedList(3);
            expectedList.Add("evening");
            expectedList.Add("morning");
            expectedList.Add("hello");

            // Act
            list.Add(item1);
            list.Add(item2);
            list.Add(item3);
            list.Add(duplicateItem);

            // Assert
            list.Should().BeEquivalentTo(expectedList);
            list.Count.Should().Be(expectedSize);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]

        public void Add_WhenStringIsNullOrEmpty_ShouldThrowExceptionAndAbortAdding(string stringToAdd)
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
            var list = new RecentlyUsedList(0);

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
        public void Add_WhenCountIsBiggerThenSize_ShouldRemoveNewItems()
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