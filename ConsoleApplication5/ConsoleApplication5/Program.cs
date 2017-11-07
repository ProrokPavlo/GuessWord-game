using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman_2
{
    class Program
    {
        static int wrongGuess, lettersLeft;

        static void Main(string[] args)
        {
            string wordToGuess = GetWordToGuess();

            char[] maskedWord = GetHiddenLetters(wordToGuess, '-');

            lettersLeft = wordToGuess.Length;
            char userGuess;

            wrongGuess = 3;

            while (wrongGuess > 0 && lettersLeft > 0)
            {
                DisplayCharacters(maskedWord);

                Console.WriteLine("Enter a letter?");
                userGuess = char.Parse(Console.ReadLine());

                maskedWord = CheckGuess(userGuess, wordToGuess, maskedWord);
            }

            Console.WriteLine("Well done! Thanks for playing.");
            Console.ReadLine();
        }

        static string GetWordToGuess()
        {
            Random number = new Random();
            int wordNumber = number.Next(0, 9);

            string[] words = { "hyperbaric", "temporal", "quantum", "radiation", "aardvark", "accident", "dracolich", "professional", "properties", "collections" };

            string selectWord = words[wordNumber];
            return selectWord;
        }

        static char[] GetHiddenLetters(string word, char mask)
        {
            char[] hidden = new char[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                hidden[i] = mask;
            }

            return hidden;
        }

        static void DisplayCharacters(char[] characters)
        {
            foreach (char letter in characters)
            {
                Console.Write(letter);
            }
            Console.WriteLine();
        }

        static char[] CheckGuess(char letterToCheck, string word, char[] characters)
        {
            if (wrongGuess > 0)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == letterToCheck)
                    {
                        characters[i] = word[i];
                        lettersLeft--;
                    }
                }
            }
            else
            {
                wrongGuess--;
            }

            return characters;
        }
    }
}