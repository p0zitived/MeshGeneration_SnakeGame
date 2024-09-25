using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeMeshGenerator : MonoBehaviour
{
    [SerializeField] private Material _bodyMaterial;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _circlesDensitiy = 1;
    [Range(4,30)]
    [SerializeField] private int _verticiesPerCircle = 4;
    [SerializeField] private GameObject _vertexPrefab;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _curveScale = 1;

    List<Vector3> allVerticies = new List<Vector3>();
    List<int> allTriangles = new List<int>();

    Mesh mesh;
    private void Awake()
    {
        mesh = new Mesh();
        _meshFilter.mesh = mesh;
    }

    public void UpdateMesh(List<SnakePart> parts,SnakeHead head)
    {
        allVerticies.Clear();
        allTriangles.Clear();

        GenerateCircle(head.transform.position, head.transform.up, head.transform.forward, _animationCurve.Evaluate(1)*_curveScale);
        for (int i =0; i < parts.Count; i++)
        {
            GenerateCircle(parts[i].transform.position, parts[i].transform.up, parts[i].transform.forward, _animationCurve.Evaluate(1 - (i + 0f)/parts.Count)*_curveScale);
        }
        GenerateTriangles();

        mesh.Clear();
        mesh.vertices = allVerticies.ToArray();
        mesh.triangles = allTriangles.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateUVDistributionMetric(0);
    }

    public void GenerateCircle(Vector3 location,Vector3 upVector,Vector3 fwdVector,float radius)
    {
        Vector3[] verticies = new Vector3[_verticiesPerCircle];
        for (int i = 0;i<_verticiesPerCircle; i++)
        {
            Vector3 targetPos = Quaternion.AngleAxis(i * 360/_verticiesPerCircle, fwdVector) * upVector * radius + location;
            verticies[i] = targetPos;
            //Instantiate(_vertexPrefab, targetPos, Quaternion.identity);
        }

        allVerticies.AddRange(verticies);
    }

    public void GenerateTriangles()
    {
        for (int i = 0;i<allVerticies.Count - _verticiesPerCircle;i++)
        {
            int currentCircleIndex = i / _verticiesPerCircle;

            allTriangles.Add(i + _verticiesPerCircle);
            allTriangles.Add((i + _verticiesPerCircle + 1) % _verticiesPerCircle + (currentCircleIndex)*_verticiesPerCircle);
            allTriangles.Add(i);

            allTriangles.Add(i + _verticiesPerCircle);
            int index = (i + _verticiesPerCircle + 1) % _verticiesPerCircle + (currentCircleIndex + 1) * _verticiesPerCircle;
            allTriangles.Add(index%allVerticies.Count);
            allTriangles.Add((i + 1) % _verticiesPerCircle + (currentCircleIndex) * _verticiesPerCircle);
        }
    }
}
