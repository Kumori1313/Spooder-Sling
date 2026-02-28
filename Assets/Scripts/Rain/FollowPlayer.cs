using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform Spider;
    void Update()
    {
        if (Spider != null)
        {
            Vector3 tempPos = transform.position;
            tempPos.x = Spider.position.x;
            transform.position = tempPos;
        }
    }
}