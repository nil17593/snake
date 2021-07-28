using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// the mass gainer food
/// score will increase by double
/// </summary>
public class MassGainerFood : MonoBehaviour
{
    [SerializeField] BoxCollider2D gridArea;
    [SerializeField] ScoreController scoreController;
    private int score = 20;

    private void Start()
    {
        SpawnRandomly();
    }

    //food spoawns randomly in specified grid area
    private void SpawnRandomly()
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
            SpawnRandomly();
            scoreController.IncreaseScore(score);
        }
    }
}
