using UnityEngine.SceneManagement; 
using UnityEngine;  

public class SceneTransition : MonoBehaviour
{
    void Update()
        {
            Scene scene = SceneManager.GetActiveScene();
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Brick");
            print(gos.Length);
            if(gos.Length == 0){
                if (scene.name == "History"){
                    SceneManager.LoadScene("SampleScene");
                } else if(scene.name == "SampleScene"){
                    SceneManager.LoadScene("Fase2");
                } else if(scene.name == "Fase2"){
                    SceneManager.LoadScene("win");
            }
        }
    }
}