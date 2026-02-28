using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Transform Spider;

    void Update()
    {
        if (Spider == null)
        {
            SceneManager.LoadScene("End Screen");
        }
    }
}