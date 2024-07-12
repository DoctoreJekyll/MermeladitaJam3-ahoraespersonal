using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Creater : MonoBehaviour
{
    [SerializeField] private GameObject objToCreate;

    private void InstantiateObject()
    {
        GameObject obj = Instantiate(objToCreate, transform.position, quaternion.identity);
    }
    


}
