namespace HangmanGame
{
    using System.Collections.Generic;
    using static System.Random;
    using System.Text;
    using System.Runtime.CompilerServices;
    using System.Reflection.Metadata.Ecma335;
    using System.Data;

    //NOTA DE AJUSTE: refazer o código melhorando a organização das funções e usando Console.Clear()

    internal class Program
    {
        internal static void printHangman(int wrong)
        {
            if (wrong == 0)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 1)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" 0  |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 2)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" 0  |");
                Console.WriteLine(" |  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 3)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" 0  |");
                Console.WriteLine("/|  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 4)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" 0  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 5)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" 0  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/   |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 6)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" 0  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/ \\ |");
                Console.WriteLine("   ===");
            }
        }
        internal static int printWord(List<char>guessedLetters, String randomWord)
        {

            int counter = 0;
            int rightLetters = 0;

            Console.Write("\r\n");

            foreach (char c in randomWord)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c + " ");
                    rightLetters += 1;
                }
                else
                {
                    Console.Write("  ");
                }
                counter += 1;
            }           
            return rightLetters;
        }

        //overlap word by special charcter
        private static void printLines(String randomWord)
        {
            Console.Write("\r");
            foreach (char c in randomWord)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.Write("\u0305 ");
            }
        }

        private static bool checkVictory(int currentLettersRight, int lengthOfWordToGuess)
        {
            return currentLettersRight == lengthOfWordToGuess;
        }
                             
        

        private static bool IsLetter(char c)
        {
            return char.IsLetter(c);
        }


        static void Main(string[] args)
        {
            string title = @" █░▒█ █▀▀█ █▄░▒█ █▀▀█ █▀▄▀█ █▀▀█ █▄░▒█   █▀▀█ █▀▀█ █▀▄▀█ █▀▀▀
 █▀▀█ █▄▄█ █▒█▒█ █░▄▄ █▒█▒█ █▄▄█ █▒█▒█   █░▄▄ █▄▄█ █▒█▒█ █▀▀▀
 █░▒█ █░▒█ █░░▀█ █▄▄█ █░░▒█ █░▒█ █░░▀█   █▄▄█ █░▒█ █░░▒█ █▄▄▄";
            Console.WriteLine($"{title}\n");

            

            Random random = new Random();

            Dictionary<string, List<string>> themes = new Dictionary<string, List<string>>
            {
                { "Objeto", new List<string> { "abajur", "casa", "armadura", "brinco", "dentadura", "esmalte", "fivela", "incenso", "lamparina" } },
                { "Animal", new List<string> { "cabra", "esquilo", "cachorro", "gato", "vaca", "axolote" } },
                { "Marca", new List<string> { "ferrari", "prada", "rolex", "volvo", "boticario", "renner", "rufles", "goob", "dolly" } },
                { "Cor", new List<string> { "amarelo", "purpura", "marrom", "rosa", "roxo", "carmim" } }
            };

            string[] categories = { "Objeto", "Animal", "Marca", "Cor" };

            string randomCategory = categories[random.Next(categories.Length)];
            List<string> randomTheme = themes[randomCategory];

            int randomWordIndex = random.Next(randomTheme.Count);
            string randomWord = randomTheme[randomWordIndex];          

            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 0;
            int amountOfTimesRight = 0;

            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;

            printHangman(amountOfTimesWrong);

            foreach (char x in randomWord)
            {
                Console.Write("_ ");
            }


            // Tip
            Console.WriteLine($"\r\nO tema da palavra é {randomCategory}!!");


            //MAIN LOOP           
            while (amountOfTimesWrong != 6 && currentLettersRight != lengthOfWordToGuess)
            {               
                
                Console.Write("\r\nLetras que você já tentou: ");
                foreach (char letter in currentLettersGuessed)
                {
                    Console.Write(letter + " ");
                }
                Console.WriteLine($"\r\nAtenção você errou {amountOfTimesWrong}/6 vezes!");


                // Prompt user for input
                char letterGuessed;

                do
                {                    
                    Console.Write("\nAdvinhe uma letra:");
                    letterGuessed = Console.ReadLine().ToLower()[0];                    
                } while (!IsLetter(letterGuessed));

                Console.WriteLine("####################################"); ;

                // Check if that letter has already been guessed
                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    Console.Write("\r\n Você ja tentou essa letra!");
                    printHangman(amountOfTimesWrong);
                    currentLettersGuessed.Add(letterGuessed);
                    currentLettersRight = printWord(currentLettersGuessed, randomWord);
                    Console.Write("\r\n");
                    printLines(randomWord);
                }
                else
                {
                    // Check if letter is in randomWord
                    bool right = false;
                    for (int i = 0; i < randomWord.Length; i++) 
                    {
                        if (letterGuessed == randomWord[i]) 
                        { 
                            right = true; 
                        } 
                    }

                    // User is right
                    if (right)
                    {
                        amountOfTimesRight += 1;
                        printHangman(amountOfTimesWrong);

                        // Print word
                        currentLettersGuessed.Add(letterGuessed);
                        currentLettersRight = printWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        printLines(randomWord);
                    }
                    // User was wrong
                    else
                    {
                        amountOfTimesWrong += 1;
                        currentLettersGuessed.Add(letterGuessed);
                        // Update the drawing
                        printHangman(amountOfTimesWrong);

                        // Print word
                        currentLettersRight = printWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        printLines(randomWord);
                        
                    }                   
                }
                if (checkVictory(currentLettersRight, lengthOfWordToGuess))
                {
                    string victoryMessage = "Você ganhou!";
                    Console.WriteLine($"\r\n{victoryMessage}");
                    break;  // Quit loop, user win
                }
                else if (amountOfTimesWrong == 6)
                {
                    string defeatMessage = "Você perdeu!";
                    Console.WriteLine($"\r\n{defeatMessage}");
                    Console.WriteLine($"\r\nA palavra era {randomWord}");
                    break; // Quit loop, user lost
                }
                
            }

            Console.WriteLine("\r\nO jogo terminou, espero que tenha se divertido!");
        }
            
        
    }
}