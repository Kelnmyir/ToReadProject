using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithNucs
{
    class Nuc
    {
        private char nucValue;
        public char NucValue
        {
            get => nucValue;
            set
            {
                switch (value)
                {
                    case char c when ((c == 'A') || (c == 'C') || (c == 'G') || (c == 'T')):
                        nucValue = value;
                        break;
                    default:
                        nucValue = 'N';
                        break;
                }
            }
        }
        public Nuc(char n)
        {
            switch (n)
            {
                case char c when ((c == 'A') || (c == 'C') || (c == 'G') || (c == 'T')):
                    nucValue = n;
                    break;
                default:
                    nucValue = 'N';
                    break;
            }
        }
        public Nuc Complementory()
        {
            char compNuc;
            switch (this.nucValue)
            {
                case 'A':
                    compNuc = 'T';
                    break;
                case 'T':
                    compNuc = 'A';
                    break;
                case 'C':
                    compNuc = 'G';
                    break;
                case 'G':
                    compNuc = 'C';
                    break;
                default:
                    compNuc = 'N';
                    break;
            }
            return new Nuc(compNuc);
        }
    }
}
