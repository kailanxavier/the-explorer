using System;
using System.Collections.Generic;

namespace TextAdventureGame {
    class Program {
        // Player structure to hold player data
        struct Player {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Gold { get; set; }
        }

        // Class-level Random instance
        private static readonly Random RandomInstance = new Random();

        // Main function
        static void Main(string[] args) {
            // Initialize player
            Player player = new Player { Name = "", Health = 100, Gold = 0 };

            // Game introduction
            Console.WriteLine("Welcome to the Text Adventure Game!");
            player.Name = GetValidPlayerName();
            Console.WriteLine($"Greetings, {player.Name}! Your adventure begins now.\n");

            // Start game loop
            bool isPlaying = true;
            while (isPlaying) {
                if (player.Health <= 0) {
                    Console.WriteLine("You have perished on your journey. Game over.");
                    break;
                }

                ShowStatus(player);
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Explore the forest");
                Console.WriteLine("2. Visit the village");
                Console.WriteLine("3. Rest");
                Console.WriteLine("4. Quit");

                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice) {
                    case "1":
                        player = ExploreForest(player);
                        break;
                    case "2":
                        player = VisitVillage(player);
                        break;
                    case "3":
                        player = Rest(player);
                        break;
                    case "4":
                        EndGame();
                        isPlaying = false;
                        break;
                    default:
                        DisplayErrorMessage();
                        break;
                }
            }
        }

        // Function to get a valid player name
        static string GetValidPlayerName() {
            string name;
            do {
                Console.Write("Enter your name: ");
                name = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(name)) {
                    Console.WriteLine("Name cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(name));

            return name;
        }

        // Function to show player status
        static void ShowStatus(Player player) {
            Console.WriteLine($"\n{player.Name}'s Status: Health = {player.Health}, Gold = {player.Gold}\n");
        }

        // Function for exploring the forest
        static Player ExploreForest(Player player) {
            Console.WriteLine("You venture into the forest...");

            int encounter = RandomInstance.Next(1, 4);
            switch (encounter) {
                case 1:
                    Console.WriteLine("You find a treasure chest with 50 gold!");
                    player.Gold += 50;
                    break;
                case 2:
                    Console.WriteLine("A wild wolf attacks you! You lose 20 health.");
                    player.Health -= 20;
                    break;
                case 3:
                    Console.WriteLine("The forest is calm. Nothing happens.");
                    break;
            }

            return player;
        }

        // Function for visiting the village
        static Player VisitVillage(Player player) {
            Console.WriteLine("You arrive at a small village.");
            Console.WriteLine("A merchant offers you a health potion for 30 gold.");

            if (player.Gold >= 30) {
                Console.Write("Do you want to buy the potion? (yes/no): ");
                string choice = (Console.ReadLine() ?? "").ToLower();
                if (choice == "yes") {
                    Console.WriteLine("You buy the potion and restore 20 health.");
                    player.Gold -= 30;
                    player.Health = Math.Min(player.Health + 20, 100);
                } else {
                    Console.WriteLine("You decline the offer and leave the village.");
                }
            } else {
                Console.WriteLine("You don't have enough gold to buy the potion.");
            }

            return player;
        }

        // Function for resting
        static Player Rest(Player player) {
            Console.WriteLine("You take a rest and restore 10 health.");
            player.Health = Math.Min(player.Health + 10, 100);
            return player;
        }

        // Function to display an error message
        static void DisplayErrorMessage() {
            Console.WriteLine("Invalid choice. Try again.\n");
        }

        // Function to end the game
        static void EndGame() {
            Console.WriteLine("Thanks for playing! Goodbye.");
        }
    }
}
