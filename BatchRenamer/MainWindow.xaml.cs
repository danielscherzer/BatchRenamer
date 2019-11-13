﻿using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainViewModel viewModel;

		public MainWindow()
		{
			InitializeComponent();
			viewModel = new MainViewModel();
			DataContext = viewModel;
		}

		private void Clear(object sender, RoutedEventArgs e) => viewModel.InputFiles.Clear();

		private void InputList_Drop(object sender, DragEventArgs e)
		{
			var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
			foreach (var fileName in fileNames)
			{
				viewModel.InputFiles.Add(fileName);
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			viewModel.Save();
		}

		private void Rename(object sender, RoutedEventArgs e)
		{
			viewModel.Rename();
		}

		private void Positions_Click(object sender, RoutedEventArgs e)
		{
			var editPositionsDialog = new EditPositionsDialog(viewModel.Output.ToLines(), this); // owner for centering on owner
			if (editPositionsDialog.ShowDialog() == true)
			{
				viewModel.Output = editPositionsDialog.Output.JoinIntoString();
			}
		}

		private void Replace_Click(object sender, RoutedEventArgs e)
		{
			var replaceDialog = new ReplaceDialog(output.SelectedText, this); // owner for centering on owner
			if (replaceDialog.ShowDialog() == true)
			{
				viewModel.Output = viewModel.Output.Replace(replaceDialog.SearchText, replaceDialog.ReplaceText);
			}
		}

		private void Undo_Click(object sender, RoutedEventArgs e)
		{
			viewModel.Undo();
		}

		private void Word_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			var wordText = button.Content.ToString();
			viewModel.Output = viewModel.Output.RunOperationForEachLine(name => FileRenameOperations.ToggleWord(name, wordText));
		}
	}
}
