using System;
using System.Threading;

namespace Lab2
{
    class Program
    {
        static AutoResetEvent waitHandler1 = new AutoResetEvent(false);
        static AutoResetEvent waitHandler2 = new AutoResetEvent(false);
        static AutoResetEvent waitHandler3 = new AutoResetEvent(false);
        static AutoResetEvent waitHandler4 = new AutoResetEvent(false);
        static AutoResetEvent waitHandler5 = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            
            Thread t1 = new Thread(T1);
            t1.Name = "1";
            Thread t6 = new Thread(T6);
            t6.Name = "6";
            Thread t3 = new Thread(T3);
            t3.Name = "3";
            t3.Start();
            t1.Start();
            t6.Start();
        }

        static void T1()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            waitHandler1.Set();
            waitHandler5.WaitOne();
            Thread t2 = new Thread(T2);
            t2.Name = "2";
            t2.Start();
            
        }
        static void T2()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            Thread t5 = new Thread(T5);
            t5.Name = "5";
            t5.Start();
           
        }
        static void T3()
        {
            AutoResetEvent.WaitAll(new WaitHandle[] {waitHandler1, waitHandler2, waitHandler3, waitHandler4});
            Console.WriteLine(Thread.CurrentThread.Name);
        }
        static void T4()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            Thread t7 = new Thread(T7);
            t7.Name = "7";
            t7.Start();
           
        }
        static void T5()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            waitHandler3.Set();
        }
        static void T6()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            waitHandler2.Set();
            Thread t4 = new Thread(T4);
            t4.Name = "4";
            t4.Start();
            
        }
        static void T7()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            waitHandler4.Set();
            waitHandler5.Set();
        }
    }
}