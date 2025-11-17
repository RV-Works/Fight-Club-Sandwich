using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavesSystem : MonoBehaviour
{
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private List<SabotageItem> sabotageItems;
    [SerializeField] private int WavesCount = 1;          // current wave index (1-based)
    [SerializeField] private float Timer = 30f;           // duration of each wave in seconds
    [SerializeField] private float SpawnInterval = 5f;    // how often to spawn during an active wave
    [SerializeField] private TMP_Text timerText;          // assign your TextMeshPro - Text component here
    [SerializeField] private float SpawnHeight = 10f;     // vertical offset above the prefab's y to spawn from

    private float _waveTimer;
    private float _spawnTimer;
    private bool _waveActive;

    void Start()
    {
        // Optional: initialize UI display before game starts
        if (timerText != null)
            timerText.text = FormatTime(0f);
    }

    void Update()
    {
        if (!_waveActive) return;

        _waveTimer -= Time.deltaTime;
        _spawnTimer -= Time.deltaTime;

        // Update TMP display every frame while wave is active
        if (timerText != null)
            timerText.text = FormatTime(Mathf.Max(0f, _waveTimer));

        if (_spawnTimer <= 0f)
        {
            SpawnAll();
            _spawnTimer = SpawnInterval;
        }

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
                // Ensure UI shows zero when all waves finished
                if (timerText != null)
                    timerText.text = FormatTime(0f);
            }
        }
    }

    private void StartWave(int waveNumber)
    {
        _waveTimer = Timer;
        _waveActive = true;
        _spawnTimer = 0f; // spawn immediately on wave start

        // For now, every wave spawns all prefabs in the lists at random positions
        switch (waveNumber)
        {
            case 1:
            case 2:
            case 3:
                SpawnAll();
                _spawnTimer = SpawnInterval; // reset after the immediate spawn
                break;
            default:
                Debug.LogWarning($"StartWave called with unsupported wave number: {waveNumber}");
                break;
        }

        // Update UI immediately when wave starts
        if (timerText != null)
            timerText.text = FormatTime(_waveTimer);

        Debug.Log($"Wave {waveNumber} started. Duration: {_waveTimer}s, spawn every {SpawnInterval}s");
    }

    private void SpawnAll()
    {
        // Spawn ingredients
        foreach (var ingredient in ingredients)
        {
            if (ingredient == null) continue;
            Vector3 pos = new Vector3(
                Random.Range(-10f, 10f),
                ingredient.transform.position.y + SpawnHeight,
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
                sabotage.transform.position.y + SpawnHeight,
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
            Debug.LogWarning("Timer ended! Next wave!");
            Timer = 30f;
        }

        if (SpawnInterval <= 0f)
        {
            Debug.LogWarning("SpawnInterval is zero or negative. Setting to default 5 seconds.");
            SpawnInterval = 5f;
        }

        WavesCount = 1;
        StartWave(WavesCount);

        // Ensure UI shows starting timer immediately
        if (timerText != null)
            timerText.text = FormatTime(_waveTimer);
    }

    private string FormatTime(float timeSeconds)
    {
        timeSeconds = Mathf.Max(0f, timeSeconds);
        int minutes = Mathf.FloorToInt(timeSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeSeconds % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}