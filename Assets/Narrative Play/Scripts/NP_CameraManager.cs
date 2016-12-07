using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class NP_CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_targetTransform;

    private Vector3 m_originalPos;
    private Quaternion m_originalRot;

    public float moveSpeed = 5f;
    public float autoMoveDistanceThreshold = 0.01f;
    private bool m_isAutoMoving;

    public bool IsAutoMoving
    {
        set { m_isAutoMoving = false; }
    }

    public void AutoMoveTo(Transform targetTransform)
    {
        m_originalPos = transform.position;
        m_originalRot = transform.rotation;

        m_isAutoMoving = true;
        m_targetTransform = targetTransform;
    }

    public void CutTo(Transform targetTransform)
    {
        m_originalPos = transform.position;
        m_originalRot = transform.rotation;

        transform.position = targetTransform.position;
        targetTransform.rotation = targetTransform.rotation;
    }
    public void CutBackToOrigin()
    {
        transform.position = m_originalPos;
        transform.rotation = m_originalRot;
    }

	void Start ()
    {
        m_originalPos = transform.position;
        m_originalRot = transform.rotation;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    AutoMoveTo(m_targetTransform);
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    CutBackToOrigin();
        //}
    }

	void FixedUpdate ()
    {
        if (m_isAutoMoving)
        {
            transform.rotation = m_targetTransform.rotation;

            Vector3 currentForward = transform.forward;
            if (Mathf.Abs((transform.position - m_targetTransform.position).magnitude) > autoMoveDistanceThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, m_targetTransform.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                m_isAutoMoving = false;
            }
        }
	}
}
