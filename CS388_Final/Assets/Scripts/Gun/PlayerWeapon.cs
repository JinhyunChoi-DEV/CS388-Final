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
        var switchGun = PlayerInput.Instance.InputData.SwitchGun;

        var currentGun = GetCurrentGun();
        currentGun.Holder.SetActive(state.State != State.Dodge);
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

        UpdateCurrentGun();
    }

    private void UpdateCurrentGun()
    {
        pistol.gameObject.SetActive(CurrentGun == GunType.Pistol);
        rifle.gameObject.SetActive(CurrentGun == GunType.Rifle);
        sniper.gameObject.SetActive(CurrentGun == GunType.Sniper);
    }

}
