using TMPro;
using UnityEngine;

public class PlayerWeaponUI : MonoBehaviour
{
    [SerializeField] private PlayerWeapon weapon;
    [SerializeField] private GameObject pistolImage;
    [SerializeField] private GameObject rifleImage;
    [SerializeField] private GameObject sniperImage;
    [SerializeField] private TMP_Text bulletText;

    void Start()
    {

    }

    void Update()
    {
        pistolImage.SetActive(weapon.CurrentGun == PlayerWeapon.GunType.Pistol);
        rifleImage.SetActive(weapon.CurrentGun == PlayerWeapon.GunType.Rifle);
        sniperImage.SetActive(weapon.CurrentGun == PlayerWeapon.GunType.Sniper);

        bulletText.text = GetBulletText();
    }

    private string GetBulletText()
    {
        var currentGun = weapon.GetCurrentGun();

        if (currentGun == null)
            return string.Empty;

        if (currentGun.TotalBullets == -1)
            return string.Format("{0} / {1}", currentGun.CurrentBulletCount, "INF");

        return string.Format("{0} / {1}", currentGun.CurrentBulletCount, currentGun.TotalBullets);
    }
}
