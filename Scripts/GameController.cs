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

    public GameObject player;
    public GameObject starSheild;

    public Text scoreText;
    private int score;
    public Text winText;
    public bool winner;
    public bool winnerToggle;
    public Text creditText;
    public Text gameOverText;
    public Text restartText;
    public GameObject screenFlash;


    public AudioClip musicClip;
    public AudioSource musicSource;
    public AudioClip musicClip2;
    public AudioSource musicSource2;
    public AudioClip musicClip3;
    public AudioSource musicSource3;

    private bool gameOver;
    private bool restart;

    public GameObject background;
    BGScroller scroller;

    void Start()
    {
        winner = false;
        winnerToggle = true;
        gameOver = false;
        restart = false;
        winText.text = "";
        restartText.text = "";
        gameOverText.text = "";
        creditText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves ());

        musicSource.clip = musicClip;
        musicSource.playOnAwake = false;
        musicSource2.clip = musicClip2;
        musicSource2.playOnAwake = false;
        musicSource3.clip = musicClip3;

        
        scroller = background.GetComponent<BGScroller>();
    }

    private void Update()
    {
        if (starSheild.activeInHierarchy)
        {
            StartCoroutine(PowerDecline());
        }
        if (restart)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if(winner ==  true && winnerToggle == true)
        {
            musicSource3.Pause();
            musicSource.Play();
            winnerToggle = false;
        }
        if (winner == false && gameOver == true)
        {
            musicSource3.Pause();
            musicSource2.Play();
            winnerToggle = false;
            winner = true;
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void UpdateLose()
    {
        gameOver = true;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            StartCoroutine(WinAnimation());
            winText.text = "YOU WIN!";
            winner = true;
            creditText.text = "GAME MADE BY AUSTIN SAMBELLS";
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
                gameOverText.text = "GAME OVER";
                restartText.text = "Press (Space) to Restart";
                restart = true;
                break;
            }
        }
    }

    IEnumerator WinAnimation()
    {
        screenFlash.SetActive(true);
        player.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        scroller.scrollSpeed = -30;
        yield return new WaitForSeconds(10);
        screenFlash.SetActive(false);
    }

    IEnumerator PowerDecline()
    {
        yield return new WaitForSeconds(5);
        starSheild.SetActive(false);
    }
}
