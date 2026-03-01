using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private string firstLevelSceneName = "SampleScene"; // sua fase 1

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(firstLevelSceneName);
        }
    }

    // opcional: se você ainda tiver botão
    public void PlayAgain()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }
}