package com.securish.pross.securish;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by pross on 4/9/2016.
 */
public class Vigenere {

    String key;
    String text = "";

    public Vigenere(String seedkey, String plaintext) {

        char[] cptext = plaintext.toCharArray();
        char[] ckey = seedkey.toCharArray();

        if(cptext.length == ckey.length)
        {
            this.text = plaintext;
            this.key = seedkey;
        }
        else if(cptext.length > ckey.length) {
            ArrayList<Character> newKey = new ArrayList<Character>();
            double factor = cptext.length / ckey.length;
            int repeats = ((int) factor) + 1;
            for (int k = 0; k < repeats; k++) {
                for (char c : ckey) {
                    newKey.add(c);
                }
                while (newKey.size() > cptext.length) {
                    int lastChar = newKey.size() - 1;
                    newKey.remove(lastChar);
                }//newkey is now just as long plaintext
                String sNewkey = "";
                for (char c : newKey) {
                    sNewkey += c;
                }

                this.key = sNewkey;
                this.text = plaintext;
            }
        }
        else if(cptext.length < ckey.length)
        {
            ArrayList<Character> newKey = new ArrayList<Character>();

                for (char c : ckey) {
                    newKey.add(c);

                while (newKey.size() > cptext.length) {
                    int lastChar = newKey.size() - 1;
                    newKey.remove(lastChar);
                }//newkey is now just as long plaintext
                String sNewkey = "";
                for (char k : newKey) {
                    sNewkey += k;
                }
                this.key = sNewkey;
                this.text = plaintext;
            }
        }
    }


    public String encrypt() {
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

    public String decrypt() {
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
