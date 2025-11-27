using System.Collections;
using UnityEngine;

public class BadIngredient : Ingredient
{
    private GameObject _thrownPlayer;

    public override void Collect(GameObject player)
    {
        Grounded();
        base.Collect(player);
    }

    internal override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && collision.gameObject != _thrownPlayer)
        {
            Collect(collision.gameObject);
        }

        base.OnCollisionEnter(collision);
    }

    public IEnumerator ImmuneThrowPlayer(GameObject player)
    {
        _thrownPlayer = player;
        yield return new WaitForSeconds(1);
        _thrownPlayer = null;
    }
}
