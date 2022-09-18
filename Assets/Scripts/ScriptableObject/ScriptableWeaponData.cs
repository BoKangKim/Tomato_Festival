using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New WeaponData",menuName = "ScriptableObject/WeaponData")]
public class ScriptableWeaponData : ScriptableObject
{
    [SerializeField] float attackRange;
    [SerializeField] float attackDamage;
    [SerializeField] float numberOfBullet;
    [SerializeField] float attackInterval;
}
