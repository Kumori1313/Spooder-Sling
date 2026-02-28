using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public Transform spider;
    public TMP_Text timerText;
    public float totalTime = 5f;

    void Update()
    {
        if (spider == null) return;

        totalTime -= Time.deltaTime;
        totalTime = Mathf.Max(totalTime, 0f);
        timerText.text = $"Time Left: {totalTime:F2}s";
    }
}