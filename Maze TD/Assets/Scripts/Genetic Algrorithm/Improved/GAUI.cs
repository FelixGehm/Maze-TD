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
		[SerializeField] private TextMeshProUGUI _currentFitnessTxt;


		private void Update()
		{
			if (!_ga.IsRunning)
				return;

			_stateTxt.text = $"{_ga.CurrentState}";
			_generationTxt.text = $"Generation: {_ga.Population.Generation}";
			_bestFitnessTxt.text = $"Best Fitness: {_ga.BestFitness}";
			_averageFitness.text = $"Average Fitness: {_ga.AverageFitness}";
			//_currentFitnessTxt.text = $"Current Fitness: {_ga.CurrentFintess}";
		}
	}
}

