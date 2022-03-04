using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundDataBase : MonoBehaviour
{
    public static SoundDataBase instance;
    static AudioSource audioSource;

    static Dictionary<string, AudioClip> seAudioDic = new Dictionary<string, AudioClip>();
    static Dictionary<string, AudioClip> bgmAudioDic = new Dictionary<string, AudioClip>();

    [SerializeField] AudioClip[] audioClipSEArray = new AudioClip[0];
    [SerializeField] AudioClip[] audioClipsBGMArray = new AudioClip[0];
    [SerializeField] string[] seAudioNameArray = new string[0];
    [SerializeField] string[] bgmAudioNameArray = new string[0];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("sinngleton");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < audioClipSEArray.Length; i++)
        {
            seAudioDic[seAudioNameArray[i]] = audioClipSEArray[i];
        }

        for (int i = 0; i < bgmAudioNameArray.Length; i++)
        {
            bgmAudioDic[bgmAudioNameArray[i]] = audioClipsBGMArray[i];
        }
    }

    public static void SERing(string seName)
    {
        audioSource.PlayOneShot(seAudioDic[seName]);
    }

    public static void BGMRing(string bgmName)
    {
        audioSource.PlayOneShot(bgmAudioDic[bgmName]);
    }
}
