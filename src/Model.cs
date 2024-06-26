﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Zenseless.Patterns.Property;

namespace BatchRenamer
{
	internal class Model : NotifyPropertyChanged
	{
		public Model(bool ignoreExtensions, IEnumerable<string> inputFiles, string output)
		{
			_ignoreExt = ignoreExtensions;
			InputFiles = new ObservableCollection<string>(inputFiles);
			Output = output;
			InputFiles.CollectionChanged += InputFiles_CollectionChanged;
		}

		private void InputFiles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Output = string.Join(Environment.NewLine, InputFiles.Select(filePath => _ignoreExt ? Path.GetFileNameWithoutExtension(filePath) : Path.GetFileName(filePath)));
		}

		//TODO: Add InputFiles to undoBuffer?
		public ObservableCollection<string> InputFiles { get; }

		public string Output
		{
			get => _output;
			set => Set(ref _output, value, undoBuffer.Push);
		}

		public bool IgnoreExtension
		{
			get => _ignoreExt;
			set => Set(ref _ignoreExt, value, v => InputFiles_CollectionChanged(InputFiles, null));
		}

		public void Rename()
		{
			var savedOutput = Output;
			var outputFileNames = Output.ToLines();
			var renamePairs = InputFiles.Zip(outputFileNames, (input, output) => (input, output)).ToArray();
			InputFiles.Clear();
			foreach (var (input, output) in renamePairs)
			{
				var dir = Path.GetDirectoryName(input);
				var ext = _ignoreExt ? Path.GetExtension(input) : string.Empty;
				var outputPath = $"{dir}{Path.DirectorySeparatorChar}{output}{ext}";
				InputFiles.Add(Rename(input, outputPath) ? outputPath : input);
			}
			Output = savedOutput;
		}

		public void Undo()
		{
			if (2 > undoBuffer.Count) return;
			undoBuffer.Pop();
			Output = undoBuffer.Pop();
		}

		private string _output;
		private bool _ignoreExt;
		private readonly Stack<string> undoBuffer = new();

		private static bool Rename(string input, string output)
		{
			if (input == output) return true;
			try
			{
				if (File.Exists(input))
				{
					File.Move(input, output);
				}
				else
				{
					//assume directory
					Directory.Move(input, output);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
