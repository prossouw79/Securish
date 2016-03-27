using System;

namespace crypto
{
	public class vigenere
	{
        public string DoVigenere(string txt, string pw, bool enc)
        {
        	int d;
        	if (enc) 
        		d = 1;
        	else
        		d = -1;
        
            int pwi = 0, tmp;
            string ns = "";
            txt = txt.ToUpper();
            pw = pw.ToUpper();
            foreach (char t in txt)
            {
                if (t < 65) continue;
                tmp = t - 65 + d * (pw[pwi] - 65);
                if (tmp < 0) tmp += 26;
                ns += Convert.ToChar(65 + ( tmp % 26) );
                if (++pwi == pw.Length) pwi = 0;
            }
            return ns;
        }
    }
}

