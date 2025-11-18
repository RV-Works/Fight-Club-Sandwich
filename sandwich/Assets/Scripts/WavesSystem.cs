using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WavesSystem : MonoBehaviour
{
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private List<SabotageItem> sabotageItems;
    [SerializeField] private int WavesCount = 1;
    [SerializeField] private float Timer = 30f;
    [SerializeField] private float SpawnInterval = 5f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float SpawnHeight = 10f;
    private float _waveTimer;
    private float _spawnTimer;
    private bool _waveActive;
    private int _spawnMultiplier = 1;


    void Start()
    {
        // If a timer text is assigned, initialize it to "00:00" (no active wave yet).
        if (timerText != null)
            timerText.text = FormatTime(0f);
    }

    void Update()
    {
        // If no wave is active, do nothing each frame.
        if (!_waveActive) return;

        // Decrease the wave and spawn timers by elapsed time since last frame.
        _waveTimer -= Time.deltaTime;
        _spawnTimer -= Time.deltaTime;

        if (timerText != null)
            timerText.text = FormatTime(Mathf.Max(0f, _waveTimer));

        // When the spawn timer reaches zero or below, spawn all configured prefabs and reset the spawn timer.
        if (_spawnTimer <= 0f)
        {
            SpawnAll(_spawnMultiplier);
            _spawnTimer = SpawnInterval;
        }

        if (_waveTimer <= 0f)
        {
            _waveActive = false;    
            WavesCount++;          

            if (WavesCount <= 3)   
            {
                StartWave(WavesCount); // start next wave
            }
            else
            {
                // All waves finished - log and ensure the UI shows zero time.
                Debug.Log("All waves finished.");
                if (timerText != null)
                    timerText.text = FormatTime(0f);
            }
        }
    }

    private void StartWave(int waveNumber)
    {
        _waveTimer = Timer;
        _waveActive = true;
        _spawnTimer = 0f;


        switch (waveNumber)
        {
            case 1:
                _spawnMultiplier = 1;
                SpawnAll(_spawnMultiplier);
                _spawnTimer = SpawnInterval;
                break;
            case 2:
                _spawnMultiplier = 2; // double the number of instances spawned per spawn event
                SpawnAll(_spawnMultiplier);
                _spawnTimer = SpawnInterval;
                break;
            case 3:
                _spawnMultiplier = 3; // triple the number of instances spawned per spawn event
                SpawnAll(_spawnMultiplier);
                _spawnTimer = SpawnInterval;
                break;
            default:
                // Warn if StartWave is called with an unsupported wave number.
                Debug.LogWarning($"StartWave called with unsupported wave number: {waveNumber}");
                break;
        }

        // Update the UI immediately to show the remaining time for the new wave.
        if (timerText != null)
            timerText.text = FormatTime(_waveTimer);

        Debug.Log($"Wave {waveNumber} started. Duration: {_waveTimer}s, spawn every {SpawnInterval}s (multiplier: {_spawnMultiplier})");
    }

 
    private void SpawnAll(int multiplier = 1)
    {
        if (multiplier < 1) multiplier = 1;
        foreach (var ingredient in ingredients)
        {
            if (ingredient == null) continue;

            for (int i = 0; i < multiplier; i++)
            {
                // Choose a random position in x/z and apply the prefab's y + SpawnHeight so it spawns above the ground.
                Vector3 pos = new Vector3(
                    Random.Range(-10f, 10f),
                    ingredient.transform.position.y + SpawnHeight,
                    Random.Range(-10f, 10f)
                );

               Instantiate(ingredient, pos, Quaternion.identity);
            }
        }

        // Spawn sabotage items
        foreach (var sabotage in sabotageItems)
        {
            if (sabotage == null) continue;

            for (int i = 0; i < multiplier; i++)
            {
                // Choose a random x/z and set y to the prefab's y + SpawnHeight.
                Vector3 pos = new Vector3(
                    Random.Range(-10f, 10f),
                    sabotage.transform.position.y + SpawnHeight,
                    Random.Range(-10f, 10f)
                );

                // Instantiate the sabotage item at the computed position with no rotation.
                Instantiate(sabotage, pos, Quaternion.identity);
            }
        }
    }


    public void OnGameStart()
    {
        // Validate configured Timer. If it's zero or negative, log a warning and set a sane default.
        if (Timer <= 0f)
        {
            Debug.LogWarning("Timer ended! Next wave!");
            Timer = 30f;
        }

        // Validate SpawnInterval. If zero or negative, log a warning and set a sane default.
        if (SpawnInterval <= 0f)
        {
            Debug.LogWarning("SpawnInterval is zero or negative. Setting to default 5 seconds.");
            SpawnInterval = 5f;
        }

        // Reset wave counter to the first wave and start it.
        WavesCount = 1;
        StartWave(WavesCount);

        // Ensure the UI shows the starting wave timer immediately after starting.
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