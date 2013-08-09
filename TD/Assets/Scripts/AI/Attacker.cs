using UnityEngine;
using System.Collections;

public class Attacker : Unit {
	public Waypoint[] waypoints;
	
	private WaypointTracker[] trackers;
	
	public override void OnStart() {
		
		this.trackers = new WaypointTracker[this.waypoints.Length];
		
		for(int i = 0; i < this.waypoints.Length; i++){
			this.trackers[i] = new WaypointTracker(this.waypoints[i]);	
		}
	}
	
	public override void OnUpdate() {
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
}
