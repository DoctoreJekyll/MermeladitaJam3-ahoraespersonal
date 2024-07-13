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
    private Vector3 topLeft;
    private Vector3 topRight;
    void Start()
    {
        mainCamera = Camera.main; // Obtener la cámara principal
                
        topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        xPosLeft = topLeft.x;
        xPostRigth = topRight.x;
    }
    
    private void InstantiateObject()
    {
        Vector3 randomPos = new Vector3(Random.Range(xPosLeft, xPostRigth), topLeft.y + 1, 0);
        GameObject obj = Instantiate(objToCreate, randomPos, quaternion.identity);
    }

    private void Update()
    {
        timeToSpawn += Time.deltaTime;
        
        if (timeToSpawn > timeToCreate)
        {
            InstantiateObject();
            timeToSpawn = 0;
            Debug.Log("Projectil");
        }
    }
}
