using UnityEngine;

public class BadIngredient : Ingredient
{
    private bool pickupable = true;

    public override void Collect(GameObject player)
    {
        if (!pickupable)
            player.GetComponent<PlayerIngredients>().AddIngredient(gameObject);
    }
}
