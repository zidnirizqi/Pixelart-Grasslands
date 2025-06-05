using UnityEngine;
using UnityEngine.SceneManagement;

public class LineDead : MonoBehaviour
{
    [Header("Batas Dunia")]
    public float leftBound = -10f;
    public float rightBound = 10f;
    public float upperBound = 5f;
    public float lowerBound = -5f;

    [Header("Target Player")]
    public Transform player;

    void Update()
    {
        if (player == null) return;

        Vector3 pos = player.position;

        if (pos.x < leftBound || pos.x > rightBound || pos.y < lowerBound || pos.y > upperBound)
        {
            Debug.Log("Player keluar batas! Game over.");
            SceneManager.LoadScene("kalah");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 topLeft     = new Vector3(leftBound, upperBound, 0);
        Vector3 topRight    = new Vector3(rightBound, upperBound, 0);
        Vector3 bottomRight = new Vector3(rightBound, lowerBound, 0);
        Vector3 bottomLeft  = new Vector3(leftBound, lowerBound, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
