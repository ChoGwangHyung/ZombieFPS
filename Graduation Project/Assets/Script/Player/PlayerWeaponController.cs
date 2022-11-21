using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : WeaponController
{
    private PlayerRangedWeapon curRangedWeapon;
    public PlayerRangedWeapon _curRangedWeapon { get { return curRangedWeapon; } }

    private AudioSource audioSource;
    private float audioSourceOriginPitch;
    [SerializeField] private AudioClip reload = null;
    [SerializeField] private AudioClip shoot = null;
    [SerializeField] private AudioClip draw = null;

    [SerializeField] private PlayerRangedWeapon AK = null;
    [SerializeField] private PlayerRangedWeapon Sniper = null;

    [SerializeField] private GameObject gunEntry = null;
    private Vector3 gunEntryPos;

    private bool doWeaponChange = false;

    void Awake()
    {
        gunEntryPos = gunEntry.transform.localPosition;
        curRangedWeapon = AK;    
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSourceOriginPitch = audioSource.pitch;
        AudioController.instance.AddAudioSource(audioSource);
    }

    public void Fire() { if (doWeaponChange == true) return; curRangedWeapon.Fire(); PlayShootAudio(); }
    public void Reload() { if (doWeaponChange == true) return; curRangedWeapon.Reload(); PlayReloadAudio(); }
    public bool CanFire() { return doWeaponChange == false && curRangedWeapon.CanFire(); }    

    public void WeaponChange()
    {
        doWeaponChange = true;
        curRangedWeapon.DoWeaponChange();
        if (audioSource.isPlaying == true || IsInvoking("ReturnAudioOriginPitch") == true)
        {
            audioSource.Stop();
            CancelInvoke("ReturnAudioOriginPitch");
            ReturnAudioOriginPitch();
        }
        PlayDrawAudio();

        if (curRangedWeapon == AK)
        {            
            curRangedWeapon.GetComponent<SkinnedMeshRenderer>().enabled = false;            
            curRangedWeapon = Sniper;
            curRangedWeapon.gameObject.SetActive(true);
        }
        else
        {
            curRangedWeapon.gameObject.SetActive(false);
            curRangedWeapon = AK;
            curRangedWeapon.GetComponent<SkinnedMeshRenderer>().enabled = true;
            gunEntry.transform.localPosition = gunEntryPos;
        }

        UiController.instance.ChangeWeaponImage(curRangedWeapon._weaponStat._weaponName);
    }

    public void Zoom()
    {
        if (curRangedWeapon != Sniper || doWeaponChange == true) return;
        curRangedWeapon.Zoom();
    }
    public void StopZoom()
    {
        if (curRangedWeapon != Sniper) return;
        curRangedWeapon.StopZoom();
    }    

    public void FinishWeaponChange() { doWeaponChange = false; }

    private void PlayReloadAudio()
    {
        audioSource.pitch = reload.length / curRangedWeapon.GetReloadTime();
        audioSource.PlayOneShot(reload);
        Invoke("ReturnAudioOriginPitch", curRangedWeapon.GetReloadTime());
    }
    private void ReturnAudioOriginPitch() { audioSource.pitch = audioSourceOriginPitch; }
    private void PlayShootAudio() { audioSource.PlayOneShot(shoot); }
    private void PlayDrawAudio() { audioSource.PlayOneShot(draw); }
}
