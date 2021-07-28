using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// by eating this food the score will increase
/// </summary>
public class Food : MonoBehaviour
{
    [SerializeField] BoxCollider2D gridArea;
    [SerializeField] ScoreController scoreController;
    private int score=10;


    private void Start()
    {
        RandmizePosition();
    }

    //food will spawn randomly
    private void RandmizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandmizePosition();
            scoreController.IncreaseScore(score);
        }
    }
}
