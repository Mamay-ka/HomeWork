using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerUIA : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;//ќбъект сцены Reference Text, предназначенный дл€ задани€ свойства text
    [SerializeField] private SettingsPopup settingsPopup;

    private int _score;

    private void Awake()//ќбъ€вл€ем, какой метод отвечает на событие ENEMY_HIT.
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();
        settingsPopup.Close();
    }
    /*void Update()
    {
        scoreLabel.text = Time.realtimeSinceStartup.ToString();
    }*/
      
    public void OnOpenSettings()//ћетод, вызываемый кнопкой настроек
    {
        //Debug.Log("Open settings");
        settingsPopup.Open();
    }
    
    public void OnPointerDown()
    {
        Debug.Log("Pointer down");
    }

    private void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }
}
