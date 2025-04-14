namespace Labb2_Threads_And_Async
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hit enter or write status + enter to see how the race proceeds.\n");
            Console.Write("Race starts in ");
            for (int i = 3; i > 0; i--)
            {
                Console.Write(i + "...");
                Task.Delay(1000).Wait();
            }
            Console.Write("GO!!!\n\n");

            var car1 = new Car("Volvo");
            var car2 = new Car("SAAB");

            var race1 = Car.Race(car1);
            var race2 = Car.Race(car2);

            var raceList = new List<(Car car, Task<string> raceTask)>
            {
                (car1, race1),
                (car2, race2)
            };

            Car.Check(raceList);

            Task<string> winnerTask = await Task.WhenAny(race1, race2);
            string winner = await winnerTask;
            Console.WriteLine($"{winner} wins this thrilling race! Weee-hooooo!!!");

            await Task.WhenAll(race1, race2);
            Console.WriteLine("And now the loosing car crosses the finishing line. Good job. But not great.");
        }

       
    }
}
