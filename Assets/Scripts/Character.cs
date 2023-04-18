using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weapon
{
    public string name;
    public int damage;

    public Weapon(string name, int damage)
    {
        this.name = name;
        this.damage = damage;
    }

    public void PrintWeaponStat()
    {
        Debug.LogFormat("Weapont: {0} - {1} DMG", name, damage);
    }
}
public class Character 
{
    public string _name;
    public int _exp = 0;

    public Character()
    {
        _name = "Not assigned";
    }

    public Character(string name)
    {
        _name = name;
    }

    
    public virtual void PrintStatInfo()
    {
        Debug.LogFormat("Hero: {0} - {1} EXP", _name, _exp);
    }

    private void Reset()
    {
        _name = "Not assigned";
        _exp = 0;
    }
}
