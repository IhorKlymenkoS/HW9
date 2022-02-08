using HW9;
using NUnit.Framework;
using System;

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

        [TestCase(new int[] { }, 10, new[] { 10 })]
        [TestCase(new[] { 5 }, 8, new[] { 8, 5 })]
        [TestCase(new[] { 1, 2, 3 }, 7, new[] { 7, 1, 2, 3 })]
        public void AddFront_WhenValidParamsPassed_ShouldInsertElementAtTheFront(
           int[] sourceArray, int insertValue, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.AddFront(insertValue);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { }, 10, new[] { 10 })]
        [TestCase(new[] { 5 }, 8, new[] { 5, 8 })]
        [TestCase(new[] { 1, 2, 3 }, 7, new[] { 1, 2, 3, 7 })]

        public void AddBack_WhenValidParamsPassed_ShouldInsertElementAtTheEnd(
   int[] sourceArray, int insertValue, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.AddBack(insertValue);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { }, 0, 10, new[] { 10 })]
        [TestCase(new [] { 5 }, 0, 3, new[] { 3, 5 })]
        [TestCase(new[] { 6 }, 1, 8, new[] { 6, 8 })]
        [TestCase(new[] { 1, 2, 3 }, 0, 5, new[] { 5, 1, 2, 3 })]
        [TestCase(new[] { 1, 2, 3 }, 2, 7, new[] { 1, 2, 7, 3 })]
        [TestCase(new[] { 1, 2, 3 }, 3, 8, new[] { 1, 2, 3, 8 })]
        public void AddByIndex_WhenValidParamsPassed_ShouldInsertElementByIndex(
            int[] sourceArray, int insertIndex, int insertValue, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.AddByIndex(insertIndex, insertValue);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new[] { 5 }, new int[] { })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 2, 3 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, new[] { 9, 2, 4, 5 })]
        public void RemoveFront_WhenValidParamsPassed_ShouldRemoveElementAtTheFront(
   int[] sourceArray, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.RemoveFrontElement();

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { })]
        public void RemoveFront_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.RemoveFrontElement();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new[] { 5 }, new int[] { })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, new[] { 7, 9, 2, 4 })]
        public void RemoveBack_WhenValidParamsPassed_ShouldRemoveElementAtTheBack(
int[] sourceArray, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.RemoveBackElement();

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { })]
        public void RemoveBack_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.RemoveBackElement();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new[] { 5 }, 0, new int[] { })]
        [TestCase(new[] { 1, 2, 3 }, 1, new[] { 1, 3 })]
        [TestCase(new[] { 1, 2, 8, 3, 5 }, 3, new[] { 1, 2, 8, 5 })]
        [TestCase(new[] { 1, 2, 8, 3, 5 }, 0, new[] { 2, 8, 3, 5 })]
        [TestCase(new[] { 1, 2, 8, 3, 5 }, 4, new[] { 1, 2, 8, 3 })]
        public void RemoveByIndexElement_WhenValidParamsPassed_ShouldRemoveElementByIndex(
int[] sourceArray, int index, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.RemoveByIndexElement(index);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { }, 0)]
        [TestCase(new[] { 1, 2, 3 }, -1)]
        [TestCase(new[] { 1, 2, 8, 3, 5 }, 7)]
        public void RemoveByIndexElement_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray, int index)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.RemoveByIndexElement(index);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new[] { 5 }, 1, new int[] { })]
        [TestCase(new[] { 1, 2, 3 }, 2, new[] { 3 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, 3, new[] { 4, 5 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, 0, new[] { 7, 9, 2, 4, 5 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, 5, new int[] { })]
        public void RemoveFrontNElements_WhenValidParamsPassed_ShouldRemoveNElementsAtTheFront(
int[] sourceArray, int count, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.RemoveFrontNElements(count);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { }, 1)]
        [TestCase(new[] { 1, 2, 3 }, 8)]
        [TestCase(new[] { 1, 2, 3 }, -1)]
        public void RemoveFrontNElements_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray, int count)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.RemoveFrontNElements(count);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new[] { 5 }, 1, new int[] { })]
        [TestCase(new[] { 1, 2, 3 }, 2, new[] { 1 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, 3, new[] { 7, 9 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, 0, new[] { 7, 9, 2, 4, 5 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, 5, new int[] { })]
        public void RemoveBackNElements_WhenValidParamsPassed_ShouldRemoveNElementsAtTheBack(
int[] sourceArray, int count, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.RemoveBackNElements(count);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { }, 1)]
        [TestCase(new[] { 1, 2, 3 }, 8)]
        [TestCase(new[] { 1, 2, 3 }, -1)]
        public void RemoveBackNElements_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray, int count)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.RemoveBackNElements(count);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new[] { 5 }, 0, 1, new int[] {})]
        [TestCase(new[] { 1, 2, 3 }, 0, 1, new [] { 2, 3 })]
        [TestCase(new[] { 1, 2, 3 }, 0, 0, new [] {1, 2, 3 })]
        [TestCase(new[] { 1, 2, 3 }, 0, 2, new [] { 3 })]
        [TestCase(new[] { 1, 2, 3 }, 0, 3, new int[] {})]
        [TestCase(new[] { 1, 2, 3 }, 1, 2, new[] { 1 })]
        [TestCase(new[] { 1, 2, 3 }, 2, 1, new[] { 1, 2 })]
        [TestCase(new[] { 1, 2, 3 }, 1, 1, new[] { 1, 3 })]
        [TestCase(new[] { 7, 9, 2, 4, 5 }, 2, 2, new[] { 7, 9, 5 })]
        public void RemoveNByIndex_WhenValidArgsPassed_ShouldRemoveElements
            (int[] sourceArray, int index, int count, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.RemoveByIndexNElements(index, count);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { }, 0, 1)]
        [TestCase(new int[] { }, 1, 0)]
        [TestCase(new[] { 1, 2, 3 }, -1, 0)]
        [TestCase(new[] { 1, 2, 3 }, -1, -1)]
        [TestCase(new[] { 1, 2, 3 }, 8, 2)]
        [TestCase(new[] { 1, 2, 3 }, 1, 10)]
        public void RemoveNByIndex_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray, int index, int count)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.RemoveByIndexNElements(index, count);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new[] { 1, 2, 3 }, 2, 1)]
        [TestCase(new[] { 3, 2, 3 }, 3, 0)]
        [TestCase(new[] { 7, 9, 4, 4, 4 }, 4, 2)]
        [TestCase(new[] { 7, 9, 4, 4, 4 }, 11, -1)]
        [TestCase(new int[] { }, 2, -1)]
        public void GetFirstIndexByValue_WhenValidArgsPassed_ShouldGetIndex
    (int[] sourceArray, int value, int expectedIndex)
        {
            IList actualList = new MyArrayList(sourceArray);

            int actualIndex = actualList.FirstIndexByValue(value);

            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new[] { 1 }, new [] { 1 })]
        [TestCase(new[] { 1, 2, 3 }, new [] { 3, 2, 1 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, new [] { 6, 5, 4, 3, 2, 1 })]
        public void ReverseArray_WhenValidArgsPassed_ShouldReverseArray
    (int[] sourceArray, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.Reverse();

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new[] { 1, 2, 3 }, 3)]
        [TestCase(new[] { 7, 5, 2, 9, 2, 4 }, 9)]
        [TestCase(new[] { 3, 3, 3, 4, 4, 3 }, 4)]
        [TestCase(new[] { -1, -2, -3, -4, -5, -6 }, -1)]
        [TestCase(new[] { -1, -2, -3, 0, -5, -6 }, 0)]
        [TestCase(new[] { -1, -2, -3, 4, 5, 6 }, 6)]
        public void GetMaxElementValue_WhenValidArgsPassed_ShouldGetMaxElementValue
(int[] sourceArray, int expectedValue)
        {
            IList actualList = new MyArrayList(sourceArray);

            int actualValue = actualList.GetMaxElementValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(new int[] { })]
        public void GetMaxElementValue_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.GetMaxElementValue();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new[] { 1, 2, 3 }, 1)]
        [TestCase(new[] { 7, 5, 9, 9, 2, 4 }, 2)]
        [TestCase(new[] { 3, 3, 3, 3, 1, 1 }, 1)]
        [TestCase(new[] { -1, -2, -3, -4, -5, -6 }, -6)]
        [TestCase(new[] { 0, 1, 2, 3, 4, 5 }, 0)]
        [TestCase(new[] { -1, -2, -3, 4, 5, 6 }, -3)]
        public void GetMinElementValue_WhenValidArgsPassed_ShouldGetMinElementValue
(int[] sourceArray, int expectedValue)
        {
            IList actualList = new MyArrayList(sourceArray);

            int actualValue = actualList.GetMinElementValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(new int[] { })]
        public void GetMinElementValue_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.GetMinElementValue();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new int[] { 1 }, 0)]
        [TestCase(new[] { 1, 2, 3 }, 2)]
        [TestCase(new[] { 7, 5, 2, 2, 2, 11 }, 5)]
        [TestCase(new[] { 3, 3, 3, 4, 4, 3 }, 3)]
        [TestCase(new[] { -1, -2, -3, -4, -5, -6 }, 0)]
        [TestCase(new[] { -1, -2, -3, -4, 0, -6 }, 4)]
        [TestCase(new[] { -1, -2, -3, 4, 5, 6 }, 5)]
        public void GetMaxElementIndex_WhenValidArgsPassed_ShouldGetMaxElementIndex
(int[] sourceArray, int expectedIndex)
        {
            IList actualList = new MyArrayList(sourceArray);

            int actualIndex = actualList.GetMaxElementIndex();

            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase(new int[] { })]
        public void GetMaxElementIndex_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.GetMaxElementIndex();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new int[] { 1 }, 0)]
        [TestCase(new[] { 1, 2, 3 }, 0)]
        [TestCase(new[] { 7, 5, 9, 9, 7, 4 }, 5)]
        [TestCase(new[] { 3, 3, 3, 3, 1, 1 }, 4)]
        [TestCase(new[] { -1, -2, -3, -4, -5, -6 }, 5)]
        [TestCase(new[] { 0, 1, 2, 3, 4, 5 }, 0)]
        [TestCase(new[] { -1, -2, -3, 4, 5, 6 }, 2)]
        public void GetMinElementIndex_WhenValidArgsPassed_ShouldGetMinElementIndex
(int[] sourceArray, int expectedIndex)
        {
            IList actualList = new MyArrayList(sourceArray);

            int actualIndex = actualList.GetMinElementIndex();

            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase(new int[] { })]
        public void GetMinElementIndex_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.GetMinElementIndex();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new int[] { }, true, new int[] { })]
        [TestCase(new[] { 1 }, true, new[] { 1 })]
        [TestCase(new[] { 4, 1, 3 }, true, new[] { 1, 3, 4 })]
        [TestCase(new[] { 4, 1, 3, 9, 4, 2 }, true, new[] { 1, 2, 3, 4, 4, 9 })]
        [TestCase(new[] { 11, - 22, 44, 0, 72, -5, -22 }, true, new[] { -22, -22, -5, 0, 11, 44, 72 })]
        [TestCase(new int[] { }, false, new int[] { })]
        [TestCase(new[] { 1 }, false, new[] { 1 })]
        [TestCase(new[] { 4, 1, 3 }, false, new[] { 4, 3, 1 })]
        [TestCase(new[] { 4, 1, 3, 9, 4, 2 }, false, new[] { 9, 4, 4, 3, 2, 1 })]
        [TestCase(new[] { 11, -22, 44, 0, 72, -5, -22 }, false, new[] { 72, 44, 11, 0, -5, -22, -22 })]
        public void SortArray_WhenValidArgsPassed_ShouldSortArray
(int[] sourceArray, bool ascending, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.Sort(ascending);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new[] { 1 }, 1, new int[] { })]
        [TestCase(new[] { 4, 1, 3 }, 3, new[] { 4, 1 })]
        [TestCase(new[] { 4, 1, 3, 9, 4, 2 }, 4, new[] { 1, 3, 9, 4, 2 })]
        [TestCase(new[] { 11, -22, 44, 0, 72, -5, -22 }, -22, new[] { 11, 44, 0, 72, -5, -22 })]
        public void DeleteByValueFirst_WhenValidArgsPassed_ShouldDeleteByValueFirst
(int[] sourceArray, int value, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.DeleteByValueFirst(value);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new int[] { }, 2)]
        [TestCase(new[] { 1 }, 2)]
        [TestCase(new[] { 11, -22, 44, 0, 72, -5, -22 }, -99)]
        public void DeleteByValueFirst_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray, int value)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.DeleteByValueFirst(value);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new[] { 1 }, 1, 1, new int[] { })]
        [TestCase(new[] { 4, 1, 3 }, 3, 1, new[] { 4, 1 })]
        [TestCase(new[] { 4, 1, 3, 9, 4, 2 }, 4, 2, new[] { 1, 3, 9, 2 })]
        [TestCase(new[] { 11, -22, 44, 0, 72, -5, -22 }, -22, 2, new[] { 11, 44, 0, 72, -5 })]
        [TestCase(new[] { 1, 0, 0, 1, 1, 0, 1 }, 0, 3, new[] { 1, 1, 1, 1 })]
        public void DeleteByValueAll_WhenValidArgsPassed_ShouldDeleteByValueAll
(int[] sourceArray, int value, int expectedCount, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            int actualCount = actualList.DeleteByValueAll(value);

            CollectionAssert.AreEqual(expectedArray, actualList);
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(new int[] { }, 2)]
        [TestCase(new[] { 1 }, 2)]
        [TestCase(new[] { 11, -22, 44, 0, 72, -5, -22 }, -99)]
        public void DeleteByValueAll_WhenValidParamsNotPassed_ShouldCatchArgumentException(
int[] sourceArray, int value)
        {
            IList actualList = new MyArrayList(sourceArray);
            try
            {
                actualList.DeleteByValueAll(value);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value does not fall within the expected range.", ex.Message);
                Assert.Pass();
            }


            Assert.Fail();
        }

        [TestCase(new[] { 4, 1, 3 }, new[] { 1, 3 }, new[] { 1, 3, 4, 1, 3 })]
        public void AddFrontArray_WhenValidArgsPassed_ShouldAddFrontArray
(int[] sourceArray, int[] addArray, int[] expectedArray)
        {
            IList actualList = new MyArrayList(sourceArray);

            actualList.AddFrontArray(addArray);

            CollectionAssert.AreEqual(expectedArray, actualList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 9, 9, 9 }, 2, new[] { 1, 2, 9, 9, 9, 4, 5 })]
        public void AddByIndex_WhenArrayPassedWithValidIndexesAndNotEmpty_ShouldInsertArrayByPosition
     (int[] sourceArray, int[] arrayToInsert, int insertPosition, int[] expectedArray)
        {
            //Arrange
            IList actualList = new MyArrayList(sourceArray);

            //Act
            actualList.AddByIndex(insertPosition, arrayToInsert);

            //Assert
            CollectionAssert.AreEqual(expectedArray, actualList);
        }
    }
}