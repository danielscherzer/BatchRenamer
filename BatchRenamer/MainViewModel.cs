using BatchRenamer.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BatchRenamer
{
	class MainViewModel : INotifyPropertyChanged
	{
		private Model model;

		internal MainViewModel()
		{
			model = new Model(Settings.Default.IgnoreExtension, Settings.Default.InputFiles.Cast<string>(), Settings.Default.Output);
			words = new Words();
			var args = Environment.GetCommandLineArgs().Skip(1);
			if(args.Any())
			{
				model.InputFiles.Clear();
				foreach (var fileName in args)
				{
					model.InputFiles.Add(fileName);
				}
			}
			model.PropertyChanged += (s, e) => PropertyChanged?.Invoke(this, e);
		}

		public ObservableCollection<string> InputFiles => model.InputFiles;

		public bool IgnoreExtension
		{
			get => model.IgnoreExtension;
			set => model.IgnoreExtension = value;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public string Output
		{
			get => model.Output;
			set => model.Output = value;
		}

		private readonly Words words;
		public IEnumerable<string> Words => words.Items;

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

		internal void Undo() => model.Undo();

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
			if(int.TryParse(text, out var start))
			{
				model.Output = model.Output.RunOperationForEachLine(name => name + start++.ToString().PadLeft(text.Length, '0'));
			}
		}
	}
}
