using System.Collections;
using UnityEngine;

public class ReloadBarUI : MonoBehaviour
{
    [SerializeField] PlayerWeapon weapon;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject reloadImage;

    public bool DoingReload { get; private set; }

    private Gun currentGun => weapon.GetCurrentGun();

    void Start()
    {
        panel.SetActive(false);
        DoingReload = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeSelf == false && currentGun.WaitReload)
            StartCoroutine(UpdateReloadUI());
    }

    IEnumerator UpdateReloadUI()
    {
        DoingReload = true;
        panel.SetActive(true);
        bar.transform.position =
            new Vector3(start.transform.position.x, bar.transform.position.y, bar.transform.position.z);

        float timer = 0.0f;
        while (timer < currentGun.ReloadDelay)
        {
            timer += Time.deltaTime;
            var t = timer / currentGun.ReloadDelay;
            var x = Mathf.Lerp(start.transform.position.x, end.transform.position.x, t);
            var rotation = Mathf.Lerp(0, 360, t);

            bar.transform.position = new Vector3(x, bar.transform.position.y, bar.transform.position.z);
            reloadImage.transform.localRotation = Quaternion.Euler(0, 0, rotation);
            yield return null;
        }

        reloadImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
        DoingReload = false;
        panel.SetActive(false);
    }
}
