using System;
using System.Text;

namespace Securish
{
	public class Vernam_text
	{
		private string plaintext="";
		private string cipherText="";
		private string result = "";
		private int keyLength = 0;
		private string key = "";
		private Boolean keyAccess = false;

		public Vernam_text(string key)
		{
			this.key = key;
			this.result = DeCrypt (key);
		}

		public Vernam_text(int keylength,string msg)
		{
			this.keyLength = keylength;
			this.plaintext = msg;
			this.key = GenerateKey (this.keyLength);

			this.result = EnCrypt (this.plaintext, this.key);
			this.keyAccess = true;
		}

		public string getResult()
		{
			return result;
		}

		public string getKey()
		{
			if (keyAccess)
				return this.key;
			else
				return "key denied";
		}

		private string GenerateKey(int KeyLength)
		{
			int Lbound = 48;
			int Ubound = 122;
			int k;
			string strKey = string.Empty;
			Random rnd = new Random();


			for (int x=0; x < KeyLength; x++)
			{
				// This section here is what generates a random key with the help of
				// NextDouble method from the Random() class which produces a random
				// float values between 0 and 1
				k = Convert.ToInt32(((Ubound - Lbound) + 1) * rnd.NextDouble() + Lbound);
				// The cast the resulting binary value to its corresponding character value
				strKey = strKey + (char) k;
			}
			return strKey;
		}

		private string EnCrypt(string Message,string key)
		{
			byte[] chKey, chMsg;
			int k;
			int msglen = Message.Length;
			string txtCipher = string.Empty;
			ASCIIEncoding asc = new ASCIIEncoding();


			// GetBytes - returns an array of binary values of each character
			// of the string being passed in the parameter.
			chKey = asc.GetBytes(this.GenerateKey(this.keyLength));
			chMsg = asc.GetBytes(Message);


			for (int x=0; x < msglen; x++)
			{
				// 'k' gets the character exclusion (^) of each character in the
				// message and key in the same character index position
				k = chMsg[x] ^ chKey[x];
				txtCipher += (char) k;
			}
			return txtCipher;
		}

		private string DeCrypt(string key)
		{
			byte[] chKey, chMsg;
			int k;
			int msglen = key.Length;
			string txtPlain = string.Empty;
			ASCIIEncoding asc = new ASCIIEncoding();


			// GetBytes - returns an array of binary values of each character
			// of the string being passed in the parameter.
			chKey = asc.GetBytes(this.GenerateKey(this.keyLength));
			chMsg = asc.GetBytes(key);


			for (int x=0; x < msglen; x++)
			{
				// 'k' gets the character exclusion (^) of each character in the
				// message and key in the same character index position
				k = chMsg[x] ^ chKey[x];
				txtPlain += (char) k;
			}
			return txtPlain;
		}
	}
}

