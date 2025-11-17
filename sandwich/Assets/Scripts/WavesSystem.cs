using System.Collections.Generic;
using UnityEngine;

public class WavesSystem : MonoBehaviour
{
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private List<SabotageItem> sabotageItems;
    [SerializeField] private int WavesCount = 1;          // current wave index (1-based)
    [SerializeField] private float Timer = 30f;           // duration of each wave in seconds

    private float _waveTimer;
    private bool _waveActive;

    void Start()
    {
        
    }

    void Update()
    {
        if (!_waveActive) return;

        _waveTimer -= Time.deltaTime;
        if (_waveTimer <= 0f)
        {
            _waveActive = false;
            WavesCount++;
            if (WavesCount <= 3)
            {
                StartWave(WavesCount);
            }
            else
            {
                Debug.Log("All waves finished.");
            }
        }
    }

    private void StartWave(int waveNumber)
    {
        _waveTimer = Timer;
        _waveActive = true;

        // For now, every wave spawns all prefabs in the lists at random positions
        switch (waveNumber)
        {
            case 1:
            case 2:
            case 3:
                SpawnAll();
                break;
            default:
                Debug.LogWarning($"StartWave called with unsupported wave number: {waveNumber}");
                break;
        }

        Debug.Log($"Wave {waveNumber} started. Duration: {_waveTimer}s");
    }

    private void SpawnAll()
    {
        // Spawn ingredients
        foreach (var ingredient in ingredients)
        {
            if (ingredient == null) continue;
            Vector3 pos = new Vector3(
                Random.Range(-10f, 10f),
                ingredient.transform.position.y,
                Random.Range(-10f, 10f)
            );
            Instantiate(ingredient, pos, Quaternion.identity);
        }

        // Spawn sabotage items
        foreach (var sabotage in sabotageItems)
        {
            if (sabotage == null) continue;
            Vector3 pos = new Vector3(
                Random.Range(-10f, 10f),
                sabotage.transform.position.y,
                Random.Range(-10f, 10f)
            );
            Instantiate(sabotage, pos, Quaternion.identity);
        }
    }

    public void OnGameStart()
    {
        // Initialize and start the first wave
        if (Timer <= 0f)
        {
            Debug.LogWarning("Timer is zero or negative. Setting to default 30 seconds.");
            Timer = 30f;
        }

        WavesCount = 1;
        StartWave(WavesCount);
    }
}