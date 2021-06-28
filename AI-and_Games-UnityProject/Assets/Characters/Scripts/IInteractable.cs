using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
	void Interact(GameObject initiater);
	float GetInteractRange();
	Vector3 GetInteractCenter();
}
