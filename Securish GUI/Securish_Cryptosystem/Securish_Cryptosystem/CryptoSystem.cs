using System;
using System.Collections.Generic;

namespace Securish_Cryptosystem
{
	public class CryptoSystem<T>
	{
		public CryptoSystem ()
		{
		}
		
		public void DoSubstitution(byte[] inBytes, ref byte[] outBytes, int value, bool encryption)
		{
			int absVal = Math.Abs (value);
			if (!encryption)
				absVal *= -1;//minus value for decryption no matter input value



			if (inBytes.Length != outBytes.Length)
			{
				Console.WriteLine ("!!!!Bytes in: " + inBytes.Length);
				Console.WriteLine ("!!!!Bytes out: " + inBytes.Length);

				throw new ArgumentException ("Byte-array are not of same length");
			}

			for (int i = 0; i < inBytes.Length; i++) {
				outBytes [i] = (byte)(inBytes [i]+absVal);
			}

		}

		public string DoSubstitutionText(string input, int value, bool encryption)
		{
			string outp = "";
			int absVal = Math.Abs (value);

			if (!encryption)
				absVal *= -1;//minus value for decryption no matter input value

			char[] tmp = input.ToCharArray ();
			foreach (char c in tmp) {
				outp += (char) ((int)c + absVal);
			}
			return outp;
		}
		
		bool charsIsPrime;
		int key = 1994;

		public T[] doTransposition(T[] data, bool encryption)
		{
			T[] tmp;
			if (encryption)
				tmp = Enc (data, key);
			else
				tmp = Dec (data, key);

			return tmp;

		}

		private int[] GetExchanges(int size, int key)
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

		private T[] Enc(T[] data, int key)
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

		private T[] Dec(T[] data, int key)
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
		
		public void DoVernam(byte[] inBytes, byte[] keyBytes, ref byte[] outBytes)
		{
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
				outBytes [i] = (byte)(inBytes [i] ^ keyBytes [i]);
			}
			//Console.WriteLine ();
		}


		private byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		private string GetString(byte[] bytes)
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
        
            int pwi = 0, tmp;
            string ns = "";
            txt = prepareText(txt);
            pw = prepareText(pw);
            foreach (char t in txt)
            {
                if (t < 65) continue;
                tmp = t - 65 + d * (pw[pwi] - 65);
                if (tmp < 0) tmp += 26;
                ns += Convert.ToChar(65 + (tmp % 26));
                if (++pwi == pw.Length) pwi = 0;
            }
            return ns;
        }
        
        private string prepareText (string text)
		{
        	string tmp = text.ToUpper();
        	
        	char[] arr = tmp.ToCharArray();
			arr = Array.FindAll<char>(arr, (c => (char.IsLetter(c))));                             
			tmp = new string(arr);
        	return tmp;
		}

	}
}

