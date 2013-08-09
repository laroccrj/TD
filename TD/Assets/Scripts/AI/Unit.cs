using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public static CustomArrayList<Unit> towers = new CustomArrayList<Unit>();
	public static CustomArrayList<Unit> attackers = new CustomArrayList<Unit>();
	
	public string unitName;
	public int maxHealth;
	public int damage;
	public float attackRange;
	public float aggroRange;
	public float attackSpeed;
	public float moveSpeed;
	
	private bool alive = true;
	protected Unit target;
	public int currentHealth;
	private float nextAttack = 0;
	protected CustomArrayList<Unit> targets = new CustomArrayList<Unit>();
	
	public virtual void Start() {
		this.currentHealth = this.maxHealth;
	}
	
	
	public virtual void Update() {
		if(!alive) return;
		
		if(this.target == null || !this.targets.Contains(target)) {
			this.FindTarget();
		}
		
		if(this.target != null) {
			transform.LookAt(this.target.transform.position);
			
			if(Vector3.Distance(transform.position, target.transform.position) < attackRange) {
				if(nextAttack < Time.time) {
					Attack(this.target);
				}
			} else {
				transform.Translate(transform.InverseTransformDirection(Vector3.Normalize(target.transform.position - transform.position) * moveSpeed * Time.deltaTime));
			}
		}
	}
	
	protected virtual void FindTarget() {
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
