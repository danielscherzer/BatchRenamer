namespace BatchRenamer
{
	public static class FileRenameOperations
	{
		public static string Encase(this string word)
		{
			return "(" + word + ")";
		}

		public static string CleanupSpaces(this string fileName)
		{
			var newFileName = fileName.Replace('.', ' ');
			newFileName = newFileName.Replace('_', ' ');
			return newFileName;
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
