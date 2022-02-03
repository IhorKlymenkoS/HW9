using HW9;
using NUnit.Framework;

namespace ListsUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DefaultConsructor_ShouldCreateEmptyList()
        {
            IList actualList = new MyArrayList();

            CollectionAssert.AreEqual(new int[] { }, actualList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2, 3, 4, 5 })]
        public void ConstructorFromArray_WhenSourceArrayChanged_ShouldNotChangeList
            (int[] sourceArray, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            sourceArray[2] = 10;

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2, 3, 4, 5 })]
        public void ConstructorFromArray_WhenArrayNotEmpty_ShouldFillValuesInList
            (int[] sourceArray, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 9, 9, 9 }, 2, new[] { 1, 2, 9, 9, 9, 3, 4, 5 })]
        public void AddByIndex_WhenArrayPassedWithValidIndexesAndNotEmpty_ShouldInsertArrayByPosition
            (int[] sourceArray, int[] arrayToInsert, int insertPosition, int[] expectedArray)
        {
            //Arrange
            IList actualList = new MyArrayList(sourceArray);

            //Act`
            actualList.AddByIndex(insertPosition, arrayToInsert);

            //Assert
            CollectionAssert.AreEqual(expectedArray, actualList);
        }
    }
}