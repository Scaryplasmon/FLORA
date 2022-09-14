using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Data{

    [CreateAssetMenu(fileName = "New Weapon", menuName = "Data/WeaponData")]

    public class WeaponDataSO : ScriptableObject{
        public int damage;
        public string weaponName;
        public GameObject weaponPrefab;
    }
}
