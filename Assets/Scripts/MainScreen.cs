using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace SMG
{
	public class MainScreen : MonoBehaviour
	{
		[SerializeField] private Resources _resources;
		[SerializeField] private CardView _cardViewTemplate;
		[SerializeField] private Transform _cardsPanel;
		[SerializeField] private Image _blockPanel;
		[SerializeField] private Text _time;
		
		public event Action<int> CardViewOpen;
		public event Action Restart;
		private List<CardView> _cardViews;
		private CardView _emptyCardView;
		
		private int _id1;
		private int _id2;
		private float _delay;
		private bool _close;
		private bool _block;
		private float _gameTime;
		private bool _timeoutRestart;
		
		public void Init(List<Card> cards, int seconds = 0)
		{
			Debug.Log(PrettyLog.GetMessage("MainScreen","Init", "Game time is", seconds));

			if (_cardViews != null && _cardViews.Any()) _cardViews.ForEach(cv => DestroyImmediate(cv.gameObject));
			if (_emptyCardView != null) DestroyImmediate(_emptyCardView.gameObject);

			_gameTime = seconds;
			if (seconds == 0) _time.text = string.Empty;
			_timeoutRestart = seconds > 0;
			
			var index = new System.Random().Next(24);
			var i = 0;

			_cardViews = new List<CardView>();
			cards.ForEach(card =>
			{
				var cardView = Instantiate(_cardViewTemplate);
				cardView.GetComponent<RawImage>().texture = null;
				cardView.gameObject.SetActive(true);
				cardView.transform.SetParent(_cardsPanel);
				cardView.GetComponent<CardView>().Init(card, OnCardViewClick);
				_cardViews.Add(cardView);

				if (i == index) //add empty
				{
					_emptyCardView = Instantiate(_cardViewTemplate);
					_emptyCardView.GetComponent<RawImage>().texture = null;
					_emptyCardView.gameObject.SetActive(true);
					_emptyCardView.transform.SetParent(_cardsPanel);	
				}
				i++;
			});
			
			gameObject.SetActive(true);
		}

		private void OnCardViewClick(int id)
		{
			var cardView = _cardViews[id];
			
			Debug.Log(PrettyLog.GetMessage("MainScreen","OnCardViewClick", "Card id is", cardView.Card));
			
			if (CardViewOpen != null)
				CardViewOpen(id);
		}

		public void OpenCardView(int id)
		{
			var cardView = _cardViews[id];
			
			cardView.GetComponent<RawImage>().texture = _resources.GetCardImage(cardView.Card.CardType).texture;
			cardView.GetComponent<RawImage>().color = _resources.GetCardImage(cardView.Card.CardType).color;
		}

		public void CloseCardView(int id)
		{
			var cardView = _cardViews[id];
			
			Debug.Log(PrettyLog.GetMessage("MainScreen","CloseCardView", "Card is", cardView.Card));
			
			cardView.GetComponent<RawImage>().texture = null; 
			cardView.GetComponent<RawImage>().color = Color.black;
		}

		public void BlockCardView(int id)
		{
			var cardView = _cardViews[id];
			
			Debug.Log(PrettyLog.GetMessage("MainScreen","BlockCardView", "Card is", cardView.Card));

			cardView.enabled = false;
			cardView.GetComponent<RawImage>().texture = null; 
			cardView.GetComponent<RawImage>().color = Color.white;
		}
		
		public void CloseCardViews(int id1, int id2)
		{
			var ids = string.Format("id1 = {0}, id2 = {1}", id1, id2);
			
			Debug.Log(PrettyLog.GetMessage("MainScreen","CloseCardViews", "Card ids are", ids));

			_blockPanel.gameObject.SetActive(true);
			_id1 = id1;
			_id2 = id2;
			_delay = 1f;
			_close = true;
		}

		public void BlockCardViews(int id1, int id2)
		{
			var ids = string.Format("id1 = {0}, id2 = {1}", id1, id2);
			
			Debug.Log(PrettyLog.GetMessage("MainScreen","BlockCardViews", "Card ids are", ids));
			
			_blockPanel.gameObject.SetActive(true);
			_id1 = id1;
			_id2 = id2;
			_delay = 1f;
			_block = true;
		}
		
		private void Update()
		{
			if (_delay > 0) _delay -= Time.deltaTime;
			else if(_delay <= 0)
			{
				if (_close)
				{
					CloseCardView(_id1);
					CloseCardView(_id2);
					_close = false;
					_blockPanel.gameObject.SetActive(false);
				}
				
				if (_block)
				{
					BlockCardView(_id1);
					BlockCardView(_id2);
					_block = false;
					_blockPanel.gameObject.SetActive(false);
				}
			}

			if (_gameTime > 0)
			{
				_gameTime -= Time.deltaTime;
				_time.text = string.Format("The remaining time is {0}", (int) _gameTime);
			}
			else if (_gameTime <= 0 && _timeoutRestart)
			{
				_timeoutRestart = false;
				if (Restart != null) Restart();
			}
		}
	}
}