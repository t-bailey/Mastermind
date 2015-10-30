/* Title: Mastermind Game
 * Autor: Tyler Bailey
 * Last Updated: 10/29/15
 * Description: This application will let the user play
 *              a console version of Mastermind.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMastermindGame
{
    class Program
    {
        const int MAX_NUMBER = 6666;
        const int MIN_NUMBER = 1111;
        const int MAX_INDIV_NUMBER = 6;
        const int MIN_INDIV_NUMBER = 1;


        static void Main(string[] args)
        {
            //Loop necessary to play multiple games
            while (true)
            {
                //Intro Sequence & Game Setup
                Console.WriteLine("Welcome to Mastermind!");
                System.Threading.Thread.Sleep(2000);

                int intGuesses = NumGuesses();
                int intSecretCode = GenerateSecretCode();

                bool winState = false;

                bool[] guessAry = { false, false, false, false };
                bool[] answerAry = { false, false, false, false };
                Console.Clear();

                //Guesses Loop
                while (intGuesses > 0)
                {
                    Console.WriteLine("Guesses Remaining: " + intGuesses.ToString());

                    Console.WriteLine("\nMake your guess:\n");
                    int intUserGuess = 0;
                    string strUserGuess = Console.ReadLine();


                    if (isGuessCorrectFormat(ref strUserGuess, intSecretCode))
                    {
                        intUserGuess = Int32.Parse(strUserGuess);

                        if (intUserGuess == intSecretCode) //Game has been won.
                        {
                            winState = true;
                            break;
                        }

                        int inPlaceCount = getInPlaceCount(intUserGuess, guessAry, answerAry, intSecretCode);
                        int outOfPlaceCount = getOutOfPlaceCount(intUserGuess, guessAry, answerAry, intSecretCode);
                        
                        string strFeedback = "\nScore: ";

                        //Switch statement builds feedback string.
                        #region Switch statement w/cases
                        switch (inPlaceCount)
                        {
                            case 0:
                                break;
                            case 1:
                                strFeedback += "+";
                                break;
                            case 2:
                                strFeedback += "++";
                                break;
                            case 3:
                                strFeedback += "+++";
                                break;
                        }
                        switch (outOfPlaceCount)
                        {
                            case 0:
                                break;
                            case 1:
                                strFeedback += "-";
                                break;
                            case 2:
                                strFeedback += "--";
                                break;
                            case 3:
                                strFeedback += "---";
                                break;
                            case 4:
                                strFeedback += "----";
                                break;
                        }
                        #endregion

                        Console.WriteLine(strFeedback + "\n");
                        Console.WriteLine("--------------------\n");
                        intGuesses--;
                    }
                    else
                        Console.WriteLine("Make sure your input is between 1111 and 6666, with each digit being no larger that 6.");
                }
                if (winState)
                {
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("\nYou solved it!");
                }
                else
                {
                    Console.WriteLine("\nYou lose. :(\n");
                    Console.WriteLine("The code was " + intSecretCode);
                }
                if (EndGameDisplay())
                {
                    Console.Clear();
                    continue;
                }
                break;
            }
        }
        #region Functions

        /// <summary>
        /// Displays the End Game screen.
        /// </summary>
        /// <returns>True if the user wishes to play again, false otherwise</returns>
        private static bool EndGameDisplay()
        {
            Console.WriteLine("\nWould you like to play again? (Y/N)\n");
            while (true)
            {
                string strPlayAgain = Console.ReadLine();
                if (strPlayAgain == "N" || strPlayAgain == "n" || strPlayAgain == "No" || strPlayAgain == "no")
                {
                    return false;
                }
                else if (strPlayAgain == "Y" || strPlayAgain == "y" || strPlayAgain == "Yes" || strPlayAgain == "yes")
                {
                    return true;
                }
                Console.WriteLine("\nPlease enter either a Y or a N.\n");
            }
        }

        /// <summary>
        /// Calculates the number of digits that are correct and in place.
        /// </summary>
        /// <param name="intUserGuess">The input provided by the user entered guess</param>
        /// <param name="guessAry">The array that determines if the user guess digits are in the correct place.</param>
        /// <param name="answerAry"></param>
        /// <param name="intSecretCode">The secret code.</param>
        /// <returns>The number of in place digits.</returns>
        private static int getInPlaceCount(int intUserGuess, bool[] guessAry, bool[] answerAry, int intSecretCode)
        {
            for (int i = 0; i < 4; i++)
            {
                guessAry[i] = false;
                answerAry[i] = false;
            }

            int inPlaceCount = 0;
            int guessDigit = 0;
            int randDigit = 0;
            int tempGuess = intUserGuess;
            int tempRand = intSecretCode;

            for (int i = 0; i < 4; i++)
            {
                guessDigit = tempGuess % 10;
                tempGuess = tempGuess / 10;
                randDigit = tempRand % 10;
                tempRand = tempRand / 10;

                if (guessDigit == randDigit)
                {
                    guessAry[i] = true;
                    answerAry[i] = true;
                    inPlaceCount++;
                }
            }
            return inPlaceCount;
        }

        /// <summary>
        /// Calulates the number of digits that are correct, but out of place
        /// </summary>
        /// <param name="intUserGuess">The input provided by the user entered guess</param>
        /// <param name="guessAry">The array that determines if the user guess digits are in the correct place.</param>
        /// <param name="answerAry"></param>
        /// <param name="intSecretCode">The secret code.</param>
        /// <returns>The number of out of place digits</returns>
        public static int getOutOfPlaceCount(int userGuess, bool[] guessAry, bool[] answerAry, int intSecretCode)
        {
            int outOfPlaceCount = 0;
            int guessDigit;
            int randDigit;
            int tempRand = intSecretCode;

            for(int i = 0; i < 4; i++)
            {
                guessDigit = userGuess % 10;
                userGuess = userGuess / 10;
                randDigit = tempRand % 10;
                tempRand = intSecretCode;
                if (guessAry[i] == false)
                {
                    for(int j = 0; j < 4; j++)
                    {
                        randDigit = tempRand %10;
                        tempRand = tempRand / 10;
                        if(answerAry[j] == false)
                        {
                            if (guessDigit == randDigit)
                            {
                                outOfPlaceCount++;
                                guessAry[i] = true;
                                answerAry[j] = true;
                                break;
                            }
                        }
                    }
                }
            }
            return outOfPlaceCount;
        }

        /// <summary>
        /// Checks to see if the user guess is correct.
        /// </summary>
        /// <param name="strUserGuess">The string representation of the user entered guess, passed by reference so that the string is updated in Main.</param>
        /// <param name="intSecretCode">The randomly generated secret code.</param>
        /// <returns>True, if guess is correct, False otherwise</returns>
        private static bool isGuessCorrectFormat(ref string strUserGuess, int intSecretCode)
        {
            int intUserGuess = 0;
            try
            {
                intUserGuess = Int32.Parse(strUserGuess);
                int guessDigit = 0;
                int tempGuess = intUserGuess;
                for (int i = 0; i < 4; i++)
                {
                    guessDigit = tempGuess % 10;
                    if (guessDigit > MAX_INDIV_NUMBER || guessDigit < MIN_INDIV_NUMBER || intUserGuess < MIN_NUMBER || intUserGuess > MAX_NUMBER)
                    {
                        throw( new Exception());
                    }
                }
            }
            catch
            {
                Console.WriteLine("\nMake sure your input is between 1111 and 6666, with each digit being no larger that 6.");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("\nMake your guess:\n");
                strUserGuess = Console.ReadLine();
                if (isGuessCorrectFormat(ref strUserGuess, intSecretCode))
                    return true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Generates a code from 1111 to 6666
        /// </summary>
        /// <returns>An int from 1111 to 6666</returns>
        private static int GenerateSecretCode()
        {
            string strCraftedNum ="";
            Random srand = new Random((Int32)DateTime.Now.Ticks);
            for ( int i = 0; i<4; i++)
            {
                strCraftedNum = strCraftedNum.Insert(strCraftedNum.Length, srand.Next(MIN_INDIV_NUMBER, MAX_INDIV_NUMBER).ToString());
            }
            return Int32.Parse(strCraftedNum);
        }

        /// <summary>
        /// Recursive function for input of number of Guesses
        /// </summary>
        /// <returns>The Number of Guesses</returns>
        private static int NumGuesses()
        {
            Console.Clear();
            Console.WriteLine("How many guesses would you like to have?\n");
            int intGuesses = 0;
            try
            {
                intGuesses = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("\nNumber of guesses must be an integer for me to understand.\n");
                System.Threading.Thread.Sleep(2000);
                intGuesses = NumGuesses();
            }
            return intGuesses;
        }

        #endregion
    }
}
