using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text gameOverLabel;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.GAME_WIN, OnGameWin);
    }
    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.GAME_WIN, OnGameWin);
    }

    void Start()
    {
        gameOverLabel.text = "";

    }

    private void OnGameWin()
    {
        gameOverLabel.text = "YOU WIN!";
    }
}