using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class NP_Portal : MonoBehaviour
{
    [SerializeField]
    private Transform m_target;

    [SerializeField]
    private GameObject portalEffect;


    [SerializeField]
    private bool m_enableEnter = false;

    void Start()
    {
        if (m_target == null)
        {
            Debug.LogError("[Portal] Target not assigned");
        }

        portalEffect.SetActive(false);
    }

    public void EnableEnter()
    {
        portalEffect.SetActive(true);

        m_enableEnter = true;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (!m_enableEnter || col.gameObject.tag != "Player")
        {
            return;
        }

        StartCoroutine(Respawn(col));
    }

    private IEnumerator Respawn(Collider col)
    {
        NP_GameManager.instance.ScreenFadeBlack();

        //FirstPersonController controller = col.gameObject.GetComponent<FirstPersonController>();
        //controller.enabled = false;

        yield return new WaitForSeconds(1.0f);

        col.gameObject.transform.position = m_target.position;
        col.gameObject.transform.rotation = m_target.rotation;

        //controller.enabled = true;
    }
}
