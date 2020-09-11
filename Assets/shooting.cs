using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
    public float frequency = 0.1f;
    private float currentTime;
    public int magCapacity = 30;
    private int currentMag;
    public float reloadTime = 1f;

    private float shotDuration = .07f;
    private float weaponRange = 50f;
    private float hitForce = 100f;
    public LineRenderer rayLine;
    public Camera kamera;
    public Transform barrel;
    private bool canShoot = true;

    public Text ammo;
    public Image ammoImage;
    private float animationReloadTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentMag = magCapacity;
        ammo.text = currentMag.ToString() + "/" + magCapacity.ToString();
        ammoImage.fillAmount = (float)currentMag / (float)magCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot == false)
        {
            animationReloadTime += Time.deltaTime;
            ammoImage.fillAmount = animationReloadTime / reloadTime;

        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            animationReloadTime = 0;
            StartCoroutine(reload());
        }

        if (currentTime < frequency)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButton(0) && canShoot)
            {

                currentMag--;
                
                if (currentMag <= 0)
                {
                    currentMag = 0;
                    ammo.text = currentMag.ToString() + "/" + magCapacity.ToString();
                    ammoImage.fillAmount = (float)currentMag / (float)magCapacity;
                    return;
                }

                ammo.text = currentMag.ToString() + "/" + magCapacity.ToString();
                ammoImage.fillAmount = (float)currentMag / (float)magCapacity;

                StartCoroutine(shotEffect());

                Vector3 origin = kamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
                RaycastHit hit;
                rayLine.SetPosition(0, barrel.position);
                if (Physics.Raycast(origin, kamera.transform.forward, out hit, weaponRange))
                {
                    rayLine.SetPosition(1, hit.point);
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                    }
                }
                else
                {
                    rayLine.SetPosition(1, origin + (kamera.transform.forward * weaponRange));
                }


                currentTime = 0;
            }
        }
    }

    IEnumerator shotEffect()
    {
        rayLine.enabled = true;

        yield return new WaitForSeconds(shotDuration);

        rayLine.enabled = false;
    }
    IEnumerator reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        currentMag = magCapacity;
        ammo.text = currentMag.ToString() + "/" + magCapacity.ToString();
        ammoImage.fillAmount = (float)currentMag / (float)magCapacity;
        canShoot = true;
    }
}
