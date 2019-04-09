using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TowerPreview))]
public class TowerPreviewUI : MonoBehaviour
{
	[Header("Setup Fields")]
	[SerializeField]
	private TowerPreview _towerPreview;
	[SerializeField]
	private Image _placeIndicatorImg;

	[Header("Attributes")]
	[SerializeField]
	private Color _canPlaceColor;
	[SerializeField]
	private Color _cannotPlaceColor;


	private void Reset()
	{
		_towerPreview = GetComponent<TowerPreview>();
		_canPlaceColor = new Color(0, 255, 0);
		_cannotPlaceColor = new Color(255, 0, 0);
	}

	private void Start()
	{
		_towerPreview.StateChanged += UpdateIndicator;
		UpdateIndicator();
	}

	private void OnDestroy()
	{
		_towerPreview.StateChanged -= UpdateIndicator;
	}

	private void UpdateIndicator()
	{
		if (_towerPreview.CanPlace)
			_placeIndicatorImg.color = _canPlaceColor;
		else
			_placeIndicatorImg.color = _cannotPlaceColor;
	}
}
