using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SlidoCommentSpeakerGUI
{
	class RelayCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		private readonly Action<Object> _execute;
		private readonly Predicate<Object>? _canExecute;

		public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
		{
			if (execute == null) throw new ArgumentNullException(nameof(execute));
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object? parameter)
		{
			return _canExecute == null || _canExecute(parameter);
		}

		public void Execute(object? parameter)
		{
			_execute(parameter ?? "<N/A>");
		}
	}
}
