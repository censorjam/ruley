using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruley.Core;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpandoObject e = new ExpandoObject();

            ExpandoObject em = new ExpandoObject();
            em.SetValue("Number2", 100);
            e.SetValue("Inside", em);

            var v = e.GetValue("Inside.Number2");
         
            Console.WriteLine(v);

            Console.ReadLine();
        }
    }
}
