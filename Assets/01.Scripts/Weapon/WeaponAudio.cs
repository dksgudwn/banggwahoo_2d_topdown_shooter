using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : AudioPlayer
{
    public AudioClip shootBulletClip = null, outOffBulletClip = null, reloadClup = null;
    public void PlayShootSound()
    {
        PlayClip(shootBulletClip);
    }
    public void PlayOutOffBulletSound()
    {
        PlayClip(outOffBulletClip);
    }
    public void PlayReloadSound()
    {
        PlayClip(reloadClup);
    }
}
