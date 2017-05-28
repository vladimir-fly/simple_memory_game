using System.Linq;
using SMG.Mocks;
using UnityEngine;

namespace SMG
{
	public class App : MonoBehaviour
	{
		[SerializeField] private StartScreen _startScreen;
		[SerializeField] private MainScreen _mainScreen;

		private void Awake()
		{
			Debug.Log(PrettyLog.GetMessage("App", "Awake", "Welcome!", null));
		}

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			Debug.Log(PrettyLog.GetMessage("App", "Init", "Started!", null));

			var cardDeck = CardDeck.GeneratedCardDeck.OrderBy(card => card.Id).ToList();
			_startScreen.Init(
				seconds => _mainScreen.Init(cardDeck, seconds));

			MainController.Instance.Init(cardDeck);

			_mainScreen.CardViewOpen += MainController.Instance.OpenCard;
			_mainScreen.Restart += Restart;
			
			MainController.Instance.Open += _mainScreen.OpenCardView;
			MainController.Instance.Close += _mainScreen.CloseCardViews;
			MainController.Instance.Block += _mainScreen.BlockCardViews;
			MainController.Instance.End += Restart;
		}

		private void Restart()
		{
			var cardDeck = CardDeck.GeneratedCardDeck.OrderBy(card => card.Id).ToList();
			_mainScreen.Init(cardDeck);

			MainController.Instance.Init(cardDeck);
		}
	}
}