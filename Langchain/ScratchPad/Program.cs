// See https://aka.ms/new-console-template for more information

using Langchain.Llms;
using Langchain.Schema;

var llm = new AI21();
var info = llm.Call("hello man");
var g = info.Generations;
Console.WriteLine("Hello, World!");
