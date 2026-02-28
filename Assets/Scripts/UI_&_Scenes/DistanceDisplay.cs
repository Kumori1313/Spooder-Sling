using UnityEngine;
using TMPro;

public class DistanceDisplay : MonoBehaviour
{
    public Transform Spider;
    public TMP_Text distanceText;

    void Update()
    {
        if (Spider != null)
        {
            float Distance = Spider.position.x;
            distanceText.text = $"Distance: {Distance:F2} cm";
        }
    }
}
