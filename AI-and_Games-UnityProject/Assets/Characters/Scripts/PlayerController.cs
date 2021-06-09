using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavagentMover))]
public class PlayerController : MonoBehaviour
{
	private NavagentMover _navagentMover;
	private NavagentMover navagentMover { get { if (!_navagentMover) { _navagentMover = GetComponent<NavagentMover>(); } return _navagentMover; } }
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (InputManager.Instance.IsMouseRightButtonDown)
		{
			Ray r = Camera.main.ScreenPointToRay(InputManager.Instance.MousePosition);
			if (Physics.Raycast(r, out RaycastHit raycastHit))
			{
				navagentMover.MoveToPosition(raycastHit.point);
			}
		}
	}


}
