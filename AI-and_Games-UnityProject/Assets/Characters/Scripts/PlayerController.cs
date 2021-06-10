using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavagentMover))]
public class PlayerController : MonoBehaviour
{
	private IInteractable interactTarget = null;
	private NavagentMover _navagentMover;
	private NavagentMover navagentMover { get { if (!_navagentMover) { _navagentMover = GetComponent<NavagentMover>(); } return _navagentMover; } }

	// Update is called once per frame
	void Update()
	{
		CheckUserInput();
		CheckInteractable();
	}

	private void CheckUserInput()
	{
		Ray r = GetMouseRay();
		bool hitSomething = Physics.Raycast(r, out RaycastHit raycastHit);
		if (hitSomething)
		{
			IInteractable interactable = raycastHit.transform.GetComponent<IInteractable>();
			if (interactable != null)
			{
				PlayerMouseOverInteractable(interactable);
			}
			else
			{
				PlayerMouseOverNavigatableArea(raycastHit.point);
			}
		}
	}

	private void PlayerMouseOverNavigatableArea(Vector3 point)
	{
		if (InputManager.Instance.IsMouseRightButtonDown)
		{
			interactTarget = null;
			navagentMover.MoveToPosition(point);
		}
	}

	private void PlayerMouseOverInteractable(IInteractable interactable)
	{
		if (InputManager.Instance.IsMouseRightButtonDown)
		{
			interactTarget = interactable;
		}
	}
	private void CheckInteractable()
	{
		if (interactTarget != null)
		{
			if (IsInInteractRadius(interactTarget))
			{
				navagentMover.StopMoving();
				interactTarget.Interact(this.gameObject);
				interactTarget = null;
			}
			else
			{
				navagentMover.MoveToPosition(interactTarget.GetPosition());
			}
		}
	}
	private bool IsInInteractRadius(IInteractable go)
	{
		return Vector3.Distance(transform.position, go.GetPosition()) <= go.GetInteractRange();
	}

	private static void TalkToPrisoner(Prisoner p)
	{
		Director.Instance.TalkToPrisoner(p);
	}

	private static Ray GetMouseRay()
	{
		return Camera.main.ScreenPointToRay(InputManager.Instance.MousePosition);
	}
}
