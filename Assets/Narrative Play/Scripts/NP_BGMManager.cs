using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class NP_BGMManager : MonoBehaviour
{
    public bool PlayOnAwake;
    public bool Loop;

    [Range(0.1f, 2.0f), SerializeField]
    private float switchDamp = 1.0f;
    [Range(0.0f, 3.0f), SerializeField]
    private float pauseDuration = 1.0f;

    private AudioSource bgmSource;

    [SerializeField]
    private AudioClip clip_angry;
    [SerializeField]
    private AudioClip clip_peaceful;

    private Hashtable m_bgmDatabase;


    public enum BGMID
    {
        None,
        Angry,
        Peaceful,
    }
    private BGMID m_currrentBGM;


    void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.playOnAwake = PlayOnAwake;
        bgmSource.loop = Loop;

        m_bgmDatabase = new Hashtable();
        m_bgmDatabase.Add(BGMID.Angry, clip_angry);
        m_bgmDatabase.Add(BGMID.Peaceful, clip_peaceful);

        m_currrentBGM = BGMID.None;
    }


    public void SwitchTo(BGMID bgm)
    {
        if (bgm != BGMID.Angry)
        {
            NP_GameManager.instance.SwitchStormEffect(false);
        }

        if (bgm != m_currrentBGM)
        {
            StartCoroutine(FadeAudio(bgm));
        }
    }

    private IEnumerator FadeAudio(BGMID next)
    {
        // Fade Out
        while (bgmSource.volume > 0.01)
        {
            bgmSource.volume -= Time.deltaTime / switchDamp;
            yield return null;
        }
        bgmSource.Stop();
        bgmSource.clip = (m_bgmDatabase[next] as AudioClip);
        m_currrentBGM = next;

        yield return new WaitForSeconds(1.0f);

        // Fade In
        bgmSource.Play();

        while (bgmSource.volume < 1)
        {
            bgmSource.volume += Time.deltaTime / 1.0f;
            yield return null;
        }
    }
}
