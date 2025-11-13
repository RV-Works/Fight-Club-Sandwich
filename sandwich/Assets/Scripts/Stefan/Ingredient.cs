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
        player.GetComponent<PlayerIngredients>().AddIngredient(gameObject);
    }
}
