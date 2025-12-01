using UnityEngine;

public class Fork : MonoBehaviour, IThrowable
{
    [SerializeField] private float stunTime = 2f;
    private GameObject _thrownPlayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _thrownPlayer)
            return;

        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.TryGetComponent<ThirdPersonMovement>(out ThirdPersonMovement playerMovement))
            {
                playerMovement.Stun(stunTime);
            }
        }

        Destroy(gameObject);
    }

    public void Throw(GameObject player)
    {
        _thrownPlayer = player;
    }
}
