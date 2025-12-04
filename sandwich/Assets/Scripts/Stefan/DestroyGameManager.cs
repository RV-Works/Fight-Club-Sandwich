using UnityEngine;

public class DestroyGameManager : MonoBehaviour
{
    public void DestroyNow()
    {
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
            GameManager.instance = null;
            PlayerIngredients.s_id = 0;
        }
    }
}
