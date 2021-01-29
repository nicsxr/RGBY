using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed;

    public GameObject bullet;
    public Transform shootPos;

    private float shootCooldownReset;
    public float shootCooldown;

    public Transform[] movePositions;
    private int randSpot;
    private float moveCountdown;
    public float moveCooldown;


    private void Start()
    {
        shootCooldownReset = shootCooldown;
        Physics2D.queriesStartInColliders = false;
        randSpot = Random.Range(0, movePositions.Length);
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePositions[randSpot].position, moveSpeed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 150);
        //Debug.DrawLine(transform.position, hitInfo.point, Color.red);
        if (hitInfo.collider.CompareTag("Player") && shootCooldownReset <= 0)
        {
            Debug.Log(hitInfo.collider);
            fireAtPlayer();
            shootCooldownReset = shootCooldown;
        }
        else
        {
        shootCooldownReset -= Time.deltaTime;
        }

        if(Vector2.Distance(transform.position, movePositions[randSpot].position) < 0.1f){
            if(moveCountdown <= 0){
                randSpot = Random.Range(0, movePositions.Length);
                moveCountdown = moveCooldown;
            }else{
                moveCountdown -= Time.deltaTime;
            }
        }
    }

    public void fireAtPlayer()
    {
        Instantiate(bullet, shootPos.position, transform.rotation);
    }
}