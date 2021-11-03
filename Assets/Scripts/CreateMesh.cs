using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CreateMesh : MonoBehaviour {

    public enum MeshType
    {
        Triangle = 0,
        Rect =     1,
        Sector =   2,

    }
    public MeshType meshType;

    public Color meshColor;
    public Material material;


    void OnValidate()
    {
        Start();
    }

    // Use this for initialization
    void Start () {

        //メッシュの生成
        var mesh = SelectMeshType();
        
        //メッシュの色を変更
        var vertexCount = mesh.vertexCount;
        var colors = new Color[vertexCount];
        for(int i=0;i<vertexCount; i++)
        {
            colors[i] = meshColor;
        }
        mesh.colors = colors;
 

        //フィルターに生成したメッシュをいれる
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
    }

    //種類選択
    Mesh SelectMeshType()
    {
        switch (meshType)
        {
            case MeshType.Triangle:
                return CreateTriangleMesh();
            case MeshType.Rect:
                return CreateRectMesh();
            case MeshType.Sector:
                return CreateSectorMesh();
        }

        return CreateTriangleMesh();
    }

    //四角形
    Mesh CreateRectMesh()
    {
        //動的にメッシュを生成
        var mesh = new Mesh();
        mesh.vertices = new Vector3[] 
        {
            new Vector3 (-1, 0, -1), //左下 0
            new Vector3 (-1, 0, 1),  //左上 1
            new Vector3 (1, 0, -1),  //右下 2
            new Vector3(1, 0, 1)     //右上 3
        };

        mesh.triangles = new int[] { 0, 1, 3, 0, 3, 2 };
        mesh.RecalculateNormals();
        return mesh;
    }

    //三角形
    Mesh CreateTriangleMesh()
    {
        var mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            new Vector3( -1, 0, -1 ),   //左下
            new Vector3( 1, 0, -1 ),    //右下
            new Vector3( 0, 0, 1 )      //上
        };
        mesh.triangles = new int[] { 0, 1, 2 };
        mesh.RecalculateNormals();
        return mesh;
    }

    public float radius = 10.0f;
    public float startDegree = 10.0f;
    public float endDegree = 170.0f;
    public int triangleNum = 5;

    Mesh CreateSectorMesh()
    {
        Mesh mesh = new Mesh();
        //頂点座標計算
        Vector3[] vertices = new Vector3[2 + triangleNum];
        Vector2[] uv = new Vector2[2 + triangleNum];
        vertices[0] = new Vector3(0f, 0f, 0f);
        uv[0] = new Vector2(0.5f, 0.5f);
        float deltaRad = Mathf.Deg2Rad * ((endDegree - startDegree) / (float)triangleNum);
        for (int i = 1; i < 2 + triangleNum; i++)
        {
            float x = Mathf.Cos(deltaRad * (i - 1) + (Mathf.Deg2Rad * startDegree));
            float y = Mathf.Sin(deltaRad * (i - 1) + (Mathf.Deg2Rad * startDegree));
            vertices[i] = new Vector3(
                x * radius,
                y * radius,
                0.0f);
            uv[i] = new Vector2(x * 0.5f + 0.5f, y * 0.5f + 0.5f);
        }

        mesh.vertices = vertices;
        mesh.uv = uv;

        //三角形を構成する頂点のindexを，順に設定していく
        int[] triangles = new int[3 * triangleNum];
        for (int i = 0; i < triangleNum; i++)
        {
            triangles[(i * 3)] = 0;
            triangles[(i * 3) + 1] = i + 1;
            triangles[(i * 3) + 2] = i + 2;
        }
        mesh.triangles = triangles;
        return mesh;
    }
}
