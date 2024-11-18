using UnityEngine;

using System.Collections;
using System;


public class AudioManager : SingletonBehaviour<AudioManager>
{
    public AudioClip[] backGroundAudioClips;
    public AudioClip[] effectClips;
    private AudioSource[] audioSource;

    private void Start()
    {
        audioSource = GetComponents<AudioSource>();   
    }


    public void BackGroundAudioPlay(int index)
    {
      
        StartCoroutine(WaitForAudioToEnd(index));
    }

    private IEnumerator WaitForAudioToEnd(int index) //오디오 bgm 종료후 다른 브금 재생후 다시 bgm실행
    {
        audioSource[0].clip = backGroundAudioClips[index];
        if (audioSource[0].clip != null)
        {
            audioSource[0].loop = false;
            audioSource[0].Play();
          
            yield return new WaitForSeconds(audioSource[0].clip.length);  // 클립 길이만큼 대기

                audioSource[0].clip = backGroundAudioClips[0];
                audioSource[0].loop=true;
                audioSource[0].Play();
            
        }
    }
    public void EffectsSourcePlay(int index)
    {
        audioSource[1].clip = effectClips[index];
        audioSource[1].Play();
    }
}
