using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinLoseScript : MonoBehaviour
{
    [SerializeField] GameObject[] _winnerScreens = new GameObject[4];
    [SerializeField] GameObject[] _loserScreens = new GameObject[4];
    private Dictionary<int, List<Ingredients>> _players = new Dictionary<int, List<Ingredients>>();
    private float timer = 5f;

    void Start()
    {
        if (GameManager.instance != null)
        {
            _players = GameManager.instance.Players;
        }
        else
        {
            _players = new Dictionary<int, List<Ingredients>>()
                {
                    { 0, new List<Ingredients>() { Ingredients.sla, Ingredients.tomaat } },
                    { 1, new List<Ingredients>() { Ingredients.sla, Ingredients.tomaat, Ingredients.ham } },
                    { 2, new List<Ingredients>() { Ingredients.sla, Ingredients.tomaat, Ingredients.ham } },
                    { 3, new List<Ingredients>() { Ingredients.sla, Ingredients.tomaat } }
                };
        }


    }

    void Update()
    {
        if (timer <= 0)
        {
            Debug.Log("winlose triggered");
            ShowWinLose();
        }

        timer -= Time.deltaTime;
    }

    private void ShowWinLose()
    {
        var ordered = _players.OrderByDescending(x => x.Value.Count);
        _winnerScreens[ordered.First().Key].SetActive(true);
        Debug.Log("winner = " + ordered.First().Key);
        
        var lastPlayer = ordered.First();
        foreach (var player in ordered)
        {
            if (lastPlayer.Equals(player)) continue;

            if (player.Value.Count == lastPlayer.Value.Count)
            {
                _winnerScreens[player.Key].SetActive(true);
                Debug.Log("second winner = " + player.Key);
            }
        }

        var loseOrdered = _players.OrderBy(x => x.Value.Count);
        _loserScreens[loseOrdered.First().Key].SetActive(true);
        Debug.Log("loser = " + loseOrdered.First().Key);
        
        lastPlayer = loseOrdered.First();
        foreach (var player in loseOrdered)
        {
            if (lastPlayer.Equals(player)) continue;

            if (player.Value.Count == lastPlayer.Value.Count)
            {
                _loserScreens[player.Key].SetActive(true);
                Debug.Log("another loser = " + player.Key);
            }
        }

    }
}
