Dictionary<string, decimal> dirMap = new();
decimal total = 0M;
int maxLength = 0;
switch (args.Length)
{
	case int len1 when len1 == 1:
	case int len2 when len2 == 2:
		if (args.Length == 2 && (!args[1].Equals("-r") && !args[1].Equals("-h"))) { ShowHelp(); return; }
		if (args.Length == 2 && args[1].Equals("-h")) { ShowHelp(); return; }

		if (!Directory.Exists(args[0])) { Console.WriteLine("Directory does not exists."); return; }

		bool isRecursive = args.Length == 2 && args[1].Equals("-r") 
			? true
			: false;

		var files = isRecursive
			? Directory.GetFiles(args[0], string.Empty, SearchOption.AllDirectories)
			: Directory.GetFiles(args[0]);

		if(files.Length > 0) {
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo fi = new(files[i]);
				if (string.IsNullOrEmpty(fi.Extension)) { continue; }
				if (maxLength < fi.Extension.Length)    { maxLength = fi.Extension.Length; }
				if (!dirMap.ContainsKey(fi.Extension))
				{
					dirMap.Add(fi.Extension, fi.Length);
				}
				else
				{
					dirMap[fi.Extension] += fi.Length;
				}
			}
		}
		else { Console.WriteLine("No files found."); return; }

		Console.WriteLine("=========SUMMARY=========");
							
		foreach(var kv in dirMap)
		{
			total += kv.Value;
			Console.WriteLine($"[FILE TYPE]: {kv.Key.PadLeft(maxLength)} = {Math.Round(kv.Value / 1024)}Kb");
		} 

		Console.WriteLine("=========================");
		Console.WriteLine($"[TOTAL] = {Math.Round(total/(1024 * 1024))}Mb");
		break;
	default:
		ShowHelp();
		break;
}

void ShowHelp()
{
	Console.WriteLine("USAGE: dan <directory> [OPTION]");
	Console.WriteLine("OPTION");
	Console.WriteLine("    -h Show this help message");
	Console.WriteLine("    -r Search recursively");
}
