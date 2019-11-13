using System;
using System.Linq;
using System.Windows;

namespace BatchRenamer
{
	/// <summary>
	/// Interaction logic for EditPositionsDialog.xaml
	/// </summary>
	public partial class EditPositionsDialog : Window
	{
		public EditPositionsDialog(string[] input, Window owner)
		{
			InitializeComponent();
			Owner = owner;
			Output = input;
			var maxLength = Output.Max(line => line.Length);
			exampleText.Text = Output.First(line => line.Length == maxLength);
			exampleText.TextChanged += ExampleText_TextChanged;
		}

		public string[] Output { get; }

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			exampleText.Focus();
		}

		private void Ok_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void ExampleText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			foreach(var change in e.Changes)
			{
				for(int i = 0; i < Output.Length; ++i)
				{
					if (change.RemovedLength > 0)
					{
						if (change.Offset + change.RemovedLength < Output[i].Length)
						{
							Output[i] = Output[i].Remove(change.Offset, change.RemovedLength);
						}
					}
					else
					{
						var newSubString = exampleText.Text.Substring(change.Offset, change.AddedLength);
						Output[i] = Output[i].Insert(change.Offset, newSubString);
					}
				}
			}
		}
	}
}
