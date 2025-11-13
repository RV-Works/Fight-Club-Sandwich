using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
 
  [SerializeField] public string Level;

    public void GoToGame()
    {
        Debug.Log("Loading Level");
        SceneManager.LoadScene(Level);
    }

    public void Exit()
    {
        Application.Quit();
    }

   }

