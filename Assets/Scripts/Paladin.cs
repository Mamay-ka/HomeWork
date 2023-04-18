using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : Character
{
    public Weapon weapon;

    public Paladin (string name, Weapon weapon) : base(name)
    {
        this.weapon = weapon;
    }
    public override void PrintStatInfo()
    {
        Debug.LogFormat("Paladin`s name: {0}, weapon: {1}, dmg: {2}", _name, weapon.name, weapon.damage);
    }

}
