using System;
using System.IO;
using System.Collections.Generic;

namespace crypto
{
	public class vernam
	{
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

	}
}