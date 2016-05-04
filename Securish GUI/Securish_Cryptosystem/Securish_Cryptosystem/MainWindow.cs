using System;
using Gtk;
using Securish_Cryptosystem;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public partial class MainWindow: Gtk.Window
{	
	int mode, algorithm,shiftValue;
	string plainText, keyText,filePath;
	bool fileMode;

	CryptoSystem<char> crypt_text = new CryptoSystem<char>();
	CryptoSystem<byte> crypt_data = new CryptoSystem<byte>();
	
	/*
	substitution sub = new substitution ();
	transposition<byte> trans_data = new transposition<byte>();
	transposition<char> trans_text = new transposition<char> ();
	vernam vern = new vernam ();
	vigenere vig = new vigenere ();*/

	List<String> modes = new List<string> ();
	List<String> algorithms = new List<string> ();


	string instructionStart,instr_vernam,instr_substitution,instr_vigenere,instr_transposition,instructionEnd;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		//initialization

		instructionStart = 	"-> Firstly, provide input, either by selecting a file or input text in the provided box.\n" +
							"-> Secondly, choose your encryption algorithm and mode using the provided comboboxes.\n" +
							"-> Finally, provide and encryption key (password) or numeric shift value number\n\n";

		instr_substitution = "X> Provide input text or file and select a shift value to use for encryption/decryption.\n";
		instr_vigenere = "X> Provide input TEXT ONLY (files unsupported) as well as an encryption key for encryption/decryption\n";
		instr_transposition = "X> Provide input text or file, no key or shift value is needed for encryption/decryption.\n";
		instr_vernam = "X> Provide input text or file as well as an encryption key for encryption/decryption.\n";

		instructionEnd = "Press the 'Go!' Button\n";

		mode = -1;
		algorithm = -1;
		plainText = "-";
		keyText = "-";
		filePath = "-";
		modes.Add ("Encryption");modes.Add("Decryption");
		algorithms.Add("Substitution");algorithms.Add("Vernam");algorithms.Add("Transposition");algorithms.Add("Vigenere");
			
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnBtnGoClicked (object sender, EventArgs e)
	{
		writeState ();
		/////////check input/////////
		//input text or file

		plainText = txt_plaintext_input.Text;
		mode = cmb_mode.Active;
		algorithm = cmb_algorithm.Active;
		keyText = txt_keytext_input.Text;
		shiftValue = spb_shift.ValueAsInt;


		bool inputReady = (plainText != "" || filePath != "-");
		bool modeSelected = (mode != -1 || algorithm != -1); 
		bool keyProvided = (keyText != "-" || algorithm == 0); //key not needed for substitution

		if (!inputReady) 
			MessageBox.ShowMessage ("Please check input file or text!");
		else if (!modeSelected) 
			MessageBox.ShowMessage ("Please select an algorithm and mode!");
		else if (!keyProvided) 
			MessageBox.ShowMessage ("Please provide an encryption/decryption key!");

		byte[] fileToProcess = new byte[5];//dummy arr
		if (filePath != "-")
			fileToProcess = this.readFileToByteArr (filePath);
		else
			fileToProcess = this.GetBytes (plainText);


		if (inputReady && modeSelected && keyProvided) {
			try {
				if (mode == 0) { //encryption
					switch (algorithm) {
					case 0: //substitution encryption
						{
							//addToLog ("substitution encryption NOT IMPLEMENTED");
							string outputText = "";
							byte[] outputBytes = fileToProcess;
							if (filePath == "-") {
								outputText = crypt_text.DoSubstitutionText (plainText, shiftValue, true);
								txt_log.Buffer.Text += "\n\n\t" + "Ciphertext:\t\t" + outputText;
							} else {
								crypt_data.DoSubstitution (fileToProcess, ref outputBytes, shiftValue, true);
								writeByteArrToFile (outputBytes, filePath + "_enc");
								addToLog ("Encrypted file written to " + filePath + "_enc");
							}

							break;
						}
					case 1: //vernam encryption
						{
							//addToLog ("vernam encryption NOT IMPLEMENTED");
						
							string outputText = "";
							byte[] outputBytes = fileToProcess;

							if (filePath == "-") {
								crypt_text.DoVernam (GetBytes (plainText), GetBytes (keyText), ref outputBytes);
								outputText = GetString (outputBytes);
								txt_log.Buffer.Text += "\n\n\t" + "Ciphertext:\t\t" + outputText;
							} else {
								crypt_data.DoVernam (fileToProcess, GetBytes (keyText), ref outputBytes);
								writeByteArrToFile (outputBytes, filePath + "_enc");
								addToLog ("Encrypted file written to " + filePath + "_enc");
							}
							break;
						}
					case 2: //transposition encryption
						{
							//addToLog ("transposition encryption NOT IMPLEMENTED");

							string outputText = "";
							byte[] outputBytes = fileToProcess;

							if (filePath == "-") {
								outputText = GetString (crypt_text.doTransposition (plainText.ToCharArray (), true));
								txt_log.Buffer.Text += "\n\n\t" + "Ciphertext:\t\t" + outputText;
							} else {
								outputBytes = crypt_data.doTransposition (fileToProcess, true);
								writeByteArrToFile (outputBytes, filePath + "_enc");
								addToLog ("Encrypted file written to " + filePath + "_enc");
							}
							break;
						}
					case 3: //vigenere encryption
						{
							//addToLog ("vigenere encryption NOT IMPLEMENTED");
							string outputText;
							if (filePath != "-") 
								MessageBox.ShowMessage ("File encryption/decryption is not possible with this Vigenere implementation, text only.");
							else {							
								outputText = crypt_text.DoVigenere (plainText, keyText, true);
								txt_log.Buffer.Text += "\n\n\t" + "Ciphertext:\t\t" + outputText;
							}
							break;
						}
					}

				} else if (mode == 1) { //decryption
					switch (algorithm) {
					case 0: //substitution decryption
						{
							//addToLog ("substitution decryption NOT IMPLEMENTED");
							string outputText = "";
							byte[] outputBytes = fileToProcess;
							if (filePath == "-") {
								outputText = crypt_text.DoSubstitutionText (plainText, shiftValue, false);
								txt_log.Buffer.Text += "\n\n\t" + "Ciphertext:\t\t" + outputText;
							} else {
								crypt_data.DoSubstitution (fileToProcess, ref outputBytes, shiftValue, false);
								writeByteArrToFile (outputBytes, filePath + "_dec");
								addToLog ("Decrypted file written to " + filePath + "_dec");
							}

							break;
						}
					case 1: //vernam decryption
						{
							//addToLog ("vernam decryption NOT IMPLEMENTED");

							string outputText = "";
							byte[] outputBytes = fileToProcess;

							if (filePath == "-") {
								crypt_text.DoVernam (GetBytes (plainText), GetBytes (keyText), ref outputBytes);
								outputText = GetString (outputBytes);
								addToLog ("Original Text:\t" + outputText);
							} else {
								crypt_data.DoVernam (fileToProcess, GetBytes (keyText), ref outputBytes);
								writeByteArrToFile (outputBytes, filePath + "_dec");
								addToLog ("Decrypted file written to " + filePath + "_dec");
							}

							break;
						}
					case 2: //transposition decryption
						{
							//addToLog ("transposition decryption NOT IMPLEMENTED");
							string outputText = "";
							byte[] outputBytes = fileToProcess;
							if (filePath == "-") {
								outputText = GetString (crypt_text.doTransposition (plainText.ToCharArray (), false));
								txt_log.Buffer.Text += "\n\n\t" + "Ciphertext:\t\t" + outputText;
							} else {
								outputBytes = crypt_data.doTransposition (fileToProcess, false); 
								writeByteArrToFile (outputBytes, filePath + "_dec");
								addToLog ("Decrypted file written to " + filePath + "_dec");
							}


							break;
						}
					case 3: //vigenere decryption
						{
							//addToLog ("vigenere decryption NOT IMPLEMENTED");
					
							string outputText;
							if (filePath != "-") 
								MessageBox.ShowMessage ("File encryption/decryption is not possible with this Vigenere implementation, text only.");
							else {
								outputText = crypt_text.DoVigenere (plainText, keyText, false);
								txt_log.Buffer.Text += "\n\n\t" + "Ciphertext:\t\t" + outputText;
							}
					
							break;
						}
					}
				}
			} catch (Exception ex) {
				MessageBox.ShowMessage("Please check input and needs of chosen algorithm before continuing");
			}
		}
	}


	protected void OnBtnFileInputClicked (object sender, EventArgs e)
	{
		txt_plaintext_input.Text = "";
		fileMode = true;	
		clearLog ();
		filePath = OpenFile ("Select File to Encrypt/Decrypt");
		addToLog ("Input file:\t" + filePath);
	}
	
	//used functions

	protected void clearLog()
	{
		txt_log.Buffer.Text = "";
	}

	protected void addToLog(string msg)
	{
		txt_log.Buffer.Text += DateTime.Now.ToString()+"\t---\t"+ msg + "\n";
	}

	protected void writeState()
	{
		try{
		clearLog ();
		bool file = (filePath != "-");
		if (!file)
			addToLog ("Input type:\t\tTEXT");
		else 
			addToLog ("Input type:\t\tFILE");

		addToLog("Mode:\t\t\t" + modes.ElementAt(cmb_mode.Active));
		addToLog("Using:\t\t\t" + algorithms.ElementAt(cmb_algorithm.Active));

		if(file)
			addToLog("Output to:\t\t" + filePath + ".enc");
		else
			addToLog("Output to:\t\tLOG");
		}
		catch(Exception ex)
		{
			MessageBox.ShowMessage (ex.Message);
			clearLog ();
		}

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
			new Gtk.FileChooserDialog(message,this,FileChooserAction.Save,
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

	private string GetString(char[] chars)
	{
		string msg = "";

		foreach (char c in chars)
			msg += c.ToString();

		return msg;
						
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


	protected void OnSpbShiftValueChanged (object sender, EventArgs e)
	{
		shiftValue = spb_shift.ValueAsInt;
	}

	protected void OnCmbAlgorithmChanged (object sender, EventArgs e)
	{
		txt_instruction.Buffer.Clear ();
		txt_instruction.Buffer.Text = instructionStart; 

		switch (cmb_algorithm.Active) {
		case 0:
			{
			txt_instruction.Buffer.Text += instr_substitution; 
				spb_shift.Visible = true;
				lbl_subshift.Visible = true;

				lbl_keytext.Visible = false;
				txt_keytext_input.Visible = false;

				break;
			}
		case 1:
			{
			txt_instruction.Buffer.Text += instr_vernam; 
				spb_shift.Visible = false;
				lbl_subshift.Visible = false;

				lbl_keytext.Visible = true;
				txt_keytext_input.Visible = true;	

				break;
			}
		case 2:
			{
			txt_instruction.Buffer.Text += instr_transposition; 
				spb_shift.Visible = false;
				lbl_subshift.Visible = false;

				lbl_keytext.Visible = true;
				txt_keytext_input.Visible = true;
				break;
			}
		case 3:
			{
			txt_instruction.Buffer.Text += instr_vigenere; 
				spb_shift.Visible = false;
				lbl_subshift.Visible = false;

				lbl_keytext.Visible = true;
				txt_keytext_input.Visible = true;
				break;
			}
		}
		txt_instruction.Buffer.Text += instructionEnd; 
	}

	protected void OnTxtPlaintextInputChanged (object sender, EventArgs e)
	{
		fileMode = false;
		filePath = "-";
	}
}
