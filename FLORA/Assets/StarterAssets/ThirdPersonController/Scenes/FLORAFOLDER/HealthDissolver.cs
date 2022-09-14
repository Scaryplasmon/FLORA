using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDissolver : MonoBehaviour
{

    [SerializeField] private ThirdPersonShooterController health = null;
    [SerializeField] private Renderer[] healthRenderers = new Renderer[0];

   
    private float targetDissolveValue = 1f;
    [Range(0.1f, 0.9f)]
    private float currentDissolveValue = 1f;

    private void OnEnable() => health.OnHealthChanged += HandleHealthChanged;
    private void OnDisable() => health.OnHealthChanged -= HandleHealthChanged;

     public void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Enemy"){
                Debug.Log("Enemy");
                TakeDamage();

            }
            if(other.tag == "HealthFlower"){
                Debug.Log("HealthFlower");
                PickUpHealth();
            }
        
        void TakeDamage(){

            currentDissolveValue -= 0.3f;
            
        }
        void PickUpHealth(){

            currentDissolveValue += 0.2f;
            
            
        }
    }

    // Update is called once per frame
    private void Update()
    {
        

        foreach (Renderer renderer in healthRenderers)
        {
            renderer.material.SetFloat("_Health", currentDissolveValue);
            Debug.Log("Hey,I'm Dying");
            
        }
        
    }

    private void HandleHealthChanged(int health, int maxHealth)
    {
        targetDissolveValue = (float)health / maxHealth;
    }
}
