using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
public class WindowPlayerSettings : EditorWindow
{
	private PlayerSettings _playerSettings;
	private SerializedObject _sPlayerSettings;

	[MenuItem("Game Settings/Player")]
	static void OpenWindow()
	{
		GetWindow<WindowPlayerSettings>("Player Settings");
	}

	private void OnEnable()
	{
		_playerSettings = FindPlayerSettings();
		Debug.Log(_playerSettings == null);
		_sPlayerSettings = new SerializedObject(_playerSettings);
	}

	private void OnGUI()
	{
		return;
		EditorGUILayout.PropertyField(_sPlayerSettings.FindProperty("StartMoney"), GUIContent.none);
		EditorGUILayout.PropertyField(_sPlayerSettings.FindProperty("StartLives"), GUIContent.none);
	}

	private PlayerSettings FindPlayerSettings()
	{
		string[] guids = AssetDatabase.FindAssets("t:PlayerSettings");
		Debug.Log(guids.Length);
		return AssetDatabase.LoadAssetAtPath<PlayerSettings>(AssetDatabase.GUIDToAssetPath(guids[0]));
	}
}
*/
