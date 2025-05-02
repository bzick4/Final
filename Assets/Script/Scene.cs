using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene : MonoBehaviour
{
   public void ExitToMenu()
    {
       SceneManager.LoadScene(0);
    }
    
    public void Load(int level)
    {
       SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
       Application.Quit();
    }
    
}
