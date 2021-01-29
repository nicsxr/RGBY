using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;
    private GameManager gm;
    public bool isAbilityProjectile;

    public int abilityProjectileDamage;

    private AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 2.5f);
        gm = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAbilityProjectile == false)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Player 1"))
            {
                gm.p1Damaged(gm.p2.GetComponent<playerShooting>().bulletDamage);
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Player 2"))
            {
                gm.p2Damaged(gm.p1.GetComponent<playerShooting>().bulletDamage);
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<bulletScript>().isAbilityProjectile == false)
            {
                Destroy(collision.gameObject);
                audioManager.bulletExplosion();
                gm.spawnExplosion(transform.position);
                Destroy(gameObject);
            }
        }
        if (isAbilityProjectile == true)
        {
            if (collision.gameObject.CompareTag("Player 1"))
            {
                gm.p1Damaged(abilityProjectileDamage);
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Player 2"))
            {
                gm.p2Damaged(abilityProjectileDamage);
                Destroy(gameObject);
            }
            if(collision.gameObject.CompareTag("Bullet")){
                Destroy(collision.gameObject);
            }
        }
    }
}
