﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchain.Llms
{
    public class AI21 : BaseLlm
    {
        public AI21(string model = "j2-jumbo-instruct") 
        {
            Parameters["model"] = model;

            PyInstance = Instance.langchain.llms.AI21;
        }
    }
}