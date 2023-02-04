using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
  kConstantMovement,
  kSineMovement,
  kCircularMovement,
  kFollowTarget
}

public enum MovementMethod
{
  kTransform,
  kRigidbody,
}

public class DTKMovement : MonoBehaviour
{
  [Header("General settings")]
  public MovementType m_movementType;
  public MovementMethod m_movementMethod;
  public float m_speed = 1.0f; //units per second
  public Rigidbody m_rb;

  [Header("Constant movement settings")]
  public Vector3 m_constantDirection;

  [Header("Sine movement settigns")]
  public Vector3 m_moveAxis;
  public float m_moveDistance;
  Vector3 m_startPosition;

  [Header("Circular movement settings")]
  public float m_radius = 1.0f;

  [Header("Follow movement settings")]
  public Transform m_target;
  public float m_stopRadius = 0.5f;
  public float m_followRadius = 5.0f;

  // Start is called before the first frame update
  void Start()
  {
    m_startPosition = transform.position;

    if (m_movementMethod == MovementMethod.kRigidbody)
    {
      if (m_rb == null)
      {
        m_rb = GetComponent<Rigidbody>();
        if (m_rb == null)
        {
          m_rb = gameObject.AddComponent<Rigidbody>();
        }
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (m_movementMethod == MovementMethod.kTransform)
    {
      switch (m_movementType)
      {
        case MovementType.kConstantMovement:
          Vector3 transformedDir = transform.TransformDirection(m_constantDirection);
          transform.position += transformedDir * m_speed * Time.deltaTime;
          break;
        case MovementType.kSineMovement:
          Vector3 sineOffset = Mathf.Sin(Time.time * m_speed) * m_moveDistance * m_moveAxis;
          transform.position = m_startPosition + sineOffset;
          break;
        case MovementType.kCircularMovement:
          float sine = Mathf.Sin(Time.time * m_speed) * m_radius;
          float cos = Mathf.Cos(Time.time * m_speed) * m_radius;
          transform.position = m_startPosition + new Vector3(cos, 0, sine);
          break;
        case MovementType.kFollowTarget:
          Vector3 moveDirection = m_target.position - transform.position;
          if (moveDirection.magnitude < m_followRadius && moveDirection.magnitude > m_stopRadius)
          {
            moveDirection.y = 0;
            transform.position += moveDirection.normalized * Time.deltaTime * m_speed;
          }
          break;
        default:
          break;
      }
    }
  }

  private void FixedUpdate()
  {
    if (m_movementMethod == MovementMethod.kRigidbody)
    {
      switch (m_movementType)
      {
        case MovementType.kConstantMovement:
          Vector3 transformedDir = transform.TransformDirection(m_constantDirection);
          m_rb.velocity = transformedDir * m_speed;
          break;
        case MovementType.kSineMovement:
          Vector3 sineOffset = Mathf.Sin(Time.time * m_speed) * m_moveDistance * m_moveAxis;
          m_rb.MovePosition(m_startPosition + sineOffset);
          break;
        case MovementType.kCircularMovement:
          float sine = Mathf.Sin(Time.time * m_speed) * m_radius;
          float cos = Mathf.Cos(Time.time * m_speed) * m_radius;
          m_rb.MovePosition(m_startPosition + new Vector3(cos, 0, sine));
          break;
        case MovementType.kFollowTarget:
          Vector3 moveDirection = m_target.position - transform.position;
          if (moveDirection.magnitude < m_followRadius && moveDirection.magnitude > m_stopRadius)
          {
            moveDirection.y = 0;
            m_rb.velocity = moveDirection.normalized * m_speed;
          }
          else
          {
            m_rb.velocity = Vector3.zero;
          }
          break;
        default:
          break;
      }
    }
  }

}
