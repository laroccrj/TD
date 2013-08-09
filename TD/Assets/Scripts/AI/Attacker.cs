using UnityEngine;
using System.Collections;

public class Attacker : Unit {
	public Waypoint[] waypoints;
	
	private WaypointTracker[] trackers;
	
	public override void Start (){
		this.trackers = new WaypointTracker[this.waypoints.Length];
		
		for(int i = 0; i < this.waypoints.Length; i++){
			this.trackers[i] = new WaypointTracker(this.waypoints[i]);	
		}
		
		Unit.attackers.Add(this);
		
		base.Start();
	}
	
	public override void Update() {
		
		base.Update();
		
		if(this.target == null) {
			foreach(WaypointTracker tracker in this.trackers){
				if(!tracker.reached) {
					Vector3 target = tracker.waypoint.transform.position;
				
					if(tracker.waypoint.direction == WaypointDitection.Z){
						target.Set(transform.position.x, transform.position.y, target.z);
					} else {
						target.Set(target.x, transform.position.y, transform.position.z);
					}
					
					transform.LookAt(target);
					transform.Translate(transform.InverseTransformDirection(Vector3.Normalize(target - transform.position) * moveSpeed * Time.deltaTime));
					
					if(Vector3.Distance(target, transform.position) <= 1) {
						tracker.reached = true;	
					}
					
					break;
				}
			}
		}
	}
	
	public override void Death (){
		Unit.attackers.Remove(this);
		Destroy(gameObject);		
	}
	
	protected override void FindTarget ()
	{
		this.targets.Clear();
		
		foreach(Unit target in Unit.towers)
			if(Vector3.Distance(transform.position, target.transform.position) <= this.aggroRange)
				this.targets.Add(target);
		
		base.FindTarget ();
	}
}
