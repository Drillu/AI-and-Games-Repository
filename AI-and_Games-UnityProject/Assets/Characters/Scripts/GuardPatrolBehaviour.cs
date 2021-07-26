using UnityEngine;

public class GuardPatrolBehaviour : PatrolBehavior
{
	public float chasingRange = 0;
	public LayerMask chasingTargetLayer;
	public override void Act()
	{
		base.Act();
		if (Director.Instance.isPrequisitioning)
		{
			Agent player = PlayerInChasingRange();
			if (player)
			{
				GetComponent<Guard>().SwitchToPrequisitionBehaviour(player);
			}
		}
	}

	private Agent PlayerInChasingRange()
	{
		RaycastHit[] hitinfos = Physics.SphereCastAll(transform.position, chasingRange, transform.forward, 0, chasingTargetLayer);
		foreach (RaycastHit hitinfo in hitinfos)
		{
			if (hitinfo.collider.gameObject.GetComponent<Player>())
			{
				return hitinfo.collider.gameObject.GetComponent<Player>();
			}
		}
		return null;
	}
}