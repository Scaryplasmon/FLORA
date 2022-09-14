using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowFlowers : MonoBehaviour
{

    public List<MeshRenderer> GrowFlowerMeshes;
    public float timeToGrow = 5;
    public float refreshRate = 0.05f;
    [Range(0,1)]
    public float minGrow = 0.2f;
    [Range(0,1)]
    public float maxGrow = 0.97f;

    private List<Material> GrowFlowerMaterials = new List<Material>();
    private bool fullyGrown;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<GrowFlowerMeshes.Count; i++){

            for(int j=0; j<GrowFlowerMeshes[i].materials.Length; j++){

                if(GrowFlowerMeshes[i].materials[j].HasProperty("_Grow")){

                    GrowFlowerMeshes[i].materials[j].SetFloat("_Grow", minGrow);
                    GrowFlowerMaterials.Add(GrowFlowerMeshes[i].materials[j]);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<GrowFlowerMaterials.Count; i++){
            StartCoroutine(GrowFlower(GrowFlowerMaterials[i]));
        
        }
        
    }

    IEnumerator GrowFlower (Material mat){

        float growValue = mat.GetFloat("_Grow");

        if(!fullyGrown)
        {

            while(growValue < maxGrow)
            {
                growValue += 1/(timeToGrow/refreshRate);
                mat.SetFloat("_Grow", growValue);

                yield return new WaitForSeconds (refreshRate);
            }
        }

        if (growValue >= maxGrow)
            fullyGrown = true;
        else
            fullyGrown = false;
       
    }
}
