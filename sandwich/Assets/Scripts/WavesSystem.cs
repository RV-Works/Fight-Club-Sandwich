using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WavesSystem : MonoBehaviour
{
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private List<SabotageItem> sabotageItems;
    [SerializeField] private List<PuddleScript> puddles;
    [SerializeField] private int WavesCount = 1;
    [SerializeField] private float Timer = 30f;
    [SerializeField] private float SpawnInterval = 5f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float SpawnHeight = 10f;
    private float _waveTimer;
    private float _spawnTimer;
    private bool _waveActive;
    private int _spawnMultiplier = 1;

    // Puddle management
    private readonly List<GameObject> _activePuddles = new List<GameObject>();
    private readonly Dictionary<GameObject, int> _puddleOrigin = new Dictionary<GameObject, int>();
    private bool _puddleSpawnedWave1;
    private bool _puddleRemovedWave1;
    private bool _puddleSpawnedWave2;
    private bool _puddleRemovedWave2;
    private bool _puddlesSpawnedWave3;

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

        // Wave-specific puddle logic
        switch (WavesCount)
        {
            case 1:
                // Spawn one puddle at the start of wave 1 (once)
                if (!_puddleSpawnedWave1)
                {
                    SpawnPuddleForWave(1);
                    _puddleSpawnedWave1 = true;
                }

                // Remove wave1 puddle when timer hits 10 seconds left
                if (!_puddleRemovedWave1 && _waveTimer <= 10f)
                {
                    RemovePuddlesForWave(1);
                    _puddleRemovedWave1 = true;
                }
                break;

            case 2:
                // Spawn one puddle when there's 20 seconds left in wave 2
                if (!_puddleSpawnedWave2 && _waveTimer <= 20f)
                {
                    SpawnPuddleForWave(2);
                    _puddleSpawnedWave2 = true;
                }

                // Remove the wave2 puddle when there's 5 seconds left
                if (!_puddleRemovedWave2 && _waveTimer <= 5f)
                {
                    RemovePuddlesForWave(2);
                    _puddleRemovedWave2 = true;
                }
                break;

            case 3:
                // Spawn two puddles at the start of wave 3 and keep them
                if (!_puddlesSpawnedWave3)
                {
                    SpawnPuddleForWave(3);
                    SpawnPuddleForWave(3);
                    _puddlesSpawnedWave3 = true;
                }
                break;
        }

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

        // Reset per-wave puddle flags for the new wave (we keep previously spawned puddles unless removed explicitly)
        _puddleSpawnedWave1 = false;
        _puddleRemovedWave1 = false;
        _puddleSpawnedWave2 = false;
        _puddleRemovedWave2 = false;
        _puddlesSpawnedWave3 = false;

        switch (waveNumber)
        {
            case 1:
                _spawnMultiplier = 1;
                SpawnAll(_spawnMultiplier);
                _spawnTimer = SpawnInterval;
                // spawn happens in Update logic at start of wave
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
                // puddles for wave 3 spawn in Update logic
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

    // ---------- Puddle helper methods ----------

    private void SpawnPuddleForWave(int waveNumber)
    {
        if (puddles == null || puddles.Count == 0)
        {
            Debug.LogWarning("[WavesSystem] No puddle prefabs assigned.");
            return;
        }

        // pick a random puddle prefab
        var prefab = puddles[Random.Range(0, puddles.Count)];
        if (prefab == null)
        {
            Debug.LogWarning("[WavesSystem] Selected puddle prefab was null.");
            return;
        }

        // choose a random x/z; place puddle at prefab's y (assumed ground) or 0 if prefab transform y is 0
        Vector3 pos = new Vector3(
            Random.Range(-10f, 10f),
            prefab.transform.position.y,
            Random.Range(-10f, 10f)
        );

        GameObject instance = Instantiate(prefab.gameObject, pos, prefab.transform.rotation);
        _activePuddles.Add(instance);
        _puddleOrigin[instance] = waveNumber;

        Debug.Log($"[WavesSystem] Spawned puddle for wave {waveNumber} at {pos}");
    }

    private void RemovePuddlesForWave(int waveNumber)
    {
        if (_activePuddles.Count == 0) return;

        // collect to-remove to avoid modifying the list while iterating
        var toRemove = new List<GameObject>();
        foreach (var go in _activePuddles)
        {
            if (go == null) continue;
            if (_puddleOrigin.TryGetValue(go, out int origin) && origin == waveNumber)
                toRemove.Add(go);
        }

        foreach (var go in toRemove)
        {
            if (go == null) continue;
            _puddleOrigin.Remove(go);
            _activePuddles.Remove(go);
            Destroy(go);
            Debug.Log($"[WavesSystem] Removed puddle (wave {waveNumber})");
        }
    }

    // Expose a public cleanup in case you need to remove all puddles externally
    public void RemoveAllPuddles()
    {
        foreach (var go in new List<GameObject>(_activePuddles))
        {
            if (go == null) continue;
            _puddleOrigin.Remove(go);
            _activePuddles.Remove(go);
            Destroy(go);
        }

        Debug.Log("[WavesSystem] All puddles removed.");
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