using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Output_App.TestClasses
{
    [DataContract]
    class Library
    {
        [DataMember]
        public List<Bookshelf> Bookshelves { get; set; }

        public Library()
        {

        }
    }
}
