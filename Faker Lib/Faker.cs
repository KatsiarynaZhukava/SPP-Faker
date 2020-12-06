using Faker_Lib.FieldGenerators;
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
        public Dictionary<Type, IGenerator> generatorDictionary;
        private Stack<Type> circleStack;

        public Faker()
        {
            generatorDictionary = new Dictionary<Type, IGenerator>();
            circleStack = new Stack<Type>();
            Assembly generatorsAssembly = Assembly.GetAssembly(typeof(IGenerator));
            AddGenerators(generatorsAssembly);
            LoadPlugins();
        }
        public void AddGenerators(Assembly assembly)
        {
            Type[] typeArray = assembly.GetTypes();
            foreach (Type type in typeArray)
            {
                if (typeof(IGenerator).IsAssignableFrom(type) && type.IsClass)
                {
                    ConstructorInfo[] constructors = type.GetConstructors();
                    IGenerator generator = (IGenerator)constructors[0].Invoke(new object[] {});
                    if (generatorDictionary.Keys.Contains(generator.generatedType))
                        continue;
                    else
                        generatorDictionary.Add(generator.generatedType, generator);
                }
            }
        }
        public void LoadPlugins()
        {
            const string directory = "D:\\BSUIR\\Labs\\Labs_Sem5\\SPP\\Plugins";

            foreach (string path in Directory.GetFiles(directory, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFile(new FileInfo(path).FullName);
                AddGenerators(assembly);
            }
        }
    }
}
