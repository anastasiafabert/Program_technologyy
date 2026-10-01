using System;
using System.Collections.Generic;

namespace CosmodromeProject_Hw
{
   
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Выберите источник данных:");
                Console.WriteLine("1 — InMemory");
                Console.WriteLine("2 — CSV");
                Console.Write("Ваш выбор: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Неверный выбор");
                    return;
                }

                List<Mission> missions;
                List<Cosmonaut> cosmonauts;
                List<Rocket> rockets;

                switch (choice)
                {
                    case 1:
                        InMemoryRepository mem = new InMemoryRepository();
                        missions = mem.GetMissions();
                        cosmonauts = mem.GetCosmonauts();
                        rockets = mem.GetRockets();
                        break;

                    case 2:
                        CsvRepository csv = new CsvRepository("data");
                        missions = csv.GetMissions();
                        cosmonauts = csv.GetCosmonauts();
                        rockets = csv.GetRockets();
                        break;

                    default:
                        Console.WriteLine("Неверный выбор");
                        return;
                }

                
                Cosmonaut c = FindCosmonaut(rockets, cosmonauts, "Союз-2");
                if (c is not null)
                    Console.WriteLine("1. FindCosmonaut(\"Союз-2\"): " + c.GetInfo());
                else
                    Console.WriteLine("1. FindCosmonaut(\"Союз-2\"): null");

            
                Mission m = FindMission(missions, rockets[0]);
                if (m is not null)
                    Console.WriteLine("2. FindMission(rocket \"Союз-2\"): " + m.Info);
                else
                    Console.WriteLine("2. FindMission(rocket \"Союз-2\"): null");

                
                Console.WriteLine("3. GetTotalPayload: " + GetTotalPayload(rockets) + " кг");

                
                Console.WriteLine("4. GetCosmonautsByRank:");
                Dictionary<string, List<Cosmonaut>> byRank = GetCosmonautsByRank(cosmonauts);
                foreach (var pair in byRank)
                {
                    Console.WriteLine($"   {pair.Key} — {pair.Value.Count}");
                }

                
                Console.WriteLine("5. PrintAllRockets:");
                PrintAllRockets(rockets, cosmonauts, missions);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Поиск космонавта по модели ракеты.
        /// </summary>
        static Cosmonaut FindCosmonaut(List<Rocket> rockets, List<Cosmonaut> cosmonauts, string model)
        {
            if (rockets is null || cosmonauts is null || model is null) return null;

            Rocket foundRocket = null;
            for (int i = 0; i < rockets.Count; i++)
            {
                if (rockets[i].Model == model)
                {
                    foundRocket = rockets[i];
                    break;
                }
            }

            if (foundRocket is null) return null;

            for (int i = 0; i < cosmonauts.Count; i++)
            {
                if (cosmonauts[i].Id == foundRocket.CosmonautId)
                    return cosmonauts[i];
            }

            return null;
        }

        /// <summary>
        /// Поиск миссии для ракеты.
        /// </summary>
        static Mission FindMission(List<Mission> missions, Rocket rocket)
        {
            if (missions is null || rocket is null) return null;

            for (int i = 0; i < missions.Count; i++)
            {
                if (missions[i].Id == rocket.MissionId)
                    return missions[i];
            }

            return null;
        }

        /// <summary>
        /// Суммарная полезная нагрузка всех ракет.
        /// </summary>
        static int GetTotalPayload(List<Rocket> rockets)
        {
            if (rockets is null || rockets.Count == 0) return 0;

            int sum = 0;
            for (int i = 0; i < rockets.Count; i++)
            {
                sum += rockets[i].Payload;
            }
            return sum;
        }

        /// <summary>
        /// Группировка космонавтов по званию.
        /// </summary>
        static Dictionary<string, List<Cosmonaut>> GetCosmonautsByRank(List<Cosmonaut> cosmonauts)
        {
            if (cosmonauts is null) return new Dictionary<string, List<Cosmonaut>>();

            Dictionary<string, List<Cosmonaut>> result = new Dictionary<string, List<Cosmonaut>>();

            for (int i = 0; i < cosmonauts.Count; i++)
            {
                Cosmonaut c = cosmonauts[i];

                if (result.ContainsKey(c.Rank))
                {
                    result[c.Rank].Add(c);
                }
                else
                {
                    List<Cosmonaut> newList = new List<Cosmonaut>();
                    newList.Add(c);
                    result.Add(c.Rank, newList);
                }
            }

            return result;
        }

        /// <summary>
        /// Вывод всех ракет с космонавтом и миссией.
        /// </summary>
        static void PrintAllRockets(List<Rocket> rockets, List<Cosmonaut> cosmonauts, List<Mission> missions)
        {
            if (rockets is null || cosmonauts is null || missions is null) return;

            for (int i = 0; i < rockets.Count; i++)
            {
                Rocket r = rockets[i];

                string cosmonautName = "—";
                for (int j = 0; j < cosmonauts.Count; j++)
                {
                    if (cosmonauts[j].Id == r.CosmonautId)
                    {
                        cosmonautName = cosmonauts[j].FullName;
                        break;
                    }
                }

                string missionName = "—";
                for (int j = 0; j < missions.Count; j++)
                {
                    if (missions[j].Id == r.MissionId)
                    {
                        missionName = missions[j].Name;
                        break;
                    }
                }

                Console.WriteLine($"\"{r.Model}\" — космонавт {cosmonautName}, миссия \"{missionName}\"");
            }
        }
    }
}