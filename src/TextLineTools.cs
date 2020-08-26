using System;
using System.Collections.Generic;
using System.Linq;

namespace BatchRenamer
{
	public static class TextLineTools
	{
		public static string[] ToLines(this string input)
		{
			return input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
		}

		public static string JoinIntoString(this IEnumerable<string> lines) => string.Join(Environment.NewLine, lines);

		public static string AddWord(this string input, string word)
		{
			return input.RunOperationForEachLine(line => line);
		}

		public static string RunOperationForEachLine(this string input, Func<string, string> operation)
		{
			if (operation == null) throw new ArgumentNullException(nameof(operation));

			var lines = input.ToLines();
			var newLines = lines.Select(operation);
			return newLines.JoinIntoString();
		}
	}
}
