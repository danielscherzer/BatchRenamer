using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BatchRenamer.Tests;

[TestClass()]
public class FileRenameOperationsTests
{
	[TestMethod()]
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
		Assert.AreEqual(expectedOutput, input.FormatYear());
	}

	[TestMethod()]
	public void FormatWordsTest()
	{
		var input = "3 1980 Ninjas - High Noon at Mega Mountain 1898 GE WS  (EN) HD  1080p";
		var expectedOutput = "3 1980 Ninjas - High Noon at Mega Mountain 1898 (1080p) (HD) (WS) (EN) (GE)";
		var words = new string[] { "1080p", "HD", "WS", "EN", "GE" };
		Assert.AreEqual(expectedOutput, input.FormatWords(words));
	}

	[TestMethod()]
	[DataRow(" ", " ")]
	[DataRow(" !AZaz09~", " !AZaz09~")]
	[DataRow("\u0000\u0001\u0002\u0003\u0004\u0005\u0019", "")]
	[DataRow("¥\t¼\tÑ\tñ", "")]
	public void RemoveUnicodeTest(string input, string expectedOutput)
	{
		Assert.AreEqual(expectedOutput, input.RemoveUnicode());
	}
}