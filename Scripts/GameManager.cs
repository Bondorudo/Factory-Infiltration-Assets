using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject player;
    private UI_Script uiScript;

    private Vector2 spawnPosTop;
    private Vector2 spawnPosBot;

    private float xPosition = 10f;
    [SerializeField] private float invokeStart = 2;
    [SerializeField] private float startMinRepeat = 2f;
    private float minRepeat;
    [SerializeField] private float startMaxRepeat = 2.5f;
    private float maxRepeat;
    [SerializeField] private float modulo = 5;

    private float topBoundMax = 8f;
    private float topBoundMin = 3f;

    private float botBoundMax = 12f;
    private float botBoundMin = 13f;


    private float timer;
    private float showTimer;

    private bool pauseGame;
    private bool canIncreaseDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
        pauseGame = false;
        maxRepeat = startMaxRepeat;
        minRepeat = startMinRepeat;
        canIncreaseDifficulty = true;
        InvokeRepeating("InstantiateObstacles", invokeStart, Random.Range(minRepeat, maxRepeat));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = true;
            uiScript.PauseMenu();
        }
        if (pauseGame == true)
        {
            Time.timeScale = 0;
        }
        else if (pauseGame == false)
        {
            Time.timeScale = 1;
        }

        IncrementTimer();
        IncreaseDifficulty();
    }

    private void InstantiateObstacles()
    {
        spawnPosTop = new Vector2(xPosition, Random.Range(topBoundMin, topBoundMax));
        spawnPosBot = new Vector2(xPosition, spawnPosTop.y - Random.Range(botBoundMin, botBoundMax));

        // Instantiate obstacles at a random height.
        Instantiate(obstaclePrefab, spawnPosTop, Quaternion.identity);
        Instantiate(obstaclePrefab, spawnPosBot, Quaternion.identity);
    }

    public void IncrementTimer()
    {
        if (pauseGame == false)
        {
            timer += Time.deltaTime;
            showTimer = (float)Math.Round(timer, 2);
            uiScript.scoreText.text = "timer: " + showTimer;
        }
    }

    public void IncreaseDifficulty()
    {
       
        if (showTimer % modulo == 0 && canIncreaseDifficulty == true && minRepeat >= 0.7f)
        {
            maxRepeat -= 0.1f;
            minRepeat -= 0.1f;
            canIncreaseDifficulty = false;
        }
        else if (showTimer % modulo != 0)
        {
            canIncreaseDifficulty = true;
        }
    }

    public void PauseMenu()
    {
        pauseGame = true;
        uiScript.PauseMenu();
    }

    public void GameOver()
    {
        player.SetActive(false);
        pauseGame = true;
        uiScript.GameOver();
    }

    public void ContinueButton()
    {
        pauseGame = false;
        uiScript.ContinueButton();
    }
    public void RestartButton()
    {
        player.SetActive(true);
        uiScript.RestartButton();
    }

    public void QuitToMenu()
    {
        uiScript.QuitToMenu();
    }
}
