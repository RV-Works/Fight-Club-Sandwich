using UnityEngine;

public class Spoon : MonoBehaviour, IThrowable
{
    [SerializeField] private float _forceToAdd = 200f;
    private GameObject _thrownPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _thrownPlayer)
            return;

        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.rigidbody;
            if (playerRb != null)
            {
                playerRb.AddExplosionForce(_forceToAdd, transform.position, 50);
            }
        }

        Destroy(gameObject);
    }

    public void Throw(GameObject player)
    {
        _thrownPlayer = player;
    }
}
