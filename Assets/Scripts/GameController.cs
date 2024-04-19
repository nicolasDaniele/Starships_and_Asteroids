using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public static int score;
    public static bool moveCamera;
    public Text scoreText;
    public Text finalScoreText; // Visible in canvasWin when level is completed
    public Canvas canvasTutorial;
    public Canvas canvasEnd;
    public Canvas canvasWin;
    public Canvas canvasPause;
    Animator canvasEndAnim;

    public enum GameStates { PLAY, END, BOSSFIGHT, WIN };
    public GameStates state;

    void Start()
    {
        canvasEnd.enabled = false;
        canvasWin.enabled = false;
        moveCamera = false;
        canvasEndAnim = canvasEnd.GetComponent<Animator>();
        canvasEndAnim.enabled = false;
        canvasPause.enabled = false;
        state = GameStates.PLAY;
        score = 0;
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

    public void ReloadScene()
    {
        SceneLoader.LoadLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}