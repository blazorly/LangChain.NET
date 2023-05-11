using Langchain.Schema;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchain
{
    public static class Extensions
    {
        public static List<T> ToList<T>(this PyList pyList)
        {
            List<T> result = new List<T>();
            foreach (var item in pyList)
            {
                result.Add(item.As<T>());
            }

            return result;
        }
    }
}
