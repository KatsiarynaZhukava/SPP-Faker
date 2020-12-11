using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Output_App.TestClasses
{
    [DataContract]
    class Book
    {
        public Book()
        {

        }

        public Book(int id, string title, string authorName, DateTime publication, string cityOfPublication)
        {
            Id = id;
            Title = title;
            AuthorName = authorName;
            Publication = publication;
            CityOfPublication = cityOfPublication;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string AuthorName { get; set; }
        [DataMember]
        public DateTime Publication { get; set; }
        [DataMember]
        public string CityOfPublication { get; set; }
        [DataMember]
        public object someObject;
    }
}
