using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BatchRenamer
{
	class Words
	{
		public Words()
		{
			try
			{
				Items = File.ReadAllText("Words.txt").Split(new string[] { Environment.NewLine, " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
			}
			catch
			{
				Items = Enumerable.Empty<string>();
			}
		}

		public IEnumerable<string> Items { get; }
	}
}
