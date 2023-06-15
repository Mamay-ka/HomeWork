using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;

    private void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnSubmitName(string name)//Этот метод срабатывает в момент начала ввода данных в текстовое поле
    {
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed)//Этот метод срабатывает при изменении положения ползунка
    {
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);//Значение, заданное положением ползунка, рассылается как событие <float>.
        Debug.Log("Speed :" + speed);
       
    }
}

