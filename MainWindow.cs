using System;
using Gtk;
using Securish;
using System.Text.RegularExpressions;

public partial class MainWindow: Gtk.Window
{
	//encryption objects
	Vernam_text vernamT;
	Vernam_files vernamF;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
		
	protected void OnBtnEncryptClicked (object sender, EventArgs e)
	{


		if (rdb_vernam.Active) 
		{
			Vernam_files vfiles = new Vernam_files ();

			string inputPath = OpenFile ("Select file to encrypt");

			string outputPath = inputPath + ".vnm";
			string keypath = inputPath + "_key.txt";

			vfiles.EncryptFile (inputPath,outputPath,keypath);
		
		}
	}

	protected void OnBtnDecryptClicked (object sender, EventArgs e)
	{

		if (rdb_vernam.Active) 
		{
			Vernam_files vfiles = new Vernam_files ();

			string encryptedfile = SaveFile ("Select encrypted file to decrypt");
			string outputPath = encryptedfile + "_decr";
			string keypath = getFilePath ("Select keyfile file");;

			vfiles.DecryptFile (encryptedfile, keypath, outputPath);

		}
	}

	private string OpenFile(string message)
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

	private string SaveFile(string message)
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
