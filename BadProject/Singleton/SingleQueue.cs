using System;
using System.Collections.Generic;

namespace BadProject
{
    public class SingleQueue
    {
        private static SingleQueue _instance;
        public Queue<DateTime> MyQueue;
        private static readonly object Lck = new object();

        private SingleQueue()
        {
            MyQueue = new Queue<DateTime>();
        }

        public static SingleQueue Instance
        {

            get
            {
                lock (Lck)
                {
                    return _instance ??= new SingleQueue();
                }


            }
        }
    }
}