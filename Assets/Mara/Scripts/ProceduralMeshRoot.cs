using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MyMesh
{
  public List<Vector3> vertices = new List<Vector3>();
  public List<Vector2> uvs = new List<Vector2>();
  public List<Vector3> normals = new List<Vector3>();
  public List<int> indices = new List<int>();
}

public class ProceduralMeshRoot : MonoBehaviour
{
  [Header("Mesh")]
  public MeshFilter mMeshCmp = null;
  private Mesh mMesh;

  public float mThickness = 0.2f;
  public int mSectionsCount = 8;

  public RootMovement mRoot = null;

  [Header("Material")]
  public Material mRootMat = null;
  private MeshRenderer mMeshRendererCmp = null;

  private float mUVLenght = 0.0f;


  // Start is called before the first frame update
  void Start()
  {
    if (mMeshCmp == null)
    {
      mMeshCmp = GetComponent<MeshFilter>();
      if (mMeshCmp == null)
      {
        mMeshCmp = gameObject.AddComponent<MeshFilter>();
      }
    }
    mMeshRendererCmp = mMeshCmp.gameObject.GetComponent<MeshRenderer>();
    if (mMeshRendererCmp == null)
    {
      mMeshRendererCmp = mMeshCmp.gameObject.AddComponent<MeshRenderer>();
    }

    if (mRootMat != null)
    {
      mMeshRendererCmp.material = mRootMat;
    }

    //mMesh = GenerateRootSection(new Vector3(-1.0f, 1.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f), 8, 0.2f);
    //
    //mMeshCmp.mesh = mMesh;
  }

  // Update is called once per frame
  void Update()
  {
    if (mRoot)
    {
      int tailCount = mRoot.Tail.Count;
      if (tailCount > 0)
      {
        mUVLenght = 0.0f;

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();
        List<int> indices = new List<int>();

        MyMesh completeTail = GenerateRootSection(mRoot.mTailParent.position + new Vector3(0.0f, -1.0f, 0.0f),
                                                  mRoot.mTailParent.position,
                                                  mRoot.Tail[0],
                                                  mSectionsCount,
                                                  mThickness,
                                                  0);


        for (int i = 0; i < tailCount; ++i)
        {
          MyMesh newTail = GenerateRootSection(i == 0
                                                 ? mRoot.mTailParent.position
                                                 : mRoot.Tail[i - 1],
                                               mRoot.Tail[i],
                                               i < tailCount - 1
                                                 ? mRoot.Tail[i + 1]
                                                 : mRoot.Tail[i] + new Vector3(0.0f, 1.0f, 0.0f),
                                               mSectionsCount,
                                               mThickness,
                                               mSectionsCount * 4 * i);
          
          completeTail.vertices = completeTail.vertices.Concat(newTail.vertices).ToList();
          completeTail.uvs = completeTail.uvs.Concat(newTail.uvs).ToList();
          completeTail.normals = completeTail.normals.Concat(newTail.normals).ToList();
          completeTail.indices = completeTail.indices.Concat(newTail.indices).ToList();
        }

        mMesh = new Mesh();
        mMesh.vertices = completeTail.vertices.ToArray();
        mMesh.uv = completeTail.uvs.ToArray();
        mMesh.normals = completeTail.normals.ToArray();
        mMesh.triangles = completeTail.indices.ToArray();

        mMeshCmp.mesh = mMesh;
      }
    }
  }

  MyMesh GenerateRootSection(Vector3 lastStart, Vector3 start, Vector3 finish, int divisions, float thickness, int firstIndex)
  {
    MyMesh r = new MyMesh();

    Vector3 lastSectionDir = (start - lastStart).normalized;
    Vector3 lastSectionNormal = new Vector3(-lastSectionDir.z, lastSectionDir.y, lastSectionDir.x);
    Vector3 sectionSect = (finish - start);
    //if (sectionSect.magnitude == 0) return new MyMesh();
    float lastUVLenght = mUVLenght;
    mUVLenght += sectionSect.magnitude * 0.5f;
    Vector3 sectionDir = sectionSect.normalized;
    Vector3 sectionNormal = new Vector3(-sectionDir.z, sectionDir.y, sectionDir.x);

    Vector3 sectionPos = (start + finish) * 0.5f;

    float length = (finish - start).magnitude * 0.5f;


    float angle = (Mathf.PI * 2.0f) / divisions;
    float angle3 = Mathf.Atan2(sectionDir.y, sectionDir.x);
    float angle2 = Mathf.Atan2(lastSectionDir.y, lastSectionDir.x) - angle3;
    for (int i = 0; i < divisions; ++i)
    {
      for (int v = 0; v < 4; ++v)
      {
        int angleSum = ((v + 1) / 2) % 2;
        float trueAngle = angle * (i + angleSum);
        float z = Mathf.Cos(trueAngle) * thickness;
        float y = Mathf.Sin(trueAngle) * thickness;

        int heightSum = 2 * (v / 2) - 1;

        float x = 0.0f;
        float angleXY = 0.0f;
        float lengthXY = 0.0f;

        //float realAngle2 = angle2 * (1 - (1 + heightSum) / 2);
        //float angleXY = Mathf.Atan2(y, x) + realAngle2;
        //float lengthXY = Mathf.Sqrt(x * x + y * y);

        //x = Mathf.Cos(angleXY) * lengthXY;
        //y = Mathf.Sin(angleXY) * lengthXY;

        x += length * heightSum;

        //Debug.Log("x: " + x.ToString());
        //Debug.Log("y: " + y.ToString());
        //Debug.Log("angle: " + angle3.ToString());

        //x = x * Mathf.Cos(angle3) + y * Mathf.Sin(angle3);
        //y = -x * Mathf.Sin(angle3) + y * Mathf.Cos(angle3);

        angleXY = Mathf.Atan2(y, x) + angle3;
        lengthXY = Mathf.Sqrt(x * x + y * y);

        x = Mathf.Cos(angleXY) * lengthXY;
        y = Mathf.Sin(angleXY) * lengthXY;

        //Debug.Log("nx: " + x.ToString());
        //Debug.Log("ny: " + y.ToString());

        r.vertices.Add(sectionPos + new Vector3(x, y, z));
        r.uvs.Add(new Vector2((float)(angleSum + i) / divisions, heightSum == -1 ? lastUVLenght : mUVLenght));
        r.normals.Add(new Vector3(0.0f, 0.0f, -1.0f));
        //r.vertices.Add((heightSum == 0 ? start : finish) + new Vector3(x, y, z));
      }
    }

    for (int i = 0; i < divisions; ++i)
    {
      r.indices.Add(firstIndex + i * 4 + 2);
      r.indices.Add(firstIndex + i * 4 + 1);
      r.indices.Add(firstIndex + i * 4 + 0);

      r.indices.Add(firstIndex + i * 4 + 3);
      r.indices.Add(firstIndex + i * 4 + 2);
      r.indices.Add(firstIndex + i * 4 + 0);
    }


    return r;
  }
}
