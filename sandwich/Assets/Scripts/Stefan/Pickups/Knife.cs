using UnityEngine;

public class Knife : MonoBehaviour, IThrowable
{
    [SerializeField] private int loseIngredients = 5;
    private GameObject _thrownPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _thrownPlayer)
            return;

        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.TryGetComponent<PlayerIngredients>(out var playerMovement))
            {
                playerMovement.LoseIngredient(loseIngredients);
            }
        }

        Destroy(gameObject);
    }

    public void Throw(GameObject player)
    {
        _thrownPlayer = player;
    }
}
