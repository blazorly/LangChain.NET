﻿using Langchain.Schema;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchain.Llms
{
    public class BaseLlm : Base
    {
        public BaseLlm()
        {

        }

        public BaseLlm(PyObject py)
        {
            PyInstance = py;
        }

        public LLMResult Call(string prompt, List<string> stop = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["prompt"] = prompt;
            parameters["stop"] = stop != null ? ToList(stop.ToArray()) : null;

            PyObject py = InvokeMethod("__call__", parameters);
            return new LLMResult(py);
        }

        public async Task<LLMResult> AGenerate(List<string> prompts, List<string> stop = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["prompts"] = ToList(prompts.ToArray());
            parameters["stop"] = stop != null ? ToList(stop.ToArray()) : null;

            dynamic py = await PyInstance.agenerate(parameters["prompts"], parameters["stop"]); //InvokeMethod("agenerate", parameters);
            var w = py.close();
            return new LLMResult(py);
        }

        public LLMResult Generate(List<string> prompts, List<string> stop = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["prompts"] = ToList(prompts.ToArray());
            parameters["stop"] = stop != null ? ToList(stop.ToArray()) : null;

            PyObject py = InvokeMethod("generate", parameters);
            return new LLMResult(py);
        }

        public int GetNumTokens(string text)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["text"] = text;

            PyObject py = InvokeMethod("get_num_tokens", parameters);
            return py.As<int>();
        }
    }
}
