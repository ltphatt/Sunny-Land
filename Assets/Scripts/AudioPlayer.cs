using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Background music")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField][Range(0f, 1f)] float backgroundMusicVolume;

    public static AudioPlayer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
