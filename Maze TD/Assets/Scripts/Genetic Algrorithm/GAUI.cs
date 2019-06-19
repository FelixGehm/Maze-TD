using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GAUI : MonoBehaviour
{
	[SerializeField] private GeneticAlgorithm _ga;
	[SerializeField] private TextMeshProUGUI _stateTxt;
	[SerializeField] private TextMeshProUGUI _generationTxt;
	[SerializeField] private TextMeshProUGUI _popIndexTxt;
	[SerializeField] private TextMeshProUGUI _bestFitnessTxt;
	[SerializeField] private TextMeshProUGUI _lastFitnessTxt;
	[SerializeField] private TextMeshProUGUI _currentFitnessTxt;
	

	private void Update()
	{
		if (!_ga.IsRunning)
			return;

		_stateTxt.text = $"{_ga.CurrentState}";
		_generationTxt.text = $"Generation: {_ga.Population.Generation}";
		_popIndexTxt.text = $"DNA: {_ga.PopulationIndex}";
		_bestFitnessTxt.text = $"Best Fitness: {_ga.Population.BestFitness}";
		_lastFitnessTxt.text = $"Last Fitness: {_ga.LastFitness}";
		_currentFitnessTxt.text = $"Current Fitness: {_ga.CurrentFintess}";
	}
}
