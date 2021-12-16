using System;
using System.IO;
using Xamarin.Forms;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;

namespace DomesticWarehousemanApp.Views
{
    public partial class NotesPage : ContentPage
    {
		readonly string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");

        public NotesPage()
        {
			InitializeComponent();
		}

		private void buttonErase_Clicked(object sender, EventArgs e)
		{
			if (File.Exists(_fileName))
			{
				File.Delete(_fileName);
			}
		}

		private void buttonSave_Clicked(object sender, EventArgs e)
		{
			if (File.Exists(_fileName))
			{
				File.Delete(_fileName);
			}

			File.WriteAllText(_fileName, entryNote.Text);
		}

		private void buttonTest_Clicked(object sender, EventArgs e)
		{
			var task = new HttpClient().GetAsync("http://10.0.2.2:5000/api/test");

			try
			{
				if (task.Wait(5000))
				{
					buttonTest.Text = task.Result.ToString();
				}
				else
				{
					buttonTest.Text = "TIMEOUT";
				}
			}
			catch (Exception)
			{
				buttonTest.Text = "ERROR";
			}

		}
	}
}