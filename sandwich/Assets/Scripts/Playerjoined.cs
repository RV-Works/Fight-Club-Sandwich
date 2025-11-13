using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerJoined : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] playerCountButtons;

    [Header("Managers")]
    public PlayerInputManager playerInputManager;
    public PlayerManager playerManager;

    //Runtime state(read-only externally)
    public GameObject[] Players { get; private set; }
    public int MaxPlayers { get; private set; } = 4;
    public int JoinedPlayers { get; private set; } = 0;
    public bool SelectionMade { get; private set; } = false;

    // Event fired when all expected players have joined
    public event Action<GameObject[]> AllPlayersJoined;

    void Start()
    {
        if (playerInputManager == null)
        {
            playerInputManager = FindFirstObjectByType<PlayerInputManager>();
            if (playerInputManager != null)
                Debug.Log("PlayerInputManager automatically found.");
        }

        if (playerManager == null)
        {
            playerManager = FindFirstObjectByType<PlayerManager>();
            if (playerManager != null)
                Debug.Log("PlayerManager automatically found.");
    }

        // Ensure joining is disabled until a selection is made
        if (playerInputManager != null)
        {
            playerInputManager.DisableJoining();
            Debug.Log("PlayerInputManager joining disabled at start.");
        }
        else
{
    Debug.LogError("NO PlayerInputManager found in the scene!");
}
    }

    // Called from UI buttons to set desired player count
    public void SetPlayerCount(int count)
    {
        Debug.Log("SetPlayerCount called with: " + count);

        if (playerCountButtons == null || playerCountButtons.Length == 0)
        {
            Debug.LogError("playerCountButtons array is empty or not set!");
            return;
        }

        MaxPlayers = Mathf.Max(1, count);
        Players = new GameObject[MaxPlayers];
        JoinedPlayers = 0;
        SelectionMade = true;

        if (playerInputManager != null)
        {
            playerInputManager.EnableJoining();
            playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
            Debug.Log("PlayerInputManager enabled — players can now join!");
        }
        else
        {
            Debug.LogError("No PlayerInputManager available to enable joining.");
        }

        //Disable and hide the player count UI
        foreach (var btn in playerCountButtons)
        {
            if (btn == null) continue;
            var buttonComp = btn.GetComponent<Button>();
            if (buttonComp != null) buttonComp.interactable = false;
            btn.SetActive(false);
        }
    }

  // This method must remain public so PlayerInputManager can call it when a PlayerInput joins.
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"[PlayerSelection] OnPlayerJoined called: {playerInput.gameObject.name}");

        if (!SelectionMade)
        {
            Debug.LogWarning("Selection hasn't been made yet — rejecting join.");
            Destroy(playerInput.gameObject);
            return;
        }

        if (JoinedPlayers >= MaxPlayers)
        {
            Debug.LogWarning("Max players reached — rejecting extra join.");
            Destroy(playerInput.gameObject);
            return;
        }

        Players[JoinedPlayers] = playerInput.gameObject;
        JoinedPlayers++;
        Debug.Log($"Player added: {playerInput.gameObject.name} (Total: {JoinedPlayers}/{MaxPlayers})");

        if (playerManager != null)
        {
            playerManager.SetSpawn(playerInput);
        }
        else
        {
            Debug.LogError("PlayerManager is not assigned; cannot SetSpawn.");
        }

        if (JoinedPlayers == MaxPlayers)
        {
            Debug.Log("All players have joined. Raising AllPlayersJoined event.");
            AllPlayersJoined?.Invoke(Players);
        }
    }
}