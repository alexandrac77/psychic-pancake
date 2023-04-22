using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Collectibles : MonoBehaviour
{ 
     
    public static int collectCounter = 0;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private AudioSource collectSfx;
    public int highScore;
    public int score;

    private void Start()
    {
        // if high score is saved in playerprefs.
        if(PlayerPrefs.HasKey("highScore"))
        {
            // set high score to saved value
            highScore = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            highScore = 0;
        }
    }

    public void Update()
    {
        highscoreText.text = "High Score: " + highScore.ToString();
        scoreText.text = "Candy Collected: " + collectCounter.ToString();

        SaveScoreValue();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chocolate"))
        {
            collectSfx.Play();
            Destroy(collision.gameObject);
            collectCounter++;
            //Debug.Log("chocolate: " + collectCounter);
            PlayerPrefs.SetInt("score", collectCounter);   
        }
    }

    public void SaveScoreValue()
    {
        if(PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", collectCounter);
        }
    }
}
