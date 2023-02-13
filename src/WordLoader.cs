using System;
using System.IO;

namespace BatchRenamer;

class WordLoader
{

	public static string[] ReadFromFile()
	{
		try
		{
			return File.ReadAllText("Words.txt").Split(new string[] { "\n", "\r", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
		}
		catch
		{
			return Array.Empty<string>();
		}
	}
}
