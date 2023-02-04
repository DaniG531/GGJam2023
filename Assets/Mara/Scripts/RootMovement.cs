using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManager
{
  public List<SteeringAgent> mAgents = new List<SteeringAgent>();

  public void Update()
  {
    foreach (var a in mAgents)
    {
      a.Update();
    }
  }
}

public class SteeringAgent
{
  public SteeringManager mManager = null;

  public Transform mPos;
  public float mOriginalSpeed = 4.0f;
  public float mSpeed = 4.0f;
  public Vector3 mVelocity = new Vector3(0.0f, -8.0f, 0.0f);
  public float mSteeringForce = 1.0f;

  public float mSeparationRad = 1.0f;

  public float mMaxQueueAhead = 0.5f;
  public float mMaxQueueRad = 0.5f;

  public Vector3 mSteering;

  public void Update()
  {
    bool hardStop = Queue();

    if (hardStop)
    {
      mSpeed *= 0.3f;
    }
    else if (mSpeed < mOriginalSpeed)
    {
      mSpeed *= 3.333f;
    }

    if (mSpeed > mOriginalSpeed)
    {
      mSpeed = mOriginalSpeed;
    }

    mSteering = mSteering.normalized * mSteeringForce;
    mVelocity = (mVelocity + mSteering).normalized * mSpeed;

    mPos.transform.position += mVelocity * Time.fixedDeltaTime;
  }

  public void Seek(Vector3 seekPos)
  {
    var pos = mPos.transform.position;

    var desired_velocity = (seekPos - pos).normalized * mSpeed;

    var steering = desired_velocity - mVelocity;
    mSteering += steering.normalized * mSteeringForce;
  }

  public Vector3 Separate()
  {
    if (mManager != null)
    {
      Vector3 steering = new Vector3();
      foreach (var a in mManager.mAgents)
      {
        if (a != this)
        {
          Vector3 dir = mPos.position - a.mPos.position;
          if (dir.magnitude <= mSeparationRad)
          {
            steering += dir;
          }
        }
      }
      return steering.normalized * mSteeringForce;
    }
    return new Vector3();
  }

  public SteeringAgent GetNeighborAhead()
  {
    if (mManager != null)
    {
      Vector3 qa = mVelocity.normalized * mMaxQueueAhead;

      Vector3 ahead = mPos.position + qa;

      foreach (var neighbour in mManager.mAgents)
      {
        float d = (ahead - neighbour.mPos.position).magnitude;

        if (neighbour != this && d <= mMaxQueueRad)
        {
          return neighbour;
        }
      }
    }

    return null;
  }

  public bool Queue()
  {
    Vector3 breakF = new Vector3();

    SteeringAgent neighbour = GetNeighborAhead();
    if (neighbour != null)
    {
      breakF *= -0.8f;
      breakF += -mVelocity + Separate();

      mSteering += breakF;

      if ((mPos.position - neighbour.mPos.position).magnitude <= mMaxQueueRad)
      {
        mVelocity *= 0.3f;
        return true;
      }
    }
    return false;
  }
}

public class RootMovement : MonoBehaviour
{
  public GameObject vecLine;
  public float mMaxVelocity = 8.0f;
  public Vector3 mMovementDir = new Vector3(0.0f, -5.0f, 0.0f);
  public float mMaxSteeringForce = 10.0f;
  public float mSteeringMass = 50.0f;

  [Header ("Tail")]
  public Transform mTailParent;
  public float mRootLinkMaxLenght;

  public int mRootParts = 25;

  private List<Vector3> mPositions = new List<Vector3>();
  private List<Vector3> mPositionProyections = new List<Vector3>();
  private Vector3 mHeadProyections;
  public List<Vector3> Tail
  {
    get
    {
      return mPositions;
    }
  }


  // Start is called before the first frame update
  void Start()
  {
    mMovementDir = mMovementDir.normalized * mMaxVelocity;

    for (int i = 0; i < mRootParts; ++i)
    {
      mPositions.Add(mTailParent.position);
      mPositionProyections.Add(mTailParent.position);
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      AddLength(10);
    }
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    var mousePos = Input.mousePosition;
    mousePos.z = 10.0f;
    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

    var center = transform.position;
    var desired_velocity = (mousePos - center).normalized * mMaxVelocity;
    float newVel = (desired_velocity - mMovementDir.normalized * 1.0f).magnitude;

    //if (newVel < 1.25f)
    //{
    //  newVel = Mathf.Abs(mMovementDir.magnitude - desired_velocity.magnitude);
    //  newVel = mMovementDir.magnitude - newVel * 1.5f * Time.fixedDeltaTime;
    //}
    //else
    //{
    //  newVel = Mathf.Abs(mMovementDir.magnitude - desired_velocity.magnitude);
    //  newVel = mMovementDir.magnitude + newVel * 2.0f * Time.fixedDeltaTime;
    //  newVel = Mathf.Min(newVel, mMaxVelocity);
    //}

    var steering = desired_velocity - mMovementDir;
    steering = steering.normalized * mMaxSteeringForce;
    steering = steering / mSteeringMass;
    //mMovementDir = (mMovementDir + steering).normalized * newVel;
    mMovementDir = (mMovementDir + steering).normalized * mMaxVelocity;

    //transform.position += mMovementDir * Time.fixedDeltaTime;
    mHeadProyections = transform.position + mMovementDir * Time.fixedDeltaTime;

    if (RequestMove())
    {
      transform.position = mHeadProyections;
    }


    PlaceLineAsVector(vecLine, transform.position, mMovementDir);
  }

  public void AdjustRoot(int firstIndex, int lastIndex)
  {
    int nodesCount = lastIndex - firstIndex + 1;
    if (nodesCount == 2) return;

    Vector3 tempHead = firstIndex == -1 ? mHeadProyections : mPositions[firstIndex];

    Vector3 dir = tempHead - mPositions[lastIndex];
    float dist = dir.magnitude;
    Vector3 dirn = dir.normalized;

    if (nodesCount == 3)
    {
      Vector3 midPoint = mPositions[lastIndex] + dir * 0.5f;
      Vector3 normal = new Vector3(-dirn.y, dirn.x);
      float nordist = Mathf.Sqrt(4 * mRootLinkMaxLenght * mRootLinkMaxLenght - dist * dist) * 0.5f;
      
      Vector3 proyP1 = midPoint + normal * nordist;
      float distP1 = (proyP1 - mPositions[lastIndex - 1]).magnitude;
      Vector3 proyP2 = midPoint - normal * nordist;
      float distP2 = (proyP2 - mPositions[lastIndex - 1]).magnitude;

      mPositions[lastIndex - 1] = distP1 < distP2 ? proyP1 : proyP2;

      return;
    }

    mPositions[firstIndex + 1] = tempHead - dirn * mRootLinkMaxLenght;
    RequestMove(firstIndex + 1);
  }

  public bool RequestMove(int firstIndex = -1)
  {
    if (firstIndex >= mPositions.Count) return false;

    Vector3 dir;
    float dist;

    Vector3 tempHead = firstIndex == -1 ? mHeadProyections : mPositions[firstIndex];

    for (int i = firstIndex + 1; i < mPositions.Count; ++i)
    {
      dir = tempHead - mPositions[i];
      dist = dir.magnitude;
      Vector3 dirn = dir.normalized;

      if (dist <= mRootLinkMaxLenght * (i - firstIndex))
      {
        AdjustRoot(firstIndex, i);
        return true;
      }
    }

    return false;
  }

  void AddLength(int newSectionsCount)
  {
    for (int i = 0; i < newSectionsCount; ++i)
    {
      mPositions.Insert(0, mTailParent.position);
      mPositionProyections.Insert(0, mTailParent.position);
    }
    mRootParts += newSectionsCount;
  }

  Vector3 Truncate(Vector3 vec, float maxSize, float minSize = 0.0f)
  {
    if (vec.magnitude > maxSize)
    {
      return vec.normalized * maxSize;
    }
    if (vec.magnitude < minSize)
    {
      return vec.normalized * minSize;
    }
    return vec;
  }

  void PlaceLineAsVector(GameObject lineObj, Vector3 origin, Vector3 vec)
  {
    lineObj.transform.position = origin + vec * 0.5f;

    lineObj.transform.localScale = new Vector3(vec.magnitude, 0.1f, 0.1f);

    float angle = Mathf.Atan2(vec.y, vec.x) * 180.0f / Mathf.PI;
    lineObj.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
  }
}