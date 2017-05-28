using System;
using UnityEngine.UI;

namespace SMG
{
    public class CardView : Button
    {
        public Card Card { get; private set; }

        public void Init(Card card, Action<int> callback)
        {
            Card = card;
            onClick.AddListener(() => callback(Card.Id));
        }
    }
}