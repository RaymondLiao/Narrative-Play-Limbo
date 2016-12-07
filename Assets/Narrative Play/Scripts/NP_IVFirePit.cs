using UnityEngine;
using System.Collections;

using NarrativePlayDialogue;

public class NP_IVFirePit : NP_IVGhost
{
    public override void StartInteracting()
    {
        base.StartInteracting();

        NP_GameManager.instance.DialogueSystem.StartConversation(NP_DialogueSystem.ConversationID.SCENE_5B);
    }

    public override void EndInteracting()
    {
        base.EndInteracting();

        NP_GameManager.instance.SetInteracted(NP_GameManager.ObjectsForFnialInteraction.FirePit);
        m_currentStatus = Status.disabled;
        m_hoObj.ConstantOff();
    }
}