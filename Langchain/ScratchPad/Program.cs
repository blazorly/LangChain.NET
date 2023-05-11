// See https://aka.ms/new-console-template for more information

using Langchain.Llms;
using Langchain.Schema;

try
{
    var llm = new AI21();
    var info = llm.GetNumTokens("this is deepak");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

//var g = info.Generations;
Console.WriteLine("Hello, World!");
