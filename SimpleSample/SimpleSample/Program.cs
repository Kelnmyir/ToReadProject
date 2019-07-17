using System;
using System.Collections.Generic;
using System.Collections;

namespace SimpleSample
{
    public class DnaEnumerator : IEnumerator
    {
        private int i;
        DnaEnumerator()
        {
            i = 0;
        }

        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            if i<
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
    public enum Nuc
    {
        A,
        T,
        G,
        C
    }
    public class DnaCode : IEnumerable
    {
        private List<Nuc> sequence = new List<Nuc>();
        public List<Nuc> Seq
        {
            get { return sequence; }
            set { sequence = value; }  
        }
        public DnaCode(List<Nuc> seq)
        {
            foreach (Nuc nuc in seq)
            {
                Seq.Add(nuc);
            }
        }
        public DnaCode(Nuc[] seq)
        {
            foreach (Nuc nuc in seq)
            {
                Seq.Add(nuc);
            }
        }
        public IEnumerator GetEnumerator()
        {
            return Seq.GetEnumerator();
        }
    }
    class Program
    {
        static void Main()
        {
            Nuc[] mySeq = { Nuc.T, Nuc.T, Nuc.G, Nuc.A, Nuc.C, Nuc.A };
            DnaCode myCode = new DnaCode(mySeq);
            foreach (Nuc nuc in myCode)
            {
                Console.Write(nuc.ToString());
            }
            Console.ReadLine();
        }
    }
}
