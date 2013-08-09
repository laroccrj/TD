using UnityEngine;
using System.Collections;

public class Tower : Unit {

	public override void Start ()
	{
		Unit.towers.Add(this);
		base.Start ();
	}
	
	protected override void FindTarget ()
	{
		this.targets.Clear();
		
		foreach(Unit target in Unit.attackers)
			if(Vector3.Distance(transform.position, target.transform.position) <= this.aggroRange)
				this.targets.Add(target);
		
		base.FindTarget ();
	}
}
