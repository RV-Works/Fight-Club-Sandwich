using UnityEngine;

public class CameraMovesUp : MonoBehaviour
{
    public float targetY = 25f;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.y >= targetY)
        {
            enabled = false;
            return;
        }

        float newY = Mathf.MoveTowards(pos.y, targetY, speed * Time.deltaTime);
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}