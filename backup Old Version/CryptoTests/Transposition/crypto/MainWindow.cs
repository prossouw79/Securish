using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	string currentDirectory;//use for temporary writes

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		currentDirectory = Environment.CurrentDirectory;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	//variables



	protected void OnBtnTextEncryptClicked (object sender, EventArgs e)
	{
		//ecrypt text
	}

	protected void OnBtnTextDecryptClicked (object sender, EventArgs e)
	{
		//decrypt text
	}

	protected void OnBtnOpenFileClicked (object sender, EventArgs e)
	{
		//openfile
		string file = OpenFile ("Select a file to encrypt/decrypt");



	}

	protected void OnBtnFileEncryptClicked (object sender, EventArgs e)
	{
		//encrypt file
	}

	protected void OnBtnDecryptEncryptClicked (object sender, EventArgs e)
	{
		//decrypt file
	}


	protected string OpenFile(string message)
	{
		string path = "";

		Gtk.FileChooserDialog filechooser =
			new Gtk.FileChooserDialog(message,
			                          this,
			                          FileChooserAction.Open,
			                          "Cancel",ResponseType.Cancel,
			                          "Open",ResponseType.Accept);

		if (filechooser.Run() == (int)ResponseType.Accept) 
		{
			System.IO.FileStream file = System.IO.File.OpenRead(filechooser.Filename);
			file.Close();
		}
		path = filechooser.Filename;
		filechooser.Destroy();
		return path;
	}

	protected string SaveFile(string message)
	{
		string path = "";

		Gtk.FileChooserDialog filechooser =
			new Gtk.FileChooserDialog(message,
			                          this,
			                          FileChooserAction.Save,
			                          "Cancel",ResponseType.Cancel,
			                          "Open",ResponseType.Accept);

		if (filechooser.Run() == (int)ResponseType.Accept) 
		{
			System.IO.FileStream file = System.IO.File.OpenRead(filechooser.Filename);
			file.Close();
		}
		path = filechooser.Filename;
		filechooser.Destroy();
		return path;
	}
}
