using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyMode : MonoBehaviour
{
    public void LoadScene(string Easy_Mode)
    {
        SceneManager.LoadScene(Easy_Mode);
    }
}