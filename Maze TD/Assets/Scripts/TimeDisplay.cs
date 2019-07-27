using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _timeText;


	private bool _trackTime;
	private float _startTime;

	private void Update()
	{
		if (!_trackTime)
			return;

		float currentTime = Time.time - _startTime;
		int seconds = (int)currentTime;
		//float hours = currentTime /

		//string formatedTime = $"{}"
		_timeText.text = seconds.ToString();
		_timeText.text = FormatTime(seconds);
	}

	public void TrackTime()
	{
		_trackTime = true;
		_startTime = Time.time;
	}

	private string FormatTime(float time)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(time);

		string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		return timeText;
	}

}
