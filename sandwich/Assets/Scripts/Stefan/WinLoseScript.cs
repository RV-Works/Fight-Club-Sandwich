using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinLoseScript : MonoBehaviour
{
    private Dictionary<int, List<Ingredients>> _players = new Dictionary<int, List<Ingredients>>();
    private float timer = 15f;

    void Start()
    {
        _players = GameManager.instance.Players;
    }

    void Update()
    {
        if (timer <= 0)
        {
            ShowWinLose();
        }

        timer -= Time.deltaTime;
    }

    private void ShowWinLose()
    {
        Dictionary<int, int> winner = new Dictionary<int, int>();
        Dictionary<int, int> loser = new Dictionary<int, int>();
        int index = 0;

        foreach (var player in _players.Values)
        {
            winner.Add(0, player.Count);

            index++;
        }

        var ordered = winner.OrderBy(x => x.Value);

        Debug.Log("winner = " + ordered.First());
        ordered = loser.OrderBy(x => x.Value);
        Debug.Log("loser = " + ordered.First());
    }
}
