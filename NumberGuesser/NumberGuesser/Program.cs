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
            msgWait("- Weclome to the number guesser.", ConsoleColor.Magenta, 1500);
            msgWait("- When asked, you will need to input a number. After guessing, I will inform you" +
                " whether your number is higher than, lower than, or the same as the correct number.", ConsoleColor.Magenta, 6000);
            msgWait("- You will have a limited number of guesses. If you do not find the right number" +
                " within the alotted guesses, you lose the game.", ConsoleColor.Magenta, 6000);
            msgWait("- Good Luck!\n", ConsoleColor.Magenta, 1500);

            //Start of game and generate guessing range and number of guesses
            Game game = new Game();
            msgWait("- Your number will fall between " + game.low + " and " + game.high + ". You will have " + game.numGuess + " guesses.\n", ConsoleColor.Cyan, 3000);

            //Start gameplay until player is overall finished playing
            bool end = false;
            int guess;
            while (!end)
            {
                msgWait("- Please guess a number.", ConsoleColor.Cyan, 0);
                bool res = Int32.TryParse(Console.ReadLine(), out guess) && guess >= game.low && guess <= game.high;
                while (!res)
                {
                    msgWait("- Please guess a valid number within the game's range.", ConsoleColor.Cyan, 0);
                    res = Int32.TryParse(Console.ReadLine(), out guess) && guess >= game.low && guess <= +game.high;
                }
                game.guess();

                if (game.numGuess > 0)
                {
                    if (guess > game.correct)
                    {
                        msgWait("The correct number is lower than your guess. You have " + game.numGuess + " guesses remaining.\n", ConsoleColor.Blue, 0);
                    }
                    else if (guess < game.correct)
                    {
                        msgWait("The correct number is higher than your guess. You have " + game.numGuess + " guesses remaining.\n", ConsoleColor.Yellow, 0);
                    }
                    else
                    {
                        msgWait("- CONGRATULATIONS!!!! You guessed the right number and win with " + game.numGuess + " guesses remaining!", ConsoleColor.Green, 8000);
                        end = true;
                    }
                }
                else
                {
                    if (guess == game.correct)
                    {
                        msgWait("- NO WAY!!!! You guessed the right number on your final guess!", ConsoleColor.Green, 8000);
                        end = true;
                    }
                    else
                    {
                        msgWait("- Sorry but you've run out of guesses before finding the correct number. Try again next time.", ConsoleColor.Red, 8000);
                        end = true;
                    }
                }
            }
        }

        static void msgWait(string message, ConsoleColor color, int time)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            System.Threading.Thread.Sleep(time);
        }

        private class Game
        {
            public int low;
            public int high;
            public int numGuess;
            public int correct;

            public Game()
            {
                Random rand = new Random();
                this.low = rand.Next(1, 1000000001);
                this.high = rand.Next(1, 1000000001);
                if (this.high < this.low)
                {
                    int temp = high;
                    this.high = this.low;
                    this.low = temp;
                }
                this.numGuess = 3 + (int)Math.Ceiling(Math.Log((this.high - this.low), 2));
                this.correct = rand.Next(this.low, this.high + 1);
            }

            public void guess()
            {
                this.numGuess--;
            }
        }
    }
}
