using Faker_Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Faker_Lib.FieldGenerators.CustomGenerators;

namespace UnitTests
{
    [TestClass]
    public class Tests
    {
        private Faker faker;
        private FakerConfig fakerConfig;
        private CustomIntGenerator customIntGenerator = new CustomIntGenerator();

        private class SimpleClassWithFields
        {
            public int Int;
            public float Float;
            public long Long;
            public double Double;
            public char Char;
            public bool Bool;
            public string String { get; set; }
            public DateTime DateTime { get; set; }
        }


        private class ClassMultipleconstructors
        {
            public double Double { get; }
            public ushort UShort { get; set; }
            public string String { get; }
            public ClassMultipleconstructors(int i)
            {

            }
            public ClassMultipleconstructors(int i, int j)
            {
                Double = -42.42;
                UShort = 4242;
                String = "azaza";
            }
        }


        private class ListClass
        {
            public List<int> intList;
            public List<object> objectList;
        }


        private class RecursionClass
        {
            public RecursionClass recursionObject;
        }



        private class MutualRecursionFirstClass
        {
            public MutualRecursionSecondClass second;
        }

        private class MutualRecursionSecondClass
        {
            public MutualRecursionThirdClass third;
        }

        private class MutualRecursionThirdClass
        {
            public MutualRecursionFirstClass first;
        }

        private class CustomGenerateClass
        {
            public string String;
            public int Int;
            public int PropInt { get; }
            public int PropIntConfig { get; set; }
        }

        private class PrivateConstructorClass
        {
            public int IntProperty { get; private set; }
            public double DoubleProperty { get; set; }

            private PrivateConstructorClass(int intParameter, double doubleParameter)
            {
                IntProperty = intParameter;
                DoubleProperty = doubleParameter;
            }
        }


        [TestInitialize]
        public void Setup()
        {
            fakerConfig = new FakerConfig();
            fakerConfig.Add<CustomGenerateClass, int, CustomIntGenerator>(cl => cl.PropIntConfig);
            faker = new Faker(fakerConfig);
        }

        [TestMethod]
        public void SimpleClassWithFieldsTest()
        {
            SimpleClassWithFields simpleClassWithFields;
            simpleClassWithFields = faker.Generate<SimpleClassWithFields>();
            Assert.IsNotNull(simpleClassWithFields);
            Assert.AreNotEqual(simpleClassWithFields.Char, "");
            Assert.AreNotEqual(simpleClassWithFields.Double, null);
        }

        [TestMethod]
        public void ClassMultipleconstructorsTest()
        {
            ClassMultipleconstructors testConstructor = faker.Generate<ClassMultipleconstructors>();
            Assert.IsNotNull(testConstructor);
            Assert.AreEqual(testConstructor.Double, -42.42);
            Assert.AreEqual(testConstructor.UShort, 4242);
            Assert.AreEqual(testConstructor.String, "azaza");
        }

        [TestMethod]
        public void ListClassTest()
        {
            ListClass listClass = faker.Generate<ListClass>();
            Assert.AreNotEqual(null, listClass.intList);
            Assert.AreNotEqual(null, listClass.objectList);
            Assert.AreNotEqual(0, listClass.intList.Count);
            Assert.AreNotEqual(0, listClass.objectList.Count);
        }

        [TestMethod]
        public void RecursionClassTest()
        {
            RecursionClass recursionClass = faker.Generate<RecursionClass>();
            Assert.AreNotEqual(null, recursionClass.recursionObject);
            Assert.AreNotEqual(null, recursionClass.recursionObject.recursionObject);
            Assert.AreEqual(null, recursionClass.recursionObject.recursionObject.recursionObject);
        }

        [TestMethod]
        public void MutualRecursionTest()
        {
            MutualRecursionFirstClass first = faker.Generate<MutualRecursionFirstClass>();

            Assert.IsNotNull(first);
            Assert.IsNotNull(first.second);
            Assert.IsNotNull(first.second.third);
            Assert.IsNotNull(first.second.third.first);
            Assert.IsNull(first.second.third.first.second.third.first.second.third.first);
        }

        [TestMethod]
        public void CustomGenerationTest()
        {
            CustomGenerateClass customGenerateClass = faker.Generate<CustomGenerateClass>();
            Assert.IsNotNull(customGenerateClass);
            Assert.AreEqual(customIntGenerator.generatedValue, customGenerateClass.PropIntConfig);
            Assert.AreEqual(0, customGenerateClass.PropInt);
        }

        [TestMethod]
        public void PrivateConstructorTest()
        {
            PrivateConstructorClass privateConstructorClass = faker.Generate<PrivateConstructorClass>();
            Assert.IsNull(privateConstructorClass);
        }

    }
}
