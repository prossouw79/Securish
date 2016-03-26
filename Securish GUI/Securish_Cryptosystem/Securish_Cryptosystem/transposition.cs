using System;
using System.Collections.Generic;
using System.Linq;

namespace transpositionTest
{
	public class transposition
	{
		bool charsIsPrime;
		int key;
		public transposition()
		{
			Random rnd = new Random (1000);
			key = rnd.Next ();
		}

		public char[] doTransposition(char[] charsIn, bool encryption)
		{

			int length = charsIn.Length;
			char[] current = charsIn;
			charsIsPrime = this.isPrime (length);
			
			//shuffle in case of prime number of characters in input
			if (charsIsPrime) {
				current =  Shuffle (charsIn, key);
			}

			List<int> deviseableNumbers = new List<int> ();

			for (int k = 1; k <= length; k++) {
				if (length % k == 0)
					deviseableNumbers.Add (k);
			}

			if (encryption) 
			{
				foreach (int size in deviseableNumbers)
				{
					List<char[]> tmpGroups = getGroups (current, size);
					char[][] tmparr = tmpGroups.ToArray ();
					tmpGroups.Clear ();
					for (int k = 0; k < tmparr.Length; k++) 
					{
						tmpGroups.Add (reverseArray (tmparr [k]));
					}
					current = appendArrays (tmpGroups);
				}
			} else {
				deviseableNumbers.Reverse();
				foreach (int size in deviseableNumbers)
				{
					List<char[]> tmpGroups = getGroups (current, size);
					char[][] tmparr = tmpGroups.ToArray ();
					tmpGroups.Clear ();
					for (int k = 0; k < tmparr.Length; k++) 
					{
						tmpGroups.Add (reverseArray (tmparr [k]));
					}
					current = appendArrays (tmpGroups);
				}
			}
			if (charsIsPrime) {
				return DeShuffle (current, key);	
			} else
				return current;
		}

		private List<char[]> getGroups(char[] data, int numberOfGroups)
		{
			List<char[]> holder = new List<char[]> ();
			holder.Capacity = numberOfGroups;
			int totalCounter = 0;
			int size = (int) data.Length / numberOfGroups;

			while (holder.Count < holder.Capacity) {
				char[] tmp = getSubArray (data, totalCounter, size);
				holder.Add (tmp);
				totalCounter += size;
			}
			return holder;
		}

		private char[] getSubArray(char[] data, int index, int length)
		{
			List<char> outList = new List<char> ();

			for (int k = index; k < index + length; k++) {
				outList.Add (data [k]);
			}
			return outList.ToArray();
		}

		private char[] reverseArray(char[] inp)
		{
			List<char> list = new List<char> ();

			foreach (char c in inp)
				list.Add (c);
			list.Reverse ();
			return list.ToArray();
		}


		private char[] appendArrays(List<char[]> listIn)
		{
			List<char> chars = new List<char> ();

			foreach (char[] c in listIn)
				foreach (char k in c)
					chars.Add (k);

			return chars.ToArray ();
		}

		private string printArray(char[] arrayIn)
		{
			string msg = "|";
			foreach (char c in arrayIn)
				msg +=  c.ToString() + "|";

			return msg;
		}

		private int[] GetShuffleExchanges(int size, int key)
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

		private char[] Shuffle(char[] toShuffle, int key)
		{
			int size = toShuffle.Length;
			var exchanges = GetShuffleExchanges(size, key);
			for (int i = size - 1; i > 0; i--)
			{
				int n = exchanges[size - 1 - i];
				char tmp = toShuffle[i];
				toShuffle[i] = toShuffle[n];
				toShuffle[n] = tmp;
			}
			return toShuffle;
		}

		private char[] DeShuffle(char[] shuffled, int key)
		{
			int size = shuffled.Length;
			var exchanges = GetShuffleExchanges(size, key);
			for (int i = 1; i < size; i++)
			{
				int n = exchanges[size - i - 1];
				char tmp = shuffled[i];
				shuffled[i] = shuffled[n];
				shuffled[n] = tmp;
			}
			return shuffled;
		}

		private bool isPrime(int number)
		{
			int boundary = (int) Math.Floor(Math.Sqrt(number));

			if (number == 1) return false;
			if (number == 2) return true;

			for (int i = 2; i <= boundary; ++i)  
				if (number % i == 0)  return false;
			
			return true;        
		}

		public string getString(char[] inp)
		{
			string msg = "";
			foreach (char c in inp)
				msg += c.ToString ();
			return msg;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace transpositionTest
{
	public class transposition
	{
		bool charsIsPrime;
		int key;
		public transposition()
		{
			Random rnd = new Random (1000);
			key = rnd.Next ();
		}

		public char[] doTransposition(char[] charsIn, bool encryption)
		{

			int length = charsIn.Length;
			char[] current = charsIn;
			charsIsPrime = this.isPrime (length);
			
			//shuffle in case of prime number of characters in input
			if (charsIsPrime) {
				current =  Shuffle (charsIn, key);
			}

			List<int> deviseableNumbers = new List<int> ();

			for (int k = 1; k <= length; k++) {
				if (length % k == 0)
					deviseableNumbers.Add (k);
			}

			if (encryption) 
			{
				foreach (int size in deviseableNumbers)
				{
					List<char[]> tmpGroups = getGroups (current, size);
					char[][] tmparr = tmpGroups.ToArray ();
					tmpGroups.Clear ();
					for (int k = 0; k < tmparr.Length; k++) 
					{
						tmpGroups.Add (reverseArray (tmparr [k]));
					}
					current = appendArrays (tmpGroups);
				}
			} else {
				deviseableNumbers.Reverse();
				foreach (int size in deviseableNumbers)
				{
					List<char[]> tmpGroups = getGroups (current, size);
					char[][] tmparr = tmpGroups.ToArray ();
					tmpGroups.Clear ();
					for (int k = 0; k < tmparr.Length; k++) 
					{
						tmpGroups.Add (reverseArray (tmparr [k]));
					}
					current = appendArrays (tmpGroups);
				}
			}
			if (charsIsPrime) {
				return DeShuffle (current, key);	
			} else
				return current;
		}

		private List<char[]> getGroups(char[] data, int numberOfGroups)
		{
			List<char[]> holder = new List<char[]> ();
			holder.Capacity = numberOfGroups;
			int totalCounter = 0;
			int size = (int) data.Length / numberOfGroups;

			while (holder.Count < holder.Capacity) {
				char[] tmp = getSubArray (data, totalCounter, size);
				holder.Add (tmp);
				totalCounter += size;
			}
			return holder;
		}

		private char[] getSubArray(char[] data, int index, int length)
		{
			List<char> outList = new List<char> ();

			for (int k = index; k < index + length; k++) {
				outList.Add (data [k]);
			}
			return outList.ToArray();
		}

		private char[] reverseArray(char[] inp)
		{
			List<char> list = new List<char> ();

			foreach (char c in inp)
				list.Add (c);
			list.Reverse ();
			return list.ToArray();
		}


		private char[] appendArrays(List<char[]> listIn)
		{
			List<char> chars = new List<char> ();

			foreach (char[] c in listIn)
				foreach (char k in c)
					chars.Add (k);

			return chars.ToArray ();
		}

		private string printArray(char[] arrayIn)
		{
			string msg = "|";
			foreach (char c in arrayIn)
				msg +=  c.ToString() + "|";

			return msg;
		}

		private int[] GetShuffleExchanges(int size, int key)
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

		private char[] Shuffle(char[] toShuffle, int key)
		{
			int size = toShuffle.Length;
			var exchanges = GetShuffleExchanges(size, key);
			for (int i = size - 1; i > 0; i--)
			{
				int n = exchanges[size - 1 - i];
				char tmp = toShuffle[i];
				toShuffle[i] = toShuffle[n];
				toShuffle[n] = tmp;
			}
			return toShuffle;
		}

		private char[] DeShuffle(char[] shuffled, int key)
		{
			int size = shuffled.Length;
			var exchanges = GetShuffleExchanges(size, key);
			for (int i = 1; i < size; i++)
			{
				int n = exchanges[size - i - 1];
				char tmp = shuffled[i];
				shuffled[i] = shuffled[n];
				shuffled[n] = tmp;
			}
			return shuffled;
		}

		private bool isPrime(int number)
		{
			int boundary = (int) Math.Floor(Math.Sqrt(number));

			if (number == 1) return false;
			if (number == 2) return true;

			for (int i = 2; i <= boundary; ++i)  
				if (number % i == 0)  return false;
			
			return true;        
		}

		public string getString(char[] inp)
		{
			string msg = "";
			foreach (char c in inp)
				msg += c.ToString ();
			return msg;
		}
	}
}

