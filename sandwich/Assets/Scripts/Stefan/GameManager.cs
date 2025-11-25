using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ingredients
{
    sla,
    tomaat,
    ham,
    kip,
    bacon,
    cheese,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Dictionary<int, List<Ingredients>> Players { get; private set; } = new Dictionary<int, List<Ingredients>>();

    private void Awake()
    {
        if (instance!=null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(LogEverySecond());
        }

        if (GameManager.instance.Players.ContainsKey(sandwichId))
        {
            ingredients = GameManager.instance.Players[sandwichId];
        }
    }

    private IEnumerator LogEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            foreach (var player in Players)
            {
                Debug.Log(player.Value);
            }
        }
    }

    public void AddIngredient(int id, Ingredients ingredient)
    {
        if (Players.ContainsKey(id))
        {
            Players[id].Add(ingredient);
        }
        else
        {
            Players.Add(id, new List<Ingredients>());
            Players[id].Add(ingredient);
        }
    }

    public void RemoveIngredient(int id, Ingredients ingredient)
    {
        Players[id].Remove(ingredient);
    }
}
