using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;


    private Rigidbody bulletRigidbody;
    public float DeathTime;

    
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        float speed = 40f;
        bulletRigidbody.velocity = transform.forward * speed;
        Destroy(gameObject,DeathTime);
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent<EnemyAi>(out EnemyAi enemyaiComponent) != false) {

            enemyaiComponent.TakeDamage(1);

            //HitTarget
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            
        } else {

            Debug.Log("YaBasic");

        }
        
        Destroy(gameObject);
        
    }
    
}





