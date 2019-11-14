using System;
using System.Linq;

namespace BatchRenamer
{
	public static class FileRenameOperations
	{
		public static string Encase(this string word)
		{
			return "(" + word + ")";
		}

		public static string CleanupEncase(this string text)
		{
			return text.Replace("((", "(").Replace("))", ")");
		}

		public static string CleanupSpaces(this string fileName)
		{
			var newFileName = fileName.Replace('.', ' ');
			newFileName = newFileName.Replace('_', ' ');
			return newFileName;
		}

		public static string FormatYear(this string fileName)
		{
			int startMeta = fileName.Length;
			var splitInput = fileName.SplitIntoWords(new char[] { ' ', '.', '_', '(', ')' }).Reverse();
			//last year in file name
			foreach (var word in splitInput)
			{
				if (int.TryParse(word, out int productionYear))
				{
					//check if valid year
					if (1900 < productionYear && productionYear < 2100)
					{
						var yearString = productionYear.ToString();
						return fileName.Replace(yearString, yearString.Encase()).CleanupEncase();
					}
				}
			}
			return fileName;
		}

		public static string[] SplitIntoWords(this string line, char[] limiters)
		{
			return line.Split(limiters, StringSplitOptions.RemoveEmptyEntries);
		}

		public static string ToggleWord(this string fileName, string word)
		{
			var spacedWord = ' ' + word;
			if (fileName.Contains(spacedWord))
			{
				return fileName.Replace(spacedWord, "");
			}
			else
			{
				return fileName + spacedWord;
			}
		}
	}
}
