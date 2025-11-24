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
    chicken
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
    }

    private IEnumerator LogEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log(Players);
        }
    }

    public void AddIngredient(int id, Ingredients ingredient)
    {
        Players[id].Add(ingredient);
    }

    public void RemoveIngredient(int id, Ingredients ingredient)
    {
        Players[id].Remove(ingredient);
    }
}
