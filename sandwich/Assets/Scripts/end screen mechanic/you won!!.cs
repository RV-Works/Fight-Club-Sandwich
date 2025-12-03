using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class youwon : MonoBehaviour
{
    [SerializeField] private GameObject P1Won;
    [SerializeField] private GameObject P2Won;
    [SerializeField] private GameObject P3Won;
    [SerializeField] private GameObject P4Won;
    //[SerializeField] private GameObject idiotSandwichObj1;
    //[SerializeField] private GameObject idiotSandwichObj2;
    //[SerializeField] private GameObject idiotSandwichObj3;
    //[SerializeField] private GameObject idiotSandwichObj4;
    [SerializeField] private int timer = 15;
    private int IdiotSandwichID;
    [SerializeField] private GameObject[] IdiotSandwichObjs;

    private void Update()
    {
       GetScoresPerID();
        timer--;
        if (timer <= 0)
        {
            YouWon();
            IdiotSandwich();
            Debug.Log("yes it gets here, idiot sandwich and you won called");
        }

    }
    public int[] GetScoresPerID()
    {
        var scores = new int[4];
        var counters = Object.FindObjectsOfType<scorecounter>();
        if (counters == null || counters.Length == 0)
        {
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

    void IdiotSandwich()
    {
        int minScore = 1000;
        int[]LocalScores = GetScoresPerID();
        for (int i = 0; i < LocalScores.Length; i++)
        {
            if (LocalScores[i] > 0) 
            {
                if (LocalScores[i] < minScore) 
                { 
                    minScore = LocalScores[i]; 
                    IdiotSandwichID = i;

                }
            }
          
        }
        IdiotSandwichObjs[IdiotSandwichID].SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
      
    }

 
}