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


    void Start()
    {
        CurrentGun = GunType.Pistol;

        UpdateCurrentGun();
    }

    void Update()
    {
        var switchGun = PlayerInput.Instance.InputData.SwitchGun;

        gunTransform.gameObject.SetActive(state.State != State.Dodge);
        if (state.State != State.Dodge && switchGun)
            SwitchGun();
    }

    private void SwitchGun()
    {
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
