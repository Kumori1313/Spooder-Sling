using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void LoadScene(string Instructions)
    {
        SceneManager.LoadScene(0);
    }
}