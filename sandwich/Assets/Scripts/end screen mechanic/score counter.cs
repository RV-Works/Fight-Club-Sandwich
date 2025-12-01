using UnityEngine;

public class scorecounter : MonoBehaviour
{
    public int scoreID;
    public int Score;
    [SerializeField] TMPro.TextMeshProUGUI P1ScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        P1ScoreText.text = "Score: " + Score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Yum"))
        {
            Score++;
        }
        if (other.gameObject.CompareTag("Eugh"))
        {
            Score--;
        }

    }
}
