using UnityEngine;
using System.Collections;

public class WaypointTracker {

	public bool reached = false;
	public Waypoint waypoint;
	
	public WaypointTracker(Waypoint waypoint) {
		this.waypoint = waypoint;	
	}
}
