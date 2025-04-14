namespace Labb2_Threads_And_Async
{
    public class Car
    {
        public string Name { get; set; }
        public int Speed { get; set; } = 120;
        public double Distance { get; set; } = 5d;
        public double MadeDistance { get; set; } = 0d;

        public Car(string name)
        {
            Name = name;
            Distance = Distance;
            Speed = Speed;
            MadeDistance = MadeDistance;
        }

        public static async Task<string> Race(Car car)
        {
            bool finished = false;
            Console.WriteLine(car.Name + " has started");
            int counter = 0;

            while (!finished)
            {
                car.MadeDistance += (car.Speed * (1.0/36000.0));
                counter++;
                if (counter == 100)
                {
                    counter = 0;
                    await RandomAccident(car);
                }
                if (car.MadeDistance >= car.Distance)
                {
                    finished = true;
                }
                await Task.Delay(100);
            }
            return car.Name;
        }

        public static async Task RandomAccident(Car car)
        {
            Random random = new Random();
            int rnd = random.Next(1, 50);

            if (rnd == 1)
            {
                //Tanka, stanna 15 s
                Console.WriteLine($"Oh, no! {car.Name} needs to fill the tank! Stops for 15 seconds...");
                await Task.Delay(15000);
            }
            else if (rnd > 1 && rnd < 4)
            {
                //Punktering, stanna 10 s
                Console.WriteLine($"Oh, no! {car.Name} has a flat tire! Stops for 10 seconds...");
                await Task.Delay(10000);
            }
            else if (rnd > 3 && rnd < 9)
            {
                //Fågel på vindrutan, stanna 5 s
                Console.WriteLine($"SCMACK! A bird hit the front screen! {car.Name} stops for 5 seconds... ");
                await Task.Delay(5000);
            }
            else if (rnd > 8 && rnd < 19)
            {
                //Motorfel, hastigheten sänks med 1 km/h
                car.Speed--;
                Console.WriteLine($"Something is wrong with the {car.Name} motor. Speed decreases to {car.Speed} km/h");
            }
        }
        public static async void Check(List<(Car car, Task<string> raceTask)> raceList)
        {
            while (true)
            {
                string input = Console.ReadLine().ToLower();
                if (input == "status" || input == "")
                {
                    foreach (var (car, task) in raceList)
                    {
                        Console.WriteLine($"{car.Name} has driven {Math.Round(car.MadeDistance, 2)} km.");
                        input = "";
                    }
                }
                await Task.Delay(100);
            }
        }
    }
}
