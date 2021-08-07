using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class GameLoader : MonoBehaviour
{
	[SerializeField] AssetReference mainMenuRef;
	private void Start()
	{
		Addressables.LoadSceneAsync(mainMenuRef, UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
