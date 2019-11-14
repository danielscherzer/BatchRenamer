using BatchRenamer.Properties;
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
			Words = new Words();
			model.PropertyChanged += (s, e) => PropertyChanged?.Invoke(this, e);
		}

		public ObservableCollection<string> InputFiles => model.InputFiles;

		public bool IgnoreExtension
		{
			get => model.IgnoreExtension;
			set => model.IgnoreExtension = value;
		}
		public string Output
		{
			get => model.Output;
			set => model.Output = value;
		}

		public Words Words { get; }

		public event PropertyChangedEventHandler PropertyChanged;

		internal void Rename() => model.Rename();

		internal void Save()
		{
			Settings.Default.InputFiles = new System.Collections.Specialized.StringCollection();
			Settings.Default.InputFiles.AddRange(model.InputFiles.ToArray());
			Settings.Default.Output = model.Output;
			Settings.Default.IgnoreExtension = model.IgnoreExtension;
			Settings.Default.Save();
		}

		internal void Undo() => model.Undo();
	}
}
