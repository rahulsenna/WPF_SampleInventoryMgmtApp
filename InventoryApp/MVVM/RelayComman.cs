using System.Windows.Input;

namespace InventoryApp.MVVM;

public class RelayCommand : ICommand
{
	private Action<object> execute;
	private Func<object, bool> canExecute;

	public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
	{
		this.execute = execute;
		this.canExecute = canExecute;
	}

	public event EventHandler? CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}

	public bool CanExecute(object? parameter)
	{
		return canExecute == null || canExecute(parameter);
	}

	public void Execute(object? parameter)
	{
		execute(parameter);
	}
}
public class RelayCommandAsync : ICommand
{
	private Func<object, Task> execute;
	private Func<object, bool> canExecute;
	bool _isExecuting;

	public RelayCommandAsync(Func<object, Task> execute, Func<object, bool> canExecute = null)
	{
		this.execute = execute;
		this.canExecute = canExecute;
	}

	public event EventHandler? CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}

	public bool CanExecute(object? parameter)
	{
		return !_isExecuting && (canExecute == null || canExecute(parameter));
	}

	public async void Execute(object? parameter)
	{
		_isExecuting = true;
		CommandManager.InvalidateRequerySuggested();
		try
		{
			await execute(parameter);
		}
		finally
		{
			_isExecuting = false;
			CommandManager.InvalidateRequerySuggested();
		}
	}
}