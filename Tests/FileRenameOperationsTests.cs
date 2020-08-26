using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BatchRenamer.Tests
{
	[TestClass()]
	public class FileRenameOperationsTests
	{
		[DataTestMethod()]
		[DataRow("3 Ninjas - High Noon at Mega Mountain (1998) (1080p) (HD)(WS)(EN)(GE).mkv",
				 "3 Ninjas - High Noon at Mega Mountain (1998) (1080p) (HD)(WS)(EN)(GE).mkv")]
		[DataRow("3 Ninjas - High Noon at Mega Mountain 1998 (1080p) (HD)(WS)(EN)(GE).mkv",
				 "3 Ninjas - High Noon at Mega Mountain (1998) (1080p) (HD)(WS)(EN)(GE).mkv")]
		[DataRow("3 1980 Ninjas - High Noon at Mega Mountain 1998 (1080) (HD)(WS)(EN)(GE).mkv",
				 "3 1980 Ninjas - High Noon at Mega Mountain (1998) (1080) (HD)(WS)(EN)(GE).mkv")]
		[DataRow("3 1980 Ninjas - High Noon at Mega Mountain 1898 (1080) (HD)(WS)(EN)(GE).mkv", 
			     "3 (1980) Ninjas - High Noon at Mega Mountain 1898 (1080) (HD)(WS)(EN)(GE).mkv")]
		public void FormatYearTest(string input, string expectedOutput)
		{
			Assert.AreEqual(input.FormatYear(), expectedOutput);
		}


		[TestMethod()]
		public void FormatWordsTest()
		{
			var input = "3 1980 Ninjas - High Noon at Mega Mountain 1898 GE WS  (EN) HD  1080p";
			var expectedOutput = "3 1980 Ninjas - High Noon at Mega Mountain 1898 (1080p) (HD) (WS) (EN) (GE)";
			var words = new string[] { "1080p", "HD", "WS", "EN", "GE" };
			Assert.AreEqual(input.FormatWords(words), expectedOutput);
		}
	}
}