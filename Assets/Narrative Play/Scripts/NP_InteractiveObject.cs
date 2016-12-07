using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NP_InteractiveObject : MonoBehaviour
{
    [SerializeField]
    private Text m_EbuttonText;

    [SerializeField]
    protected Transform m_cameraTarget;
    public Transform cameraTarget
    {
        get { return m_cameraTarget; }
    }

    public enum Status
    {
        normal,
        waiting,
        interacting,
        disabled,
    }
    [SerializeField]
    protected Status m_currentStatus;
    public Status CurrentStatus
    {
        set { m_currentStatus = value; }
    }

    protected HighlightableObject m_hoObj;

    // Use this for initialization
    public virtual void Start()
    {
        m_hoObj = gameObject.AddComponent<HighlightableObject>();

        if (m_cameraTarget == null)
        {
            Debug.LogError("[Interactive Object] Camera Target not assigned.");
        }

        if (m_EbuttonText == null)
        {
            Debug.LogError("[Interactive Object] Button E Text not assigned.");
        }
        else
        {
            m_EbuttonText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (m_currentStatus == Status.waiting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                NP_GameManager.instance.StartInteracting(this);
            }
        }

    }

    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player")
            return;

        if (m_currentStatus == Status.normal)
        {
            m_hoObj.ConstantOn();
            m_currentStatus = Status.waiting;

            m_EbuttonText.gameObject.SetActive(true);
        }
    }
    protected void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != "Player")
            return;

        m_hoObj.ConstantOff();
        m_EbuttonText.gameObject.SetActive(false);

        if (m_currentStatus == Status.waiting)
        {
            m_currentStatus = Status.normal;
        }
    }

    public virtual void StartInteracting()
    {
        m_hoObj.ConstantOff();
        m_EbuttonText.gameObject.SetActive(false);

        m_currentStatus = Status.interacting;
    }
    public virtual void EndInteracting()
    {
        //m_currentStatus = Status.normal;       // Default: can interact again
    }

}
