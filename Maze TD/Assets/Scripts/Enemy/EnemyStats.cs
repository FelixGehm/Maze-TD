using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyStats : MonoBehaviour, INotifyPropertyChanged
{
	private int _hitpoints;
	private float _speed;
	private int _dmg;

	public int MaxHitpoints { get; private set; }

	public int Hitpoints
	{
		get
		{
			return _hitpoints;
		}
		private set
		{
			_hitpoints = value;
			OnPropertyChanged("Hitpoints");
		}
	}
	public float Speed
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;
			OnPropertyChanged("Speed");
		}
	}
	public int Damage
	{
		get
		{
			return _dmg;
		}
		private set
		{
			_dmg = value;
			OnPropertyChanged("Damage");
		}
	}
	public event PropertyChangedEventHandler PropertyChanged;

	public void TakeDamage(int dmg)
	{
		Hitpoints -= dmg;
		if (Hitpoints <= 0)
			Die();

	}

	private void Start()
	{
		EnemySettings settings = Settings.Instance.EnemySettings;
		MaxHitpoints = settings.MaxHitpoints;
		Hitpoints = MaxHitpoints;
		Speed = settings.Speed;
	}

	private void Die()
	{
		Destroy(gameObject);
		Debug.Log(gameObject.name + $"{gameObject.name} died.");
	}

	private void OnPropertyChanged(string name)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
