using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Data{
    
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponDataSO weaponData;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player"){
                Debug.Log("Hey");
                WeaponManager weaponManager = other.GetComponent<WeaponManager>();
                if (weaponManager != null)
                    weaponManager.EquipWeapon(weaponData);
                Destroy(gameObject);
            } else{

                Debug.Log("HOI");
            }

            
        }
    }
        
  
}

