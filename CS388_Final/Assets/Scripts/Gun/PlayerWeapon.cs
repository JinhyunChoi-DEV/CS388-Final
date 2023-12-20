/*
* Name: PlayerWeapon
* Author: Jinhyun Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public enum GunType
    {
        Pistol, Rifle, Sniper
    }

    [SerializeField] private PlayerState state;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Gun pistol;
    [SerializeField] private Gun rifle;
    [SerializeField] private Gun sniper;
    public GunType CurrentGun { get; private set; }
    public AudioClip Swap_Clip;

    public Gun GetCurrentGun()
    {
        if (CurrentGun == GunType.Pistol)
            return pistol;
        if (CurrentGun == GunType.Rifle)
            return rifle;
        if (CurrentGun == GunType.Sniper)
            return sniper;

        return null;
    }

    void Start()
    {
        CurrentGun = GunType.Pistol;

        UpdateCurrentGun();
    }

    void Update()
    {
        if (!PlayerData.IsAlive)
        {
            pistol.gameObject.SetActive(false);
            rifle.gameObject.SetActive(false);
            sniper.gameObject.SetActive(false);
            return;
        }

        var switchGun = PlayerInput.Instance.InputData.SwitchGun;

        var currentGun = GetCurrentGun();
        currentGun.Holder.SetActive(state.State != State.Dodge && state.State != State.DoingDodge);
        if (state.State != State.Dodge && switchGun && !GetCurrentGun().WaitReload)
            SwitchGun();
    }

    private void SwitchGun()
    {
        var gun = GetCurrentGun();
        gun.ClearFlag();

        if (CurrentGun == GunType.Pistol)
            CurrentGun = GunType.Rifle;
        else if (CurrentGun == GunType.Rifle)
            CurrentGun = GunType.Sniper;
        else if (CurrentGun == GunType.Sniper)
            CurrentGun = GunType.Pistol;

        SoundManager.instance.SFXPlay("Swap", Swap_Clip);
        UpdateCurrentGun();
    }

    private void UpdateCurrentGun()
    {
        pistol.gameObject.SetActive(CurrentGun == GunType.Pistol);
        rifle.gameObject.SetActive(CurrentGun == GunType.Rifle);
        sniper.gameObject.SetActive(CurrentGun == GunType.Sniper);
    }

}
