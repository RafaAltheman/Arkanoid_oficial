using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Config")]
    public int startingLives = 10;
    public int pointsPerBrick = 10;

    [Header("Scene names")]
    public string fase1Scene = "Fase1";
    public string fase2Scene = "Fase2";
    public string winScene   = "Win";
    public string gameOverScene = "SampleScene"; // ← DERROTA AQUI

    public int Score { get; private set; }
    public int Lives { get; private set; }

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        Score = 0;
        Lives = startingLives;
    }

    public void AddBrickScore()
    {
        Score += pointsPerBrick;
    }

    public void LoseLife()
    {
        Lives--;

        if (Lives <= 0)
        {
            SceneManager.LoadScene(gameOverScene);
        }
    }

    public void CheckLevelCleared()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        if (bricks.Length == 0)
        {
            string current = SceneManager.GetActiveScene().name;

            if (current == fase1Scene)
                SceneManager.LoadScene(fase2Scene);
            else if (current == fase2Scene)
                SceneManager.LoadScene(winScene);
        }
    }
}