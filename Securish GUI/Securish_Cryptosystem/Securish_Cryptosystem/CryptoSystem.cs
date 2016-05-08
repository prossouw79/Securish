using System;
using System.Collections.Generic;

namespace Securish_Cryptosystem
{
	public class CryptoSystem<T>
	{
		public CryptoSystem ()
		{
		}
		
		bool charsIsPrime;
		int key = 1994;


		public byte[] DoSubstitution(byte[] inBytes, int value, bool encryption)
		{

			byte[] outBytes = inBytes;
			int absVal = Math.Abs (value);
			if (!encryption)
				absVal *= -1;//minus value for decryption no matter input value

			for (int i = 0; i < inBytes.Length; i++) {
				outBytes [i] = (byte)(inBytes [i]+absVal); //shift the byte value
			}
			return outBytes;

		}

		public string DoSubstitutionText(string input, int value, bool encryption)
		{
			string outp = "";
			int absVal = Math.Abs (value);

			if (!encryption)
				absVal *= -1;//minus value for decryption no matter input value

			char[] tmp = input.ToCharArray ();
			foreach (char c in tmp) {
				outp += (char) ((int)c + absVal); //apply shift value
			}
			return outp;
		}


		public T[] doTransposition(T[] data, bool encryption)
		{
			T[] tmp;
			if (encryption)
				tmp = Enc (data, key);
			else
				tmp = Dec (data, key);

			return tmp;

		}

		private int[] GetExchanges(int size, int key) //create swapping pairs
		{
			int[] exchanges = new int[size - 1];
			var rand = new Random(key);
			for (int i = size - 1; i > 0; i--)
			{
				int n = rand.Next(i + 1);
				exchanges[size - 1 - i] = n;
			}
			return exchanges;
		}

		private T[] Enc(T[] data, int key)//transposition encryption
		{
			int size = data.Length;
			int[] ex = GetExchanges(size, key);
			for (int i = size - 1; i > 0; i--)
			{
				int n = ex[size - 1 - i];
				T tmp = data[i];
				data[i] = data[n];
				data[n] = tmp;
			}
			return data;
		}

		private T[] Dec(T[] data, int key) //transposition decryption
		{
			int size = data.Length;
			int[] ex = GetExchanges(size, key);
			for (int i = 1; i < size; i++)
			{
				int n = ex[size - i - 1];
				T tmp = data[i];
				data[i] = data[n];
				data[n] = tmp;
			}
			return data;
		}
		
		public byte[] DoVernam(byte[] inBytes, byte[] keyBytes)
		{
			byte[] outBytes = inBytes;
			//ensure message and key are equal in size
			if (inBytes.Length > keyBytes.Length) { 
				List<byte> newKey = new List<byte> ();

				double factor = inBytes.Length / keyBytes.Length;
				int repeats = ((int)factor) + 1;

				for (int k = 0; k < repeats; k++) {
					foreach (byte j in keyBytes) {
						newKey.Add (j);
					}
					Console.WriteLine ("plaintextlength: " + inBytes.Length + " newKey Length " + newKey.Count);
				}//newkey must now be equal or longer than plaintextbytes

				while (newKey.Count > inBytes.Length) {
					int lastByte = newKey.Count - 1;
					newKey.RemoveAt (lastByte);
				}
				keyBytes = newKey.ToArray ();

			} else if (inBytes.Length < keyBytes.Length) { 
				List<byte> newKey = new List<byte> ();

				foreach (byte k in keyBytes) {
					newKey.Add (k);
				}


				while (newKey.Count > inBytes.Length) {
					int lastByte = newKey.Count - 1;
					newKey.RemoveAt (lastByte);
				}
				keyBytes = newKey.ToArray ();

			} else {}//key and message are same length

			//do vernam operation
			for (int i = 0; i < inBytes.Length; i++) {
				outBytes [i] = (byte)(inBytes [i] ^ keyBytes [i]);//XOR bytes together
			}
			return outBytes;
		}


		private byte[] GetBytes(string str)//method for converting from string to byte[]
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		private string GetString(byte[] bytes)//method for getting a string representation of a byte[]
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}
		
		 public string DoVigenere(string txt, string pw, bool enc)
        {
        	int d;
        	if (enc) 
        		d = 1;
        	else
        		d = -1;
        
			int counter = 0;
			int tempCharValue;
            string ns = "";
			//pretpare both text and key strings
            txt = prepareText(txt);
            pw = prepareText(pw);
            foreach (char t in txt)
            {
				if (t < 65) {
					continue; //skip to next text
				}
                tempCharValue = t - 65 + d * (pw[counter] - 65);
				if (tempCharValue < 0) {
					tempCharValue += 26;//shift char into alphabet
				}
                ns += Convert.ToChar(65 + (tempCharValue % 26)); 
				if (++counter == pw.Length) {
					counter = 0;
				}
            }
            return ns;
        }
        
        private string prepareText (string text) //method to prepare text for viginere
		{
        	string tmp = text.ToUpper();
        	char[] arr = tmp.ToCharArray();
			arr = Array.FindAll<char>(arr, (c => (char.IsLetter(c))));                             
			tmp = new string(arr);
        	return tmp;
		}
		public string CalculateChecksumFile(byte[] byteToCalculate)
		{
			int checksum = 0;
			foreach (byte chData in byteToCalculate)
			{
				checksum += chData;
			}
			checksum &= 0xff;
			return checksum.ToString("X2");
		}

	}
}

