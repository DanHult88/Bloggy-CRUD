using Bloggy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggy
{
   
        internal class Product
        {

            public string Title;

            public Category Category;

            public Category Text;

           
            public void printProductInfo()
            {
                Console.WriteLine("\n-----Category-----\n: " + Category.Name + ". \n-----Title-----\n: " + Title + ". \n-----Text-----\n: " + Text.Name );
            }
        }
    
}
