// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Collections;

Console.WriteLine("Hello, World!");

_ = BenchmarkRunner.Run<BenchmarkLists>();