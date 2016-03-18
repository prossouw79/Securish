using System;
using Gtk;
using crypto;
using System.IO;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{	
	//variables
	string currentDirectory;//use for temporary writes
	byte[] last_cipherText;
	int text_shiftValue = 0;
	int file_shiftValue = 0;
	

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		currentDirectory = Environment.CurrentDirectory + "/";
		Console.WriteLine ("Current Directory:\t" + currentDirectory);

		substitution sub = new substitution ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected void OnBtnTextEncryptClicked (object sender, EventArgs e)
	{
		substitution sub_enc = new substitution ();
		txt_encryption_view.Buffer.Text = sub_enc.DoSubstitutionText(txt_encrypt_text.Text,text_shiftValue,true);

	}

	protected void OnBtnTextDecryptClicked (object sender, EventArgs e)
	{
		substitution sub_dec = new substitution ();
		txt_encryption_view.Buffer.Text = sub_dec.DoSubstitutionText(txt_encrypt_text.Text,text_shiftValue,false);

	}

	

	protected void OnBtnFileEncryptClicked (object sender, EventArgs e)
	{

		substitution sub_enc = new substitution ();

		string path = OpenFile ("Select File to Encrypt");

		byte[] FileBytes;

		//read file
		using (FileStream fs = new FileStream(path, FileMode.Open))
		{
			FileBytes = new byte[fs.Length];
			fs.Read(FileBytes, 0, FileBytes.Length);
		}

			byte[] encryptedBytes = new byte[FileBytes.Length];
		sub_enc.DoSubstitution (FileBytes, ref encryptedBytes,file_shiftValue,true);

			last_cipherText = encryptedBytes;

			writeByteArrToFile (last_cipherText, path + "_enc");
	}
	

	protected void OnBtnDecryptEncryptClicked (object sender, EventArgs e)
	{
		substitution sub_dec = new substitution ();


		string path = OpenFile ("Select File to Decrypt");

		byte[] CipherFileBytes;

		//read file
		using (FileStream fs = new FileStream(path, FileMode.Open))
		{
			CipherFileBytes = new byte[fs.Length];
			fs.Read(CipherFileBytes, 0, CipherFileBytes.Length);
		}

	
			byte[] decryptedBytes = new byte[CipherFileBytes.Length];
		sub_dec.DoSubstitution (CipherFileBytes, ref decryptedBytes,file_shiftValue,false);

			last_cipherText = decryptedBytes;

			writeByteArrToFile (last_cipherText, path + "_decrypted");

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

	protected void addToLog(string msg)
	{
		txt_log.Buffer.Text += "\n" +DateTime.Now.ToString()+"\t---\t"+ msg;
	}
	protected void clearLog()
	{
		txt_log.Buffer.Text = "";
	}

	private byte[] GetBytes(string str)
	{
		return System.Text.Encoding.UTF32.GetBytes (str);
	}

	private string GetString(byte[] bytes)
	{
		return System.Text.Encoding.UTF32.GetString (bytes);
	}

	private byte[] readFileToByteArr(string path)
	{
		byte[] tmpBytes;
		using (FileStream fs = new FileStream(path, FileMode.Open))
		{
			tmpBytes = new byte[fs.Length];
			fs.Read(tmpBytes, 0, tmpBytes.Length);
		}
		return tmpBytes;
	}

	private void writeByteArrToFile(byte[] bytes, string path)
	{
		using (FileStream fs = new FileStream(path, FileMode.Create))
		{
			fs.Write(bytes, 0, bytes.Length);
		}
	}

	protected void OnBtnSaveCipherKeyClicked (object sender, EventArgs e)
	{
		string unique = DateTime.Now.ToString();

		writeByteArrToFile (last_cipherText, currentDirectory + unique+ "_encryptedFile");
		addToLog ("SAVED CipherText and Keypair to " + currentDirectory);
	}

	protected void OnBtnLoadcipherkeyClicked (object sender, EventArgs e)
	{
		string encryptedFile;
		encryptedFile = OpenFile ("Please Select Encrypted File");

		last_cipherText = readFileToByteArr (encryptedFile);
	
		txt_encrypt_text.Text = GetString (last_cipherText);
	}
	
	protected void OnSpnTextShiftValueChanged (object sender, EventArgs e)
	{
		text_shiftValue = (int)spn_text_shift.Value;
	}

	protected void OnSpnFileShiftValueChanged (object sender, EventArgs e)
	{
		file_shiftValue = (int)spn_file_shift.Value;
	}
}
