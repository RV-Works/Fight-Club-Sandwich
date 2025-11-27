using UnityEngine;

public class BadIngredient : Ingredient
{
    public override void Collect(GameObject player)
    {
        player.GetComponent<PlayerIngredients>().AddIngredient(gameObject);
    }
}
