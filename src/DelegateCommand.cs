using System;
using System.Windows.Input;

namespace BatchRenamer;

public class DelegateCommand(Action<object> execute, Predicate<object> canExecute = null) : ICommand
{
	private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

	public event EventHandler CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}

	public bool CanExecute(object parameter)
	{
		return canExecute == null || canExecute(parameter);
	}

	public void Execute(object parameter) => _execute(parameter);

	public static void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
}
