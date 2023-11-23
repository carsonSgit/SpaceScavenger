﻿using System;
using System.Linq;

namespace SpriggsProject
{
    class Program
    {
        static string UserSymbol = "-";
        static string DelDupe = " ";
        static int UserX = 60, UserY = 13, MeSpeed = 1, OldX, OldY, TargetX, TargetY, spaceDebrisX, spaceDebrisY;
        static int NumCrystal = 1;
        static int keyA, keyW, keyS, keyD;
        static bool gameover = false;
        static int score;
        static int[] keystrokes = new int[4];
        static void Main(string[] args)
        {
            // Opens validation to let Menu work
            bool displayMenu = true;
            while (displayMenu)
            {
                displayMenu = Menu();
            }
            // Closes menu validation
        }
        static bool Menu()
        {
            Console.Clear();
            string choice;
            gameover = false;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.CursorVisible = false;

            Console.WriteLine
                (
                "\n Welcome to Space-Harvest" +
                "\n\n Enter 1. To Play" +
                "\n\n Enter 2. To Read the rules" +
                "\n\n Enter 3. To Exit"
                );
            choice = Console.ReadLine();
            if (choice == "1" || choice == "2" || choice == "3")
            {
            }
            else
            {
                Console.WriteLine("Error: Misinput, please input a listed variable (1, 2, 3)");
                choice = Console.ReadLine();
            }
            Console.Clear();
            switch (choice) // Calls to functions accordingly
            {
                case "1":
                    mainGame();
                    return true;
                case "2":
                    rules();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }
        /* +++++++++++++++++++++++++++++++++++++++++++
           +        Beginning of game loop           +
           +++++++++++++++++++++++++++++++++++++++++++ */
        static void mainGame()
        {
            int wt = 0;
            int spaceDebris_create = 20000;
            int end_game = 250000;
            while (!gameover)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (Console.KeyAvailable)
                {
                    readUserKey();
                }
                print();
                checkCrystal();
                wt++;
                if (wt % spaceDebris_create == 0)
                {
                    spaceDebris();
                }
                if (wt % end_game == 0)
                {
                    gameover = true;
                }
            }
            keyArrayPopulate();
            Console.Clear();
            Console.WriteLine("Game over. Hit enter to reach end-screen");
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n                 Congratulations! You got a score of " + score + "!  \n                      Enter a key to start again");
            Console.WriteLine("\n   You hit W key {0} times, A key {1} times, S key {2} times, and D key {3} times", keystrokes[0], keystrokes[1], keystrokes[2], keystrokes[3]);
            Console.ReadLine();
        }
        static void readUserKey() // Reads user keys and moves accordingly
        {
            OldX = UserX;
            OldY = UserY;
            ConsoleKey k;
            k = Console.ReadKey(true).Key;
            if (k == ConsoleKey.W)
            {
                UserY = UserY - MeSpeed;
                keyW++;
            }
            if (k == ConsoleKey.S)
            {
                UserY = UserY + MeSpeed;
                keyS++;
            }
            if (k == ConsoleKey.A)
            {
                UserX = UserX - MeSpeed;
                keyA++;
            }
            if (k == ConsoleKey.D)
            {
                UserX = UserX + MeSpeed;
                keyD++;
            }
            if (UserX < 0 || UserX > Console.WindowWidth - 2) // Resets UserX value to the center of screen if they pass border limit
            {
                UserX = 60;
            }
            if (UserY < 0 || UserY > Console.WindowHeight - 2) // Resets UserY value to the center of screen if they pass border limit
            {
                UserY = 13;
            }
            if (k == ConsoleKey.Escape)
            {
                gameover = true;
            }
            if (k == ConsoleKey.O)
            {
                spawnCrystal();
            }
        }
        static void keyArrayPopulate() // Pouplates array to hold keystrokes
        {
            keystrokes[0] = keyA;
            keystrokes[1] = keyW;
            keystrokes[2] = keyS;
            keystrokes[3] = keyD;
        }
        static void print() // Displays user on screen and deletes trace
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(UserX, UserY);
            Console.WriteLine(UserSymbol);
            Console.SetCursorPosition(OldX, OldY);
            Console.WriteLine(DelDupe);
        }
        static void checkCrystal() // Calls to coordCompare and if is true, gives user + 10 score
        {
            bool collisionVerify, test = true;
            collisionVerify = coordCompare(test);
            if (collisionVerify == true)
            {
                score = score + 10;
            }
        }
        public static bool coordCompare(bool a) // Checks if User collected a crystal
        {

            if (UserX == TargetX && UserY == TargetY)
            {
                a = true;
                TargetX = -1;
                TargetY = -1;
            }
            else
            {
                a = false;
            }
            return a;
        }
        static void spawnCrystal() // Spawns crystals
        {
            for (int i = 0; i < NumCrystal; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Random rand = new Random();
                TargetX = rand.Next(1, Console.WindowWidth - 2);
                TargetY = rand.Next(1, Console.WindowHeight - 2);
                Console.SetCursorPosition(TargetX, TargetY);
                Console.WriteLine("v");
            }
        }
        static void spaceDebris() // Spawns space-debris
        {
            for (int i = 0; i < NumCrystal; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Random rand = new Random();
                spaceDebrisX = rand.Next(1, Console.WindowWidth - 2);
                spaceDebrisY = rand.Next(1, Console.WindowHeight - 2);
                Console.SetCursorPosition(spaceDebrisX, spaceDebrisY);
                Console.WriteLine("x");
            }
        }
        /* +++++++++++++++++++++++++++++++++++++++++
           +            End of game loop           +
           +++++++++++++++++++++++++++++++++++++++++ */
        static void rules() // Rules and instructions
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to the rules and instructions! Please read carefully");
            Console.WriteLine(
                "\n 1. Movement keys are the following;" +
                "\n     -W to move up by 1" +
                "\n     -A to move left by 1" +
                "\n     -S to move down by 1" +
                "\n     -D to move right by 1\n" +
                "\n 2. Hit 'O' to spawn in a crystal, do one at a time because only one counts at a time.\n Move over them to collect them and you will gain 10 score each" +
                "\n     -Once you collect a crystal, hit 'O' again to spawn in another one!\n" +
                "\n 3. Blue 'X's will spawn, these are distractions (Space-Debris). Walking over them does nothing,\n so don't waste your time on them!\n" +
                "\n 4. If you want to exit before the game ends, hit the Escape key\n" +
                "\n 5. The game runs on a timer, collect as many crystals as you can before the time runs out!" +
                "\n\n Hit any key to return to menu"
                );
            Console.ReadKey();
        }
    }
}