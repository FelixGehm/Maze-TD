using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyStats))]
public class EnemyUI : MonoBehaviour
{
	[Header("SetupFields")]
	[SerializeField]
	private EnemyStats _stats;
	[Header("UIElemtns")]
	[SerializeField]
	private Image _healthbarImg;


	private void Reset()
	{
		_stats = GetComponent<EnemyStats>();
	}

	private void Start()
	{
		if (_stats == null)
			return;

		_stats.PropertyChanged += EnemyStats_OnPropertyChanged;
	}

	private void OnDestroy()
	{
		_stats.PropertyChanged -= EnemyStats_OnPropertyChanged;
	}

	private void EnemyStats_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		_healthbarImg.fillAmount = (float)_stats.Hitpoints / (float)_stats.MaxHitpoints;
	}
}

