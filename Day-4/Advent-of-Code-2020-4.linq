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
	var input = File.ReadAllText(path);

	var passports =
		from item in Regex.Split(input, @"^\s*$", RegexOptions.Multiline)
		let kvps =
			from kvp in Regex.Split(item, @"\s")
			where !string.IsNullOrEmpty(kvp)
			select kvp.Split(':')
		let dict = kvps.ToDictionary(x => x[0], x => x[1])
		select new
		{
			Fields = dict,
			Keys = new HashSet<string>(dict.Keys)
		};

	var requiredFields = new HashSet<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
	var validPassports = passports.Where(passport => passport.Keys.Intersect(requiredFields).Count() == requiredFields.Count);
	validPassports.Count().Dump("Part 1");

	bool isValid(string key, string value)
	{
		return key switch
		{
			"byr" => Regex.IsMatch(value, @"^(19[2-9][\d]|200[0-2])$"),
			"iyr" => Regex.IsMatch(value, @"^(201\d|2020)$"),
			"eyr" => Regex.IsMatch(value, @"^(202\d|2030)$"),
			"hgt" => Regex.IsMatch(value, @"^(1([5-8][\d]|9[0-3])cm|(59|6[\d]|7[0-6])in)$"),
			"hcl" => Regex.IsMatch(value, @"^#[a-f\d]{6}$"),
			"ecl" => Regex.IsMatch(value, @"^(amb|blu|brn|gry|grn|hzl|oth)$"),
			"pid" => Regex.IsMatch(value, @"^\d{9}$"),
			"cid" => true,
			_ => throw new Exception($"Unhandled key '{key}'")
		};
	};

	validPassports.Count(passport => passport.Fields.Select(x => isValid(x.Key, x.Value)).All(x => x)).Dump("Part 2");
}
