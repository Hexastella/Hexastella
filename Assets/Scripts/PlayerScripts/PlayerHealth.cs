using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
	// Health Slider Component
    public Slider healthSlider;
	// Damage Image Component
    public Image damageImage;
	// Set the death clip of the player
    public AudioClip deathClip;
	// Set the flash speed of the damageImage
    public float flashSpeed = 20f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        GameManager.instance.playerLose = true;

        isDead = true;

        //anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        //playerMovement.enabled = false;
    }
}
