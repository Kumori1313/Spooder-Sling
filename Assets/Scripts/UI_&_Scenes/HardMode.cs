using UnityEngine;
using UnityEngine.SceneManagement;

public class HardMode : MonoBehaviour
{
    public void LoadScene(string Hard_Mode)
    {
        SceneManager.LoadScene(Hard_Mode);
    }
}