using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Water : MonoBehaviour
{
    MeshFilter _meshFilter;

    [SerializeField]
    float _size = 10f;

    [SerializeField]
    int _divisions = 10;

    public float Seed = 1;
    public float Speed = 1f;
    public float WaveLength = 3f;
    public float WaveHeight = 1f;

    public bool ShowGizmos = true;
    public Color GizmosColor = Color.blue;

    void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }

    void Start()
    {
        RebuildMesh();
    }

    void RebuildMesh()
    {
        _meshFilter.mesh = CreateMesh();
    }

    Mesh CreateMesh()
    {
        var mesh = new Mesh();
        mesh.name = "Water";

        var vertices = new Vector3[_divisions * _divisions];
        var triangles = new int[(_divisions - 1) * (_divisions - 1) * 6];
        var index = 0;

        for (int i = 0; i < _divisions; ++i)
        {
            for (int j = 0; j < _divisions; ++j)
            {
                var vertex = PositionForIndex(i, j);
                vertices[GetVertexIndex(i, j)] = vertex;

                if (i < (_divisions - 1) && j < (_divisions - 1))
                {
                    triangles[index] = j * _divisions + i;
                    triangles[index + 1] = (j + 1) * _divisions + i;
                    triangles[index + 2] = (j + 1) * _divisions + i + 1;

                    triangles[index + 3] = j * _divisions + i + 1;
                    triangles[index + 4] = j * _divisions + i;
                    triangles[index + 5] = (j + 1) * _divisions + i + 1;

                    index += 6;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }

    Vector3 PositionForIndex(int i, int j)
    {
        var pi = (i / (_divisions - 1f));
        var pj = (j / (_divisions - 1f));

        var position = new Vector3();
        position.x = -_size * 0.5f + pi * _size;
        position.z = -_size * 0.5f + pj * _size;

        var noise = Time.timeSinceLevelLoad * Speed;
        var time = Mathf.PerlinNoise(pi * WaveLength + noise, pj * WaveLength + noise);
        time /= (1f / WaveLength);
        position.y = Mathf.Sin(time) * WaveHeight;

        return position;
    }

    void Update()
    {
        var mesh = _meshFilter.mesh;

        if (mesh == null)
            return;

        var vertices = mesh.vertices;

        for (int i = 0; i < _divisions; ++i)
        {
            for (int j = 0; j < _divisions; ++j)
            {
                vertices[i + j * _divisions] = PositionForIndex(i, j);
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    int GetVertexIndex(int i, int j)
    {
        return i * _divisions + j;
    }

    void OnDrawGizmos()
    {
        if (!ShowGizmos)
            return;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = GizmosColor.WithAlpha(0.25f);

        var scale = Vector3.one * _size;
        scale.y = 0f;

        Gizmos.DrawCube(Vector3.zero, scale);

        Gizmos.matrix = Matrix4x4.identity;
    }
}
