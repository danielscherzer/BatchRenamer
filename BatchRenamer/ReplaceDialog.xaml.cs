using System;
using System.Windows;

namespace BatchRenamer
{
	/// <summary>
	/// Interaction logic for Replace.xaml
	/// </summary>
	public partial class ReplaceDialog : Window
	{
		public ReplaceDialog(string searchText)
		{
			InitializeComponent();
			this.searchText.Text = searchText;
		}

		public string SearchText => searchText.Text;
		public string ReplaceText => replaceText.Text;

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			searchText.SelectAll();
			searchText.Focus();
		}

		private void ReplaceAll_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
