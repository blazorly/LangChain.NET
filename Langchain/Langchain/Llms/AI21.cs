using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchain.Llms
{
    public class AI21 : BaseLlm
    {
        public AI21(string model = "j2-jumbo-instruct", float temperature = 0.7f, int maxTokens = 256, int minTokens = 0, float topP = 0.1f, 
            AI21PenaltyData presencePenalty = null, AI21PenaltyData countPenalty = null, AI21PenaltyData frequencyPenalty = null, int numResults = 1,
            Dictionary<string, float> logitBias = null,  string ai21_api_key = null, string[] stop = null, string base_url = null) 
        {
            if (presencePenalty == null)
                presencePenalty = new AI21PenaltyData();

            if (countPenalty == null)
                countPenalty = new AI21PenaltyData();

            if (frequencyPenalty == null)
                frequencyPenalty = new AI21PenaltyData();

            PyInstance = Instance.langchain.llms.AI21(model: model, temperature: temperature, maxTokens: maxTokens, minTokens: minTokens, topP: topP, 
                presencePenalty: presencePenalty.ToPython(), countPenalty: countPenalty.ToPython(), frequencyPenalty: frequencyPenalty.ToPython(), ai21_api_key: ai21_api_key);
        }
    }

    public class AI21PenaltyData : Base
    {
        public AI21PenaltyData(int scale = 0, bool applyToWhitespaces = true, bool applyToPunctuations = true, 
            bool applyToNumbers = true, bool applyToStopwords = true, bool applyToEmojis = true) 
        {
            PyInstance = Instance.langchain.llms.ai21.AI21PenaltyData(scale: scale, applyToWhitespaces: applyToWhitespaces, 
                applyToPunctuations: applyToPunctuations, applyToNumbers: applyToNumbers, applyToStopwords :applyToStopwords, applyToEmojis: applyToEmojis);
        }

        internal AI21PenaltyData(PyObject py)
        {
            PyInstance = py;
        }

        public int Scale => PyInstance.scale;

        public bool ApplyToWhitespaces => PyInstance.applyToWhitespaces;

        public bool ApplyToPunctuations => PyInstance.applyToPunctuations;

        public bool ApplyToNumbers => PyInstance.applyToNumbers;

        public bool ApplyToStopwords => PyInstance.applyToStopwords;

        public bool ApplyToEmojis => PyInstance.applyToEmojis;
    }
}
