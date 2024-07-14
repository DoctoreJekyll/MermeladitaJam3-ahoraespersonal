using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Creater : MonoBehaviour
{
    [SerializeField] private GameObject objToCreate;
    [SerializeField] private float timeToCreate;
    private float xPosLeft;
    private float xPostRigth;
    private float timeToSpawn;

    Camera mainCamera;
    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform topRight;
    void Start()
    {
        mainCamera = Camera.main; // Obtener la cámara principal
        xPosLeft = topLeft.position.x;
        xPostRigth = topRight.position.x;
    }
    
    private void InstantiateObject()
    {
        Vector3 randomPos = new Vector3(Random.Range(xPosLeft, xPostRigth), topLeft.position.y + 1, 0);
        GameObject obj = Instantiate(objToCreate, randomPos, quaternion.identity);
    }

    private void Update()
    {
        timeToSpawn += Time.deltaTime;
        
        if (timeToSpawn > timeToCreate)
        {
            InstantiateObject();
            timeToSpawn = 0;
        }
    }
}
