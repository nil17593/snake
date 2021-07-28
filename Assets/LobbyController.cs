using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// lobby Ui managed
/// </summary>
public class LobbyController : MonoBehaviour
{
    [SerializeField] Button button;


    private void Awake()
    {
        button.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
