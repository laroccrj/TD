using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public string unitName;
	public int health;
	public int damage;
	public float attackRange;
	public float aggroRange;
	public float attackSpeed;
	public float moveSpeed;
	
	private Unit target;
	private int currentHealth;
	private float nextAttack = 0;
	protected CustomArrayList<Unit> targets = new CustomArrayList<Unit>();
	
	public void Start() {
		this.currentHealth = this.health;
		
		SphereCollider collider = gameObject.GetComponent<SphereCollider>();
		collider.radius = aggroRange;
		
		this.OnStart();
	}
	
	public virtual void OnStart() {}
	
	public void Update() {
		
		this.OnUpdate();
		
		if(this.target == null || !this.targets.Contains(target)) {
			this.FindTarget();
			return;
		}
		
		transform.LookAt(this.target.transform.position);
		
		if(Vector3.Distance(transform.position, target.transform.position) < attackRange) {
			if(nextAttack < Time.time) {
				Attack(this.target);
			}
		} else {
			transform.position = Vector3.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
		}
	}
	
	public virtual void OnUpdate() {}
	
	private void FindTarget() {
		Unit closest = null;
		float distance = 0;
		
		foreach(Unit target in this.targets) {
			if(closest == null){
				closest = target;
				distance = Vector3.Distance(transform.position, closest.transform.position);
			}
			else if(Vector3.Distance(transform.position, target.transform.position) < distance) {
				closest = target;
				distance = Vector3.Distance(transform.position, closest.transform.position);
			}
		}
		
		this.target = closest;
	}
	
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
