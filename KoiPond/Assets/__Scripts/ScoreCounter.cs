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

    public List<Image> _characterList = new List<Image>();
    
    
    
    private int _kills = 0;
    private bool _switchAvailable = false;

    public static ScoreCounter ScoreInstance;

    private void Awake()
    {
        ScoreInstance = this;
    }

    private void IncrementKillCounter()
    {
        _kills += 1;
        killSlider.value += 1;
        if (killSlider.value == killSlider.maxValue) _switchAvailable = true;
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

    public bool GetSwitchAvailable()
    {
        return _switchAvailable;
    }

    public void SetSwitchAvailable(bool newValue)
    {
        _switchAvailable = newValue;
        // if the newValue is set to false reset the value of the slider to 0
        if (!newValue) killSlider.value = 0;
    }

    public void SetSelectedCharacter(int selectedCharacter)
    {
        _characterList[selectedCharacter].color = Color.yellow;
    }

    public void DeselectCharacter(int selectedCharacter)
    {
        _characterList[selectedCharacter].color = Color.white;
    }

    public void PlayCharacter(int selectedCharacter)
    {
        Debug.Log(_characterList[selectedCharacter].name);
        _characterList[selectedCharacter].color = Color.red;
    }
    
    
}
