using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapon")]
public class WeaponData : ItemData
{
    public GameObject controller;
    public GameObject prefab;
    public float speed;
    public float damage;
    public float cooldownDuration;
    public int pierce;
    public float lifeTime;
    public int damagePlus;

    public List<string> Descriptions;
}
