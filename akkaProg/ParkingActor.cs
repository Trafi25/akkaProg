using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace akkaProg
{
    class ParkingActor : UntypedActor
    {
        private string name;
        public ParkingActor(string name) {
            this.name = name;       
        
        }
        public void Send(string message)
        {
            Console.WriteLine($"Check: {message}");
        }

        protected override void OnReceive(object message)
        {
            Console.WriteLine($"Comand: {message} -- {name}");
        }
        protected override void PreStart() => Console.WriteLine($"{name} started");

        protected override void PostStop() => Console.WriteLine($"{name} stopped");
    }
}
