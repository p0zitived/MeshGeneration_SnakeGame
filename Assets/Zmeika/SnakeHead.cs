using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _distanceBtwParts = 1;
    [SerializeField] private float _foodCheckRadius = 1;
    [SerializeField] private LayerMask _foodLayerMask;
    [SerializeField] float _rotationSpeed = 10;
    [Header("Snake Parts")]
    [SerializeField] SnakePart _snakePartPrefab;
    [SerializeField] private Transform _partsParent;
    [SerializeField] private int _initialPartsCount = 3;
    [SerializeField] private SnakeMeshGenerator _meshGenerator;

    [SerializeField] private List<SnakePart> _snakeParts;

    private void Start()
    {
        for (int i = 0; i < _initialPartsCount; i++)
        {
            SnakePart aux = Instantiate(_snakePartPrefab, _partsParent);
            aux.SetUp(i == 0 ? transform : _snakeParts[i-1].transform,_distanceBtwParts);
            _snakeParts.Add(aux);
        }

        _meshGenerator.UpdateMesh(_snakeParts,this);
    }

    private void FixedUpdate()
    {
        CheckForFood();
        _meshGenerator.UpdateMesh(_snakeParts, this);
    }

    Vector3 lastPostion;
    Vector3 lastTargetDirection;
    private void Update()
    {
        if ((transform.position - lastPostion).magnitude > 0.5f)
        {
            lastTargetDirection = (transform.position - lastPostion).normalized;
            lastPostion = transform.position;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lastTargetDirection),_rotationSpeed*Time.deltaTime);
    }

    private void CheckForFood()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _foodCheckRadius,_foodLayerMask);
        foreach (Collider collider in colliders)
        {
            SnakeFood food = collider.GetComponent<SnakeFood>();
            if (food != null)
            {
                AddPart(food.foodValue);
            }
            Destroy(collider.gameObject);
        }
    }
 
    public void AddPart(int count = 1)
    {
        for (int i = 0;i<count;i++)
        {
            SnakePart aux = Instantiate(_snakePartPrefab, _partsParent);
            aux.SetUp(_snakeParts.Count == 0 ? transform : _snakeParts[_snakeParts.Count - 1].transform, _distanceBtwParts);
            _snakeParts.Add(aux);
        }
    }
}
