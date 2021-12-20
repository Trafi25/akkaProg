using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Akka.Actor;

namespace akkaProg
{
    class ActorDriver : UntypedActor
    {
        private IActorRef[] parkPlaces; 
        private string name;
        private int parkSize=10;      
        private string[] cars;


        public ActorDriver(string name)
        {          
            this.name = name;
            parkPlaces = new IActorRef[parkSize];        
            var Context = ActorSystem.Create("actor-driver");
            cars = new string[parkSize];
        }

        protected override void OnReceive(object message)
        {
            Console.WriteLine($"Message received |{message}| for {name}");
            //parkingActor.Send(message?.ToString());
            whatMessage(message);
        }

        private void whatMessage(object message)
        {
            string[] subsMessage = message.ToString().Split(new char[] {'-'});
            switch (subsMessage[0]) {
                case "I want to park":                   
                    Console.WriteLine("Result: It was free place");
                    for (int i = 0; i < 10; i++) {
                        if (parkPlaces[i] == null)
                        {
                            parkPlaces[i] = Context.ActorOf(Props.Create<ParkingActor>(subsMessage[1]));
                            parkPlaces[i].Tell("I want to park");
                            cars[i] = subsMessage[1];
                     
                            break;
                        }
                       
                    }
                    break;
                case "I left":                   
                    Console.WriteLine("Result: Now the place is free");
                    for (int i = 0; i < 10; i++)
                    {
                        if (subsMessage[1].Equals(cars[i])) {
                            Thread.Sleep(500);
                            Context.Stop(parkPlaces[i]);
                            parkPlaces[i] = null;
                            cars[i] = null;
                        }
                    }
                        break;
                case "Watch places":
                    int j = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        if (parkPlaces[i] != null)
                        {
                            Thread.Sleep(100);
                            Console.WriteLine($"Path:{cars[i]} and this place num:{j + 1}");
                            j++;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Result: This is strange message");
                    break;

            }
               
        }

        protected override void PreStart() => Console.WriteLine($"{name} started");

        protected override void PostStop() => Console.WriteLine($"{name} stopped");
    }
}

  