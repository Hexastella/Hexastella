using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    internal int currentHealth;
    public Slider healthSlider;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    //Animator anim;
    AudioSource playerAudio;
    PlayerController playerController;
    bool isDead;
    //bool damaged;

    void Awake()
    {
        //anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerController = GetComponent <PlayerController> ();
    }

    void Update()
    {
        if (isDead && playerController)
            Destroy(GetComponent<PlayerController>());
    }

    public void TakeDamage(int amount)
    {
        //damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

    void Death()
    {
        isDead = true;

        //anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
}
