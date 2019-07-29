using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace GeneticAlgorithmAdvanced
{
	public class GAUI : MonoBehaviour
	{
		[SerializeField] private GeneticAlgorithmImproved _ga;
		[SerializeField] private TextMeshProUGUI _stateTxt;
		[SerializeField] private TextMeshProUGUI _generationTxt;
		[SerializeField] private TextMeshProUGUI _bestFitnessTxt;
		[SerializeField] private TextMeshProUGUI _averageFitness;
		[SerializeField] private TextMeshProUGUI _bestFitnessGenerationTxt;
		[SerializeField] private TextMeshProUGUI _bestFitnessTimeTxt;

		private void Update()
		{
			if (!_ga.IsRunning)
				return;

			_stateTxt.text = $"{_ga.CurrentState}";
			_generationTxt.text = $"Generation: {_ga.Population.Generation}";
			_bestFitnessTxt.text = $"Best Fitness: {_ga.BestFitness}";
			_averageFitness.text = $"Average Fitness: {_ga.AverageFitness}";
			_bestFitnessGenerationTxt.text = $"Best Fitness at Generation {_ga.BestFintessGeneration}";
			_bestFitnessTimeTxt.text = $"Best Fitness at Time {FormatTime(_ga.BestFitnessTime)}";
		}

		private string FormatTime(float time)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(time);

			string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			return timeText;
		}
	}
}

