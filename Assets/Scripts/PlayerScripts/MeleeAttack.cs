using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    public int attackDamage = 20;
    private EnemyHealth mainEnemyHealth;
    private GameObject enemy;
    private Vector3 hitPoint;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        mainEnemyHealth = enemy.GetComponent<EnemyHealth>();
    }

	void Update()
    {
        
	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0) && other.GetComponent<EnemyHealth>())
        {
            if (mainEnemyHealth.currentHealth > 0)
            {
                Debug.Log("Enemy hit!");
                mainEnemyHealth.TakeDamage(attackDamage, hitPoint);
            }
        }
    }
}
