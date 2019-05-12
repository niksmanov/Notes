using NotesUI.Models;
using NotesUI.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NotesUI.Commands;

namespace NotesUI.ViewModels
{
	internal class ShellViewModel
	{
		public ShellViewModel()
		{
			Notes = new ObservableCollection<Note>();
			NoteModel = new NoteViewModel(this);
			GetNotes();
			SelectCommand = new NoteSelectCommand(this);
		}

		public NoteViewModel NoteModel { get; set; }

		public ObservableCollection<Note> Notes { get; set; }

		public ICommand SelectCommand { get; set; }

		public void GetNotes()
		{
			Notes.Clear();
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notes");

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			var directory = new DirectoryInfo(path);

			foreach (var file in directory.GetFiles())
				Notes.Add(GetNoteFromFile(file));
		}

		public Note GetNoteById(Guid? id = null)
		{
			if (!id.HasValue)
				return new Note();

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Notes\\{id}.txt");
			var file = new FileInfo(path);
			return GetNoteFromFile(file);
		}

		private Note GetNoteFromFile(FileInfo file)
		{
			string noteId = file.Name.Remove(file.Name.Length - 4);
			var note = new Note { Id = new Guid(noteId) };
			using (StreamReader reader = file.OpenText())
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if (line.StartsWith(NoteConstants.TITLE))
						note.Title = line.Replace(NoteConstants.TITLE, string.Empty);
					else if (line.StartsWith(NoteConstants.TEXT))
						note.Text = line.Replace(NoteConstants.TEXT, string.Empty);
					else if (line.StartsWith(NoteConstants.COLOR))
						note.Color = line.Replace(NoteConstants.COLOR, string.Empty);
					else if (line.StartsWith(NoteConstants.DATE_CREATED))
						note.DateCreated = line;
				}
			}
			return note;
		}

	}
}
