using UnityEngine;
using System.Collections;

public class Tower : Unit {

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.GetComponent<Attacker>()){
			this.targets.Add(collider.gameObject.GetComponent<Unit>());
		}
	}
	
	void OnTriggerExit(Collider collider) {
		if(collider.gameObject.GetComponent<Attacker>()){
			this.targets.Remove(collider.gameObject.GetComponent<Unit>());
		}
	}
}
