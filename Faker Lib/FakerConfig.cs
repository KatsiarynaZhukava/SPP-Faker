using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Faker_Lib.FieldGenerators;

namespace Faker_Lib
{
    public class FakerConfig
    {
        protected Dictionary<PropertyInfo, ISimpleTypeGenerator> generators;

        public Dictionary<PropertyInfo, ISimpleTypeGenerator> Generators
        {
            get;
            //get => new Dictionary<PropertyInfo, ISimpleTypeGenerator>(Generators);

        }

        public void Add<TClass, TPropertyType, TGenerator>(Expression<Func<TClass, TPropertyType>> expression)
            where TClass : class
            where TGenerator : ISimpleTypeGenerator, new()
        {
            Expression expressionBody = expression.Body;
            if (expressionBody.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Illegal expression");
            }
            ISimpleTypeGenerator generator = (ISimpleTypeGenerator)Activator.CreateInstance(typeof(TGenerator));
            if (!generator.GeneratedType.Equals(typeof(TPropertyType)))
            {
                throw new ArgumentException("Illegal generator");
            }
            Generators.Add((PropertyInfo)((MemberExpression)expressionBody).Member, generator);
        }

        public FakerConfig()
        {
            Generators = new Dictionary<PropertyInfo, ISimpleTypeGenerator>();
        }
    }
}
