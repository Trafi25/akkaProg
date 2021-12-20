namespace akkaProg
{
    using Akka.Actor;
    using System;
    using System.Threading;

    namespace AkkaConsoleSimple
    {
        public class Program
        {

            static void Main(string[] args)
            {
                Runner program = new Runner(10);
                program.Start();

               Console.ReadLine();             

            }
        }
    }

}

