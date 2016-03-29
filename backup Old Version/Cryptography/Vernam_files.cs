using System;
using System.IO;

namespace Securish
{
	public class Vernam_files
	{
		/// <summary>
		/// Encrypts a file by the Vernam-algorithm
		/// </summary>
		/// <param name="originalFile">
		/// Name of the file to be encrypted. Data is read from this file.
		/// </param>
		/// <param name="encryptedFile">
		/// Name of the encrypted file. The encrypted data gets written to that file.
		/// </param>
		/// <param name="keyFile">
		/// Name of the key file. The one time key gets written to that file.
		/// </param>
		public void EncryptFile(string originalFile, string encryptedFile, string keyFile)
		{
			// Read in the bytes from the original file:
			byte[] originalBytes;
			using (FileStream fs = new FileStream(originalFile, FileMode.Open))
			{
				originalBytes = new byte[fs.Length];
				fs.Read(originalBytes, 0, originalBytes.Length);
			}

			// Create the one time key for encryption. This is done
			// by generating random bytes that are of the same lenght 
			// as the original bytes:
			byte[] keyBytes = new byte[originalBytes.Length];
			Random random = new Random();
			random.NextBytes(keyBytes);

			// Write the key to the file:
			using (FileStream fs = new FileStream(keyFile, FileMode.Create))
			{
				fs.Write(keyBytes, 0, keyBytes.Length);
			}

			// Encrypt the data with the Vernam-algorithm:
			byte[] encryptedBytes = new byte[originalBytes.Length];
			DoVernam(originalBytes, keyBytes, ref encryptedBytes);

			// Write the encrypted file:
			using (FileStream fs = new FileStream(encryptedFile, FileMode.Create))
			{
				fs.Write(encryptedBytes, 0, encryptedBytes.Length);
			}
		}
		//---------------------------------------------------------------------
		/// <summary>
		/// Decrypts a file by Vernam-algorithm
		/// </summary>
		/// <param name="encryptedFile">
		/// Name of the encrypted file
		/// </param>
		/// <param name="keyFile">
		/// Name of the key file. The content of this file has to be the same
		/// as the content generated while encrypting
		/// </param>
		/// <param name="decryptedFile">
		/// Name of the decrypted file. The decrypted data gets written to this 
		/// file
		/// </param>
		public void DecryptFile(string encryptedFile, string keyFile, string decryptedFile)
		{
			// Read in the encrypted bytes:
			byte[] encryptedBytes;
			using (FileStream fs = new FileStream(encryptedFile, FileMode.Open))
			{
				encryptedBytes = new byte[fs.Length];
				fs.Read(encryptedBytes, 0, encryptedBytes.Length);
			}

			// Read in the key:
			byte[] keyBytes;
			using (FileStream fs = new FileStream(keyFile, FileMode.Open))
			{
				keyBytes = new byte[fs.Length];
				fs.Read(keyBytes, 0, keyBytes.Length);
			}

			// Decrypt the data with the Vernam-algorithm:
			byte[] decryptedBytes = new byte[encryptedBytes.Length];
			DoVernam(encryptedBytes, keyBytes, ref decryptedBytes);

			// Write the decrypted file:
			using (FileStream fs = new FileStream(decryptedFile, FileMode.Create))
			{
				fs.Write(decryptedBytes, 0, decryptedBytes.Length);
			}
		}
		//---------------------------------------------------------------------
		/// <summary>
		/// Computes the Vernam-encryption/decryption
		/// </summary>
		/// <param name="inBytes"></param>
		/// <param name="keyBytes"></param>
		/// <param name="outBytes"></param>
		private void DoVernam(byte[] inBytes, byte[] keyBytes, ref byte[] outBytes)
		{
			// Check arguments:
			if ((inBytes.Length != keyBytes.Length) ||
				(keyBytes.Length != outBytes.Length))
				throw new ArgumentException("Byte-array are not of same length");

			// Encrypt/decrypt by XOR:
			for (int i = 0; i < inBytes.Length; i++)
				outBytes[i] = (byte)(inBytes[i] ^ keyBytes[i]);
		}
	}
}