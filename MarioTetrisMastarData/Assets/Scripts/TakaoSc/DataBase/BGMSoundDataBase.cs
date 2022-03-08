using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSoundDataBase : MonoBehaviour
{
    public static BGMSoundDataBase instance;
    static AudioSource audioSource;

    static Dictionary<string, AudioClip> bgmAudioDic = new Dictionary<string, AudioClip>();

    [SerializeField] AudioClip[] bgmAudioClipsArray = new AudioClip[0];
    [SerializeField] string[] bgmAudioNameArray = new string[0];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bgmAudioNameArray.Length; i++)
        {
            bgmAudioDic[bgmAudioNameArray[i]] = bgmAudioClipsArray[i];
        }
    }

    public static void BGMRing(string bgmName)
    {
        audioSource.PlayOneShot(bgmAudioDic[bgmName]);
    }
}
