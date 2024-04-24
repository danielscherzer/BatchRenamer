using BatchRenamer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BatchRenamer;

internal class MainViewModel
{
	private readonly Model model;

	internal MainViewModel()
	{
		model = new Model(Settings.Default.IgnoreExtension, Settings.Default.InputFiles.Cast<string>(), Settings.Default.Output);
		var args = Environment.GetCommandLineArgs().Skip(1);
		if (args.Any())
		{
			model.InputFiles.Clear();
			foreach (var fileName in args)
			{
				model.InputFiles.Add(fileName);
			}
		}
	}

	public Model Model => model;

	private static readonly string[] words = WordLoader.ReadFromFile();
	public static IEnumerable<string> Words => words;

	internal void FormatYear()
	{
		model.Output = model.Output.RunOperationForEachLine(FileRenameOperations.FormatYear);
	}

	internal void Rename() => model.Rename();

	internal void Save()
	{
		Settings.Default.InputFiles = new System.Collections.Specialized.StringCollection();
		Settings.Default.InputFiles.AddRange(model.InputFiles.ToArray());
		Settings.Default.Output = model.Output;
		Settings.Default.IgnoreExtension = model.IgnoreExtension;
		Settings.Default.Save();
	}

	internal void ToggleWord(string word)
	{
		var wordText = word.Encase();
		model.Output = model.Output.RunOperationForEachLine(name => FileRenameOperations.ToggleWord(name, wordText));
	}

	internal void FormatWords()
	{
		model.Output = model.Output.RunOperationForEachLine(line => FileRenameOperations.FormatWords(line, Words));
	}

	internal void CleanupSpaces()
	{
		model.Output = model.Output.RunOperationForEachLine(FileRenameOperations.CleanupSpaces);
	}

	internal void AddCounter(string text)
	{
		if (int.TryParse(text, out var start))
		{
			model.Output = model.Output.RunOperationForEachLine(name => name + start++.ToString().PadLeft(text.Length, '0'));
		}
	}
}
