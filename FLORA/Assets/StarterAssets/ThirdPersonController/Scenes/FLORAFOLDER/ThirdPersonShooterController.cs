using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    public event Action<int, int> OnHealthChanged;
    public GameOverScreen GameOverScreen;
    public YouWinScreen YouWinScreen;
    
    public int maxHealth = 160;
    public int currentHealth;


    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    private Animator animator;
    


    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        
    }
    private void SetHealth(int value){

        currentHealth = value;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void Update() {
        
        Vector3 mouseWorldPosition = Vector3.zero;
        
        

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            
        } else   {
            mouseWorldPosition = ray.GetPoint(10);
            debugTransform.position = ray.GetPoint(10);
        }
        


        if (starterAssetsInputs.aim) {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        } else {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
        //Shooting
        if (starterAssetsInputs.shoot) {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile,spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;
        }

        if (currentHealth <= 0){

            GameOver();
        }
      
    }
    public void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Enemy"){
                Debug.Log("Enemy");
                TakeDamage(30);

            }

             if(other.tag == "HealthFlower"){
                Debug.Log("HealthFlower");
                PickUpHealth(20);
                

            }

            if(other.tag == "Flag"){
                Debug.Log("Flag");
                YouWin();
            }
        
        void TakeDamage(int damage){

            currentHealth -= damage;
            
        }
    }   
        void PickUpHealth(int pickUp){

            currentHealth += pickUp;
            
            
        }
    private void GameOver(){


        GameOverScreen.Setup();
        Destroy(gameObject);
        Debug.Log("YaBasic!");
    }

    public void YouWin(){

        YouWinScreen.Setup();
        currentHealth += 700;
        Debug.Log("Wow,yai'nt basic no more");
        
    }

    
    

}
