using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;

    private int score;
    public bool score_reached;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.COIN_OBTAINED, OnCoinObtained);
        Messenger.AddListener(GameEvent.TEN_COINS, OnTenCoins);
    }
    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.COIN_OBTAINED, OnCoinObtained);
        Messenger.RemoveListener(GameEvent.TEN_COINS, OnTenCoins);
    }

    void Start()
    {
        score = 0;
        scoreLabel.text = score.ToString();
        score_reached = false;

    }

    private void OnCoinObtained()
    {
        score += 1;
        scoreLabel.text = score.ToString();
        if (score == 10)
        {
            Messenger.Broadcast(GameEvent.TEN_COINS);
        }
    }

    private void OnTenCoins()
    {
        score_reached = true;
    }
}