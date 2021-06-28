using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
	[SerializeField] Collectible spawnee;
	[SerializeField] Transform spawnPosition;
	[SerializeField] bool respawnable;
	[SerializeField] float respawnInterval;
	[SerializeField] int amountToSpawn;

	private bool isSpawnedPickedup;

	private void Awake()
	{
		Spawn();
	}

	private void Spawn()
	{
		Collectible item = Instantiate(spawnee, spawnPosition.position, Quaternion.identity);
		item.transform.parent = this.transform;
		item.Initialize(OnSpawneePickedUp);
		isSpawnedPickedup = false;
	}
	private void OnSpawneePickedUp(Collectible obj)
	{
		isSpawnedPickedup = true;
		StartCoroutine(RespawnCR());
	}

	IEnumerator RespawnCR()
	{
		yield return new WaitForSeconds(GetNextRespawnTimer());
		Spawn();
	}

	float GetNextRespawnTimer()
	{
		return respawnInterval;
	}
}
