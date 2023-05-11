using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchain.Schema
{
    public class LLMResult : Base
    {
        public LLMResult() { }

        internal LLMResult(PyObject py)
        {
            PyInstance = py;
        }

        public List<List<Generation>> Generations
        {
            get
            {
                List<List<Generation>> result = new List<List<Generation>>();

                PyList list = new PyList(PyInstance.generations);
                foreach (var l1 in list)
                {
                    var l2List = new PyList(l1);
                    result.Add(l2List.ToList<Generation>());
                }

                return result;
            }
        }

        public PyDict LlmOutput
        {
            get
            {
                PyDict dict = new PyDict(PyInstance.llm_output);
                return dict;
            }
        }
    }
}
