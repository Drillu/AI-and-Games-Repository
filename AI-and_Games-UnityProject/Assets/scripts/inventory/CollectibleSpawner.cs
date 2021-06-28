using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
	float minLimit = 10;
	float maxLimit = 600;
	[SerializeField] Collectible spawnee;
	[SerializeField] List<Transform> spawnPoints;
	[SerializeField] [Range(0, 20)] float interactRange;

	[Header("Spawner Config")]
	[SerializeField] bool respawnable;
	[SerializeField] float minRespawnInterval;
	[SerializeField] int amountToSpawn;

	private bool isSpawnedPickedup;

	private void Awake()
	{
		Spawn();
	}

	private void Spawn()
	{
		Collectible item = Instantiate(spawnee, GetRandomSpawnPoint().position, Quaternion.identity);
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
		return minRespawnInterval;
	}
	Transform GetRandomSpawnPoint()
	{
		return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		foreach (Transform spawnPoint in spawnPoints)
		{
			Gizmos.DrawWireSphere(spawnPoint.position, interactRange);
		}
	}

}
