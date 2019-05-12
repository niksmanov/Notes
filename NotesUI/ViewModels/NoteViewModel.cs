using NotesUI.Commands;
using NotesUI.Models;
using NotesUI.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace NotesUI.ViewModels
{
	internal class NoteViewModel : INotifyPropertyChanged
	{
		private Note note;

		public NoteViewModel(ShellViewModel parent)
		{
			Note = new Note();
			ParentModel = parent;
			UpdateCommand = new NoteUpdateCommand(this);
			DeleteCommand = new NoteDeleteCommand(this);
		}


		public Note Note
		{
			get { return note; }
			set
			{
				note = value;
				OnPropertyChanged("Note");
			}
		}

		public ShellViewModel ParentModel { get; set; }
		public IEnumerable<string> Colors
		{
			get { return new string[] { "Yellow", "Orange", "IndianRed", "LightGreen", "LightBlue" }; }
		}
		public ICommand UpdateCommand { get; set; }
		public bool CanUpdate { get { return !string.IsNullOrWhiteSpace(Note?.Title); } }
		public ICommand DeleteCommand { get; set; }
		public bool CanDelete { get { return Note.Id != Guid.Empty; } }


		public void SaveChanges(Guid id)
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notes");

			bool isExist = id != Guid.Empty;
			string noteName = isExist ? $"{id}.txt" : $"{Guid.NewGuid()}.txt";
			path = $"{path}\\{noteName}";

			File.WriteAllText(path, string.Empty);
			using (var writer = new StreamWriter(path))
			{
				writer.WriteLine(NoteConstants.TITLE + Note.Title);
				writer.WriteLine(NoteConstants.TEXT + Note.Text);
				writer.WriteLine(NoteConstants.COLOR + Note.Color);
				writer.WriteLine(NoteConstants.DATE_CREATED + DateTime.UtcNow);
			}

			Note = new Note();
			ParentModel.GetNotes();
		}


		public void Delete(Guid id)
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notes", $"{id}.txt");
			File.Delete(path);

			Note = new Note();
			ParentModel.GetNotes();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
