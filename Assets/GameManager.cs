using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Config")]
    public int startingLives = 10;
    public int pointsPerBrick = 10;

    [Header("Scenes (nomes exatos dos arquivos .unity)")]
    public string menuScene = "History";
    public string fase1Scene = "Fase1";
    public string fase2Scene = "Fase2";
    public string winScene = "Win";
    public string gameOverScene = "SampleScene"; // derrota

    public int fase1MaxScore = 54;

    public int fase2Bricks = 116;

    [Header("UI (TextMeshProUGUI)")]
    public TMP_Text livesText;
    public TMP_Text scoreText;

    public int Lives { get; private set; }
    public int Score { get; private set; }

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Inicializa uma vez
        if (Lives <= 0) NewGame();
        UpdateUI();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sempre que trocar de cena, tenta achar os textos automaticamente
        AutoFindHUDTexts();

        // Se entrou no menu/derrota, reinicia o jogo (você pediu “acabar agora”)
        if (scene.name == menuScene || scene.name == gameOverScene)
            NewGame();

        UpdateUI();
    }

    void AutoFindHUDTexts()
    {
        // Procura por nome exato dos objetos (do jeito mais simples)
        // Renomeie seus Texts para: "LivesText" e "ScoreText"
        var livesGO = GameObject.Find("LivesText");
        if (livesGO != null) livesText = livesGO.GetComponent<TMP_Text>();

        var scoreGO = GameObject.Find("ScoreText");
        if (scoreGO != null) scoreText = scoreGO.GetComponent<TMP_Text>();
    }

    public void NewGame()
    {
        Lives = startingLives;
        Score = 0;
        UpdateUI();
    }

    public void AddBrickScore()
    {
        Score += pointsPerBrick;
        UpdateUI();

        string currentScene = SceneManager.GetActiveScene().name;

        // alvo acumulado de vitória (fase1 + fase2)
        int fase2WinScore = fase1MaxScore + (fase2Bricks * pointsPerBrick);

        if (currentScene == fase1Scene && Score >= fase1MaxScore)
        {
            SceneManager.LoadScene(fase2Scene);
            return;
        }

        if (currentScene == fase2Scene && Score >= fase2WinScore)
        {
            SceneManager.LoadScene(winScene);
            return;
        }
    }

    public void LoseLife()
    {
        Lives--;
        UpdateUI();

        if (Lives <= 0)
        {
            SceneManager.LoadScene("SampleScene"); // tela de derrota
        }
    }

    public void CheckLevelCleared()
    {
        // Se não tiver nenhum Brick na cena, passa de fase
        var bricks = GameObject.FindGameObjectsWithTag("Brick");
        if (bricks.Length != 0) return;

        string current = SceneManager.GetActiveScene().name;

        if (current == fase1Scene)
            SceneManager.LoadScene(fase2Scene);
        else if (current == fase2Scene)
            SceneManager.LoadScene(winScene);
    }

    void UpdateUI()
    {
        if (livesText != null) livesText.text = $"Vidas: {Lives}";
        if (scoreText != null) scoreText.text = $"Pontos: {Score}";
    }

    public void ResetScore()
    {
        Score = 0;
        UpdateUI();
    }
}