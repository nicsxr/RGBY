using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerShooting : MonoBehaviour
{
    public KeyCode shootBut;
    public Transform shootPoint;

    public int bulletDamage;
    
    //ATTACK SPEED
    private float shootCountdown;
    public float attackSpeed;



    public GameObject bullet;
    public GameObject abilityProjectile;

    private Animator anim;
    private AudioManager audioManager;


    void Start()
    {
        bulletDamage = 15;
        audioManager = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(shootBut) && shootCountdown <= 0)
        {
            anim.SetTrigger("Shoot");
            Instantiate(bullet, shootPoint.position, transform.rotation);
            audioManager.Shoot();
            shootCountdown = attackSpeed;
        }
        else
        {
            shootCountdown -= Time.deltaTime;
        }
    }
}
