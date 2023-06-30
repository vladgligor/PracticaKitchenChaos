using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
	[SerializeField] private PlatesCounter platesCounter;
	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform plateVisualPrefab;
	
	//private float plateOffset = 0.1f;
	private List<GameObject> plateVisualGameObjectList; 
	private void Start()
	{
		platesCounter.OnPlateSpawned += OnPlatesSpawned;
		platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
	}

	private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
	{
		GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
		plateVisualGameObjectList.Remove(plateGameObject);
		Destroy(plateGameObject);
	}

	private void Awake()
	{
		plateVisualGameObjectList = new List<GameObject>();
	} 

	private void OnPlatesSpawned(object sender, EventArgs e)
	{
		/*GameObject spawnPlate = Instantiate(plateVisual, counterTopPoint);
		spawnPlate.transform.localPosition = new Vector3(0, plateOffset * currentPlates.Count, 0);
		currentPlates.Add(spawnPlate);*/
		Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
		float plateOffsetY = .1f;
		plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
		plateVisualGameObjectList.Add(plateVisualTransform.gameObject);

	}
}
