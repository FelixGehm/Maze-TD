using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GA.Shakespear
{
	public class Shakespear : MonoBehaviour
	{
		[SerializeField] private string _target;
		[SerializeField] private int _populationSize;
		[SerializeField] private float _mutationRate;

		[SerializeField] TextMeshProUGUI _bestPhraseTxt, _bestFitness, _generationTxt, _populationSizeTxt, _mutationRateTxt, _allPhrases;

		private Population _population;

		// Start is called before the first frame update
		void Start()
		{
			_population = new Population(_mutationRate, _populationSize, _target);
		}

		// Update is called once per frame
		void Update()
		{
			if (_population.BestFitness == 1)
			{
				DisplayInfo();
				return;
			}
				

			_population.CalcFitness();
			_population.NewGeneration();
			DisplayInfo();
		}

		private void DisplayInfo()
		{
			string bestPhrase = "";
			foreach (char c in _population.BestGenes.Genes)
				bestPhrase += c;

			_bestPhraseTxt.text = bestPhrase;
			_bestFitness.text = _population.BestFitness.ToString();
			_generationTxt.text = $"Generation: {_population.Generation}";
			_populationSizeTxt.text = _populationSize.ToString();
			_mutationRateTxt.text = _mutationRate.ToString();
			_allPhrases.text = _population.GetAllPhrases();
		}
	}
}

