using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetEndScore : MonoBehaviour
{
    Text scoreText;
    GlobalsKeeper gk;
    void Start()
    {
        scoreText = GetComponent<Text>();
        gk = GlobalsKeeper.GK;
        scoreText.text = gk.playerScore.ToString();
    }
}
