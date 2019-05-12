using NotesUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesUI.Commands
{
	internal class NoteSelectCommand : ICommand
	{
		private ShellViewModel viewModel;

		public NoteSelectCommand(ShellViewModel viewModel)
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
			return true;
		}

		public void Execute(object parameter)
		{
			viewModel.NoteModel.Note = viewModel.GetNoteById((Guid?)parameter);
		}
	}
}
