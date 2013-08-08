using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public string name;
	public int health;
	public int damage;
	public float attackRange;
	public float aggroRange;
	public float attackSpeed;
	public float moveSpeed;
	
	private Unit target;
	private int currentHealth;
	private float nextAttack = 0;
	
	public void Start() {
		this.currentHealth = this.health;
		this.OnStart();
	}
	
	public virtual void OnStart() {}
	
	public void Update() {
		
		this.OnUpdate();
		
		if(this.target == null) return;
		
		if(Vector3.Distance(transform.position, target.transform.position) < attackRange) {
			if(nextAttack < Time.time) {
				Attack(this.target);
			}
		} else {
				
		}
	}
	
	public virtual void OnUpdate() {}
	
	public void Damage(Unit attacker, int damage) {
		this.currentHealth -= damage;
		this.OnDamage(attacker, damage);
		if(this.currentHealth <= 0) {
			this.Death();
		}
	}
	
	public virtual void OnDamage(Unit attacker, int damage) {}
	
	public void Attack(Unit target) {
		target.Damage(this, this.damage);
		this.nextAttack = Time.time + this.attackSpeed;
		this.OnAttack(target);
	}
	
	public virtual void OnAttack(Unit target) {}
	
	public int getCurrentHealth() {
		return this.currentHealth;	
	}
	
	public virtual void Death() {}
}
