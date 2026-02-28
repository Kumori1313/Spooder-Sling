using UnityEngine;

public class RainDamage : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Destroy(other);
    }
}