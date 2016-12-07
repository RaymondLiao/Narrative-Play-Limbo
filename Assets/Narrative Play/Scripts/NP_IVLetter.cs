using UnityEngine;
using System.Collections;

public class NP_IVLetter : NP_InteractiveObject
{
    [SerializeField]
    private CinemaDirector.CutsceneTrigger cutsceneTrigger;

    [SerializeField]
    private GameObject m_BGMs;

    public override void Start()
    {
        base.Start();

        if (cutsceneTrigger == null)
        {
            Debug.LogError("[Father's Letter] Cutscene Trigger not assigned.");
        }
    }

    public override void StartInteracting()
    {
        base.StartInteracting();

        StartCoroutine(PlayEndingCutscene());
        m_BGMs.SetActive(false);
    }

    private IEnumerator PlayEndingCutscene()
    {
        yield return new WaitForSeconds(1.0f);

        cutsceneTrigger.gameObject.SetActive(true);
    }
}