using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    // find the first key as a substring
    static int StartsWithDictKey(string str, Dictionary<string, int> dict)
    {
        foreach (KeyValuePair<string, int> num in dict)
        {
            if (str.StartsWith(num.Key))
            {
                return num.Value;
            }
        }
        return 0;
    }

    static int StartsWithDigit(string str)
    {
        if (Char.IsNumber(str[0]))
        {
            return int.Parse(str[0].ToString());
        }
        return 0;
    }

    static void Main()
    {
        string pathTestInput  = "test-input.txt";
        string pathTestInput2 = "test-input-2.txt";
        string pathInput      = "input.txt";

        // part One
        try
        {
            using (StreamReader sr = File.OpenText(pathInput))
            {
                string s;
                int sum = 0;
                while ((s = sr.ReadLine()) != null) {
                    int firstNumber;
                    int i = 0;
                    do {
                        firstNumber = StartsWithDigit(s.Substring(i));
                        i++;

                    } while (firstNumber == 0 && i < s.Length);

                    int lastNumber;
                    int k = s.Length-1;
                    do {
                        lastNumber = StartsWithDigit(s.Substring(k));
                        k--;
                    } while (lastNumber == 0 && k >= 0);

                    sum += (firstNumber*10 + lastNumber);
                }
                Console.Write("Part One: ");
                Console.WriteLine(sum);
            }

            // part Two
            using (StreamReader sr = File.OpenText(pathInput))
            {
                int sum = 0;
                Dictionary<string, int> numberMap = new Dictionary<string, int>();

                numberMap.Add("one", 1);
                numberMap.Add("two", 2);
                numberMap.Add("three", 3);
                numberMap.Add("four", 4);
                numberMap.Add("five", 5);
                numberMap.Add("six", 6);
                numberMap.Add("seven", 7);
                numberMap.Add("eight", 8);
                numberMap.Add("nine", 9);

                string t;
                while ((t = sr.ReadLine()) != null)
                {
                    int firstNumber;
                    int lastNumber;
                    int i = 0;
                    int k = t.Length - 1;

                    do {
                        firstNumber = StartsWithDigit(t.Substring(i)) + StartsWithDictKey(t.Substring(i), numberMap);
                        i++;

                    } while (firstNumber == 0);

                    do {
                        lastNumber = StartsWithDigit(t.Substring(k)) + StartsWithDictKey(t.Substring(k), numberMap);
                        k--;
                    } while (lastNumber == 0);

                    sum += (firstNumber*10 + lastNumber);
                }

                Console.Write("Part Two: ");
                Console.WriteLine(sum);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
