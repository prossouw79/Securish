using System;
using Securish_Cryptosystem;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Securish_Cryptosystem
{
	public partial class demonstration : Gtk.Window
	{
		byte[] data = new byte[16];
		Random random = new Random();
		string text = "Right now, the date and time is: " + DateTime.Now; 



		CryptoSystem<char> crypt_c = new CryptoSystem<char> ();
		CryptoSystem<byte> crypt_b = new CryptoSystem<byte> ();

		public demonstration () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();

			createRandomBytes (ref data);
			string dataChecksum = CalculateChecksumFile(data);

			txt_output.Buffer.Text = "-----------------------------INPUT-----------------------------";
			txt_output.Buffer.Text += "\nSAMPLE TEXT: \t" + text;
			txt_output.Buffer.Text += "\nDATA BYTES:\t" + displayArr (data);
			txt_output.Buffer.Text += "DATA CHECKSUM:\t" + dataChecksum;
		
			txt_output.Buffer.Text += "\n---------------------TEXT ENCRYPTION---------------------";
			txt_output.Buffer.Text += "\n[SUBSTITUTION(4)]:";
			string str_tmp_enc = crypt_c.DoSubstitutionText (text, 4, true);
			txt_output.Buffer.Text += "\n\tEncryption:\t" + str_tmp_enc;
			string str_tmp_dec = crypt_c.DoSubstitutionText (str_tmp_enc, 4, false);
			txt_output.Buffer.Text += "\n\tDecryption:\t" + str_tmp_dec + "\n";

			txt_output.Buffer.Text += "\n[TRANSPOSITION]:";
			str_tmp_enc = GetString(crypt_c.doTransposition (text.ToCharArray (), true));
			txt_output.Buffer.Text += "\n\tEncryption:\t" + str_tmp_enc;
			str_tmp_dec = GetString(crypt_c.doTransposition (str_tmp_enc.ToCharArray(), false));
			txt_output.Buffer.Text += "\n\tDecryption:\t" + str_tmp_dec + "\n";
		
			txt_output.Buffer.Text += "\n[VERNAM] with key 'password':";
			str_tmp_enc = GetString(crypt_c.DoVernam(GetBytes(text),GetBytes("password")));
			txt_output.Buffer.Text += "\n\tEncryption:\t" + str_tmp_enc;
			str_tmp_dec = GetString(crypt_c.DoVernam(GetBytes(str_tmp_enc),GetBytes("password")));
			txt_output.Buffer.Text += "\n\tDecryption:\t" + str_tmp_dec + "\n";
		
			txt_output.Buffer.Text += "\n[VIGINERE] with key 'password':";
			str_tmp_enc = crypt_c.DoVigenere(text,"password",true);
			txt_output.Buffer.Text += "\n\tEncryption:\t" + str_tmp_enc;
			str_tmp_dec = crypt_c.DoVigenere(str_tmp_enc,"password",false);
			txt_output.Buffer.Text += "\n\tDecryption:\t" + str_tmp_dec + "\n";
		
			byte[] tmpBytes = data;

			txt_output.Buffer.Text += "\n---------------------FILE ENCRYPTION---------------------";
			txt_output.Buffer.Text += "\nOriginal file checksum:\t\t" + CalculateChecksumFile(data)+"\n";

			txt_output.Buffer.Text += "\n[SUBSTITUTION(4)]:";
			tmpBytes = crypt_b.DoSubstitution(data,4,true);
			str_tmp_enc = CalculateChecksumFile(tmpBytes);
			txt_output.Buffer.Text += "\n\tEncrypted file checksum:\t" + str_tmp_enc;
			tmpBytes = crypt_b.DoSubstitution(tmpBytes,4,false);
			str_tmp_enc = CalculateChecksumFile(tmpBytes);
			txt_output.Buffer.Text += "\n\tDeccrypted file checksum:\t" + str_tmp_enc;
			txt_output.Buffer.Text += "\nDecrypted file matches original: " + (str_tmp_enc == dataChecksum);

			txt_output.Buffer.Text += "\n[TRANSPOSITION]:";
			tmpBytes = crypt_b.doTransposition (data, true);
			str_tmp_enc = CalculateChecksumFile(tmpBytes);
			txt_output.Buffer.Text += "\n\tEncrypted file checksum:\t" + str_tmp_enc;
			tmpBytes = crypt_b.doTransposition(tmpBytes,false);
			str_tmp_enc = CalculateChecksumFile(tmpBytes);
			txt_output.Buffer.Text += "\n\tDeccrypted file checksum:\t" + str_tmp_enc;
			txt_output.Buffer.Text += "\nDecrypted file matches original: " + (str_tmp_enc == dataChecksum);

			txt_output.Buffer.Text += "\n[VERNAM with key 'password':";
			tmpBytes = crypt_b.DoVernam (data,GetBytes("password"));
			str_tmp_enc = CalculateChecksumFile(tmpBytes);
			txt_output.Buffer.Text += "\n\tEncrypted file checksum:\t" + str_tmp_enc;
			tmpBytes = crypt_b.DoVernam (tmpBytes,GetBytes("password"));
			str_tmp_enc = CalculateChecksumFile(tmpBytes);
			txt_output.Buffer.Text += "\n\tDeccrypted file checksum:\t" + str_tmp_enc;
			txt_output.Buffer.Text += "\nDecrypted file matches original: " + (str_tmp_enc == dataChecksum);
		}

		private string displayArr(byte[] array)
		{
			string msg = "";
			foreach (byte value in array)
				msg += value + " ";
			return msg + "\n";
		}

		private void createRandomBytes(ref byte[] data)
		{
			random.NextBytes (data);
		}

		private string GetString(char[] chars)
		{
			string msg = "";

			foreach (char c in chars)
				msg += c.ToString();

			return msg;				
		}

		private string GetString(byte[] bytes)
		{
			return System.Text.Encoding.UTF32.GetString (bytes);
		}

		private byte[] GetBytes(string str)
		{
			return System.Text.Encoding.UTF32.GetBytes (str);
		}

		private string CalculateChecksumFile(byte[] byteToCalculate)
		{
			MD5CryptoServiceProvider myMD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] hash = myMD5CryptoServiceProvider.ComputeHash(byteToCalculate);

			string result = "";
			foreach (byte b in hash)
			{
				result += b.ToString("X2");
			}

			return result;
		}
	}
}

