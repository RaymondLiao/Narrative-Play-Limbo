using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using UnityStandardAssets.Characters.FirstPerson;
using NarrativePlayDialogue;

[RequireComponent(typeof(NP_DialogueSystem))]
public class NP_GameManager : MonoBehaviour
{
    enum Status
    {
        normal,
        interacting,
    }
    [SerializeField]
    private Status m_currentStatus;

    // Pseudo Singleton
    public static NP_GameManager instance;

    private NP_DialogueSystem m_dialogueSystem;
    public NP_DialogueSystem DialogueSystem
    {
        get { return m_dialogueSystem; }
    }

    public FirstPersonController playerController;
    private NP_CameraManager m_cameraManager;

    [SerializeField]
    private NP_BGMManager m_bgmManager;
    public NP_BGMManager bgmManager
    {
        get { return m_bgmManager; }
    }
    [SerializeField]
    private ParticleSystem m_stormEffect;
    public void SwitchStormEffect(bool on)
    {
        if (on)
        {
            m_stormEffect.Play();
        }
        else
        {
            m_stormEffect.Stop();
        }
    }
    [SerializeField]
    private Animator m_screenTransitionAnim;
    public void ScreenFadeBlack()
    {
        StartCoroutine(Co_ScreenFadeBlack());
    }
    private IEnumerator Co_ScreenFadeBlack()
    {
        m_screenTransitionAnim.SetTrigger("Fade to Black");
        yield return new WaitForSeconds(1.5f);
        m_screenTransitionAnim.SetTrigger("Fade from Black");
    }

    private NP_InteractiveObject m_currentInteracive;

    private AmplifyColorEffect m_cameraFilter;

    [SerializeField]
    private Texture2D LUT_BlueBright;
    [SerializeField]
    private Texture2D LUT_BlueDull;
    [SerializeField]
    private Texture2D LUT_GreenBright;
    [SerializeField]
    private Texture2D LUT_GreenDull;
    [SerializeField]
    private Texture2D LUT_RedBright;
    [SerializeField]
    private Texture2D LUT_RedDull;
    public enum ScreenFilter
    {
        blueBright,
        blueDull,
        greenBright,
        greenDull,
        redBright,
        redDull,
    }

    [SerializeField]
    private NP_IVSpiritTree m_tree;
    [SerializeField]
    private NP_IVFirePit m_firePit;
    [SerializeField]
    private NP_IVBlueOrchid m_blueOrchid;
    [SerializeField]
    private NP_IVSerum m_serum;
    [SerializeField]
    private NP_Portal m_portal;
    [SerializeField]
    private NP_IVLetter m_letter;

    [SerializeField]
    private Image arrivalBoardImg;
    [SerializeField]
    private Image depatureBoardImg;

    private bool[] m_interactedObjects;
    public enum ObjectsForFnialInteraction
    {
        FirePit,
        BlueOrchid,
        Serum
    }
    public void SetInteracted(ObjectsForFnialInteraction obj)
    {
        m_interactedObjects[(int)obj] = true;

        if (m_interactedObjects[(int)ObjectsForFnialInteraction.FirePit] &&
            m_interactedObjects[(int)ObjectsForFnialInteraction.BlueOrchid] &&
            m_interactedObjects[(int)ObjectsForFnialInteraction.Serum])
        {
            m_tree.EnableFinalInteraction();
        }
    }

    [SerializeField]
    private GameObject greenBoarders;
    [SerializeField]
    private GameObject yellowBoarders;

    [SerializeField]
    private GameObject m_cinema;

    void Start()
    {
        m_cameraManager = Camera.main.GetComponent<NP_CameraManager>();
        m_cameraFilter = Camera.main.GetComponent<AmplifyColorEffect>();

        if (m_cameraManager == null)
        {
            Debug.LogError("[Game Manager] Camera Manager not assigned.");
        }
        if (m_screenTransitionAnim == null)
        {
            Debug.LogError("[Game Manager] Screen Transition Animator not assigned.");
        }
        if (m_bgmManager == null)
        {
            Debug.LogError("[Game Manager] BGM Manager not assigned.");
        }
        if (m_stormEffect == null)
        {
            Debug.LogError("[Game Manager] Storm Effect not assigned.");
        }

        if (m_portal == null)
        {
            Debug.LogError("[Game Manager] Portal not assigned.");
        }
        if (m_tree == null)
        {
            Debug.LogError("[Game Manager] Tree not assigned.");
        }
        if (m_firePit == null)
        {
            Debug.LogError("[Game Manager] Fire Pit not assigned.");
        }
        if (m_blueOrchid == null)
        {
            Debug.LogError("[Game Manager] Blue Orchid not assigned.");
        }
        if (m_serum == null)
        {
            Debug.LogError("[Game Manager] Serum Orchid not assigned.");
        }
        if (m_letter == null)
        {
            Debug.LogError("[Game Manager] Letter not assigned.");
        }

        if (greenBoarders == null)
        {
            Debug.LogError("[Game Manager] Green Boarders not assigned.");
        }
        if (yellowBoarders == null)
        {
            Debug.LogError("[Game Manager] Yellow Boarders not assigned.");
        }

        if (arrivalBoardImg == null)
        {
            Debug.LogError("[Game Manager] Arrival Board image not assigned.");
        }
        else
        {
            arrivalBoardImg.gameObject.SetActive(false);
        }
        if (depatureBoardImg == null)
        {
            Debug.LogError("[Game Manager] Depature Board image not assigned.");
        }
        else
        {
            depatureBoardImg.gameObject.SetActive(false);
        }


        m_firePit.gameObject.SetActive(false);
        m_blueOrchid.gameObject.SetActive(false);
        m_serum.gameObject.SetActive(false);
        m_letter.gameObject.SetActive(false);

        m_interactedObjects = new bool[3] { false, false, false };

        m_dialogueSystem = GetComponent<NP_DialogueSystem>();

        instance = this;
    }

    void Update()
    {
        if (m_currentStatus != Status.normal)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (m_tree.FirstInteraction == true)
        {
            m_firePit.gameObject.SetActive(true);
            m_blueOrchid.gameObject.SetActive(true);
            m_serum.gameObject.SetActive(true);

            greenBoarders.SetActive(false);
            arrivalBoardImg.gameObject.SetActive(true);
        }
    }

    public void StartInteracting(NP_InteractiveObject currentInteractive)
    {
        m_currentInteracive = currentInteractive;
        m_currentInteracive.StartInteracting();

        playerController.enabled = false;
        m_cameraManager.AutoMoveTo(m_currentInteracive.cameraTarget);

        m_currentStatus = Status.interacting;
    }

    public void EndInteracting()
    {
        m_currentInteracive.EndInteracting();
        m_currentInteracive = null;

        playerController.enabled = true;
        m_cameraManager.CutBackToOrigin();

        m_currentStatus = Status.normal;


        m_cameraManager.IsAutoMoving = false;
    }

    public void ChangeScreenFilter(ScreenFilter type, bool boomEffect = true)
    {
        switch (type)
        {
            case ScreenFilter.blueBright:
                m_cameraFilter.LutBlendTexture = LUT_BlueBright;
                break;

            case ScreenFilter.blueDull:
                m_cameraFilter.LutBlendTexture = LUT_BlueDull;
                break;

            case ScreenFilter.greenBright:
                m_cameraFilter.LutBlendTexture = LUT_GreenBright;
                break;

            case ScreenFilter.greenDull:
                m_cameraFilter.LutBlendTexture = LUT_GreenDull;
                break;

            case ScreenFilter.redBright:
                m_cameraFilter.LutBlendTexture = LUT_RedBright;
                break;

            case ScreenFilter.redDull:
                m_cameraFilter.LutBlendTexture = LUT_RedDull;
                break;

        }

        m_cameraFilter.BlendAmount = 1;

        if (boomEffect)
            StartCoroutine(ShowBloomEffect());
                
    }
    private IEnumerator ShowBloomEffect()
    {
        Animator mainCamAnim = Camera.main.gameObject.GetComponent<Animator>();
        mainCamAnim.SetTrigger("Bloom Up");

        yield return new WaitForSeconds(3.0f);
        mainCamAnim.SetTrigger("Bloom Down");
    }

    public void EnaleDepature()
    {
        m_portal.EnableEnter();

        m_letter.gameObject.SetActive(true);

        m_cameraManager.IsAutoMoving = false;
        m_cinema.SetActive(true);

        yellowBoarders.SetActive(false);

        depatureBoardImg.gameObject.SetActive(true);
    }
}