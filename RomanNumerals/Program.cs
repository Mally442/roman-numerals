using System;

namespace RomanNumerals
{
    class Program
    {
        /// <summary>
        /// Converts a single digit (excluding 4 and 9) to it's Roman Numeral symbol
        /// </summary>
        /// <param name="digit">The integer digit to convert</param>
        /// <param name="one">The Roman Numeral symbol for a 1 of this digit</param>
        /// <param name="five">The Roman Numeral symbol for a 5 of this digit</param>
        /// <returns>A string of the Roman Numeral representation of the input digit</returns>
        static string WriteNumeralsDigit(int digit, string one, string five)
        {
            // Check that number is between 1 and 8 excluding 4
            if (digit < 1 || digit > 8 || digit == 4)
            {
                throw new ArgumentException("Digit must be between 1 and 8 but not 4", nameof(digit));
            }

            var numerals = "";

            // If the digit is 5 or greater, start with the 5 symbol
            if (digit >= 5)
            {
                numerals = five;
            }

            // Add as many 1 symbols as remain when dividing the digit by 5
            var remainder = digit % 5;
            for (var i = 0; i < remainder; i++)
            {
                numerals += one;
            }

            return numerals;
        }

        /// <summary>
        /// Converts an integer number between 1 and 2000 to Roman Numerals
        /// </summary>
        /// <param name="number">The integer number to convert</param>
        /// <returns>A string of the Roman Numeral representation of the input number</returns>
        static string WriteNumerals(int number)
        {
            // Check that number is between 1 and 2000
            if (number < 1 || number > 2000)
            {
                throw new ArgumentException("Number must be between 1 and 2000", nameof(number));
            }

            // Initialise the numeral string and arrays of the Roman symbols
            var numerals = "";
            var ones = new [] { "I", "X", "C", "M" };
            var fives = new [] { "V", "L", "D", null };

            // Starting with units, loop through each digit of the number
            for(var i = 0; i <= 3; i++)
            {
                // Get the digit
                var digit = (number / (int) Math.Pow(10, i)) % 10;
                switch (digit)
                {
                    case 0:
                        // If the digit is 0, continue to up to the next digit
                        continue;
                    case 4:
                        // If the digit is 4, prepend a 1 and 5 symbol
                        numerals = $"{ones[i]}{fives[i]}" + numerals;
                        break;
                    case 9:
                        // If the digit is 9, prepend a 1 symbol for this digit and a 1 symbol for the next digit up
                        numerals = $"{ones[i]}{ones[i + 1]}" + numerals;
                        break;
                    default:
                        // For all other digits, call helper and prepend
                        numerals = WriteNumeralsDigit(digit, ones[i], fives[i]) + numerals;
                        break;
                }
            }

            return numerals;
        }

        static void Main(string[] args)
        {
            // Receive input
            Console.WriteLine("Enter a number between 1 and 2000");
            var input = Console.ReadLine();

            // Try parse input into an integer between 1 and 2000
            int number;
            while (int.TryParse(input, out number) == false || number < 1 || number > 2000)
            {
                // If cannot parse or outside bounds, ask again
                Console.WriteLine("Enter a number between 1 and 2000");
                input = Console.ReadLine();
            }

            // Write input integer as Roman Numerals
            Console.WriteLine($"{number} written as Roman Numerals is: {WriteNumerals(number)}");
            Console.ReadLine();
        }
    }
}
