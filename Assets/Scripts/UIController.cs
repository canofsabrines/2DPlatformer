using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;

    private int score;

    
    void OnEnable()
    {
        Messenger.AddListener(GameEvent.COIN_OBTAINED, OnCoinObtained);
    }
    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.COIN_OBTAINED, OnCoinObtained);
    }

    void Start()
    {
        score = 0;
        scoreLabel.text = score.ToString();

    }

    private void OnCoinObtained()
    {
        score += 1;
        scoreLabel.text = score.ToString();
    }
}