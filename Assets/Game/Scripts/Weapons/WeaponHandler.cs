using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}
public class WeaponHandler : MonoBehaviour
{
    public Animator anim;
    public WeaponFireType weaponFireType;


    [SerializeField]
    private AudioClip shootAudioClip;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private ParticleSystem muzzleFlash;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShootAnimation()
    {
        anim.SetTrigger("Shoot");
       // Play_ShootSound();
    }
    public void Play_ShootSound()
    {
        audioSource.clip = shootAudioClip;
        audioSource.Play();
        muzzleFlashOn();
    }
    public void muzzleFlashOn()
    {
        muzzleFlash.Play();
        //StartCoroutine(MuzzleEffect());
    }
  
    //private IEnumerator MuzzleEffect()
    //{
      //  muzzleFlash.SetActive(true);
       // yield return new WaitForSeconds(.2f);
        //muzzleFlash.SetActive(false);
    //}
}
