using System;
using System.Security.Cryptography;
using System.IO;

namespace Securish
{
	public class Crypt_Text_AES
	{
		RijndaelManaged aesAlg;
		ICryptoTransform encryptor;
		MemoryStream msEncrypt;

		public Crypt_Text_AES()
		{
			aesAlg = new RijndaelManaged();
			aesAlg.BlockSize = 128;
			aesAlg.KeySize = 256;
			aesAlg.Mode = CipherMode.CFB;
			aesAlg.FeedbackSize = 8;
			aesAlg.Padding = PaddingMode.None;

			aesAlg.GenerateKey();
			aesAlg.GenerateIV();

			encryptor = aesAlg.CreateEncryptor();
			msEncrypt = new MemoryStream();
		}

		public byte[] encrypt(string plainText)
		{

			using (CryptoStream csEncrypt = new CryptoStream (msEncrypt, encryptor, CryptoStreamMode.Write)) {
				using (StreamWriter swEncrypt = new StreamWriter (csEncrypt)) {
					swEncrypt.Write (plainText);
				}
			}
			return msEncrypt.ToArray ();
		}


		public string decrypt(byte[] customArray)
		{

		ICryptoTransform decryptor = aesAlg.CreateDecryptor();

		MemoryStream msDecrypt = new MemoryStream(customArray);
		using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
			using (StreamReader swDecrypt = new StreamReader(csDecrypt)) {
				return swDecrypt.ReadToEnd();
			}
		}

	}
}
}
