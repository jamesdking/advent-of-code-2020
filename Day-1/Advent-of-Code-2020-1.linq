<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <RemoveNamespace>System.Xml</RemoveNamespace>
  <RemoveNamespace>System.Xml.Linq</RemoveNamespace>
  <RemoveNamespace>System.Xml.XPath</RemoveNamespace>
</Query>

var dir = Path.GetDirectoryName(Util.CurrentQueryPath);
var path = Path.Combine(dir, "input.txt");
var input = File.ReadAllLines(path).Select(int.Parse);
var result = (
	from x in input
	from y in input
	let sum = x + y
	where sum == 2020
	select x * y).First();

result.Dump("Part 1");

var result2 = (
	from x in input
	from y in input
	from z in input
	let sum = x + y + z
	where sum == 2020
	select x * y * z).First();
	
result2.Dump("Part 2");