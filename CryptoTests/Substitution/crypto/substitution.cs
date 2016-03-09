using System;

namespace crypto
{
	public class substitution
	{
		public substitution ()
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
	}
}

