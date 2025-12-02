using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    
    [SerializeField] private GameObject[] _playersUi = new GameObject[4];
    [SerializeField] private PickupUi[] _playersItem = new PickupUi[4];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        
    }

    public PickupUi GetPlayerUi()
    {
        int index = 0;
        foreach (GameObject playerUi in _playersUi)
        {
            if (!playerUi.activeInHierarchy)
            {
                playerUi.SetActive(true);
                return _playersItem[index];
            }
            index++;
        }
        return null;
    }
}
