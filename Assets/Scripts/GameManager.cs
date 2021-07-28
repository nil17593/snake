using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SnakeController snakeController;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    [SerializeField] int p1life;
    [SerializeField] int p2life;

    [SerializeField] GameObject p1wins;
    [SerializeField] GameObject p2wins;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (p1life < 1)
        {
            p1wins.SetActive(true);
        }
        else if (p2life < 1)
        {
            p2wins.SetActive(true);
        }
    }
}
