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
				Prisoner p = raycastHit.transform.GetComponent<Prisoner>();
				if (p)
				{
					UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
					UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(null, p.name, p.Speach);
				}
				navagentMover.MoveToPosition(raycastHit.point);
			}
		}
	}


}
