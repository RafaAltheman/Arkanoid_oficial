using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void BackToMenu()
    {
        if (GameManager.I != null)
        {
            GameManager.I.NewGame(); // reseta vidas e pontos
        }

        SceneManager.LoadScene("History");
    }
}