using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
	public event EventHandler OnPlateSpawned;
	public event EventHandler OnPlateRemoved;

	[SerializeField] private KitchenObjectSO PlateKitchenObjectSO;
	private float spawnPlateTimer;
	private const float spawnPlateTimerMax = 4f;
	private int platesSpawnedAmount;
	private int platesSpawnedAmountMax = 4;

	void Update()
	{
		spawnPlateTimer += Time.deltaTime;
		if (spawnPlateTimer > spawnPlateTimerMax /*&& stackedPlates < stackedPlatesMax*/)
		{
			spawnPlateTimer = 0f;
			//stackedPlates++;
			//OnPlateSpawned?.Invoke(this, EventArgs.Empty);
			if (platesSpawnedAmount < platesSpawnedAmountMax)
			{
				platesSpawnedAmount++;
				OnPlateSpawned?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public override void Interact(Player player)
	{
		if (!player.HasKitchenObject())
		{
			if (platesSpawnedAmount > 0)
			{
				platesSpawnedAmount--;
				KitchenObject.SpawnKitchenObject(PlateKitchenObjectSO, player);
				OnPlateRemoved?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
