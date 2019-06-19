using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	
	public void LoadScene()
	{
		SceneManager.LoadScene("DamageTest");
	}
}
