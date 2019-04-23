using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeAttackController : MonoBehaviour

{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    //testing
    public float currentTime;
    public float startingTime;

    public Text restartText;
    public Text gameOverText;
    public Text winText;
    //testing
    public Text countdownText;
    public Text timeText;

    public AudioClip losing;
    public AudioClip victory;

    //testing
    public BGScroller Speed;
    public BGScroller PRSpeed;


    private bool gameOver;
    private bool restart;
    private bool start;

    private int score;

    void Start()
    {

        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        timeText.text = "";

        UpdateScore();
        StartCoroutine(SpawnWaves());
        //testing
        currentTime = startingTime;



        if (gameOver)
        {
            gameOver = false;
            GameOver();

            gameOver = true;
            UpdateScore();
        }
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("TimeAttack");
            }
        }

        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;

            gameOver = true;
        }


    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'G' to Restart!";
                timeText.text = "Press 'T' for Time Attack!";
                restart = true;
                break;
            }

        }
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        if (currentTime <= 0)
        {
            winText.text = "You Win!";
            gameOverText.text = "GAME CREATED BY ROBYN MUND";
            gameOver = true;
            restart = true;

            //testing
            Speed.scrollSpeed = -5.0f;
            PRSpeed.scrollSpeed = -10.0f;


            AudioSource audio = GetComponent<AudioSource>();
            audio.Stop(); audio.clip = victory;
            audio.Play();
        }

    }
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;

        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop(); audio.clip = losing;
        audio.Play();
    }

}
