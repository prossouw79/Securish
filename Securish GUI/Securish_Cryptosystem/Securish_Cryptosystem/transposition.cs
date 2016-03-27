using System;
using System.Collections.Generic;
using System.Linq;

namespace Securish_Cryptosystem
{
	public class transposition<T>
	{
		bool charsIsPrime;
		int key = 1994;
		public transposition()
		{

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
	}
}

