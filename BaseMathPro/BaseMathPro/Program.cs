using System;
using System.Security.Cryptography.X509Certificates;

namespace BaseMathPro
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            /* Project Description
             * To create a number base numeric system that :
             - convert numbers from other bases to base 10
             - convert numbers from base 10 to other bases 
             - convert from one base to another bases
             - Perform basic Arithmetic Operations on number bases
             - working with bases 2 to 16
             */

            Console.WriteLine("\n\tWelcome to BaseMathPro!\n");

            while (true)
            {
                Console.WriteLine("\tChoose an operation:\n\tNBC - Number Base Conversion\n\tBMO - Base Math Operation");

                string userChoice = Console.ReadLine();

                switch (userChoice.ToLower())
                {
                    case "nbc":
                        RunNumberBaseConversion();
                        return; // Exit the loop if a valid choice is made
                    case "bmo":
                        RunBaseMathOperation();
                        return; // Exit the loop if a valid choice is made
                    default:
                        Console.WriteLine("Invalid choice. Please enter 'NBC' or 'BMO'.\n");
                        break;
                }
            }
        }
        public static void RunNumberBaseConversion()
        {
            while (true)
            {
                // Prompt the user
                Console.WriteLine("\nChoose letter 'A - C' for the Number Base Conversion you want to perform?");
                Console.WriteLine("\nEnter A - Conversion from other bases to base 10.");
                Console.WriteLine("Enter B - Conversion from Base 10 to other bases.");
                Console.WriteLine("Enter C - Conversion from one base to other bases.\n");

                string userChoice = Console.ReadLine();

                // User's number base conversion selection and validation
                switch (userChoice.ToLower())
                {
                    case "a":
                        ConvertToBase10();
                        break;

                    case "b":
                        FromBase10ToOtherBases();
                        break;

                    case "c":
                        FromOneBaseToAnother();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter A, B, or C.");
                        break;
                }

                if (userChoice.ToLower() == "exit")
                {
                    break; // Exit the loop if the user types 'exit'
                }
            }
        }

        public static void ConvertToBase10()
        {
            Console.WriteLine("Enter the source base (2 to 16) for conversion to base 10:");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int baseInput) && baseInput >= 2 && baseInput <= 16)
            {
                while (true) // Run until there is an exit or break
                {
                    // User's base number collection
                    Console.WriteLine($"\nEnter a valid base {baseInput} number (or type 'exit' to quit):\n");
                    string numberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (numberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }
                    else
                    {
                        try
                        {
                            int base10Result = ConvertToBase10(numberBaseInput, baseInput);
                            Console.WriteLine("\nBase 10 Result: " + base10Result);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a base between 2 and 16.");
            }
        }

        public static int ConvertToBase10(string numberBaseInput, int baseInput)
        {
            int result = 0;

            for (int i = 0; i < numberBaseInput.Length; i++)
            {
                int digitValue;
                char digitChar = numberBaseInput[numberBaseInput.Length - 1 - i];

                if (char.IsDigit(digitChar))
                {
                    digitValue = (int)char.GetNumericValue(digitChar);
                }
                else
                {
                    // Handle letters ('A' to 'F') for bases 11 - 16
                    if (char.ToUpper(digitChar) >= 'A' && char.ToUpper(digitChar) <= 'F')
                    {
                        digitValue = char.ToUpper(digitChar) - 'A' + 10;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid character '{digitChar}' for the specified base.");
                    }
                }

                if (digitValue < 0 || digitValue >= baseInput)
                {
                    throw new ArgumentException($"Invalid digit '{digitChar}' for the specified base.");
                }

                result += digitValue * (int)Math.Pow(baseInput, i);
            }

            return result;
        }

        public static void FromBase10ToOtherBases()
        {
            Console.WriteLine("Enter the target base for the conversion from base 10 (2 to 16):");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int targetBase) && targetBase >= 2 && targetBase <= 16)
            {
                while (true)
                {
                    // User's base-10 number collection
                    Console.WriteLine("Enter a valid base 10 number (or type 'exit' to quit):");
                    string base10Input = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (base10Input.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }
                    else
                    {
                        if (int.TryParse(base10Input, out int base10Number) && base10Number >= 0)
                        {
                            try
                            {
                                string resultInTargetBase = ConvertFromBase10(base10Number, targetBase);
                                Console.WriteLine($"Result in base {targetBase}: {resultInTargetBase}");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                       /*
                        else
                        {
                            Console.WriteLine("Invalid input. Enter a non-negative base 10 number.");
                        }
                        */
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a target base between 2 and 16.");
            }
        }

        public static string ConvertFromBase10(int base10Number, int targetBase)
        {
            if (base10Number < 0)
            {
                throw new ArgumentException("Base 10 number must be non-negative.");
            }

            if (targetBase < 2 || targetBase > 16)
            {
                throw new ArgumentException("Target base must be between 2 and 16.");
            }

            if (base10Number == 0)
            {
                return "0"; // Special case for base 10 number 0
            }

            string result = "";
            while (base10Number > 0)
            {
                int remainder = base10Number % targetBase;
                char digitChar = (remainder < 10) ? (char)(remainder + '0') : (char)(remainder - 10 + 'A');
                result = digitChar + result;
                base10Number /= targetBase;
            }

            return result;
        }


        public static void FromOneBaseToAnother()
        {
            Console.WriteLine("Enter the source base (2 to 16) for conversion:");
            string sourceBaseInput = Console.ReadLine();

            if (int.TryParse(sourceBaseInput, out int sourceBase) && sourceBase >= 2 && sourceBase <= 16)
            {
                while (true)
                {
                    // User's source base number collection
                    Console.WriteLine($"Enter a valid base {sourceBase} number (or type 'exit' to quit):");
                    string numberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (numberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }
                    else
                    {
                        try
                        {
                            int base10Result = ConvertToBase10(numberBaseInput, sourceBase);
                            Console.WriteLine("Base 10 Result: " + base10Result);

                            Console.WriteLine("Enter the target base for the conversion (2 to 16):");
                            string targetBaseInput = Console.ReadLine();

                            if (int.TryParse(targetBaseInput, out int targetBase) && targetBase >= 2 && targetBase <= 16)
                            {
                                try
                                {
                                    string resultInTargetBase = ConvertFromBase10(base10Result, targetBase);
                                    Console.WriteLine($"Result in base {targetBase}: {resultInTargetBase}");
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Enter a target base between 2 and 16.");
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a source base between 2 and 16.");
            }

        }

       public static void RunBaseMathOperation()
        {
            while (true)
            {
                // Prompt the user
                Console.WriteLine("\nWhat Base Math Operation do you want to perform?");
                Console.WriteLine("\nEnter A - For Addition.");
                Console.WriteLine("Enter S - For Subtraction.");
                Console.WriteLine("Enter D - For Division.");
                Console.WriteLine("Enter M - For Multiplication");
                Console.WriteLine("Enter R - For Remainder or Modulus.\n");

                string userChoice = Console.ReadLine();

                // User's number base conversion selection and validation
                switch (userChoice.ToLower())
                {
                    case "a":
                        BaseMathAddition();
                        break;

                    case "s":
                        BaseMathSubtraction();
                        break;

                    case "m":
                        BaseMathMultiplication();
                        break;
                    case "d":
                        BaseMathDivision();
                        break;

                    case "r":
                        BaseMathRemainder();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter A, B, or C.");
                        break;
                }

                if (userChoice.ToLower() == "exit")
                {
                    break; // Exit the loop if the user types 'exit'
                }
            }
        }

        public static void BaseMathAddition()
        {
            Console.WriteLine("Enter the source base (2 to 16) for the Addition:");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int baseInput) && baseInput >= 2 && baseInput <= 16)
            {
                while (true) // Run until there is an exit or break
                {
                    // User's first base number collection
                    Console.WriteLine($"\nEnter the first valid base {baseInput} number (or type 'exit' to quit):\n");
                    string firstNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (firstNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    // User's second base number collection
                    Console.WriteLine($"\nEnter the second valid base {baseInput} number (or type 'exit' to quit):\n");
                    string secondNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (secondNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    try
                    {
                        // Convert base numbers to base 10
                        int firstBase10 = ConvertToBase10(firstNumberBaseInput, baseInput);
                        int secondBase10 = ConvertToBase10(secondNumberBaseInput, baseInput);

                        // Perform addition on base 10 numbers
                        int resultBase10 = firstBase10 + secondBase10;

                        // Convert result back to the original base
                        string resultInOriginalBase = ConvertFromBase10(resultBase10, baseInput);

                        Console.WriteLine($"Result in base {baseInput}: {resultInOriginalBase}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a base between 2 and 16.");
            }
        }

        public static void BaseMathSubtraction()
        {
            Console.WriteLine("Enter the source base (2 to 16) for the Subtraction:");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int baseInput) && baseInput >= 2 && baseInput <= 16)
            {
                while (true) // Run until there is an exit or break
                {
                    // User's first base number collection
                    Console.WriteLine($"\nEnter the first valid base {baseInput} number (or type 'exit' to quit):\n");
                    string firstNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (firstNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    // User's second base number collection
                    Console.WriteLine($"\nEnter the second valid base {baseInput} number (or type 'exit' to quit):\n");
                    string secondNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (secondNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    try
                    {
                        // Convert base numbers to base 10
                        int firstBase10 = ConvertToBase10(firstNumberBaseInput, baseInput);
                        int secondBase10 = ConvertToBase10(secondNumberBaseInput, baseInput);

                        // Perform addition on base 10 numbers
                        int resultBase10 = firstBase10 - secondBase10;

                        // Convert result back to the original base
                        string resultInOriginalBase = ConvertFromBase10(resultBase10, baseInput);

                        Console.WriteLine($"Result in base {baseInput}: {resultInOriginalBase}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a base between 2 and 16.");
            }
        }
        public static void BaseMathMultiplication()
        {
            Console.WriteLine("Enter the source base (2 to 16) for the Multiplication:");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int baseInput) && baseInput >= 2 && baseInput <= 16)
            {
                while (true) // Run until there is an exit or break
                {
                    // User's first base number collection
                    Console.WriteLine($"\nEnter the first valid base {baseInput} number (or type 'exit' to quit):\n");
                    string firstNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (firstNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    // User's second base number collection
                    Console.WriteLine($"\nEnter the second valid base {baseInput} number (or type 'exit' to quit):\n");
                    string secondNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (secondNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    try
                    {
                        // Convert base numbers to base 10
                        int firstBase10 = ConvertToBase10(firstNumberBaseInput, baseInput);
                        int secondBase10 = ConvertToBase10(secondNumberBaseInput, baseInput);

                        // Perform addition on base 10 numbers
                        int resultBase10 = firstBase10 * secondBase10;

                        // Convert result back to the original base
                        string resultInOriginalBase = ConvertFromBase10(resultBase10, baseInput);

                        Console.WriteLine($"Result in base {baseInput}: {resultInOriginalBase}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a base between 2 and 16.");
            }
        }
        public static void BaseMathRemainder()
        {
            Console.WriteLine("Enter the source base (2 to 16) for the Modulus:");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int baseInput) && baseInput >= 2 && baseInput <= 16)
            {
                while (true) // Run until there is an exit or break
                {
                    // User's first base number collection
                    Console.WriteLine($"\nEnter the first valid base {baseInput} number (or type 'exit' to quit):\n");
                    string firstNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (firstNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    // User's second base number collection
                    Console.WriteLine($"\nEnter the second valid base {baseInput} number (or type 'exit' to quit):\n");
                    string secondNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (secondNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    try
                    {
                        // Convert base numbers to base 10
                        int firstBase10 = ConvertToBase10(firstNumberBaseInput, baseInput);
                        int secondBase10 = ConvertToBase10(secondNumberBaseInput, baseInput);

                        // Perform modulus on base 10 numbers
                        int resultBase10 = firstBase10 % secondBase10;

                        // Convert result back to the original base
                        string resultInOriginalBase = ConvertFromBase10(resultBase10, baseInput);

                        Console.WriteLine($"Result in base {baseInput}: {resultInOriginalBase}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a base between 2 and 16.");
            }
        }
        public static void BaseMathDivision()
        {
            Console.WriteLine("Enter the source base (2 to 16) for the division:");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int baseInput) && baseInput >= 2 && baseInput <= 16)
            {
                while (true) // Run until there is an exit or break
                {
                    // User's first base number collection
                    Console.WriteLine($"\nEnter the first valid base {baseInput} number (or type 'exit' to quit):\n");
                    string firstNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (firstNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    // User's second base number collection
                    Console.WriteLine($"\nEnter the second valid base {baseInput} number (or type 'exit' to quit):\n");
                    string secondNumberBaseInput = Console.ReadLine();

                    // Check if the user has entered the word 'exit', then exit
                    if (secondNumberBaseInput.ToLower() == "exit")
                    {
                        break; // Exit the loop if the user types 'exit'
                    }

                    try
                    {
                        // Convert base numbers to base 10
                        int firstBase10 = ConvertToBase10(firstNumberBaseInput, baseInput);
                        int secondBase10 = ConvertToBase10(secondNumberBaseInput, baseInput);

                        // Perform addition on base 10 numbers
                        int resultBase10 = firstBase10 / secondBase10;

                        // Convert result back to the original base
                        string resultInOriginalBase = ConvertFromBase10(resultBase10, baseInput);

                        Console.WriteLine($"Result in base {baseInput}: {resultInOriginalBase}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a base between 2 and 16.");
            }
        }












    }
}
