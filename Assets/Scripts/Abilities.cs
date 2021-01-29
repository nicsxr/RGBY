using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    private GameManager gm;
    private playerShooting ps;

    private float abCdCountdown;
    public float abilityCooldown;
    public KeyCode abKey;
    public int pId;
    private int playerNumber;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        ps = GetComponent<playerShooting>();

        if (gameObject.tag == "Player 1")
        {
            playerNumber = 1;
        }
        if (gameObject.tag == "Player 2")
        {
            playerNumber = 2;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(abKey))
        {
            if (abCdCountdown <= 0)
            {
                useAbility(pId);
                abCdCountdown = abilityCooldown;
            }
        }
        else
        {
            abCdCountdown -= Time.deltaTime;
        }
    }


    public void useAbility(int id)
    {
        switch (id)
        {
            case 1:
                if (playerNumber == 1)
                {
                    gm.p1Health += 20;
                }
                else if (playerNumber == 2)
                {
                    gm.p2Health += 20;
                }
                break;
            case 2:
                if (playerNumber == 1)
                {
                    gm.p1.transform.position += transform.right * 0.5f;
                }
                else if (playerNumber == 2)
                {
                    gm.p2.transform.position += transform.right * 0.5f;
                }
                break;
            case 3:
                StartCoroutine(powerUp());
                break;
            case 4:
                Instantiate(ps.abilityProjectile, ps.shootPoint.position + transform.right * 0.1f, transform.rotation);
                break;
        }
    }

    IEnumerator powerUp()
    {
        ps.bulletDamage = 30;
        yield return new WaitForSeconds(4);
        ps.bulletDamage = 15;
    }
}
