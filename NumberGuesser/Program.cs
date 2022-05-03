using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            //Introduction
            msgWait("Weclome to the number guesser.\n", ConsoleColor.Magenta, 1500);
            msgWait("When asked, you will need to input a number. After guessing, I will inform you" +
                " whether your number is higher than, lower than, or the same as the correct number.\n", ConsoleColor.Magenta, 6000);
            msgWait("You will have a limited number of guesses. If you do not find the right number" +
                " within the alotted guesses, you lose the game.\n", ConsoleColor.Magenta, 6000);
            msgWait("Good Luck!", ConsoleColor.Magenta, 1500);
            Console.Clear();

            //Start of game and generate guessing range and number of guesses
            int lower, upper;
            getBounds(out lower, out upper);

            Game game = new Game(lower, upper);
            msgWait("Your number will fall between " + game.low + " and " + game.high + ". You will have " + game.numGuess + " guesses.\n", ConsoleColor.Cyan, 3000);

            //Start gameplay until player is overall finished playing
            bool end = false;
            while (!end)
            {
                end = processGuess(game);
            }
        }

        static void msgWait(string message, ConsoleColor color, int time)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            System.Threading.Thread.Sleep(time);
        }

        static void getBounds(out int lower, out int upper)
        {
            msgWait("- Please enter a lower bound for your guesses.", ConsoleColor.Cyan, 3000);
            bool valid = false;
            valid = Int32.TryParse(Console.ReadLine(), out lower);
            while (!valid)
            {
                msgWait("- Try again, please enter a valid whole number.", ConsoleColor.Cyan, 3000);
                valid = Int32.TryParse(Console.ReadLine(), out lower);
            }
            Console.Clear();
            msgWait("- Please enter an upper bound for your guesses.", ConsoleColor.Cyan, 3000);
            valid = Int32.TryParse(Console.ReadLine(), out upper);
            while (!valid)
            {
                msgWait("- Try again, please enter a valid whole number.", ConsoleColor.Cyan, 3000);
                valid = Int32.TryParse(Console.ReadLine(), out upper);
            }
            if (upper < lower)
            {
                int temp = upper;
                upper = lower;
                lower = temp;
            }
            Console.Clear();
        }

        static bool processGuess(Game game)
        {
            int guess;
            msgWait("Please guess a number.", ConsoleColor.Cyan, 0);
            bool res = Int32.TryParse(Console.ReadLine(), out guess) && guess >= game.low && guess <= game.high;
            while (!res)
            {
                msgWait("Please guess a valid number within the game's range.", ConsoleColor.Cyan, 0);
                res = Int32.TryParse(Console.ReadLine(), out guess) && guess >= game.low && guess <= +game.high;
            }
            game.guess();

            if (game.numGuess > 0)
            {
                if (guess > game.correct)
                {
                    msgWait("The correct number is lower than " + guess + ". You have " + game.numGuess + " guesses remaining.\n", ConsoleColor.Blue, 0);
                    return false;
                }
                else if (guess < game.correct)
                {
                    msgWait("The correct number is higher than " + guess + ". You have " + game.numGuess + " guesses remaining.\n", ConsoleColor.Yellow, 0);
                    return false;
                }
                else
                {
                    msgWait("CONGRATULATIONS!!!! You guessed the right number and win with " + game.numGuess + " guesses remaining!", ConsoleColor.Green, 8000);
                    return true;
                }
            }
            else
            {
                if (guess == game.correct)
                {
                    msgWait("NO WAY!!!! You guessed the right number on your final guess!", ConsoleColor.Green, 8000);
                    return true;
                }
                else
                {
                    msgWait("Sorry but you've run out of guesses before finding the correct number. Try again next time.", ConsoleColor.Red, 8000);
                    return true;
                }
            }
        }

        private class Game
        {
            public int low;
            public int high;
            public int numGuess;
            public int correct;

            public Game(int lower, int upper)
            {
                Random rand = new Random();
                this.low = rand.Next(lower, upper + 1);
                this.high = rand.Next(lower, upper + 1);
                if (this.high < this.low)
                {
                    int temp = high;
                    this.high = this.low;
                    this.low = temp;
                }
                this.numGuess = 1 + (int)Math.Ceiling(Math.Log((this.high - this.low), 2));
                this.correct = rand.Next(this.low, this.high + 1);
            }

            public void guess()
            {
                this.numGuess--;
            }
        }
    }
}
