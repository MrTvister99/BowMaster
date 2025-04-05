using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ]
public class Weapons : ScriptableObject
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _bullet;
    // возможно добавить еще поля (скорость перезарядки и тд)
   
}
