using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalRegister
{
    internal class Cesar
    {
        private int off;
        private char[] text;

        private readonly int n = 26;

        internal Cesar(string text, int off)
        {
            this.off = off;
            this.text = text.ToUpper().ToCharArray();
        }

        internal string getCifrar()
        {
            return cifrar();
        }

        internal string getDescifrar()
        {
            return descrifrar();
        }

        private string cifrar()
        {
            for (int i = 0; i < text.Length; i++)
                if (text[i] > 64 && text[i] < 91)
                    text[i] = (char)((((text[i] - 'A') + off) % n) + 'A');

            return new string(text);
        }

        private string descrifrar()
        {
            int mod;

            for (int i = 0; i < text.Length; i++)
                if (text[i] > 64 && text[i] < 91) 
                {
                    mod = (((text[i] - 'A') - off) % n);
                    text[i] = (char)((mod < 0) ? (mod + 'A' + n) : (mod + 'A'));
                }

            return new string(text);
        }
    }
}
