using System.Collections.Generic;
using UnityEngine;

public enum Ingredients
{
    sla,
    tomaat,
    ham,
    chicken,
    bacon,
    cheese
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
            DontDestroyOnLoad(this);
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
