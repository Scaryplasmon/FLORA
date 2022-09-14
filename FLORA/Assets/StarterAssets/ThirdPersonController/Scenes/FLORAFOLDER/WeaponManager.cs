using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Data{
    public class WeaponManager : MonoBehaviour
    {
    

        [SerializeField]
        private Transform weaponSlot;

        private GameObject currentWeapon;

        [SerializeField]
        private WeaponDataSO equippedWeapon;

        [SerializeField]
        AudioSource pickUpSound;


        public void EquipWeapon(WeaponDataSO weaponData){

            pickUpSound.Play();
            Debug.Log("Hai");

            equippedWeapon = weaponData;

            if (currentWeapon != null)
                Destroy(currentWeapon);

            currentWeapon = Instantiate(weaponData.weaponPrefab);
            currentWeapon.transform.SetParent(weaponSlot);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
    }
}
