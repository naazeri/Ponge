using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle playerPaddle;
    public Paddle computerPaddle;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI computerScoreText;

    private int playerScore;
    private int computerScore;

    public void PlayerGotScores()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();

        ResetRound();
    }

    public void ComputerGotScore()
    {
        computerScore++;
        computerScoreText.text = computerScore.ToString();

        ResetRound();
    }

    private void ResetRound()
    {
        playerPaddle.ResetPosition();
        computerPaddle.ResetPosition();

        ball.ResetPosition();
        ball.AddStartingForce();
    }
}
