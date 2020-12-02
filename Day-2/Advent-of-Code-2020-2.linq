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
var input = File.ReadAllLines(path);

var result =
	from x in input
	let match = new Regex(@"^(?'lowerBound'\d+)-(?'upperBound'\d+)\s+(?'letter'\w):\s+(?'password'.*)$").Match(x)
	let lowerBound = int.Parse(match.Groups["lowerBound"].Value)
	let upperBound = int.Parse(match.Groups["upperBound"].Value)
	let letter = match.Groups["letter"].Value
	let password = match.Groups["password"].Value
	let matches = new Regex(letter).Matches(password)
	let matchIndexes = matches.Select(x => x.Index + 1)
	let matchCount = matches.Count()
	select new
	{
		letter,
		password,
		lowerBound,
		upperBound,
		isPart1Valid = lowerBound <= matchCount && upperBound >= matchCount,
		isPart2Valid = matchIndexes.Contains(lowerBound) ^ matchIndexes.Contains(upperBound)
	};

result.Count(x =>x.isPart1Valid).Dump("Part 1");
result.Count(x =>x.isPart2Valid).Dump("Part 2");
result.Dump();