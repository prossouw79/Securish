package com.securish.pross.securish;

import android.util.Log;

import java.io.Console;
import java.lang.reflect.Array;
import java.util.AbstractList;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.logging.ConsoleHandler;


/**
 * Created by pross on 4/8/2016.
 */
public class Crypto {

    boolean charsIsPrime;
    int key = 1994;


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

    public String doTransposition(char[] data, boolean encryption)
    {
        char[] tmp;
        if (encryption)
            tmp = Enc (data, key);
        else
            tmp = Dec (data, key);

        return charsToString(tmp);

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

    private char[] Enc(char[] data, int key)
    {
        int size = data.length;
        int[] ex = GetExchanges(size, key);
        for (int i = size - 1; i > 0; i--)
        {
            int n = ex[size - 1 - i];
            char tmp = data[i];
            data[i] = data[n];
            data[n] = tmp;
        }
        return data;
    }

    private char[] Dec(char[] data, int key)
    {
        int size = data.length;
        int[] ex = GetExchanges(size, key);
        for (int i = 1; i < size; i++)
        {
            int n = ex[size - i - 1];
            char tmp = data[i];
            data[i] = data[n];
            data[n] = tmp;
        }
        return data;
    }

    public String DoVigenere(String txt, String pw, boolean enc)
    {
        String text = txt.replaceAll("[^a-zA-Z]", "");
        Vigenere vig = new Vigenere(pw,txt);

        if()


        if (enc)
        {
            return vig.encrypt();
        }
        else
        {
            return vig.decrypt();
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


    public String charsToString(char[] inp)
    {
        String msg = "";
        for (char c : inp
             ) {
            msg += c;
        }
        return msg;

    }

}
