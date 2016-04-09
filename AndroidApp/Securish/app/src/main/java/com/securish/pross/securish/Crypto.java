package com.securish.pross.securish;

import java.lang.reflect.Array;
import java.util.AbstractList;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;


/**
 * Created by pross on 4/8/2016.
 */
public class Crypto<T> {

    String inputText = "";

    public Crypto(String input)
    {
        this.inputText = input;
    }


    public String DoSubstitutionText(String input, int value, boolean encryption)
    {
        String outp = "";
        int absVal = Math.abs(value);

        if (!encryption)
            absVal *= -1;//minus value for decryption no matter input value

        char[] tmp = input.toCharArray ();

        for (char c: tmp )
        {
            outp += (char) ((int)c + absVal);
        }

        return outp;
    }

    boolean charsIsPrime;
    int key = 1994;

    public T[] doTransposition(T[] data, boolean encryption)
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
        Random rand = new Random(key);
        for (int i = size - 1; i > 0; i--)
        {
            int n = rand.nextInt(i + 1);
            exchanges[size - 1 - i] = n;
        }
        return exchanges;
    }

    private T[] Enc(T[] data, int key)
    {
        int size = data.length;
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
        int size = data.length;
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

    public char[] DoVernam(char[] inChar, char[] keyChar, char[] out)
    {
        out = inChar;
        //ensure message and key are equal in size
        if (inChar.length > inChar.length) {
            List<Character> newKey = new ArrayList<Character>();

            double factor = inChar.length / keyChar.length;
            int repeats = ((int)factor) + 1;

            for (int k = 0; k < repeats; k++) {
                for (char j: keyChar)
                {
                    newKey.add(j);
                }
            }//newkey must now be equal or longer than plaintextchars

            while (newKey.size() > inChar.length) {
                int lastchar = newKey.size() - 1;
                newKey.remove(lastchar);
            }
            keyChar = getArrayFromList(newKey);

        } else if (inChar.length < keyChar.length) {
            ArrayList<Character> newKey = new ArrayList<Character> ();

            for (char k: keyChar) {
                newKey.add(k);
            }

            while (newKey.size() > inChar.length) {
                int lastchar = newKey.size() - 1;
                newKey.remove(lastchar);
            }
            keyChar = getArrayFromList(newKey);

        } else {}//key and message are same length
        //do vernam operation

        for(int k = 0; k < inChar.length; k++) {
            out[k] = (char) (inChar[k] ^ keyChar[k]);
        }

        return out;
        //Console.WriteLine ();
    }


    public String DoVigenere(String txt, String pw, boolean enc)
    {
        String text = txt.replaceAll("[^a-zA-Z]", "");
        if (enc)
        {
            String res = "";
            text = text.toUpperCase();
            for (int i = 0, j = 0; i < text.length(); i++) {
                char c = text.charAt(i);
                if (c < 'A' || c > 'Z') continue;
                res += (char)((c + key.charAt(j) - 2 * 'A') % 26 + 'A');
                j = ++j % key.length();
            }
            return res;
        }
        else
        {
            String res = "";
            text = text.toUpperCase();
            for (int i = 0, j = 0; i < text.length(); i++) {
                char c = text.charAt(i);
                if (c < 'A' || c > 'Z') continue;
                res += (char)((c - key.charAt(j) + 26) % 26 + 'A');
                j = ++j % key.length();
            }
            return res;
        }
    }

    public List<Character> asList(final String string) {
        return new AbstractList<Character>() {
            public int size() { return string.length(); }
            public Character get(int index) { return string.charAt(index); }
        };
    }

    public char[] getArrayFromList(List<Character> list) {
        char[] outArr = new char[list.size()];

        int cnt = 0;
        for (char c : list) {
            outArr[cnt++] = c;
        }
        return outArr;
    }

}

}
