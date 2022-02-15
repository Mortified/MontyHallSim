using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallSimulation
{
    class Program
    {
        static Random rnd = new Random();
        static string[] GenDoors(int numDoors)
        {
            string[] doors = new string[numDoors];
            for (int i = 0; i < numDoors; i++)
            {
                doors[i] = "Goat";
            }
            doors[rnd.Next(doors.Length)] = "Car";
            return doors;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Monty Hall Parallel Universes!");

            // Read in custom simulation test parameters
            Console.WriteLine("How many Doors?");
            int numDoors = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many Cycles?");
            int cycles = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Switch or Stay?");
            string choice = Console.ReadLine();

            // Set up standard parameters
            double wins = 0;
            double games = 0;
            string[] montyDoors;

            if ("Stay" == choice || "stay" == choice)
            {
                while (games < cycles)
                {
                    // Player randomly selects a door and does not swap, move directly to checking for car, do not simulate Monty's selection as it is irrelevant  
                    montyDoors = GenDoors(numDoors);
                    if (montyDoors[rnd.Next(montyDoors.Length)] == "Car")
                        wins++;
                    games++;
                }
                Console.WriteLine("Staying Won: " + wins + " times out of " + games);
                Console.WriteLine("Rendering a win rate of: " + ((100 / games) * wins) + "%");
            }
            else if ("Switch" == choice || "switch" == choice)
            {
                while (games < cycles)
                {
                    //Set up the players initial choice and the door layout
                    montyDoors = GenDoors(numDoors);
                    int playerChoice = rnd.Next(montyDoors.Length);
                    int montyChoice = 0;

                    // Simulate Monty's selection
                    for (int i = 1; i < numDoors; i++)
                    {
                        // Monty cannot take the player's choice, move to next door if the current selection is theirs
                        if (i != playerChoice)
                        {
                            // If the current unselected door is a car monty will select it so it won't be removed from play
                            // If the player has the car already Monty will pick the first door which isn't the player's
                            if (montyDoors[i] == "Car")
                            {
                                montyChoice = i;
                                break;
                            }
                            else if (montyDoors[playerChoice] == "Car")
                            {
                                montyChoice = i;
                                break;
                            }
                        }
                    }
                    // Player switches his choice for Monty's
                    playerChoice = montyChoice;
                    if (montyDoors[playerChoice] == "Car")
                        wins++;
                    games++;
                }

                Console.WriteLine("Switching Won: " + wins + " times out of " + games);
                Console.WriteLine("Rendering a win rate of: " + ((100 / games) * wins) + "%");
            }
            else
            {
                Console.WriteLine("Invalid input...");
            }
            Console.ReadKey();
        }
    }
}