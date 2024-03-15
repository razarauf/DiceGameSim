using System.Diagnostics;

namespace DiceGameSim;

class Program
{
    static void Main(string[] args)
    {
        int noOfSimulations = 10000;
        int noOfDie = 5;
        int total = 0;

        //Dictionary<int, int> result = TestRun(new List<int>(noOfDie) { 3, 1, 3, 6, 6 });
        //PrintLog(result, noOfSimulations, noOfDie);

        List<int> die = new List<int>(noOfDie);
        Dictionary<int, int> result = new Dictionary<int, int>();

        Stopwatch stopWatch = Stopwatch.StartNew();

        for (int i = 0; i < noOfSimulations; i++)
        {
            InitDie(die);
            total = 0;
            while (die.Count > 0)
            {
                RollDie(die);
                // PrintDie(die);
                total += GenerateTotal(die);
            }
            LogResult(result, total);
        }

        stopWatch.Stop();

        PrintLog(result, noOfSimulations, noOfDie, stopWatch.ElapsedMilliseconds);

        Console.ReadLine();    
    }

    public static void InitDie(List<int> die)
    {
        for (int i = 0; i < die.Capacity; i++)
        {
            die.Add(0);
        }
    }

    public static void RollDie(List<int> die)
    {
        Random random = new Random();

        for (int i = 0; i < die.Count; i++)
        {
            die[i] = random.Next(1, 12);
        }
    }

    public static int GenerateTotal(List<int> die)
    {
        int total = 0;

        if (die.IndexOf(3) >= 0)
        {
            die.RemoveAll(x => x == 3);
        }
        else if (die.Count > 0)
        {
            die.Sort();
            total = die[0];
            die.RemoveAt(0);
        }
        
        return total;
    }

    public static void LogResult(Dictionary<int, int> result, int total)
    {
        if (result.ContainsKey(total))
        {
            result[total] += 1;
        }
        else
        {
            result.Add(total, 1);
        }
    }

    public static void PrintLog(Dictionary<int, int> result, int noOfSims, int noOfDie, float timeElapsed)
    {
        Console.WriteLine($"Number of simulations was {noOfSims} using {noOfDie} dice.");

        foreach (var element in result)
        {
            Console.WriteLine($"Total {element.Key} occurs {element.Value} times");
        }

        Console.WriteLine($"Total simulation took {timeElapsed} milliseconds.");
    }

    public static void PrintDie(List<int> die)
    {
        foreach (int dice in die)
        {
            Console.Write(dice + ", ");
        }
        Console.WriteLine();
    }

    public static Dictionary<int, int> TestRun(List<int> die)
    {
        int total = 0;
        Dictionary<int, int> result = new Dictionary<int, int>();

        PrintDie(die);

        total += GenerateTotal(die);
        Console.WriteLine($"Total: {total}");
        PrintDie(die);
        LogResult(result, total);

        die = new List<int>() { 2, 5, 5 };

        total += GenerateTotal(die);
        Console.WriteLine($"Total: {total}");
        PrintDie(die);
        LogResult(result, total);

        die = new List<int>() { 6, 6 };

        total += GenerateTotal(die);
        Console.WriteLine($"Total: {total}");
        PrintDie(die);
        LogResult(result, total);

        die = new List<int>() { 3 };

        total += GenerateTotal(die);
        Console.WriteLine($"Total: {total}");
        PrintDie(die);
        LogResult(result, total);

        return result;
    }

}