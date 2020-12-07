using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Output_App.TestClasses
{
    class JustSomeClass
    {
        [DataMember]
        public DateTime dateTimeField;
        [DataMember]
        public object objectField;
        [DataMember]
        public char publicStringField;
        [DataMember]
        public bool publicBoolField;
        [DataMember]
        protected bool nonPublicBoolField;

        [DataMember]
        public int PublicIntSetter
        { get; set; }

        [DataMember]
        public int NonPublicIntSetter
        { get; protected set; }

        [DataMember]
        private readonly int nonPublicIntField;

        [DataMember]
        public bool[] publicList;

        [DataMember]
        public Bookshelf bookshelf;


        public JustSomeClass()
        {
        }
    }
}
