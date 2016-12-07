using UnityEngine;
using System.Collections;

public class NP_IVBlueOrchid : NP_IVGhost
{
    private int m_currentState = 1;

    public override void StartInteracting()
    {
        base.StartInteracting();

        switch (m_currentState)
        {
            case 1:
                NP_GameManager.instance.DialogueSystem.StartConversation(NarrativePlayDialogue.NP_DialogueSystem.ConversationID.SCENE_5D);
                break;
        }
    }

    public override void EndInteracting()
    {
        base.EndInteracting();


        m_currentStatus = Status.disabled;
        NP_GameManager.instance.SetInteracted(NP_GameManager.ObjectsForFnialInteraction.BlueOrchid);
        m_hoObj.ConstantOff();

        //gameObject.SetActive(false);

        Leave();
    }
}
