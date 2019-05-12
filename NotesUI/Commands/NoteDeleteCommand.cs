using NotesUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesUI.Commands
{
	internal class NoteDeleteCommand : ICommand
	{
		private NoteViewModel viewModel;

		public NoteDeleteCommand(NoteViewModel viewModel)
		{
			this.viewModel = viewModel;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return viewModel.CanDelete;
		}

		public void Execute(object parameter)
		{
			viewModel.Delete((Guid)parameter);
		}
	}
}
