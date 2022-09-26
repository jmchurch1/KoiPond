using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Slider killSlider;
    private ArrayList _enemyList = new ArrayList();

    private int _kills = 0;

    public static ScoreCounter ScoreInstance;

    private void Awake()
    {
        ScoreInstance = this;
    }

    private void IncrementKillCounter()
    {
        _kills += 1;
        killSlider.value = _kills;
    }
    
    public void AddEnemy(GameObject currEnemy)
    {
        _enemyList.Add(currEnemy);
    }

    public void RemoveEnemy(GameObject currEnemy)
    {
        _enemyList.Remove(currEnemy);
        IncrementKillCounter();
    }
    
}
