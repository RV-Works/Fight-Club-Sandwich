using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class IngriedentDropper : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint; 
    [SerializeField] List<Ingredients> ingriedents = new List<Ingredients>();
    [SerializeField] List<GameObject> ingriedentObjects = new List<GameObject>();
    [SerializeField] int SandwichID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {     
        StartCoroutine(Dropper());
        if (GameManager.instance.Players.ContainsKey(SandwichID))
        {
            ingriedents = GameManager.instance.Players[SandwichID];
        }
    }
    IEnumerator Dropper() 
    {
        foreach (Ingredients ingredient in ingriedents) 
        {
            Instantiate(ingriedentObjects[(int)ingredient], spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
