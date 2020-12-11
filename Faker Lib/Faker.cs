using Faker_Lib.FieldGenerators;
using Faker_Lib.FieldGenerators.GenericTypeGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Faker_Lib
{
    public class Faker
    {
        public Dictionary<Type, ISimpleTypeGenerator> baseTypesGenerators;
        public Dictionary<Type, IGenericTypeGenerator> genericTypesGenerators;
        public Dictionary<PropertyInfo, ISimpleTypeGenerator> customGenerators;

        public Stack<Type> generationStack;
        private int recursionLimit = 3;

        private static string pluginsDirectory = "D:\\BSUIR\\Labs\\Labs_Sem5\\SPP\\Faker\\Plugins";



        public T Generate<T>()
        {
            return (T)Generate(typeof(T));
        }

        internal object Generate(Type type)
        {
            object generated = null;

            if (baseTypesGenerators.TryGetValue(type, out ISimpleTypeGenerator simpleTypeGenerator))
            {
                generated = simpleTypeGenerator.Generate();
            }
            else if (type.IsGenericType && genericTypesGenerators.TryGetValue(type.GetGenericTypeDefinition(), out IGenericTypeGenerator genericTypeGenerator))
            {
                generated = genericTypeGenerator.Generate(type.GenericTypeArguments[0], this);
            }
            else if (type.IsClass && !type.IsGenericType && !type.IsPointer && !type.IsAbstract)
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

                generationStack.Push(type);
                if (CheckRecursion(type))
                {
                    if (constructorToUse == null)
                    {
                        generated = GenerateByProperties(type);
                    }
                    else
                    {
                        generated = GenerateByConstructor(type, constructorToUse);
                    }
                }
                generationStack.Pop();                
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



        private bool CheckRecursion(Type type)
        {
            int identicalElementsCount = 0;
            for (int i = 0; i < generationStack.Count; i++)
            {
                if (generationStack.ElementAt(i) == type)
                {
                    identicalElementsCount++;
                }
            }
            return (identicalElementsCount <= recursionLimit);
        }

        protected object GenerateByProperties(Type type)
        {
            object generated = null;
            try
            {
                generated = Activator.CreateInstance(type);
                foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                {
                    if (!TryGenerateByCustomGenerator(fieldInfo, out object value))
                    {
                        value = Generate(fieldInfo.FieldType);
                    }
                    fieldInfo.SetValue(generated, value);
                }

                foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                {
                    if (propertyInfo.CanWrite)
                    {
                        if (!TryGenerateByCustomGenerator(propertyInfo, out object value))
                        {
                            value = Generate(propertyInfo.PropertyType);
                        }
                        propertyInfo.SetValue(generated, value);
                    }
                }
            }
            catch { }
            return generated;
        }

        protected object GenerateByConstructor(Type type, ConstructorInfo constructor)
        {
            var parametersValues = new List<object>();

            foreach (ParameterInfo parameterInfo in constructor.GetParameters())
            {
                if (!TryGenerateByCustomGenerator(parameterInfo, type, out object value))
                {
                    value = Generate(parameterInfo.ParameterType);
                }
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


        //////////////////   Custom generators   /////////////////////////////////

        protected bool TryGenerateByCustomGenerator(PropertyInfo propertyInfo, out object generated)
        {
            if (customGenerators.TryGetValue(propertyInfo, out ISimpleTypeGenerator generator))
            {
                generated = generator.Generate();
                return true;
            }
            else
            {
                generated = default(object);
                return false;
            }
        }

        protected bool TryGenerateByCustomGenerator(FieldInfo fieldInfo, out object generated)
        {
            foreach (KeyValuePair<PropertyInfo, ISimpleTypeGenerator> keyValue in customGenerators)
            {
                if ((keyValue.Key.Name.ToLower() == fieldInfo.Name.ToLower()) && keyValue.Value.GeneratedType.Equals(fieldInfo.FieldType)
                    && keyValue.Key.ReflectedType.Equals(fieldInfo.ReflectedType))
                {
                    generated = keyValue.Value.Generate();
                    return true;
                }
            }
            generated = default(object);
            return false;
        }

        protected bool TryGenerateByCustomGenerator(ParameterInfo parameterInfo, Type type, out object generated)
        {
            foreach (KeyValuePair<PropertyInfo, ISimpleTypeGenerator> keyValue in customGenerators)
            {
                if ((keyValue.Key.Name.ToLower() == parameterInfo.Name.ToLower()) && keyValue.Value.GeneratedType.Equals(parameterInfo.ParameterType)
                    && keyValue.Key.ReflectedType.Equals(type))
                {
                    generated = keyValue.Value.Generate();
                    return true;
                }
            }
            generated = default(object);
            return false;
        }



        ///////////////////////  Faker creation  //////////////////////////////////

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

        public Faker(string pluginsPath, FakerConfig fakerConfig)
        {
            ISimpleTypeGenerator pluginGenerator;
            List<Assembly> assemblies = new List<Assembly>();

            generationStack = new Stack<Type>();
            baseTypesGenerators = CreateSimpleTypesGeneratorsDictionary();
            genericTypesGenerators = CreateGenericTypesGeneratorsDictionary(baseTypesGenerators);
            if (fakerConfig == null)
            {
                customGenerators = new Dictionary<PropertyInfo, ISimpleTypeGenerator>();
            }
            else
            {
                customGenerators = fakerConfig.Generators;
            }

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
                            baseTypesGenerators.Add(pluginGenerator.GeneratedType, pluginGenerator);
                        }
                    }
                }
            }
        }

        public Faker()
            : this(pluginsDirectory, null)
        { }

        public Faker(string pluginsPath)
            : this(pluginsPath, null)
        { }

        public Faker(FakerConfig fakerConfig)
            : this(pluginsDirectory, fakerConfig)
        { }
    }
}
