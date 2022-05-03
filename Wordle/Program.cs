using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWordle
{
    class Program
    {
        static HashSet<string> words = new HashSet<string>();
        static int guesses = 6;
        static bool firstGuess = true;
        static void Main(string[] args)
        {
            //Load Word List
            string[] lines = System.IO.File.ReadAllLines(@"../../WordList.txt");
            foreach (string s in lines)
            {
                if (s.Length == 5)
                {
                    words.Add(s);
                }
            }

            //Generate Random Word
            Random rand = new Random();
            string word = words.ElementAt(rand.Next(words.Count));

            //Introduction
            Console.WriteLine("Welcome to Wordle. You will be guess a 5-letter word.");
            Console.WriteLine("-----------------------------------------------------");

            //Start Wordle Game

            Console.WriteLine("You may begin entering 5-letter words.\n");
            bool cont = true;
            while (cont)
            {
                bool end = false;
                while (!end)
                {
                    end = ProcessGuess(Console.ReadLine(), word);
                }
                bool response = false;
                while (!response)
                {
                    string temp = Console.ReadLine();
                    if (temp == "Y")
                    {
                        word = words.ElementAt(rand.Next(words.Count));
                        for (int i = 0; i < 10 - guesses; i++)
                        {
                            ClearLine();
                        }
                        guesses = 6;
                        firstGuess = true;
                        response = true;
                    }
                    else if (temp == "N")
                    {
                        cont = false;
                        response = true;
                    }
                    else
                    {
                        ClearLine();
                    }
                }
            }
        }

        static bool ProcessGuess(string guess, string word)
        {
            //Checking Valid Guess
            if (guess.Length != 5 || !words.Contains(guess))
            {
                ClearLine();
                return false;
            }
            else
            {
                if (firstGuess)
                {
                    ClearLine();
                    firstGuess = false;
                }
                else
                {
                    ClearLine();
                    ClearLine();
                }
                guesses--;
                int res = WriteWord(guess, word);
                if (res == 5)
                {
                    ColorMsg("\nCONGRAULATIONS!!! You've guessed the correct word.\n", ConsoleColor.Green);
                    ColorMsg("Play Again? (Y/N)\n", ConsoleColor.Cyan);
                    return true;
                }
                if (guesses == 0)
                {
                    ColorMsg("\nYOU LOSE!!! The word was " + word + ", try again next time.\n", ConsoleColor.Red);
                    ColorMsg("Play Again? (Y/N)\n", ConsoleColor.Cyan);
                    return true;
                }
                Console.WriteLine("You have " + guesses + " guesses remaining.");
                return false;
            }
        }

        static int WriteWord(string guess, string word)
        {
            int sum = 0;
            char[] guessLetters = guess.ToCharArray();
            char[] wordLetters = word.ToCharArray();

            for (int i = 0; i < guessLetters.Length; i++)
            {
                if (word.Contains(guessLetters[i]))
                {
                    if (guessLetters[i] == wordLetters[i])
                    {
                        sum++;
                        ColorMsg(guessLetters[i].ToString(), ConsoleColor.Green);
                    }
                    else
                    {
                        ColorMsg(guessLetters[i].ToString(), ConsoleColor.Yellow);
                    }
                }
                else
                {
                    Console.Write(guessLetters[i]);
                }
            }
            Console.Write("\n");

            return sum;
        }

        static void ColorMsg(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
