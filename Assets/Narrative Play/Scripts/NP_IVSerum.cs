using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class NP_IVSerum : NP_InteractiveObject
{
    [SerializeField]
    private string[] paragraphs;
    private int currentParagraphID;

    [SerializeField]
    private GameObject overExposurePanel;
    [SerializeField]
    private Text script;

    [SerializeField]
    private GameObject Cinematics;

    public override void Start()
    {
        base.Start();

        script.gameObject.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (m_currentStatus == Status.interacting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentParagraphID < paragraphs.Length)
                {
                    script.text = paragraphs[currentParagraphID++];
                }
                else
                {
                    NP_GameManager.instance.EndInteracting();
                }
            }
        }
    }

    public override void StartInteracting()
    {
        base.StartInteracting();

        overExposurePanel.SetActive(true);
        script.gameObject.SetActive(true);
        script.text = paragraphs[currentParagraphID++];


        NP_GameManager.instance.ChangeScreenFilter(NP_GameManager.ScreenFilter.redBright, false);

        // Camera Bloom
        Camera.main.GetComponent<Animator>().SetTrigger("Bloom Up");

        Cinematics.SetActive(false);

        NP_GameManager.instance.bgmManager.SwitchTo(NP_BGMManager.BGMID.Angry);
        NP_GameManager.instance.SwitchStormEffect(true);

    }

    public override void EndInteracting()
    {
        base.EndInteracting();

        m_currentStatus = Status.disabled;
        NP_GameManager.instance.SetInteracted(NP_GameManager.ObjectsForFnialInteraction.Serum);
        m_hoObj.ConstantOff();

        overExposurePanel.SetActive(false);

        Camera.main.GetComponent<Animator>().SetTrigger("Bloom Down");
    }
}
