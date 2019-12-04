using System.Reflection;
using System.Windows;
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
			viewModel = new MainViewModel();
			InitializeComponent();
			DataContext = viewModel;
			var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
			Title += $" {currentVersion}";
		}

		private void CleanupSpaces_Click(object sender, RoutedEventArgs e)
		{
			viewModel.CleanupSpaces();
		}

		private void Clear(object sender, RoutedEventArgs e) => viewModel.InputFiles.Clear();

		private void SaveCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
		{
			viewModel.Rename();
		}

		private void SaveCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = viewModel.InputFiles.Count > 0;
		}

		private void InputList_DragOver(object sender, DragEventArgs dragInfo)
		{
			dragInfo.Effects = dragInfo.KeyStates.HasFlag(DragDropKeyStates.ControlKey) ? DragDropEffects.Copy : DragDropEffects.Move;
			dragInfo.Handled = true;
		}

		private void InputList_Drop(object sender, DragEventArgs dragInfo)
		{
			var fileNames = (string[])dragInfo.Data.GetData(DataFormats.FileDrop);
			if (!dragInfo.KeyStates.HasFlag(DragDropKeyStates.ControlKey)) viewModel.InputFiles.Clear();
			foreach (var fileName in fileNames)
			{
				viewModel.InputFiles.Add(fileName);
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			viewModel.Save();
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

		private void FormatWords_Click(object sender, RoutedEventArgs e)
		{
			viewModel.FormatWords();
		}

		private void FormatYear_Click(object sender, RoutedEventArgs e)
		{
			viewModel.FormatYear();
		}

		private void Word_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			viewModel.ToggleWord(button.Content.ToString());
		}
	}
}
