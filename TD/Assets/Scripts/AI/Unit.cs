using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public string name;
	public int health;
	public int damage;
	public float attackRange;
	public float aggroRange;
	public float attackSpeed;
	public bool alive = true;
	
	private int currentHealth;
	private float nextAttack = 0;
	private Vector3 startPosition;
	
	public void Start() {
		this.startPosition = transform.position;
		this.currentHealth = this.health;
	}
	
	public void Damage(int damage) {
		this.currentHealth -= damage;
		if(this.currentHealth <= 0) {
			this.Death();
		}
	}
	
	public void Attack(Unit target) {
		target.Damage(this.damage);
		this.nextAttack = Time.time + this.attackSpeed;
	}
	
	public int getCurrentHealth() {
		return this.currentHealth;	
	}
	
	private void Death() {
		alive = false;
		renderer.enabled = false;
	}
	
	public void Respawn() {
		alive = true;
		renderer.enabled = true;
		transform.position = startPosition;
	}
}
