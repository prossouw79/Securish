using System;
using System.IO;

namespace crypto
{
	public class vernam
	{
		public void EncryptFile(string originalFile, string encryptedFile, string keyFile)
		{

			byte[] originalBytes;
			using (FileStream fs = new FileStream(originalFile, FileMode.Open))
			{
				originalBytes = new byte[fs.Length];
				fs.Read(originalBytes, 0, originalBytes.Length);
			}

			byte[] keyBytes = new byte[originalBytes.Length];
			Random random = new Random();
			random.NextBytes(keyBytes);

			using (FileStream fs = new FileStream(keyFile, FileMode.Create))
			{
				fs.Write(keyBytes, 0, keyBytes.Length);
			}

			byte[] encryptedBytes = new byte[originalBytes.Length];
			DoVernam(originalBytes, keyBytes, ref encryptedBytes);

			using (FileStream fs = new FileStream(encryptedFile, FileMode.Create))
			{
				fs.Write(encryptedBytes, 0, encryptedBytes.Length);
			}
		}

		public void DecryptFile(string encryptedFile, string keyFile, string decryptedFile)
		{
			byte[] encryptedBytes;
			using (FileStream fs = new FileStream(encryptedFile, FileMode.Open))
			{
				encryptedBytes = new byte[fs.Length];
				fs.Read(encryptedBytes, 0, encryptedBytes.Length);
			}

			byte[] keyBytes;
			using (FileStream fs = new FileStream(keyFile, FileMode.Open))
			{
				keyBytes = new byte[fs.Length];
				fs.Read(keyBytes, 0, keyBytes.Length);
			}

			byte[] decryptedBytes = new byte[encryptedBytes.Length];
			DoVernam(encryptedBytes, keyBytes, ref decryptedBytes);

			using (FileStream fs = new FileStream(decryptedFile, FileMode.Create))
			{
				fs.Write(decryptedBytes, 0, decryptedBytes.Length);
			}
		}

		public void DoVernam(byte[] inBytes, byte[] keyBytes, ref byte[] outBytes)
		{
			if ((inBytes.Length != keyBytes.Length) ||
				(keyBytes.Length != outBytes.Length)) 
			{
				Console.WriteLine ("!!!!Bytes in: " + inBytes.Length);
				Console.WriteLine ("!!!!Key bytes: " + keyBytes.Length);
				Console.WriteLine ("!!!!Bytes out: " + inBytes.Length);


				throw new ArgumentException ("Byte-array are not of same length");
			}

			for (int i = 0; i < inBytes.Length; i++) {
				outBytes [i] = (byte)(inBytes [i] ^ keyBytes [i]);
				Console.WriteLine (inBytes[i] + " ^ " + keyBytes[i] + ":\t" + outBytes[i]);

			}
			Console.WriteLine ();
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