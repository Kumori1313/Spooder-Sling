using UnityEngine;

public class TimeDeath : MonoBehaviour
{
    public CountdownTimer timer;

    void Update()
    {
        if (timer.totalTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}