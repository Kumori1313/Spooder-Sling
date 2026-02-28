using UnityEngine;

public class SlingSound : MonoBehaviour
{
    private AudioSource slingSound;

    void Start()
    {
        slingSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
        {
            slingSound.Play();
            Debug.Log("Audio Played");
        }
    }
}
