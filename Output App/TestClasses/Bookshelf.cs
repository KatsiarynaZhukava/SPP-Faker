using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Output_App.TestClasses
{
    [DataContract]
    class Bookshelf
    {
        [DataMember]
        public List<Book> Books { get; set; }
    }
}
