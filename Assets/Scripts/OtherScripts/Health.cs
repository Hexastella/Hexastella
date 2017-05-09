using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;

    public AudioClip deathClip;

    bool isDead;

    Animator anim;

    void Start () {
        anim = GetComponent<Animator>();

        currentHealth = startingHealth;
    }

	void Update () {
		
	}

    public virtual void TakeDamage()
    {

    }

    public virtual void Death()
    {

    }
}
