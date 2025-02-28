using Questao2;

class Program
{
    public static async Task Main()
    {
        while (true) 
        {
            var times = await GetTeams();

            Console.WriteLine("Select team 1 by number:");
            for (int i = 0; i < times.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {times[i]}");
            }

            int indexTime1 = ValidateIndex(times.Count);
            string time1 = times[indexTime1];

            Console.WriteLine("Do you want to select team 2? (Y/N):");
            string opcao = Console.ReadLine().Trim().ToUpper();
            string time2 = null;

            if (opcao == "Y")
            {
                Console.WriteLine("Select team 2 by number:");
                int indexTime2 = ValidateIndex(times.Count);
                time2 = times[indexTime2];
            }

            Console.WriteLine("inform the year (Ex. 2025) ");
            int year = int.Parse(Console.ReadLine()?.Trim());

            Console.WriteLine("Do you want to inform the page? (Y/N):");
            opcao = Console.ReadLine()?.Trim().ToUpper();
            int? page = null;

            if (opcao == "Y")
            {
                Console.WriteLine("Enter the page");
                if (int.TryParse(Console.ReadLine(), out int pg))
                {
                    page = pg;
                }
            }

            int totalGoals = await GetTotalScoredGoals(time1, time2, year, page);

            Console.WriteLine("Team " + time1 + " scored " + totalGoals.ToString() + " goals in " + year);

            Console.WriteLine();
            Console.WriteLine("Do you want to make another inquiry? (Y/N)");
            opcao = Console.ReadLine()?.Trim().ToUpper();
            if(opcao != "Y") break;
        }  
    }

    public static int ValidateIndex( int quantity) 
    {
        int index;
        while (true) 
        {
            if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= quantity) return index - 1;
            Console.WriteLine("The number is not valid, try again");
        }
    }

    public static async Task<List<string>> GetTeams() 
    {
        using var httpClient = new HttpClient();
        var footballMatch = new ScoredGoalsService(httpClient);

        return await footballMatch.GetTeams();
    }

    public static async Task<int> GetTotalScoredGoals(string team1, string? team2, int year, int? page)
    {
        using var httpClient = new HttpClient();
        var footballMatch = new ScoredGoalsService(httpClient);

        return await footballMatch.GetGoals(team1, team2, year, page);
    }
}