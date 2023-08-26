using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static TwentyOne.Card;

namespace TwentyOne
{

    class Card
    {
        public enum Suit { Hearts, Diamond, Clubs, Spades };
        public enum Seniority { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, J = 10, Q = 10, K = 10, Ace };

        public Card(Suit suit, Seniority seniority)
        {
            this.suit = suit;
            this.senority = seniority;
        }

        public string CardToString()
        {
            return $"{suit.ToString()} {senority.ToString()}";
        }

        public Suit suit;
        public Seniority senority;
    }

    class Deck
    {
        public Deck()
        {
            CreateDeck();
            Shuffle();
        }

        public void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; --i)
            {
                int j = _random.Next(i + 1);
                Card temp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = temp;
            }
        }

        private void CreateDeck()
        {
            for (int i = 0; i < _suitSize; ++i)
            {
                for (int j = 2; j < _senioritySize; ++j)
                {
                    Card card = new Card((Suit)i, (Seniority)j);
                    _cards.Add(card);
                }
            }
        }

        public List<Card> _cards = new List<Card>(_size);

        private const int _size = 52;
        private Random _random = new Random();

        private readonly int _suitSize = Enum.GetNames(typeof(Suit)).Length;
        private readonly int _senioritySize = Enum.GetNames(typeof(Seniority)).Length;
    }

    class Player
    {
        /// <summary>
        /// Берет карту из колоды
        /// </summary>
        /// <param name="card"></param>
        public void TakeCard(Deck deck)
        {
            cardOnHands.Add(deck._cards[_deckPosition]);
            currentPoints += (int)deck._cards[_deckPosition].senority;
            _deckPosition++;
        }

        /// <summary>
        /// Удаляет очки и обнуляет позицию карты
        /// </summary>
        public void RemovePoints() 
        {
            currentPoints = 0;
            _deckPosition = 0;
            foreach(Card card in cardOnHands)
            {
                cardOnHands.Remove(card);
            }
        }

        /// <summary>
        /// Показывает карты на руках
        /// </summary>
        public void ShowHand()
        {
            Console.WriteLine($"Current points: {currentPoints}");
            Console.Write("Cards: ");
            for (int i = 0; i < cardOnHands.Count; i++)
            {
                Console.Write($"{cardOnHands[i].CardToString()}");
                if (i != cardOnHands.Count - 1)
                    Console.Write(", ");
                else
                    Console.WriteLine();
            }
        }

        /// <summary>
        /// Текущие очки
        /// </summary>
        public int currentPoints = 0;

        /// <summary>
        /// Карты игрока
        /// </summary>
        private List<Card> cardOnHands = new List<Card>();
        /// <summary>
        /// Позиция текущей карты в колоде
        /// </summary>
        private static int _deckPosition = 0;
    }
}
