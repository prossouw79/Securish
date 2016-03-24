using System;

namespace crypto
{
	public class vigenere
	{
		public vigenere ()
		{
		}


		public static Byte[] doVigenere(Byte[] text, string key, bool encr)
		{
			if (encr) {
				Byte[] result= new Byte[text.Length];

				key = key.Trim().ToUpper();

				int keyIndex = 0;
				int keylength = key.Length;

				for (int i = 0; i < text.Length; i++)
				{
					keyIndex = keyIndex % keylength;
					int shift = (int)key[keyIndex] - 65;
					result[i] = (byte)(((int)text[i] + shift) % 256);
					keyIndex++;
				}

				return result;

			} else {
				Byte[] result = new Byte[text.Length];

				key = key.Trim().ToUpper();

				int keyIndex = 0;
				int keylength = key.Length;

				for (int i = 0; i < text.Length; i++)
				{             
					keyIndex = keyIndex % keylength;
					int shift = (int)key[keyIndex] - 65;
					result[i]= (byte)(((int)text[i] + 256 - shift) % 256);
					keyIndex++;               
				}

				return result;
			}

		}

	}
}

