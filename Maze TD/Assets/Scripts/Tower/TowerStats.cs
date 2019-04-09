using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TowerStats : MonoBehaviour, INotifyPropertyChanged
{
	private float _range = 10;
	private float _fireRate = 1;
	private int _dmg = 50;
	private float _projectileSpeed = 50;

	public event PropertyChangedEventHandler PropertyChanged;

	public float Range
	{
		get
		{
			return _range;
		}
		private set
		{
			_range = value;
			OnPropertyChanged("Range");
		}
	}

	public float FireRate
	{
		get
		{
			return _fireRate;
		}
		private set
		{
			_fireRate = value;
			OnPropertyChanged("FireRate");
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

	public float ProjectileSpeed
	{
		get
		{
			return _projectileSpeed;
		}
		private set
		{
			_projectileSpeed = value;
			OnPropertyChanged("ProjectileSpeed");
		}
	}

	public void Init(float range, float fireRate, int dmg, float projectileSpeed)
	{
		Range = range;
		FireRate = FireRate;
		Damage = dmg;
		ProjectileSpeed = projectileSpeed;
	}

	private void OnPropertyChanged(string name)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}

}
