using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class youwon : MonoBehaviour
{
    [SerializeField] private GameObject P1Won;
    [SerializeField] private GameObject P2Won;
    [SerializeField] private GameObject P3Won;
    [SerializeField] private GameObject P4Won;
    [SerializeField] private GameObject idiotSandwichObj1;
    [SerializeField] private GameObject idiotSandwichObj2;
    [SerializeField] private GameObject idiotSandwichObj3;
    [SerializeField] private GameObject idiotSandwichObj4;
    [SerializeField] private int timer = 15;

    private void Update()
    {
       GetScoresPerID();
        timer--;
        if (timer <= 0)
        {
            YouWon();
            idiotSandwich();
        }

    }
    public int[] GetScoresPerID()
    {
        var scores = new int[4];
        var counters = Object.FindObjectsOfType<scorecounter>();
        if (counters == null || counters.Length == 0)
        {
            Debug.Log("youwon: No scorecounter instances found.");
            return scores;
        }

        foreach (var c in counters)
        {
            if (c == null) continue;
            int id = c.scoreID;
            if (id >= 0 && id < scores.Length)
            {
                scores[id] = c.Score;
            }
            else
            {
                Debug.LogWarning($"youwon: Ignored scorecounter with out-of-range scoreID {id}.");
            }
        }

        return scores;
    }

    // Convenience: get a single player's score by ID (returns 0 if not present or out of range)
    public int GetScoreForID(int id)
    {
        if (id < 0 || id >= 4) return 0;
        var counters = Object.FindObjectsOfType<scorecounter>();
        if (counters == null) return 0;

        foreach (var c in counters)
        {
            if (c == null) continue;
            if (c.scoreID == id) return c.Score;
        }

        return 0;
    }

    void YouWon()
    {
        if (GetScoreForID(0) >= GetScoreForID(1) &&
            GetScoreForID(0) >= GetScoreForID(2) &&
            GetScoreForID(0) >= GetScoreForID(3))
        {
            Debug.Log("Player 1 wins!");
            P1Won.SetActive(true);
        }
        else if (GetScoreForID(1) >= GetScoreForID(0) &&
                 GetScoreForID(1) >= GetScoreForID(2) &&
                 GetScoreForID(1) >= GetScoreForID(3))
        {
            Debug.Log("Player 2 wins!");
            P2Won.SetActive(true);
        }
        else if (GetScoreForID(2) >= GetScoreForID(0) &&
                 GetScoreForID(2) >= GetScoreForID(1) &&
                 GetScoreForID(2) >= GetScoreForID(3))
        {
            Debug.Log("Player 3 wins!");
            P3Won.SetActive(true);
        }
        else if (GetScoreForID(3) >= GetScoreForID(0) &&
                 GetScoreForID(3) >= GetScoreForID(1) &&
                 GetScoreForID(3) >= GetScoreForID(2))
        {
            Debug.Log("Player 4 wins!");
            P4Won.SetActive(true);
        }
    }
    void idiotSandwich()
    {
        if (GetScoreForID(0) <= GetScoreForID(1) &&
            GetScoreForID(0) <= GetScoreForID(2) &&
            GetScoreForID(0) <= GetScoreForID(3))
        {
            Debug.Log("Player 1 is an idiot sandwich!");
            idiotSandwichObj1.SetActive(true);
        }
        else if (GetScoreForID(1) <= 1 && GetScoreForID(1) <= GetScoreForID(0) &&
                 GetScoreForID(1) <= GetScoreForID(2) &&
                 GetScoreForID(1) <= GetScoreForID(3))
        {
            Debug.Log("Player 2 is an idiot sandwich!");
            idiotSandwichObj2.SetActive(true);
        }
        else if (GetScoreForID(2) >= 1 && GetScoreForID(2) <= GetScoreForID(0) &&
                 GetScoreForID(2) <= GetScoreForID(1) &&
                 GetScoreForID(2) <= GetScoreForID(3))
        {
            Debug.Log("Player 3 is an idiot sandwich!");
            idiotSandwichObj3.SetActive(true);
        }
        else if (GetScoreForID(3) >= 1 && GetScoreForID(3) <= GetScoreForID(0) &&
                 GetScoreForID(3) <= GetScoreForID(1) &&
                 GetScoreForID(3) <= GetScoreForID(2))
        {
            Debug.Log("Player 4 is an idiot sandwich!");
            idiotSandwichObj4.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
      
    }

 
}