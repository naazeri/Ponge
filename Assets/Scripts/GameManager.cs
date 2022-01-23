using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Paddle playerPaddle;
    [SerializeField] private Paddle computerPaddle;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI computerScoreText;
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;

    private int playerScore;
    private int computerScore;

    private void Awake()
    {
        // Application.targetFrameRate = 60;
        FixPositions();
    }

    private void FixPositions()
    {
        ResponsiveHelper.FixScale(topWall, ScaleType.Width);
        ResponsiveHelper.FixScale(bottomWall, ScaleType.Width);
        ResponsiveHelper.FixScale(rightWall, ScaleType.Height);
        ResponsiveHelper.FixScale(leftWall, ScaleType.Height);

        ResponsiveHelper.SetPosition(topWall, VerticalAlign.Top, HorizontalAlign.Middle);
        ResponsiveHelper.SetPosition(bottomWall, VerticalAlign.Bottom, HorizontalAlign.Middle);
        ResponsiveHelper.SetPosition(rightWall, VerticalAlign.Middle, HorizontalAlign.Right);
        ResponsiveHelper.SetPosition(leftWall, VerticalAlign.Middle, HorizontalAlign.Left);
    }

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
