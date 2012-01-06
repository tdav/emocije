using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoClassifier
{
    public class FastQueue<T>
    {
        protected T[] Data;
        protected int Write;
        protected int Read;

        protected int MaxLength;

        public FastQueue(int MaxLength)
        {
            this.MaxLength = MaxLength;
            Write = 0;
            Read = 0;
            Data = new T[MaxLength];
        }

        public void Enqueue(T[] Data)
        {
            foreach (T element in Data)
            {
                this.Data[(Write++) % MaxLength] = element;
            }
            Write %= MaxLength;
        }
        public void Enqueue(T element)
        {
            this.Data[(Write++) % MaxLength] = element;
            Write %= MaxLength;
        }

        public List<T> Dequeue(int NumberOfElements)
        {
            List<T> RetVal = new List<T>();
            for (int i = 0; i < NumberOfElements; i++)
            {
                RetVal.Add(this.Data[(Read++) % MaxLength]);
            }
            Read %= MaxLength;
            return RetVal;
        }
        public List<T> Peek(int NumberOfElements)
        {
            List<T> RetVal = new List<T>();
            int Read2 = Read;
            for (int i = 0; i < NumberOfElements; i++)
            {
                RetVal.Add(this.Data[(Read2++) % MaxLength]);
            }
            Read %= MaxLength;
            return RetVal;
        }

        public int Count 
        {
            get
            {
                if (Write > Read)
                    return Write - Read;
                else
                    return (MaxLength - (Read - Write));
            }
        }
        public void Delete(int NumberOfElements)
        {
            Read = (Read + NumberOfElements) % MaxLength;
        }

    }
}
