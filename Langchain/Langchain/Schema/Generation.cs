using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchain.Schema
{
    public class Generation : Base
    {
        public Generation() { }

        internal Generation(PyObject py)
        {
            PyInstance = py;
        }

        public string Text => PyInstance.text;

        public Dictionary<string, dynamic> GenerationInfo
        {
            get
            {
                PyDict dict = new PyDict(PyInstance.generation_info);
                Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                var keys = dict.Keys().As<string[]>();
                foreach (var item in keys)
                {
                    result.Add(item, dict[item]);
                }

                return result;
            }
        }

        public static implicit operator Generation(PyObject py)
        {
            return new Generation(py);
        }
    }
}
