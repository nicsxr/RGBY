using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject charSelectPanel;

    public TextMeshProUGUI roundTimeText;
    private float roundLength;
    public TextMeshProUGUI maxScoreText;
    private int maxScore;

    public Image p1Img;
    public Image p2Img;
    public Sprite[] characterSprites;

    private void Awake()
    {
        Time.timeScale = 1;
        roundLength = 60;
        maxScore = 5;
        PlayerPrefs.SetInt("p1Char", 0);
        PlayerPrefs.SetInt("p2Char", 0);
    }
    private void Update()
    {
        roundTimeText.SetText(roundLength.ToString());
        maxScoreText.SetText(maxScore.ToString());
    }
    
    // MATCH SETTINGS
    public void addRoundTime()
    {
        if(roundLength < 180)
        {
            roundLength += 5;
        }
        else
        {
            roundLength = 180;
        }
    }
    public void substractRoundTime()
    {
        if(roundLength > 30)
        {
            roundLength -= 5;
        }
        else
        {
            roundLength = 30;
        }
    }

    public void changeMaxScore(int op)
    {
        if(op == 1)
        {
            maxScore++;
        }
        if(op == 2 && maxScore > 1)
        {
            maxScore--;
        }
    }

    public void pressFight()
    {
        PlayerPrefs.SetFloat("roundLength", roundLength);
        PlayerPrefs.SetInt("maxScore", maxScore);
        PlayerPrefs.DeleteKey("p1Score");
        PlayerPrefs.DeleteKey("p2Score");
        SceneManager.LoadScene("SampleScene");
    }

    public void p1Selection(int id)
    {
        switch (id)
        {
            case 0:
                PlayerPrefs.SetInt("p1Char", 0);
                p1Img.overrideSprite = characterSprites[0];
                break;
            case 1:
                PlayerPrefs.SetInt("p1Char", 1);
                p1Img.overrideSprite = characterSprites[1];
                break;
            case 2:
                PlayerPrefs.SetInt("p1Char", 2);
                p1Img.overrideSprite = characterSprites[2];
                break;
            case 3:
                PlayerPrefs.SetInt("p1Char", 3);
                p1Img.overrideSprite = characterSprites[3];
                break;
        }

    }

    public void p2Selection(int id)
    {
        switch (id)
        {
            case 0:
                PlayerPrefs.SetInt("p2Char", 0);
                p2Img.overrideSprite = characterSprites[0];
                break;
            case 1:
                PlayerPrefs.SetInt("p2Char", 1);
                p2Img.overrideSprite = characterSprites[1];
                break;
            case 2:
                PlayerPrefs.SetInt("p2Char", 2);
                p2Img.overrideSprite = characterSprites[2];
                break;
            case 3:
                PlayerPrefs.SetInt("p2Char", 3);
                p2Img.overrideSprite = characterSprites[3];
                break;
        }
    }

    public void goToSelection()
    {
        startPanel.SetActive(false);
        charSelectPanel.SetActive(true);
    }
    
}
