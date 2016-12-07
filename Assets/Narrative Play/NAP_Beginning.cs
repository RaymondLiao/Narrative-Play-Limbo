using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class NAP_Beginning : MonoBehaviour
{
    public Button MakoBtn;
    public Image PanelBg;

    private AudioSource audioSrc;
    public AudioSource BGMSource;
    public AudioClip btnSFX;
    public AudioClip FountainSFX;

    public Image BlackPanel;
    public Image BeginningPanel;

    public GameObject DialogueManager;

	// Use this for initialization
	void Start ()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.playOnAwake = false;

        DialogueManager.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Enter()
    {
        MakoBtn.animator.SetTrigger("Drop");

        audioSrc.clip = btnSFX;
        audioSrc.loop = false;
        audioSrc.Play();

        BGMSource.gameObject.SetActive(true);

        
        StartCoroutine("OpenEyes");
    }

    IEnumerator OpenEyes()
    {
        yield return new WaitForSeconds(4f);

        BeginningPanel.gameObject.SetActive(false);

        BGMSource.enabled = true;
        BlackPanel.gameObject.GetComponent<Animator>().SetTrigger("OpenEyes");

        DialogueManager.SetActive(true);

        audioSrc.clip = FountainSFX;
        audioSrc.loop = true;
        audioSrc.Play();
    }
}
