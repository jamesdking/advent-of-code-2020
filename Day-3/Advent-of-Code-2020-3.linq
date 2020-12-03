<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <RemoveNamespace>System.Xml</RemoveNamespace>
  <RemoveNamespace>System.Xml.Linq</RemoveNamespace>
  <RemoveNamespace>System.Xml.XPath</RemoveNamespace>
</Query>

void Main()
{
	var dir = Path.GetDirectoryName(Util.CurrentQueryPath);
	var path = Path.Combine(dir, "input.txt");
	var input = File.ReadAllLines(path);

	CountTrees(input, (3, 1)).Dump("Part 1");

	new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) }
		.Select(x => CountTrees(input, x)).Aggregate((x, y) => x * y).Dump("Part 2");
}

long CountTrees(string[] terrain, (int right, int down) moves)
{
	var treeCount = 0L;
	for (int x = 0, y = 0; y < terrain.Length; y += moves.down, x += moves.right)
	{
		var line = terrain[y];
		var lastCharIndex = line.Length - 1;
		x = x > lastCharIndex ? (x % lastCharIndex) - 1 : x;
		if (line[x] == '#')
			treeCount++;
	}

	return treeCount;
}

