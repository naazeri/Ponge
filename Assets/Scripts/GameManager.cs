using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum GameMode { OnePlayer, TwoPlayer }

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    private GameMode gameMode;

    // Paddles
    [SerializeField] private Paddle playerPaddle;
    [SerializeField] private Paddle computerPaddle;

    // Walls
    [SerializeField] private Wall topWall;
    [SerializeField] private Wall bottomWall;
    [SerializeField] private Wall rightWall;
    [SerializeField] private Wall leftWall;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI computerScoreText;

    // Screens
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject gameplayScreen;
    // [SerializeField] private GameObject onePlayerScreen;
    // [SerializeField] private GameObject twoPlayerScreen;
    [SerializeField] private GameObject aboutScreen;
    [SerializeField] private GameObject helpScreen;
    private GameObject currentScreen;

    // Buttons
    [SerializeField] private Button onePlayerBtn;
    [SerializeField] private Button twoPlayerBtn;
    [SerializeField] private Button aboutBtn;
    [SerializeField] private Button helpBtn;
    [SerializeField] private Button exitBtn;

    // Scores
    private int playerScore;
    private int computerScore;

    private void Awake()
    {
        ActivateScreen(mainMenuScreen);
        SetButotnsClickListener();
        FixPositions();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKey(KeyCode.R))
        {
            ResetRound();
        }
    }

    private void FixPositions()
    {
        // set Paddles position
        ResponsiveHelper.SetPosition(
            computerPaddle.gameObject,
            VerticalAlign.Middle,
            HorizontalAlign.Right,
            horizontalMargin: -computerPaddle.horizontalMargin
        );

        ResponsiveHelper.SetPosition(
            playerPaddle.gameObject,
            VerticalAlign.Middle,
            HorizontalAlign.Left,
            horizontalMargin: playerPaddle.horizontalMargin
        );

        // set Ball position
        ResponsiveHelper.SetPosition(ball.gameObject, VerticalAlign.Middle, HorizontalAlign.Middle);

        // match Walls scale to screen size
        ResponsiveHelper.FixScale(topWall.gameObject, ScaleType.Width);
        ResponsiveHelper.FixScale(bottomWall.gameObject, ScaleType.Width);
        ResponsiveHelper.FixScale(rightWall.gameObject, ScaleType.Height);
        ResponsiveHelper.FixScale(leftWall.gameObject, ScaleType.Height);

        // set Walls position around screen
        ResponsiveHelper.SetPosition(
            topWall.gameObject,
            VerticalAlign.Top,
            HorizontalAlign.Middle,
            verticalMargin: -topWall.verticalMargin
        );

        ResponsiveHelper.SetPosition(
            bottomWall.gameObject,
            VerticalAlign.Bottom,
            HorizontalAlign.Middle,
            verticalMargin: bottomWall.verticalMargin
        );

        ResponsiveHelper.SetPosition(
            rightWall.gameObject,
            VerticalAlign.Middle,
            HorizontalAlign.Right,
            horizontalMargin: -rightWall.horizontalMargin
        );

        ResponsiveHelper.SetPosition(
            leftWall.gameObject,
            VerticalAlign.Middle,
            HorizontalAlign.Left,
            horizontalMargin: leftWall.horizontalMargin
        );
    }

    private void SetButotnsClickListener()
    {
        onePlayerBtn.onClick.AddListener(OnOnePlayerClicked);
        twoPlayerBtn.onClick.AddListener(OnTwoPlayerClicked);
        aboutBtn.onClick.AddListener(OnAboutClicked);
        helpBtn.onClick.AddListener(OnHelpClicked);
        exitBtn.onClick.AddListener(OnExitClicked);
    }

    // recieve scoreTrigger
    public void PlayerGotScores()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();

        PlayScoreSound();
        ResetRound();
    }

    public void ComputerGotScore()
    {
        computerScore++;
        computerScoreText.text = computerScore.ToString();

        PlayScoreSound();
        ResetRound();
    }

    public void ResetRound()
    {
        playerPaddle.ResetPosition();
        computerPaddle.ResetPosition();

        ball.ResetPosition();
        ball.AddStartingForce();
    }
    private void PlayScoreSound()
    {
        // AudioManager.instance.PlayExplosionSound();
        var audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayExplosionSound();
    }

    private void ActivateScreen(GameObject screen)
    {
        DisableAllScreens();
        screen.SetActive(true);
        currentScreen = screen;
    }

    private void DisableAllScreens()
    {
        mainMenuScreen.SetActive(false);
        gameplayScreen.SetActive(false);
        aboutScreen.SetActive(false);
        helpScreen.SetActive(false);
    }

    public void OnOnePlayerClicked()
    {
        gameMode = GameMode.OnePlayer;
        ActivateScreen(gameplayScreen);
    }

    public void OnTwoPlayerClicked()
    {
        gameMode = GameMode.TwoPlayer;
        ActivateScreen(gameplayScreen);
    }

    public void OnAboutClicked()
    {
        ActivateScreen(aboutScreen);
    }

    public void OnHelpClicked()
    {
        ActivateScreen(helpScreen);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

}
