using System.Collections;
using UnityEngine;

public class BadIngredient : Ingredient, IThrowable
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

    public void Throw(GameObject player)
    {
        StartCoroutine(ImmuneThrowPlayer(player));
    }

    public IEnumerator ImmuneThrowPlayer(GameObject player)
    {
        _thrownPlayer = player;
        yield return new WaitForSeconds(1);
        _thrownPlayer = null;
    }
}
