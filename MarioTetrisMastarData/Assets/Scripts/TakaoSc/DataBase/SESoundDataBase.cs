using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SESoundDataBase : MonoBehaviour
{
    public static SESoundDataBase instance;
    static AudioSource audioSource;

    static Dictionary<string, AudioClip> seAudioDic = new Dictionary<string, AudioClip>();
    static Dictionary<string, AudioClip> bgmAudioDic = new Dictionary<string, AudioClip>();

    [SerializeField] AudioClip[] seAudioClipArray = new AudioClip[0];
    [SerializeField] string[] seAudioNameArray = new string[0];

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

        for (int i = 0; i < seAudioClipArray.Length; i++)
        {
            seAudioDic[seAudioNameArray[i]] = seAudioClipArray[i];
        }
    }

    public static void SERing(string seName)
    {
        audioSource.PlayOneShot(seAudioDic[seName]);
    }

 
}
