using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponKey
{
    public const string Target = "TARGET";
    public const string Direction = "DIRECTION";
}

public interface IWeapon
{
    public Dictionary<string, object> attributes { get; set; }

    public void Attack();
    public bool CanAttack();
}
