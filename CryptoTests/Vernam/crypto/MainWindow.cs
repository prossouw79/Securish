using System;
using Gtk;
using crypto;
using System.IO;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{	
	//variables
	string currentDirectory;//use for temporary writes

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		currentDirectory = Environment.CurrentDirectory + "/";
		Console.WriteLine ("Current Directory:\t" + currentDirectory);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	
	protected void OnBtnTextEncryptClicked (object sender, EventArgs e)
	{
		vernam vn_enc = new vernam ();

		string plaintext, keytext;
		plaintext = txt_encrypt_text.Text;
		keytext = txt_text_key.Text;

		//convert text to bytes
		byte[] plainTextBytes = GetBytes (plaintext);
		byte[] keyBytes = GetBytes (keytext);

		//ensure message and key are equal in size
		if (plainTextBytes.Length > keyBytes.Length) { 
			List<byte> newKey = new List<byte> ();

			double factor = plainTextBytes.Length / keyBytes.Length;
			int repeats = ((int) factor) + 1;

			for(int k = 0; k < repeats ; k++)
			{
				foreach (byte j in keyBytes) {
					newKey.Add (j);
				}
				Console.WriteLine ("plaintextlength: " + plainTextBytes.Length + " newKey Length " + newKey.Count);
			}//newkey must now be equal or longer than plaintextbytes

			while (newKey.Count > plainTextBytes.Length) {
				int lastByte = newKey.Count-1;
				newKey.RemoveAt (lastByte);
			}
			keyBytes = newKey.ToArray ();

		} else if (plainTextBytes.Length < keyBytes.Length) { 
			List<byte> newKey = new List<byte> ();

			foreach (byte k in keyBytes) {
				newKey.Add (k);
			}


			while (newKey.Count > plainTextBytes.Length) {
				int lastByte = newKey.Count-1;
				newKey.RemoveAt (lastByte);
			}
			keyBytes = newKey.ToArray ();

		}
		else 												//key and message are same length
		{

		}
		if(plainTextBytes.Length == keyBytes.Length){
			byte[] encryptedBytes = new byte[plainTextBytes.Length];
			vn_enc.DoVernam (plainTextBytes, keyBytes, ref encryptedBytes);

			string encryptedString = GetString (encryptedBytes);
			txt_encryption_view.Buffer.Text = encryptedString;
	}else
		txt_encryption_view.Buffer.Text = "Not equal length!\nText: " + plainTextBytes.Length + "\t Key: " + keyBytes.Length ;
	}

	protected void OnBtnTextDecryptClicked (object sender, EventArgs e)
	{
		vernam vn_dec = new vernam ();

		string ciphertext, keytext;
		ciphertext = txt_encrypt_text.Text;
		keytext = txt_text_key.Text;

		//convert text to bytes
		byte[] cipherTextBytes = GetBytes (ciphertext);
		byte[] keyBytes = GetBytes (keytext);

		//ensure message and key are equal in size
		if (cipherTextBytes.Length > keyBytes.Length) { 
			List<byte> newKey = new List<byte> ();

			double factor = cipherTextBytes.Length / keyBytes.Length;
			int repeats = ((int) factor) + 1;

			for(int k = 0; k < repeats ; k++)
			{
				foreach (byte j in keyBytes) {
					newKey.Add (j);
				}
				Console.WriteLine ("Ciphertextbytes: " + cipherTextBytes.Length + " newKey Length " + newKey.Count);
			}//newkey must now be equal or longer than plaintextbytes

			while (newKey.Count > cipherTextBytes.Length) {
				int lastByte = newKey.Count-1;
				newKey.RemoveAt (lastByte);
			}
			keyBytes = newKey.ToArray ();

		} else if (cipherTextBytes.Length < keyBytes.Length) { 
			List<byte> newKey = new List<byte> ();

			foreach (byte k in keyBytes) {
				newKey.Add (k);
			}


			while (newKey.Count > cipherTextBytes.Length) {
				int lastByte = newKey.Count-1;
				newKey.RemoveAt (lastByte);
			}
			keyBytes = newKey.ToArray ();

		}
		else 												//key and message are same length
		{

		}
		if(cipherTextBytes.Length == keyBytes.Length){
			byte[] encryptedBytes = new byte[cipherTextBytes.Length];
			vn_dec.DoVernam (cipherTextBytes, keyBytes, ref encryptedBytes);

			string decryptedString = GetString (encryptedBytes);
			txt_encryption_view.Buffer.Text = decryptedString;
		}else
			txt_encryption_view.Buffer.Text = "Not equal length!\nText: " + cipherTextBytes.Length + "\t Key: " + keyBytes.Length ;

	}
	

	protected void OnBtnFileEncryptClicked (object sender, EventArgs e)
	{
		//encrypt file
		vernam vn_enc = new vernam ();

		string inputPath = OpenFile ("Select file to encrypt");

		string outputPath = inputPath + "_vnm";
		string keypath = inputPath + "_key";

		vn_enc.EncryptFile (inputPath,outputPath,keypath);

		addToLog ("----ENCRYPTION----");
		addToLog ("Input File:\t" + inputPath);
		addToLog ("Output File:\t" + outputPath);
		addToLog ("Key File:\t" + keypath);
		addToLog ("Done!");

	}

	protected void OnBtnDecryptEncryptClicked (object sender, EventArgs e)
	{
		//decrypt file

		vernam vn_dec = new vernam ();

		string encryptedfile = OpenFile ("Select encrypted file to decrypt");
		string outputPath = encryptedfile + "_decr";
		string keypath = OpenFile ("Select keyfile file");

		vn_dec.DecryptFile (encryptedfile, keypath, outputPath);

		addToLog ("----DECRYPTION----");
		addToLog ("Input File:\t" + encryptedfile);
		addToLog ("Key File:\t" + keypath);
		addToLog ("Output File:\t" + outputPath);
		addToLog ("Done!");
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
		txt_log.Buffer.Text += "\n" + msg;
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
		throw new NotImplementedException ();
	}

	protected void OnBtnLoadcipherkeyClicked (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}
}
