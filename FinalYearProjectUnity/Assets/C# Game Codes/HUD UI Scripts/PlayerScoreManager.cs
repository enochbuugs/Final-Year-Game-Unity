using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour {

    PlayerCarController pc;
    PlayerHealthBar phb;
    public Text scoreText;
    float score = 1;
    public float currentScore;
    public float finalScore;
    private float maxScore;
    public bool isRaceFinished;

    // Use this for initialization
    void Start()
    {
        SetScore();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();
        IncrementScore(5);
        DecreaseScore();
        GetFinalScore();
    }

    void SetScore()
    {
        currentScore = score;
        currentScore = Mathf.Clamp(currentScore, 0, 1000);
    }

    void DisplayScore()
    {
        scoreText.text = "Score: " + (int)currentScore;
    }

    void IncrementScore(float increaseRate)
    {
        pc = GetComponent<PlayerCarController>();

        if (pc.isCarMovingForward && !isRaceFinished)
        {
            currentScore += increaseRate * Time.deltaTime;
            isRaceFinished = false;
        }
    }

    void DecreaseScore()
    {
        phb = GetComponent<PlayerHealthBar>();

        if (phb.canTakeDamage)
        {
            currentScore --;
            //phb.canTakeDamage = true;
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
