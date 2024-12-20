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
            WriteColoredLine("\n====================================", ConsoleColor.Cyan);
            WriteColoredLine("        Welcome to the Text Adventure Game!        ", ConsoleColor.Yellow);
            WriteColoredLine("====================================\n", ConsoleColor.Cyan);
            player.Name = GetValidPlayerName();
            WriteColoredLine($"Greetings, {player.Name}! Your adventure begins now.\n", ConsoleColor.Green);

            // Start game loop
            bool isPlaying = true;
            while (isPlaying) {
                if (player.Health <= 0) {
                    WriteColoredLine("\nYou have perished on your journey. Game over.\n", ConsoleColor.Red);
                    break;
                }

                ShowStatus(player);
                WriteColoredLine("What would you like to do next?", ConsoleColor.Yellow);
                WriteColoredLine("------------------------------------", ConsoleColor.Cyan);
                WriteColoredLine("1. Explore the forest", ConsoleColor.Gray);
                WriteColoredLine("2. Visit the village", ConsoleColor.Gray);
                WriteColoredLine("3. Rest", ConsoleColor.Gray);
                WriteColoredLine("4. Quit", ConsoleColor.Gray);
                WriteColoredLine("------------------------------------", ConsoleColor.Cyan);

                Console.Write("Your choice: ");
                string choice = Console.ReadLine() ?? string.Empty;
                Console.WriteLine();
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
                    WriteColoredLine("Name cannot be empty. Please try again.", ConsoleColor.Red);
                }
            } while (string.IsNullOrWhiteSpace(name));

            return name;
        }

        // Function to show player status
        static void ShowStatus(Player player) {
            WriteColoredLine($"\n====================================", ConsoleColor.Cyan);
            WriteColoredLine($"{player.Name}'s Status:", ConsoleColor.Yellow);
            WriteColoredLine($"Health: {player.Health}", ConsoleColor.Green);
            WriteColoredLine($"Gold: {player.Gold}", ConsoleColor.Yellow);
            WriteColoredLine($"====================================\n", ConsoleColor.Cyan);
        }

        // Function for exploring the forest
        static Player ExploreForest(Player player) {
            WriteColoredLine("You venture into the forest...", ConsoleColor.Gray);

            int encounter = RandomInstance.Next(1, 4);
            switch (encounter) {
                case 1:
                    WriteColoredLine("You find a treasure chest with 50 gold!", ConsoleColor.Green);
                    player.Gold += 50;
                    break;
                case 2:
                    WriteColoredLine("A wild wolf attacks you! You lose 20 health.", ConsoleColor.Red);
                    player.Health -= 20;
                    break;
                case 3:
                    WriteColoredLine("The forest is calm. Nothing happens.", ConsoleColor.Gray);
                    break;
            }

            Console.WriteLine();
            return player;
        }

        // Function for visiting the village
        static Player VisitVillage(Player player) {
            WriteColoredLine("You arrive at a small village.", ConsoleColor.Gray);
            WriteColoredLine("A merchant offers you a health potion for 30 gold.", ConsoleColor.Yellow);

            if (player.Gold >= 30) {
                Console.Write("Do you want to buy the potion? (yes/no): ");
                string choice = (Console.ReadLine() ?? "").ToLower();
                if (choice == "yes") {
                    WriteColoredLine("You buy the potion and restore 20 health.", ConsoleColor.Green);
                    player.Gold -= 30;
                    player.Health = Math.Min(player.Health + 20, 100);
                } else {
                    WriteColoredLine("You decline the offer and leave the village.", ConsoleColor.Gray);
                }
            } else {
                WriteColoredLine("You don't have enough gold to buy the potion.", ConsoleColor.Red);
            }

            Console.WriteLine();
            return player;
        }

        // Function for resting
        static Player Rest(Player player) {
            WriteColoredLine("You take a rest and restore 10 health.", ConsoleColor.Green);
            player.Health = Math.Min(player.Health + 10, 100);
            Console.WriteLine();
            return player;
        }

        // Function to display an error message
        static void DisplayErrorMessage() {
            WriteColoredLine("Invalid choice. Please enter a valid number between 1 and 4.\n", ConsoleColor.Red);
        }

        // Function to end the game
        static void EndGame() {
            WriteColoredLine("\nThanks for playing! Goodbye.", ConsoleColor.Yellow);
        }

        // Helper function to write colored text
        static void WriteColoredLine(string text, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
