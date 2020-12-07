using Faker_Lib;
using Faker_Lib.FieldGenerators;
using Faker_Lib.FieldGenerators.GenericTypeGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FakerLib
{
    public class Faker
    {
        Dictionary<Type, ISimpleTypeGenerator> simpleTypesGenerators;
        Dictionary<Type, IGenericTypeGenerator> genericTypesGenerators;
        public Stack<Type> generatedTypes;
        protected const string pluginsDirectoryPath = "D:\\BSUIR\\Labs\\Labs_Sem5\\SPP\\Faker\\Plugins";

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        protected object Create(Type type)
        {
            object generated;

            if (simpleTypesGenerators.TryGetValue(type, out ISimpleTypeGenerator simpleTypeGenerator))
            {
                generated = simpleTypeGenerator.Generate();
            }
            else if (type.IsGenericType && genericTypesGenerators.TryGetValue(type.GetGenericTypeDefinition(), out IGenericTypeGenerator genericTypeGenerator))
            {
                generated = genericTypeGenerator.Generate(type.GenericTypeArguments[0]);
            }
            else if (type.IsClass && !type.IsGenericType && !type.IsArray && !type.IsPointer && !type.IsAbstract && !generatedTypes.Contains(type))
            {
                int maxConstructorFieldsCount = 0, curConstructorFieldsCount;
                ConstructorInfo constructorToUse = null;

                foreach (ConstructorInfo constructor in type.GetConstructors())
                {
                    curConstructorFieldsCount = constructor.GetParameters().Length;
                    if (curConstructorFieldsCount > maxConstructorFieldsCount)
                    {
                        maxConstructorFieldsCount = curConstructorFieldsCount;
                        constructorToUse = constructor;
                    }
                }

                generatedTypes.Push(type);
                if (constructorToUse == null)
                {
                    generated = CreateByProperties(type);
                }
                else
                {
                    generated = CreateByConstructor(type, constructorToUse);
                }
                generatedTypes.Pop();
            }
            else if (type.IsValueType)
            {
                generated = Activator.CreateInstance(type);
            }
            else
            {
                generated = null;
            }

            return generated;
        }


        protected object CreateByProperties(Type type)
        {
            object generated = Activator.CreateInstance(type);

            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                object value = Create(fieldInfo.FieldType);
                fieldInfo.SetValue(generated, value);
            }

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                if (propertyInfo.CanWrite)
                {
                    object value = Create(propertyInfo.PropertyType);
                    propertyInfo.SetValue(generated, value);
                }
            }
            return generated;
        }

        protected object CreateByConstructor(Type type, ConstructorInfo constructor)
        {
            var parametersValues = new List<object>();

            foreach (ParameterInfo parameterInfo in constructor.GetParameters())
            {
                object value = Create(parameterInfo.ParameterType);
                parametersValues.Add(value);
            }

            try
            {
                return constructor.Invoke(parametersValues.ToArray());
            }
            catch (TargetInvocationException)
            {
                return null;
            }
        }

        public Faker(string pluginsPath)
        {
            ISimpleTypeGenerator pluginGenerator;
            List<Assembly> assemblies = new List<Assembly>();

            generatedTypes = new Stack<Type>();
            simpleTypesGenerators = CreateSimpleTypesGeneratorsDictionary();
            genericTypesGenerators = CreateGenericTypesGeneratorsDictionary(simpleTypesGenerators);

            try
            {
                foreach (string file in Directory.GetFiles(pluginsPath, "*.dll"))
                {
                    try
                    {
                        assemblies.Add(Assembly.LoadFile(file));
                    }
                    catch (BadImageFormatException)
                    { }
                    catch (FileLoadException)
                    { }
                }
            }
            catch (DirectoryNotFoundException)
            { }

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    foreach (Type typeInterface in type.GetInterfaces())
                    {
                        if (typeInterface.Equals(typeof(ISimpleTypeGenerator)))
                        {
                            pluginGenerator = (ISimpleTypeGenerator)Activator.CreateInstance(type);
                            simpleTypesGenerators.Add(pluginGenerator.GeneratedType, pluginGenerator);
                        }
                    }
                }
            }
        }

        private static void AddSimpleGeneratorToDictionary(ISimpleTypeGenerator generator, Dictionary<Type, ISimpleTypeGenerator> dictionary)
        {
            dictionary.Add(generator.GeneratedType, generator);
        }

        static Dictionary<Type, ISimpleTypeGenerator> CreateSimpleTypesGeneratorsDictionary()
        {
            var dictionary = new Dictionary<Type, ISimpleTypeGenerator>();

            AddSimpleGeneratorToDictionary(new BoolGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new ByteGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new DateTimeGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new DecimalGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new DoubleGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new FloatGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new LongGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new SByteGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new ShortGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new StringGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new UIntGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new ULongGenerator(), dictionary);
            AddSimpleGeneratorToDictionary(new UShortGenerator(), dictionary);

            return dictionary;
        }

        static Dictionary<Type, IGenericTypeGenerator> CreateGenericTypesGeneratorsDictionary(Dictionary<Type, ISimpleTypeGenerator> simpleTypesGenerators)
        {
            var dictionary = new Dictionary<Type, IGenericTypeGenerator>();
            IGenericTypeGenerator generator;

            generator = new ListGenerator(simpleTypesGenerators);
            dictionary.Add(generator.GeneratedType, generator);

            return dictionary;
        }

        public Faker()
            : this(pluginsDirectoryPath)
        { }
    }
}
