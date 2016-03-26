using System;
using System.Collections.Generic;

namespace crypto
{
	public class transposition
	{
		public byte[] doTransposition(byte[] bytesIn,bool encryption, int repetition)
		{
			byte[] outputBytes = bytesIn;
			int length = bytesIn.Length;
			List<int> deviseableNumbers = new List<int> ();

			for (int k = 0; k < length; k++) {
				if (k % length == 0 && k != length)
					deviseableNumbers.Add (k);
			}

			foreach (int num in deviseableNumbers) {

			}

		}

		private List<byte[]> getGroupsOfSize(byte[] data, int size)
		{
			List<byte[]> holder = new List<byte[]> ();
			int counter = 0;

			while (counter <= data.Length)
			{
				byte[] tmp = getSubArray (data, counter, size);
				holder.Add (tmp);
				counter++;
			}
			return holder;
		}

		private byte[] getSubArray(byte[] data, int index, int length)
		{
			byte[] result = new byte[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		private byte[] reverseArray(byte[] inp)
		{
			byte[] tmp = inp;

			for (int k = inp.Length; k >= 0; k--)
				tmp [k] = inp [k];

			return tmp;
		}

		private byte[] append
	}
}

