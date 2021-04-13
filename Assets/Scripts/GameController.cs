using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject enemy;
    public GameObject asteroid;
    float asteroidPosY;
    float asteroidPosX;
    public float asteroidSpawnTime = 1.5f;
    public static int score;
    public static bool moveCamera = false;
    public Text scoreText;
    public Text finalScoreText; // Visible in canvasWin when level is completed
    public Canvas canvasEnd;
    public Canvas canvasWin;
    Animator canvasEndAnim;

    public enum GameStates { PLAY, END, BOSSFIGHT, WIN };
    public GameStates state;

    // Start is called before the first frame update
    void Start()
    {
        canvasEnd.enabled = false;
        canvasWin.enabled = false;
        canvasEndAnim = canvasEnd.GetComponent<Animator>();
        canvasEndAnim.enabled = false;
        state = GameStates.PLAY;
        score = 0;
        asteroidPosX = Random.Range(0f, 8f); // X position for asteroid spawning
        asteroidPosY = 6f; // Y position for asteroid spawning
        InvokeRepeating("InstantiateAsteroid", 0.5f, asteroidSpawnTime);
    }

    private void Update()
    {
        // Game over
        if (state == GameStates.END)
        {
            CancelInvoke();
            canvasEnd.enabled = true;
            canvasEndAnim.enabled = true;
            canvasEndAnim.Play("GameOver");
        }

        // Level completed
        if (state == GameStates.WIN)
        {
            CancelInvoke();
            canvasWin.enabled = true;
            finalScoreText.text = ("SCORE: " + score);
        }
    }
    void LateUpdate()
    {
        scoreText.text = "SCORE: " + score;
    }


    void InstantiateAsteroid()
    {
        Instantiate(asteroid, new Vector3(asteroidPosX, asteroidPosY, 0), Quaternion.identity);
    }

    public void ReloadScene()
    {
        SceneLoader.LoadLevel();
    }
}