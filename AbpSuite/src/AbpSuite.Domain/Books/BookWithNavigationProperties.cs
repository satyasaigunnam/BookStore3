using AbpSuite.Authors;

using System;
using System.Collections.Generic;

namespace AbpSuite.Books
{
    public class BookWithNavigationProperties
    {
        public Book Book { get; set; }

        public Author Author { get; set; }
        

        
    }
}