using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour {

    // script references
    PlayerCarController pc;
    PlayerHealthBar phb;

    // score variables
    public Text scoreText;
    public float score = 1;
    public float currentScore;
    public float pausedScore;
    public float finalScore;
    public float maxScore = 1000f;

    // score penalty timers
    public float easyPenaltyTimer = 5f;
    public float meduimPenaltyTimer = 10f;
    public float hardPenaltyTimer = 15f;

    // booleans
    public bool isRaceFinished;
    public bool isScorePaused;

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
        pausedScore = Mathf.Clamp(pausedScore, 0, Mathf.Infinity);
    }

    public void DisplayScore()
    {
        if (isScorePaused)
        {
            scoreText.text = "Score: " + (int)pausedScore;
        }
        else
        {
            scoreText.text = "Score: " + (int)currentScore;
        }
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
}
