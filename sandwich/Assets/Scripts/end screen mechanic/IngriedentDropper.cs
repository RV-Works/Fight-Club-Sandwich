using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class IngriedentDropper : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint; 
    [SerializeField] List<GameObject> ingriedents = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Dropper());
    }
    IEnumerator Dropper() 
    {
        for (int i = 0; i < ingriedents.Count; i++) 
        {
            Instantiate(ingriedents[i], spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
