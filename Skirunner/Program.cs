using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Skiløb
{
    class Program
    {
        public Thread[] Skirunner = new Thread[30];
        static Semaphore Lift1 = new Semaphore(10, 10);
        static Semaphore Lift2 = new Semaphore(15, 15);
        static Semaphore Lift3 = new Semaphore(30, 30);
        Random r = new Random();
        Stopwatch stopwacth = new Stopwatch();



        static void Main(string[] args)
        {
            Program p = new Program();
            p.GenerateSkier();
        }
        public void GenerateSkier()
        {
            for (int i = 0; i < 30; i++)
            {
                Skirunner[i] = new Thread(Run);
                Skirunner[i].Name = "runner_" + i;
                Skirunner[i].Start();

            }
        }
        public void Run()
        {
            TripsTaken();
        }



        public void SkiDown()
        {
            int TimeDown = r.Next(10, 20);
            int TimeMil = TimeDown * 1000;
            Console.WriteLine($"Time for trip down in milliseconds:{TimeMil}");

        }
        public void WaitTime()
        {

            Console.WriteLine("Getting into line");
            stopwacth.Start();
            Console.WriteLine("{0} is waiting in line", Thread.CurrentThread.Name);
            Lift1.WaitOne();
            stopwacth.Stop();
            Console.WriteLine($"Total waiting time is: {stopwacth.ElapsedMilliseconds}");
            Console.WriteLine("{0} is Going up in the lift", Thread.CurrentThread.Name);
            Thread.Sleep(100);
            Lift1.Release();
            Console.WriteLine("{0} Made it to the top", Thread.CurrentThread.Name);


        }
        public void TripsTaken()
        {
            int Trips = r.Next(5, 9);
            Console.WriteLine($"took {Trips} trips");

            for (int i = 0; i < Trips; i++)
            {
                SkiDown();
                WaitTime();
            }
            Console.WriteLine($"Day is finnished, total wait time: {stopwacth.ElapsedMilliseconds}");
            Console.ReadKey();
        }






    }
}
