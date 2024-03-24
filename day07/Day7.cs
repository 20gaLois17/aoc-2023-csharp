using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day7
{
    public static int PartOneSolution(StreamReader sr)
    {
        int result = 0;
        string s;
        List<Hand> hands = new List<Hand>();
        while ((s = sr.ReadLine()) != null)
        {
            string[] split = s.Split(' ');
            hands.Add(new Hand(split[0], Int32.Parse(split[1])));
        }
        Hand[] _hands = hands.ToArray();
        Array.Sort(_hands);

        int rank = 1;
        foreach (Hand hand in _hands)
        {
            result += rank * hand.bid;
            rank++;
        }

        return result;
    }

    public static int PartTwoSolution(StreamReader sr)
    {
        int result = 0;
        string s;
        List<Hand> hands = new List<Hand>();
        while ((s = sr.ReadLine()) != null)
        {
            string[] split = s.Split(' ');
            hands.Add(new Hand(split[0], Int32.Parse(split[1])));
        }
        hands.Sort(new Hand.HandJokerComparer());
        int rank = 1;
        foreach (Hand hand in hands)
        {
            result += rank * hand.bid;
            rank++;
        }
        return result;
    }

    // ordered by value from lowest to highest
    public enum Card
    {
        TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
    }
    public enum HandType
    {
        HIGH_CARD, ONE_PAIR, TWO_PAIR, THREE_OF_A_KIND, FULL_HOUSE, FOUR_OF_A_KIND, FIVE_OF_A_KIND
    }

    public static Card CardFromChar(char c)
    {
        switch (c)
        {
            case '2': return Card.TWO;
            case '3': return Card.THREE;
            case '4': return Card.FOUR;
            case '5': return Card.FIVE;
            case '6': return Card.SIX;
            case '7': return Card.SEVEN;
            case '8': return Card.EIGHT;
            case '9': return Card.NINE;
            case 'T': return Card.TEN;
            case 'J': return Card.JACK;
            case 'Q': return Card.QUEEN;
            case 'K': return Card.KING;
            case 'A': return Card.ACE;
            default: throw new Exception("Invalid Card");
        }
    }

    public class Hand : IComparable
    {
        public Card[] hand = new Card[5];
        public int bid = 0;

        public Hand(string hand, int bid)
        {
            for (int i = 0; i < hand.Length; i++)
            {
                this.hand[i] = CardFromChar(hand[i]);
            }
            this.bid = bid;
        }

        public HandType GetHandType(bool withJokers = false)
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            for (int i = 0; i < 5; i++)
            {
                if (cards.ContainsKey(hand[i]))
                {
                    cards[hand[i]]++;
                }
                else
                {
                    cards[hand[i]] = 1;
                }
            }
            if (withJokers && cards.ContainsKey(Card.JACK))
            {
                if (cards[Card.JACK] == 5)
                {
                    return HandType.FIVE_OF_A_KIND;
                }
                Card mostCommon = Card.TWO;
                int count = -1;
                foreach (KeyValuePair<Card, int> pair in cards)
                {
                    if (pair.Value > count && pair.Key != Card.JACK)
                    {
                        mostCommon = pair.Key;
                        count = pair.Value;
                    }
                }
                cards[mostCommon] += cards[Card.JACK];
                cards.Remove(Card.JACK);
            }
            switch (cards.Count)
            {
                case 1:
                    return HandType.FIVE_OF_A_KIND;

                case 2:
                    if (cards.ContainsValue(4))
                    {
                        return HandType.FOUR_OF_A_KIND;
                    }
                    return HandType.FULL_HOUSE;

                case 3:
                    if (cards.ContainsValue(3))
                    {
                        return HandType.THREE_OF_A_KIND;
                    }
                    return HandType.TWO_PAIR;

                case 4:
                    return HandType.ONE_PAIR;

                case 5:
                    return HandType.HIGH_CARD;

                default:
                    throw new Exception("Invalid Hand");
            }
        }

        // order by value of hand from lowest to highest
        int IComparable.CompareTo(object obj)
        {
            Hand h = (Hand)obj;
            if (h.GetHandType() != this.GetHandType())
            {
                return (int)this.GetHandType() - (int)h.GetHandType();
            }
            for (int i = 0; i < 5; i++)
            {
                if (h.hand[i] != this.hand[i])
                {
                    return (int)this.hand[i] - (int)h.hand[i];
                }
            }
            return 0;
        }

        public class HandJokerComparer : IComparer<Hand>
        {
            public int Compare(Hand x, Hand y)
            {
                if (x.GetHandType(true) != y.GetHandType(true))
                {
                    return (int)x.GetHandType(true) - (int)y.GetHandType(true);
                }
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] != y.hand[i] && x.hand[i] != Card.JACK && y.hand[i] != Card.JACK)
                    {
                        return (int)x.hand[i] - (int)y.hand[i];
                    }
                    if (x.hand[i] == Card.JACK && y.hand[i] != Card.JACK)
                    {
                        return -1;
                    }
                    if (y.hand[i] == Card.JACK && x.hand[i] != Card.JACK)
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }
    }

    public static void PartOneTest()
    {
        StreamReader sr = GetInput("test-input.txt");
        Console.WriteLine($"Part One Test: {PartOneSolution(sr)}");
    }

    public static void PartOne()
    {
        StreamReader sr = GetInput("input.txt");
        Console.WriteLine($"Part One: {PartOneSolution(sr)}");
    }

    public static void PartTwoTest()
    {
        StreamReader sr = GetInput("test-input.txt");
        Console.WriteLine($"Part Two Test: {PartTwoSolution(sr)}");
    }

    public static void PartTwo()
    {
        StreamReader sr = GetInput("input.txt");
        Console.WriteLine($"Part Two: {PartTwoSolution(sr)}");
    }

    static StreamReader GetInput(string path)
    {
        try
        {
            return File.OpenText(path);
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return null;
    }
}

class Program
{
    static void Main()
    {
        Day7.PartOneTest();
        Day7.PartOne();
        Day7.PartTwoTest();
        Day7.PartTwo();
    }
}
