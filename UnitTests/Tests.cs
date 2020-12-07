using FakerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class Tests
    {
        private Faker faker;

        private class SimpleClassWithFields
        {
            public int Int;
            public float Float;
            public long Long;
            public double Double;
            public char Char;
            public bool Bool;
            public string String;
            public DateTime DateTime;
        }

        private class ClassMultipleconstructors
        {
            public int Int { get; }
            public string String { get; }
            public ClassMultipleconstructors(int i)
            {

            }
            public ClassMultipleconstructors(int i, int j)
            {
                Int = -10;
                String = "done";
            }
        }

        public class ListClass
        {
            public List<int> intList;
            public List<object> objectList;
        }

        public class RecursionClass
        {
            public RecursionClass recursionObject;
        }

        public class PropertiesWithoutConstructor
        {
            public DateTime DateTimeProperty
            { get; set; }
            public string StringProperty
            { get; set; }
            public object ObjectProperty
            { get; set; }
        }



        [TestInitialize]
        public void Setup()
        {
            faker = new Faker();
        }

        [TestMethod]
        public void SimpleClassWithFieldsTest()
        {
            SimpleClassWithFields testAllType = faker.Create<SimpleClassWithFields>();
            Assert.IsNotNull(testAllType);
            Assert.AreNotEqual(testAllType.Char, "");
        }

        [TestMethod]
        public void ClassMultipleconstructorsTest()
        {
            ClassMultipleconstructors testConstructor = faker.Create<ClassMultipleconstructors>();
            Assert.IsNotNull(testConstructor);
            Assert.AreEqual(testConstructor.Int, -10);
            Assert.AreEqual(testConstructor.String, "done");
        }

        [TestMethod]
        public void ListClassTest()
        {
            ListClass listClass = faker.Create<ListClass>();
            Assert.AreNotEqual(null, listClass.intList);
            Assert.AreNotEqual(null, listClass.objectList);
            Assert.AreEqual(0, listClass.objectList.Count);
        }

        [TestMethod]
        public void RecursionClassTest()
        {
            RecursionClass noConstructor = faker.Create<RecursionClass>();
            Assert.AreEqual(null, noConstructor.recursionObject);
        }

        [TestMethod]
        public void PropertiesClassTest()
        {
            PropertiesWithoutConstructor propWithoutConstructorObject = faker.Create<PropertiesWithoutConstructor>();
            Assert.AreNotEqual(null, propWithoutConstructorObject.ObjectProperty);
            Assert.AreNotEqual(null, propWithoutConstructorObject.StringProperty);
            Assert.AreNotEqual(null, propWithoutConstructorObject.DateTimeProperty);
        }
    }
}
