namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int win = 0;
            int total = 10000;
            var rand = new Random();

            for (int i = 1; i <= total; i++)
            {
                var montyHall = new MontyHall(rand);
                char myDoor = montyHall.Doors[rand.Next(3)];
                montyHall.FirstChoice(myDoor);
                if(montyHall.LastChoice(myDoor))
                    win++;
            }
            Console.WriteLine($"If i keep the same gate i win {win} every {total}: {win * 100 / total}%");

            win = 0;
            for (int i = 1; i <= total; i++)
            {
                var montyHall = new MontyHall(rand);
                char myDoor = montyHall.Doors[rand.Next(3)];
                var newDoors = montyHall.FirstChoice(myDoor);
                myDoor = newDoors.First(d => d != myDoor);
                if(montyHall.LastChoice(myDoor))
                    win++;
            }
            Console.WriteLine($"If i switch ports i win {win} every {total}: {win * 100 / total}%");
        }
    }

    class MontyHall
    {
        private readonly char correctDoor;
        private Random rand;
        private char[] doors = { 'A', 'B', 'C' };

        public MontyHall(Random rand)
        {
            this.rand = rand;
            correctDoor = Doors[rand.Next(3)];
        }

        public char[] Doors { get => (char[])doors.Clone(); }

        public char[] FirstChoice(char door)
        {
            if (!Doors.Contains(door))
                throw new ArgumentException();

            char[] selectedDoors = { correctDoor, door };
            char[] wrongDoors = Doors.Except(selectedDoors).ToArray();

            if (wrongDoors.Length == 1)
                doors = selectedDoors;
            else
            {
                char[] newDoors = { correctDoor, wrongDoors[rand.Next(2)] };
                doors = newDoors;
            }
            return Doors;
        }

        public Boolean LastChoice(char door)
        {
            if (!Doors.Contains(door))
                throw new ArgumentException();

            return door == correctDoor;
        }
    }
}