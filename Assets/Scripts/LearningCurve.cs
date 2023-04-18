using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    private Transform _camTramsform;
    public GameObject _directionLight;
    private Transform _lightTransform;
        
    void Start()
    {
        //_directionLight = GameObject.Find("Directional Light");
        _lightTransform = _directionLight.GetComponent<Transform>();
        Debug.Log(_lightTransform.localPosition);

        _camTramsform = this.GetComponent<Transform>();
        Debug.Log(_camTramsform.localPosition);

        Character hero = new Character();
       
        Character hero2 = hero;
        hero2._name = "Sir Krane the Brave";
        hero.PrintStatInfo();
        hero2.PrintStatInfo();
        
        Character heroine = new Character("Agatha");
        heroine.PrintStatInfo();

        Weapon axe = new Weapon("Axe", 550);


        Paladin knight = new Paladin("Sir Arthur", axe);
        knight.PrintStatInfo();

        Weapon huntingBow = new Weapon("Hunting Bow", 105);
        
        Weapon warBow = huntingBow;
        warBow.name = "War Bow";
        warBow.damage = 155;
        huntingBow.PrintWeaponStat();
        warBow.PrintWeaponStat();

    }

    
    void Update()
    {
        
    }
}
