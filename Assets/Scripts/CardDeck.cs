using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace SMG.Mocks
{
    public class CardDeck
    {
        public static List<Card> SimpleCardDeck
        {
            get
            {
                return new List<Card>
                {
                    new Card(0, ECardType.WhiteBishop),
                    new Card(1, ECardType.BlackBishop),
                    new Card(2, ECardType.WhiteKing),
                    new Card(3, ECardType.BlackKing),
                    new Card(4, ECardType.WhiteKnight),
                    new Card(5, ECardType.BlackKnight),
                    new Card(6, ECardType.WhiteRook),
                    new Card(7, ECardType.BlackRook),
                    new Card(8, ECardType.WhitePawn),
                    new Card(9, ECardType.BlackPawn),
                    new Card(10, ECardType.WhiteQueen),
                    new Card(11, ECardType.BlackQueen),
                    
                    new Card(0, ECardType.WhiteBishop),
                    new Card(1, ECardType.BlackBishop),
                    new Card(2, ECardType.WhiteKing),
                    new Card(3, ECardType.BlackKing),
                    new Card(4, ECardType.WhiteKnight),
                    new Card(5, ECardType.BlackKnight),
                    new Card(6, ECardType.WhiteRook),
                    new Card(7, ECardType.BlackRook),
                    new Card(8, ECardType.WhitePawn),
                    new Card(9, ECardType.BlackPawn),
                    new Card(10, ECardType.WhiteQueen),
                    new Card(11, ECardType.BlackQueen)
                };
            }
        }

        public static List<Card> GeneratedCardDeck
        {
            get
            {
                const int max = 24;
                var result = new List<Card>();
                var random = new Random();
                var positions = Enumerable.Range(0, max).OrderBy(x => random.Next(max)).ToList();
                
                var cardTypes = (ECardType[]) Enum.GetValues(typeof(ECardType));
                var cards = new List<ECardType>(cardTypes);
                cards.AddRange(cardTypes.ToList());
                
                for (var i = 0; i < max; i++)
                    result.Add(new Card(positions[i], cards[i]));
                
                return result;
            }
        }

        
    }
}