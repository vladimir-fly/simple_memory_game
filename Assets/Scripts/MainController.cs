using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SMG
{
    public class MainController
    {
        private static MainController _instance;
        public static MainController Instance { get { return _instance ?? (_instance = new MainController()); } }
        private MainController() { }

        private List<Card> _cards;
        private int _openedCardId = -1;
        
        public event Action<int> Open;
        public event Action<int, int> Close;
        public event Action<int, int> Block;
        public event Action End;

        public void Init(List<Card> cards)
        {
            Debug.Log(PrettyLog.GetMessage("MainController", "Init", "Started!", null));

            _cards = cards;
        }

        public void OpenCard(int id)
        {
            Debug.Log(PrettyLog.GetMessage("MainController","OpenCard", "Card id is", id));

            if (_openedCardId == id) return;
            
            if (Open != null)
                Open(id);
            
            if (_openedCardId != -1)
            {
                var openedCard = _cards.First(c => c.Id == _openedCardId);
                var card = _cards.First(c => c.Id == id);

                if (openedCard.CardType == card.CardType)
                    BlockCards(_openedCardId, id);
                else
                    CloseCards(_openedCardId, id);
                _openedCardId = -1;
                return;
            }
            
            _openedCardId = id;
        }

        private void CloseCards(int id1, int id2)
        {
            var card1 = _cards.First(c => c.Id == id1);
            var card2 = _cards.First(c => c.Id == id2);
            
            Debug.Log(PrettyLog.GetMessage("MainController","CloseCards", "Card is", card1));
            Debug.Log(PrettyLog.GetMessage("MainController","CloseCards", "Card is", card2));

            if (Close != null)
                Close(id1, id2);
        }

        private void BlockCards(int id1, int id2)
        {
            var card1 = _cards.First(c => c.Id == id1);
            var card2 = _cards.First(c => c.Id == id2);
            
            Debug.Log(PrettyLog.GetMessage("MainController","CloseCards", "Card is", card1));
            Debug.Log(PrettyLog.GetMessage("MainController","CloseCards", "Card is", card2));
            
            _cards.Remove(card1);
            _cards.Remove(card2);

            if (!_cards.Any() && End != null)
            {
                End();
                return;
            }
            if (Block != null) Block(id1, id2);
        }
    }
}