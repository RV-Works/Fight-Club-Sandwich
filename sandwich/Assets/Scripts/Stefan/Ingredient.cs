using UnityEngine;

public class Ingredient : MonoBehaviour, ICollectable
{
    [SerializeField] private float m_sizeY;
    public float SizeY
    {
        get { return m_sizeY; }
    }

    public void Collect(GameObject player)
    {
        GetComponent<BoxCollider>().enabled = false;
        player.GetComponentInParent<PlayerIngredients>().AddIngredient(gameObject);
    }
}
