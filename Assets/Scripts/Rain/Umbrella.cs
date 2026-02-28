using UnityEngine;

public class Umbrella : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
            transform.SetParent(collision.transform);
            transform.localPosition = new Vector3(0.5f, 1f, 0f);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
