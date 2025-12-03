using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class IngriedentDropper : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] List<Ingredients> ingriedents = new List<Ingredients>();
    [SerializeField] List<GameObject> ingriedentObjects = new List<GameObject>();
    [SerializeField] int SandwichID;
    [SerializeField] float dropDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.instance == null)
        {

            StartCoroutine(Dropper());
            return;

        }
        if (GameManager.instance.Players.ContainsKey(SandwichID))
        {
            ingriedents = GameManager.instance.Players[SandwichID];
            StartCoroutine(Dropper());
        }

    }
    IEnumerator Dropper()
    {
        foreach (Ingredients ingredient in ingriedents)
        {
            Instantiate(ingriedentObjects[(int)ingredient], spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(dropDelay);
        }
        yield return null;
    }

}
