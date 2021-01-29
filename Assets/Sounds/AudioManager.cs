using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip bulletCollisionSound;
    [SerializeField] private AudioClip healthPickupSound;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    public void GetDamaged(){
        audioSource.PlayOneShot(damageSound);
    }

    public void Shoot(){
        audioSource.PlayOneShot(shootSound);
    }

    public void bulletExplosion(){
        audioSource.PlayOneShot(bulletCollisionSound);
    }

    public void healthPickup(){
        audioSource.PlayOneShot(healthPickupSound);
    }
}
