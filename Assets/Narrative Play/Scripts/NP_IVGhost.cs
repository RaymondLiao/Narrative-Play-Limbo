using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class NP_IVGhost : NP_InteractiveObject
{
    [SerializeField]
    private GameObject m_ghostEyes;

    [SerializeField]
    private Image m_profileUI;
    [SerializeField]
    private Sprite m_profileSprite;

    protected void Leave()
    {
        m_currentStatus = Status.disabled;
        m_ghostEyes.SetActive(false);
    }

    public override void Start()
    {
        base.Start();

        if (m_ghostEyes == null)
            Debug.LogError("[Interactive Ghost] Ghost Eyes not assigned.");

        if (m_profileUI == null)
            Debug.LogError("[Interactive Ghost] Profile UI not assigned.");
        if (m_profileSprite == null)
            Debug.LogError("[Interactive Ghost] Ghost's profile sprite not assigned.");
    }

    public override void StartInteracting()
    {
        base.StartInteracting();

        m_profileUI.sprite = m_profileSprite;
    }
}
