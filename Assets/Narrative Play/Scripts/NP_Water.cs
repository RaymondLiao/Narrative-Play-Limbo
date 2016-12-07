using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.FirstPerson;

public class NP_Water : MonoBehaviour
{
    [SerializeField]
    private Transform m_spawnPoint;
    [SerializeField]
    private Transform m_playerTrans;


    public void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag != "Player")
            return;

        
        StartCoroutine(Respawn(col));
    }

    private IEnumerator Respawn(Collider col)
    {
        NP_GameManager.instance.ScreenFadeBlack();

        //FirstPersonController controller = col.gameObject.GetComponent<FirstPersonController>();
        //controller.enabled = false;

        yield return new WaitForSeconds(1.0f);

        m_playerTrans.position = m_spawnPoint.position;
        m_playerTrans.rotation = m_spawnPoint.rotation;

        //controller.enabled = true;
    }
    
}
