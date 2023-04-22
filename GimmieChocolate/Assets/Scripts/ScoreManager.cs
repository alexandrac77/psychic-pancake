using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI inputScore; //int.Parse(inputScore.text)
    [SerializeField] private TMP_InputField inputName;
    //[SerializeField] private TMP_InputField inputScore;
    [SerializeField] private TMP_Text score;

    public UnityEvent<string, int> submitScoreEvent;

    private void Start()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            // set high score to saved value
            score.text = PlayerPrefs.GetInt("score").ToString();
        }
        else
        {
            score.text = 0.ToString();
        }
    }
    public void SubmitScore()
    {
        //score.text = PlayerPrefs.GetInt("score").ToString();
        // Get reference to input name & score.
        // Leaderboard is listening for event that contains string & int.
        // Event called when submit button is clicked.
        submitScoreEvent.Invoke(inputName.text, int.Parse(score.text));
            //int.Parse(inputScore.text));
    }
}
