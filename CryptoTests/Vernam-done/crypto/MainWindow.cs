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
	byte[] last_keyText;
	

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
		if (txt_text_key.Text.Length > 0) {
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
				int repeats = ((int)factor) + 1;

				for (int k = 0; k < repeats; k++) {
					foreach (byte j in keyBytes) {
						newKey.Add (j);
					}
					Console.WriteLine ("plaintextlength: " + plainTextBytes.Length + " newKey Length " + newKey.Count);
				}//newkey must now be equal or longer than plaintextbytes

				while (newKey.Count > plainTextBytes.Length) {
					int lastByte = newKey.Count - 1;
					newKey.RemoveAt (lastByte);
				}
				keyBytes = newKey.ToArray ();

			} else if (plainTextBytes.Length < keyBytes.Length) { 
				List<byte> newKey = new List<byte> ();

				foreach (byte k in keyBytes) {
					newKey.Add (k);
				}


				while (newKey.Count > plainTextBytes.Length) {
					int lastByte = newKey.Count - 1;
					newKey.RemoveAt (lastByte);
				}
				keyBytes = newKey.ToArray ();

			} else { 												//key and message are same length

			}
			if (plainTextBytes.Length == keyBytes.Length) {
				byte[] encryptedBytes = new byte[plainTextBytes.Length];
				vn_enc.DoVernam (plainTextBytes, keyBytes, ref encryptedBytes);

				last_cipherText = encryptedBytes;
				last_keyText = keyBytes;

				string encryptedString = GetString (encryptedBytes);
				txt_encryption_view.Buffer.Text = encryptedString;
			} else
				txt_encryption_view.Buffer.Text = "Not equal length!\nText: " + plainTextBytes.Length + "\t Key: " + keyBytes.Length;
		} else
			addToLog ("ENTER TEXT ENCRYPTION KEY FIRST!");
	}

	protected void OnBtnTextDecryptClicked (object sender, EventArgs e)
	{
		if (txt_text_key.Text.Length > 0) {
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
		} else
			addToLog ("ENTER TEXT DECRYPTION KEY FIRST!");
	}

	

	protected void OnBtnFileEncryptClicked (object sender, EventArgs e)
	{
		if (txt_file_key.Text.Length > 0) {
		vernam vn_enc = new vernam ();

		string keytext;
		string path = OpenFile ("Select File to Encrypt");
		keytext = txt_file_key.Text;


		byte[] FileBytes;
		byte[] keyBytes = GetBytes (keytext);

		//read file
		using (FileStream fs = new FileStream(path, FileMode.Open))
		{
			FileBytes = new byte[fs.Length];
			fs.Read(FileBytes, 0, FileBytes.Length);
		}

		//ensure file and key are equal in size
		if (FileBytes.Length > keyBytes.Length) { 
			List<byte> newKey = new List<byte> ();

			double factor = FileBytes.Length / keyBytes.Length;
			int repeats = ((int) factor) + 1;

			for(int k = 0; k < repeats ; k++)
			{
				foreach (byte j in keyBytes) {
					newKey.Add (j);
				}
				Console.WriteLine ("plaintextlength: " + FileBytes.Length + " newKey Length " + newKey.Count);
			}//newkey must now be equal or longer than plaintextbytes

			while (newKey.Count > FileBytes.Length) {
				int lastByte = newKey.Count-1;
				newKey.RemoveAt (lastByte);
			}
			keyBytes = newKey.ToArray ();

		} else if (FileBytes.Length < keyBytes.Length) { //unlikely
			List<byte> newKey = new List<byte> ();

			foreach (byte k in keyBytes) {
				newKey.Add (k);
			}


			while (newKey.Count > FileBytes.Length) {
				int lastByte = newKey.Count-1;
				newKey.RemoveAt (lastByte);
			}
			keyBytes = newKey.ToArray ();

		}
		else 												//key and message are same length
		{

		}
		if(FileBytes.Length == keyBytes.Length){
			byte[] encryptedBytes = new byte[FileBytes.Length];
			vn_enc.DoVernam (FileBytes, keyBytes, ref encryptedBytes);

			last_cipherText = encryptedBytes;
			last_keyText = keyBytes;

			writeByteArrToFile (last_cipherText, path + "_enc");
			addToLog ("SAVED Encrypted File and Keypair to " + currentDirectory);

			
		}else
			txt_encryption_view.Buffer.Text = "Not equal length!\nText: " + FileBytes.Length + "\t Key: " + keyBytes.Length ;
	} else
		addToLog ("ENTER FILE ENCRYPTION KEY FIRST!");
	}
	

	protected void OnBtnDecryptEncryptClicked (object sender, EventArgs e)
	{
		if (txt_file_key.Text.Length > 0) {
		vernam vn_enc = new vernam ();

		string keytext;
		string path = OpenFile ("Select File to Decrypt");
		keytext = txt_file_key.Text;


		byte[] CipherFileBytes;
		byte[] keyBytes = GetBytes (keytext);

		//read file
		using (FileStream fs = new FileStream(path, FileMode.Open))
		{
			CipherFileBytes = new byte[fs.Length];
			fs.Read(CipherFileBytes, 0, CipherFileBytes.Length);
		}

		//ensure file and key are equal in size
		if (CipherFileBytes.Length > keyBytes.Length) { 
			List<byte> newKey = new List<byte> ();

			double factor = CipherFileBytes.Length / keyBytes.Length;
			int repeats = ((int) factor) + 1;

			for(int k = 0; k < repeats ; k++)
			{
				foreach (byte j in keyBytes) {
					newKey.Add (j);
				}
				//Console.WriteLine ("plaintextlength: " + CipherFileBytes.Length + " newKey Length " + newKey.Count);
			}//newkey must now be equal or longer than plaintextbytes

			while (newKey.Count > CipherFileBytes.Length) {
				int lastByte = newKey.Count-1;
				newKey.RemoveAt (lastByte);
			}
			keyBytes = newKey.ToArray ();

		} else if (CipherFileBytes.Length < keyBytes.Length) { //unlikely
			List<byte> newKey = new List<byte> ();

			foreach (byte k in keyBytes) {
				newKey.Add (k);
			}


			while (newKey.Count > CipherFileBytes.Length) {
				int lastByte = newKey.Count-1;
				newKey.RemoveAt (lastByte);
			}
			keyBytes = newKey.ToArray ();

		}
		else 												//key and message are same length
		{

		}
		if(CipherFileBytes.Length == keyBytes.Length){

			byte[] decryptedBytes = new byte[CipherFileBytes.Length];
			vn_enc.DoVernam (CipherFileBytes, keyBytes, ref decryptedBytes);

			last_cipherText = decryptedBytes;

			writeByteArrToFile (last_cipherText, path + "_decrypted");

			addToLog ("SAVED Encrypted File and Keypair to " + path);


		}else
			txt_encryption_view.Buffer.Text = "Not equal length!\nText: " + CipherFileBytes.Length + "\t Key: " + keyBytes.Length ;
} else
	addToLog ("ENTER FILE DECRYPTION KEY FIRST!");
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
		writeByteArrToFile (last_keyText, currentDirectory +unique + "_decryptionKey");
		addToLog ("SAVED CipherText and Keypair to " + currentDirectory);
	}

	protected void OnBtnLoadcipherkeyClicked (object sender, EventArgs e)
	{
		string encryptedFile, keyFile;
		encryptedFile = OpenFile ("Please Select Encrypted File");
		keyFile = OpenFile ("Please Select Key File");

		last_cipherText = readFileToByteArr (encryptedFile);
		last_keyText = readFileToByteArr (keyFile);

		txt_encrypt_text.Text = GetString (last_cipherText);
		txt_text_key.Text = GetString (last_keyText);
	}
}
