using System;
using System.Collections.Generic;
using System.Text;

namespace Output_App.TestClasses
{
    class Book
    {
        public Book()
        {

        }

        public Book(int id, string title, string authorName)
        {
            Id = id;
            Title = title;
            AuthorName = authorName;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
    }
}
