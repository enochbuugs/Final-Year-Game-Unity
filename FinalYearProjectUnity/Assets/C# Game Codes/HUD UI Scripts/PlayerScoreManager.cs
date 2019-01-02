using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour {

    PlayerCarController pc;
    PlayerHealthBar phb;
    public Text scoreText;
    public float score = 1;
    public float currentScore;
    public float finalScore;
    public float maxScore = 1000f;
    public bool isRaceFinished;

    // Use this for initialization
    void Start()
    {
        SetScore();
    }

    // Update is called once per frame
    void Update()
    {
        ClampScore();
        DisplayScore();
        IncrementScore(5);
        GetFinalScore();
    }

    public void SetScore()
    {
        currentScore = score;
    }

    void ClampScore()
    {
        currentScore = Mathf.Clamp(currentScore, 0, Mathf.Infinity);
    }

    public void DisplayScore()
    {
        scoreText.text = "Score: " + (int)currentScore;
    }

    public void IncrementScore(float increaseRate)
    {
        pc = GetComponent<PlayerCarController>();

        if (pc.isCarMovingForward && !isRaceFinished)
        {
            currentScore += increaseRate * Time.deltaTime;
            isRaceFinished = false;
        }
    }

    void GetFinalScore()
    {
        pc = GetComponent<PlayerCarController>();

        if (pc.isCarMovingForward && isRaceFinished)
        {
            isRaceFinished = true;
            finalScore = currentScore;
        }
    }

    void WaitToIncreaseScoreAgain()
    {
        pc = GetComponent<PlayerCarController>();

        if (pc.isCarMovingForward)
        {
            currentScore++;
        }
    }

}
