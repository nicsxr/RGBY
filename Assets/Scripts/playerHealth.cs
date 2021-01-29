using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    
    private Animator anim;
    private GameManager gm;
    private AudioManager audioManager;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    IEnumerator restScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void playerDie()
    {
        anim.SetTrigger("Die");
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<playerMovement>().enabled = false;
        GetComponent<playerShooting>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            if(gameObject.CompareTag("Player 1"))
            {
                gm.healthPickup(1);
                audioManager.healthPickup();
                Destroy(collision.gameObject);
            }
            if(gameObject.CompareTag("Player 2"))
            {
                gm.healthPickup(2);
                audioManager.healthPickup();
                Destroy(collision.gameObject);
            }
        }
        if(collision.gameObject.CompareTag("DeathTrap")){
            if(gameObject.CompareTag("Player 1"))
            {
                gm.p1Damaged(1000);
                
            }
            if(gameObject.CompareTag("Player 2"))
            {
                gm.p2Damaged(1000);
            }
        }
    }

}
