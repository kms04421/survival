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

    private IEnumerator WaitForAudioToEnd(int index) //����� bgm ������ �ٸ� ��� ����� �ٽ� bgm����
    {
        audioSource[0].clip = backGroundAudioClips[index];
        if (audioSource[0].clip != null)
        {
            audioSource[0].loop = false;
            audioSource[0].Play();
          
            yield return new WaitForSeconds(audioSource[0].clip.length);  // Ŭ�� ���̸�ŭ ���

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
