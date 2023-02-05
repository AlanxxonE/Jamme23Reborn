using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothDetector : MonoBehaviour
{
    public Material adaptiveToothMaterial;

    private GameObject[] teethArray;

    // Start is called before the first frame update
    void Start()
    {
        DetectTeeth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DetectTeeth()
    {
        teethArray = GameObject.FindGameObjectsWithTag("Tooth");

        for(int nTooth = 0; nTooth < teethArray.Length; nTooth++)
        {
            teethArray[nTooth].GetComponent<Renderer>().material = adaptiveToothMaterial;

            if (teethArray[nTooth].GetComponent<MeshCollider>() == null)
            {
                teethArray[nTooth].AddComponent<MeshCollider>().sharedMesh = teethArray[nTooth].GetComponent<MeshFilter>().mesh;
                teethArray[nTooth].GetComponent<MeshCollider>().convex = true;
            }

            if (teethArray[nTooth].GetComponent<ToothBehaviour>() == null)
            {
                teethArray[nTooth].AddComponent<ToothBehaviour>();
            }
        }
    }
}
