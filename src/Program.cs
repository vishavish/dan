Dictionary<string, decimal> dirMap = new();
decimal total = 0M;
int maxLength = 0;
switch (args.Length)
{
	case 1:
	case 2:
		if (args.Length == 2 && !args[1].Equals("-r")) { ShowHelp(); return; }
		if (args.Length == 1 && (args[0].Equals("-h") || args[0].Equals("-r"))) { ShowHelp(); return; }

		if (!Directory.Exists(args[0])) { Console.WriteLine("Directory does not exists."); return; }

		var files = args.Length == 2 && args[1].Equals("-r")
					? Directory.GetFiles(args[0], string.Empty, SearchOption.AllDirectories)
					: Directory.GetFiles(args[0]);

		if (files.Length > 0) 
		{
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
			Console.WriteLine($"[FILE TYPE]: {kv.Key.PadLeft(maxLength)} = {GetSize(kv.Value)}");
		} 

		Console.WriteLine("=========================");
		Console.WriteLine($"[TOTAL] = {GetSize(total)}");

		break;
	default:
		ShowHelp();
		break;
}

void ShowHelp()
{
	Console.WriteLine("USAGE: dan <directory> [OPTION]");
	Console.WriteLine("[OPTIONS]");
	Console.WriteLine("    -h Show this help message");
	Console.WriteLine("    -r Search recursively");
}

string GetSize(decimal bytes)
{
	string[] suffix = ["bytes", "KB", "MB", "GB", "TB"];
	decimal size = bytes;
	int counter = 0;
	while(size >= 1024)
	{
		counter++;
		size = size / 1024;
	}

	return string.Format("{0:0.##}{1}", Math.Round(size), suffix[counter]);
}
