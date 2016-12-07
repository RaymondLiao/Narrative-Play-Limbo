using UnityEngine;
using System.Collections;

using NarrativePlayDialogue;

public class NP_IVSpiritTree : NP_IVGhost
{
    [SerializeField]
    private GameObject beamLight;

    [SerializeField]
    private int m_currentState = 1;
    public bool FirstInteraction;

    public override void Start()
    {
        base.Start();

        if (beamLight == null)
        {
            Debug.LogError("[Spirit Tree] Beam Light not assigned.");
        }
        else
        {
            beamLight.SetActive(false);
        }
    }

    public override void StartInteracting()
    {
        base.StartInteracting();

        switch (m_currentState)
        {
            case 1:
                NP_GameManager.instance.DialogueSystem.StartConversation(NP_DialogueSystem.ConversationID.SCENE_4);
                break;

            // Clicks Haru’s Tree again only possible after other Interactions
            case 2:
                NP_GameManager.instance.DialogueSystem.StartConversation(NP_DialogueSystem.ConversationID.SCENE_6);
                break;
        }
    }

    public override void EndInteracting()
    {
        base.EndInteracting();

        m_currentStatus = Status.disabled;
        m_hoObj.ConstantOff();

        switch (m_currentState)
        {
            case 1:
                FirstInteraction = true;

                NP_GameManager.instance.bgmManager.SwitchTo(NP_BGMManager.BGMID.Peaceful);

                break;

            case 2:
                {
                    //BGM_Angry.Stop();
                    //BGM_Peaceful.Play();
                }
                break;
        }

        if (m_currentState < 2)
        {
            m_currentState++;
        }
        else
        {
            NP_GameManager.instance.EnaleDepature();
            Leave();
        }
    }

    public void EnableFinalInteraction()
    {
        m_currentStatus = Status.normal;

        beamLight.SetActive(true);
    }
}