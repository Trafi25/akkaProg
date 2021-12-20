using Akka.Actor;
using System;
using System.Threading;

namespace akkaProg
{
    class Runner
    {
        private int parkSize;
        private int calcSize = 0;
        private ActorSystem actorSystem;

        public Runner(int parkSize)
        {
            this.parkSize = parkSize;

        }

        public void Start()
        {
            actorSystem = ActorSystem.Create("test-actor-system");
            IActorRef actor = actorSystem.ActorOf(Props.Create<ActorDriver>("Parking"));
            //handMake(ref actor);
            addCar("I want to park-Car1", ref actor);
            addCar("I want to park-Car2", ref actor);
            addCar("I want to park-Car3", ref actor);
            addCar("I want to park-Car4", ref actor);
            leaveCar("I left-Car2", ref actor);            
            addCar("I want to park-Car5", ref actor);
            addCar("I want to park-Car6", ref actor);
             addCar("I want to park-Car7", ref actor);
            addCar("I want to park-Car8", ref actor);
            addCar("I want to park-Car9", ref actor);
            addCar("I want to park-Car10", ref actor);
            addCar("I want to park-Car11", ref actor);        
            addCar("I want to park-Car12", ref actor);
            Console.ReadLine();
            WatchPark(actor);
            }

        private void handMake(ref IActorRef actor)
        {
            while (true)
            {
                var message = Console.ReadLine();
                if (message == "q") break;
                actor.Tell(message);
            }
        }

        private void WatchPark(IActorRef actor)
        {
            actor.Tell("Watch places");
        }

        private void leaveCar(string message, ref IActorRef actorRef)
        {
            if (calcSize > 0)
            {
                actorRef.Tell(message);                
                calcSize--;
            }

        }

        private void addCar(string message, ref IActorRef actor)
        {
            if (calcSize < parkSize)
            {
                calcSize++;
                actor.Tell(message);
            }
            else
            {
                Console.WriteLine($"There was no place for the {message}, it left");                
            }
        }
    }
}
