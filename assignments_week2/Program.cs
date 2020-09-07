using System;
using System.Collections.Generic;
using System.Linq;


namespace assignments_week2
{

    public static class ItemExtender
    {
        // we create a sorted list with ascending level and return the last one (excercise 2)
        public static Item GetHighestLevelItem(this Player player)
        {
            return player.Items.OrderBy(i => i.Level).ToList()[player.Items.Count - 1];
        }
    }

    class Program
    {
        // Excercise 1

        public static void createPlayers(Player[] players)
        {

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
                players[i].Id = Guid.NewGuid();
                players[i].Items = new List<Item>();
            }
        }
        public static bool checkDuplicates(Player[] players)
        {

            List<Player> sortedPlayers = players.OrderBy(p => p.Id).ToList();

            //Console.WriteLine("Players sorted!");

            for (int i = 0; i < sortedPlayers.Count - 1; i++)
            {
                if (sortedPlayers[i].Id == sortedPlayers[i + 1].Id)
                {
                    return true;
                }
            }

            return false;
        }
        // End of excercise 1

        // Excercise 3
        public static Item[] GetItems(Player p)
        {
            Item[] items = new Item[p.Items.Count];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new Item() { Id = p.Items[i].Id, Level = p.Items[i].Level };
            }

            return items;
        }

        public static Item[] GetItemsWithLinq(Player p)
        {
            return p.Items.ToArray();
        }
        // End of excercise 3

        public static Item GetItem(Player p)
        {
            if (p.Items.Count == 0)
                return null;

            return p.Items[0];
        }

        // Excercise 4
        public static Item GetItemWithLinq(Player p)
        {
            return p.Items.First();
        }
        // End of excercise 4

        // Excercise 5
        public static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach (Item item in player.Items)
            {
                process(item);
            }
        }

        public static void processItem(Item item)
        {
            Console.WriteLine("GUID: " + item.Id + " Level " + item.Level);
        }
        // End of excercise 5


        static void Main(string[] args)
        {
            Player[] players = new Player[1000000];

            Console.WriteLine("======================================================\nExcercise 1.");

            createPlayers(players);

            Console.WriteLine("Players created");

            if (checkDuplicates(players))
            {
                Console.WriteLine("Duplicates found! This is awkward.");
            }
            else
            {
                Console.WriteLine("No duplicates found. This was expected.");
            }

            Console.WriteLine("Excercise 2");

            for (int i = 0; i < 20; i++)        // creating 20 objects and their levels from 20 to 1
            {
                players[0].Items.Add(new Item() { Id = Guid.NewGuid(), Level = 20 - i });
            }


            Console.WriteLine("Highest level item is " + ItemExtender.GetHighestLevelItem(players[0]).Level + "\n");



            Console.WriteLine("Excercise 3 (without LINQ) and 5: Printing list of items returned from GetItems() using delegate");

            Item[] items = GetItems(players[0]);
            ProcessEachItem(players[0], processItem);

            Console.WriteLine();
            Console.WriteLine("Excercise 3 (With LINQ) and 6: Printing list of items returned from GetItemsWithLinq() using lambda expression");

            items = GetItemsWithLinq(players[0]);
            ProcessEachItem(players[0], item => { Console.WriteLine("GUID: " + item.Id + " Level " + item.Level); });

            Console.WriteLine();

            Console.WriteLine("Excercise 4");
            Item item = GetItem(players[0]);
            processItem(item);

            item = GetItemWithLinq(players[0]);
            processItem(item);

            Console.WriteLine("Excercise 7");
            List<Player> newPlayers = new List<Player>();

            for (int i = 0; i < 20; i++)
            {
                newPlayers.Add(new Player { Score = 2 * i });
            }

            Game<Player> game1 = new Game<Player>(newPlayers);

            IPlayer[] top10 = game1.GetTop10Players();

            Console.WriteLine("\nTop 10 scores from game1");
            for (int i = 0; i < top10.Length; i++)
            {
                IPlayer p = top10[i];
                Console.WriteLine((i + 1) + ". " + p.Score);
            }


            List<PlayerForAnotherGame> newPlayersFromAnotherGame = new List<PlayerForAnotherGame>();

            for (int i = 0; i < 20; i++)
            {
                newPlayersFromAnotherGame.Add(new PlayerForAnotherGame { Score = 3 * i });
            }

            Game<PlayerForAnotherGame> game2 = new Game<PlayerForAnotherGame>(newPlayersFromAnotherGame);

            top10 = game2.GetTop10Players();

            Console.WriteLine("\nTop 10 scores from game2");
            for (int i = 0; i < top10.Length; i++)
            {
                IPlayer p = top10[i];
                Console.WriteLine((i + 1) + ". " + p.Score);
            }


        }

    }
}
