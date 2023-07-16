using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameVersionText;
    [SerializeField] private Ball ball;

    // Paddles
    [SerializeField] private Paddle playerPaddleLeft;
    [SerializeField] private Paddle playerPaddleRight;
    [SerializeField] private Paddle computerPaddle;
    private Paddle rightPaddle;

    // Walls
    [SerializeField] private Wall topWall;
    [SerializeField] private Wall bottomWall;
    [SerializeField] private Wall rightWall;
    [SerializeField] private Wall leftWall;
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text computerScoreText;

    // Screens
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private GameObject aboutScreen;
    [SerializeField] private GameObject helpScreen;
    private GameObject currentScreen;

    // Buttons
    [SerializeField] private GameObject touchButtons;
    [SerializeField] private Button onePlayerBtn;
    [SerializeField] private Button twoPlayerBtn;
    [SerializeField] private Button aboutBtn;
    [SerializeField] private Button helpBtn;
    [SerializeField] private Button exitBtn;

    // Scores
    private int playerScore;
    private int computerScore;
    private bool isGameplaying = false;

    private void Awake()
    {
        gameVersionText.text = "Version: " + Application.version;

        ActivateScreen(mainMenuScreen);
        SetMenuButtonsClickListener();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameplaying)
            {
                isGameplaying = false;
                ResetScore();
            }

            OnBackToMenuClicked();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (isGameplaying)
                ResetRound();
        }
    }

    private void InitGamePlaySceneObjects()
    {
        bool activateStatus = Application.isMobilePlatform;
        touchButtons.SetActive(activateStatus);

        // set Ball position
        ResponsiveHelper.SetPosition(ball.gameObject, VerticalAlign.Middle, HorizontalAlign.Middle);

        // set Left Paddles position
        ResponsiveHelper.SetPosition(
            playerPaddleLeft.gameObject,
            VerticalAlign.Middle,
            HorizontalAlign.Left,
            horizontalMargin: playerPaddleLeft.horizontalMargin
        );

        ResponsiveHelper.SetPosition(
            rightPaddle.gameObject,
            VerticalAlign.Middle,
            HorizontalAlign.Right,
            horizontalMargin: -rightPaddle.horizontalMargin
        );

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

    private void SetMenuButtonsClickListener()
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

    // recieve scoreTrigger
    public void ComputerGotScore()
    {
        computerScore++;
        computerScoreText.text = computerScore.ToString();

        PlayScoreSound();
        ResetRound();
    }

    public void ResetRound()
    {
        playerPaddleLeft.ResetPosition();
        rightPaddle.ResetPosition();
        ball.Reset();
        ball.AddStartingForce();
    }

    private void ResetScore()
    {
        playerScore = 0;
        computerScore = 0;

        playerScoreText.text = playerScore.ToString();
        computerScoreText.text = computerScore.ToString();
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

    private void SetGameMode(GameMode gameMode)
    {
        Configs.gameMode = gameMode;

        playerPaddleLeft.gameObject.SetActive(true);

        // set Right Paddles position
        rightPaddle = Configs.gameMode == GameMode.OnePlayer ? computerPaddle : playerPaddleRight;

        rightPaddle.gameObject.SetActive(true);
    }

    private void InitGamePlay(GameMode gameMode)
    {
        SetGameMode(gameMode);
        InitGamePlaySceneObjects();
        ActivateScreen(gameplayScreen);
        isGameplaying = true;

        if (!Application.isEditor && !Application.isMobilePlatform)
            CursorHelper.SetVisible(false);
    }

    public void OnOnePlayerClicked()
    {
        InitGamePlay(GameMode.OnePlayer);
    }

    public void OnTwoPlayerClicked()
    {
        InitGamePlay(GameMode.TwoPlayer);
    }

    public void OnAboutClicked()
    {
        ActivateScreen(aboutScreen);
    }

    public void OnHelpClicked()
    {
        ActivateScreen(helpScreen);
    }

    public void OpenSiteUrl()
    {
        Application.OpenURL("https://naazeri.ir/");
    }

    public void OnBackToMenuClicked()
    {
        if (!currentScreen.Equals(mainMenuScreen))
            ActivateScreen(mainMenuScreen);
        else
            Application.Quit();
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

}
