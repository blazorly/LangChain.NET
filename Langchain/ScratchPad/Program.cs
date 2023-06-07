// See https://aka.ms/new-console-template for more information

using Langchain.Llms;
using Langchain.Schema;
using Python.Runtime;

try
{
    var llm = new AI21(ai21_api_key: "aaaa");
    var result = await llm.AGenerate(new List<string>() { "hello" });
    var info = llm.GetNumTokens("this is deepak");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

//var g = info.Generations;
Console.WriteLine("Hello, World!");
