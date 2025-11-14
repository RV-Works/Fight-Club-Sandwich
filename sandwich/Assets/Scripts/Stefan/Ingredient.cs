using UnityEngine;

public class Ingredient : MonoBehaviour, ICollectable
{
    public void Collect(GameObject player)
    {
        GetComponent<BoxCollider>().enabled = false;
        player.GetComponentInParent<PlayerIngredients>().AddIngredient(gameObject);
    }
}
