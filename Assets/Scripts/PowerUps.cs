using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] BoxCollider2D gridArea;
    [SerializeField] GameObject scoreBooster;
    //[SerializeField] GameObject 

    private void Start()
    {
        SpawnRandomly();
    }
    private void SpawnRandomly()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
}
