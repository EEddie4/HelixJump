using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bestText.text = "Best: " + GameManager.singleton.best;
        scoreText.text = "Score : " + GameManager.singleton.score;
    }
}
