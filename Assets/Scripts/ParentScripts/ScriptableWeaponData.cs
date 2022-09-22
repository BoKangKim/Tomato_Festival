using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New WeaponData",menuName = "ScriptableObject/WeaponData")]
public class ScriptableWeaponData : ScriptableObject
{
    
    [Header("���̸�")] // Handgun(����) - Repeater(������) - Shotgun - SniperRifle(������)
    [SerializeField] string gunName;
    public string GunName { get { return gunName; } }

    [Header("�� �̹���")]
    [SerializeField] Sprite gunImg;
    public Sprite GunImg { get { return gunImg; } }
    
    [Header("��Ÿ�")] // ��Ÿ� 15 - 25 - 20 - 40
    [SerializeField] float attackRange;
    public float AttackRange { get { return attackRange; } }

    [Header("���󰡴� �ӵ�")] // �ӵ� 30 - 40 - 50 - 70
    [SerializeField] float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    [Header("���ݷ�")] // ���ݷ� 10 - 15 - (20~10) - 30
    [SerializeField] float attackDamage;
    public float AttackDamage { get { return attackDamage; } }

    [Header("�⺻ �Ѿ� ����")] // �⺻ ���� �Ѿ� �� 15 - 20 - 30 - 10
    [SerializeField] float numberOfBullet;
    public float NumberOfBullet { get { return numberOfBullet; } }

    [Header("����ӵ�")] // ����ӵ� 1.5 - 0.5 - 1 - 2.5
    [SerializeField] float attackInterval;
    public float AttackInterval { get { return attackInterval; } }

  
}
