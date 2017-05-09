using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    public int attackDamage = 20;
    public float raycastDistance = 10f;

    private float yOffset = 8f;
    private EnemyHealth mainEnemyHealth;
    private GameObject enemy;
    private Vector3 playerOrigin;
    private Vector3 hitPoint;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        mainEnemyHealth = enemy.GetComponent<EnemyHealth>();
    }

	void Update()
    {
        playerOrigin = transform.position;
        playerOrigin.y += yOffset;

        Debug.DrawRay(playerOrigin, Vector3.back * raycastDistance);

        if (Input.GetMouseButtonDown(0) && playerRange())
        {
            print("Swish swosh");
            if(mainEnemyHealth.currentHealth > 0)
            {
                mainEnemyHealth.TakeDamage(attackDamage, hitPoint);
            }
        }
	}

    bool playerRange()
    {
        return Physics.Raycast(playerOrigin, Vector3.back, raycastDistance);
    }
}
