using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesUI.Models
{
	internal class Note : INotifyPropertyChanged
	{
		private Guid id;
		private string title;
		private string text;
		private string color = "Yellow";
		private string dateCreated;

		public Guid Id
		{
			get { return id; }
			set
			{
				id = value;
				OnPropertyChanged("Id");
			}
		}

		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				OnPropertyChanged("Title");
			}
		}

		public string Text
		{
			get { return text; }
			set
			{
				text = value;
				OnPropertyChanged("Text");
			}
		}

		public string Color
		{
			get { return color; }
			set
			{
				color = value;
				OnPropertyChanged("Color");
			}
		}

		public string DateCreated
		{
			get { return dateCreated; }
			set
			{
				dateCreated = value;
				OnPropertyChanged("DateCreated");
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
