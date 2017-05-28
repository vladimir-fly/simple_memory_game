using System;
using UnityEngine;
using UnityEngine.UI;

namespace SMG
{
	public class StartScreen : MonoBehaviour
	{
		[SerializeField] private Button _startButton;
		[SerializeField] private Slider _timeSlider;
		[SerializeField] private Text _timeValue;
		
		public void Init(Action<int> startCallback)
		{
			Debug.Log(PrettyLog.GetMessage("StartScreen", "Init", "Game start!", null));

			if (startCallback == null)
			{
				Debug.Log(PrettyLog.GetMessage("StartScreen", "Init", "Start button callback is null!", null));
				return;
			}

			gameObject.SetActive(true);
			_timeSlider.onValueChanged.AddListener(
				time => _timeValue.text = string.Format("Game time is {0}", _timeSlider.value));
			
			_startButton.onClick.AddListener(() =>
			{
				gameObject.SetActive(false);
				startCallback((int) _timeSlider.value);
			});
		}
	}
}