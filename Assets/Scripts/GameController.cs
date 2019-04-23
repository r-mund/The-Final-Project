using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
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

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;

    //testing
    public Text countdownText;
    public Text timeText;

    public AudioClip losing;
    public AudioClip victory;

    //testing
    public Mover astro1;
    public Mover astro2;
    public Mover astro3;
    public Mover enemy4;

    public BGScroller Speed;
    public BGScroller PRSpeed;


    private bool gameOver;
    private bool restart;
    private bool start;
    private bool soundVic = false;


    private int score;

    void Start()
    {

        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        timeText.text = "";
        score = 0;
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
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "SampleScene")
        {
            if (restart)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }

        if (sceneName == "SampleScene")
        {
            if (restart)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }

        if (sceneName == "TimeAttack")
        {
            if (restart)
            {

                if (Input.GetKeyDown(KeyCode.G))
                {
                    SceneManager.LoadScene("TimeAttack");
                }
            }
        }

        if (sceneName == "TimeAttack")
        {
            if (restart)
            {

                if (Input.GetKeyDown(KeyCode.M))
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }

        if (sceneName == "HardMode")
        {
            if (restart)
            {

                if (Input.GetKeyDown(KeyCode.G))
                {
                    SceneManager.LoadScene("HardMode");
                }
            }
        }

        if (sceneName == "HardMode")
        {
            if (restart)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }



        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }

        if (sceneName == "TimeAttack")
        {
            if (!soundVic)
            {
                ScoreText.text = "Points: " + score;

                if (currentTime <= 0)
                {
                    winText.text = "Time's Up!";
                    gameOverText.text = "GAME CREATED BY ROBYN MUND";
                    gameOver = true;
                    restart = true;

                    //testing
                    Speed.scrollSpeed = -5.0f;
                    PRSpeed.scrollSpeed = -10.0f;

                    soundVic = true;
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Stop(); audio.clip = victory;
                    audio.loop = false;
                    audio.Play();
                }
            }
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
                    timeText.text = "Press 'M' for Main Menu!";
                    restart = true;
                    gameOver = true;
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
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            ScoreText.text = "Points: " + score;
            if (sceneName == "SampleScene")
            {
                ScoreText.text = "Points: " + score;
                if (score >= 100)
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
                    audio.loop = false;
                    audio.Play();
                }
            }

            if (sceneName == "HardMode")
            {
                astro1.speed = -5.0f;
                astro2.speed = -10.0f;
                astro3.speed = -15.0f;
                enemy4.speed = -10.0f;

                ScoreText.text = "Points: " + score;

                if (score >= 200)
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
                    audio.loop = false;
                    audio.Play();
                }
            }

        }
        public void GameOver()
        {
            gameOverText.text = "Game Over!";
            gameOver = true;

            AudioSource audio = GetComponent<AudioSource>();
            audio.Stop(); audio.clip = losing;
            audio.loop = false;
            audio.Play();
        }

    }