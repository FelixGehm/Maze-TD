using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.Shakespear
{
	public class Population : MonoBehaviour
	{
		private float _mutationRate;
		private string _target;

		private float _fitnessSum;
		private System.Random _random;

		public List<DNA> Pop { get; private set; }
		public int Generation { get; private set; }
		public float BestFitness { get; private set; }
		public DNA BestGenes { get; private set; }

		public Population(float mutationRate, int populationSize, string target)
		{
			_target = target;
			_mutationRate = mutationRate;
			Pop = new List<DNA>();
			Generation = 1;
			BestFitness = 0;

			for (int i = 0; i < populationSize; i++)
			{
				Pop.Add(new DNA(target.Length));
			}

			_random = new System.Random();
		}

		public void NewGeneration()
		{
			if (Pop.Count <= 0)
				return;


			CalculateFitnessSum();
			List<DNA> newPopulation = new List<DNA>();

			newPopulation.Add(BestGenes);

			for (int i = 1; i < Pop.Count; i++)
			{
				DNA parent1 = ChooseParent();
				DNA parent2 = ChooseParent();
				DNA child = parent1.Crossover(parent2);

				child.Mutate(_mutationRate);

				newPopulation.Add(child);
			}

			Pop = newPopulation;
			Generation++;
		}

		public void CalcFitness()
		{
			foreach (var dna in Pop)
				dna.CalcFitness(_target);
		}

		private void CalculateFitnessSum()
		{
			_fitnessSum = 0;
			DNA best = Pop[0];
			DNA secondBest = Pop[1];

			foreach (DNA e in Pop)
			{
				_fitnessSum += e.Fitness;
				if (e.Fitness > best.Fitness)
				{
					best = e;
				}
			}

			BestFitness = best.Fitness;
			BestGenes = best;
		}

		private DNA ChooseParent()
		{
			double randomNumber = _random.NextDouble() * _fitnessSum;

			foreach (DNA e in Pop)
			{
				if (randomNumber < e.Fitness)
				{
					return e;
				}

				randomNumber -= e.Fitness;
			}

			return null;
		}

		public string GetAllPhrases()
		{
			string allPhrases = "";

			foreach (var dna in Pop)
				allPhrases += dna.GetPhrase() + "\n";

			return allPhrases;
		}
	}
}

