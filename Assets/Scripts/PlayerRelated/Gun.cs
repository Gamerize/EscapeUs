using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Gun Stats")]
    public int magazineSize = 30;
    public int usableBullets = 270;
    public float fireRate = 0.1f;
    public float ReloadTime = 2f;
    bool IsReloading;

    //Gun Flashes
    public Image flashImage;
    public Sprite[] gunFlash; 

    public Transform shootPoint;
    [SerializeField] private GameObject[] m_bulletHoleSprites;
    [SerializeField] private ParticleSystem GunSparks;

    public TextMeshProUGUI m_AmmoText;
    public TextMeshProUGUI m_BulletText;

    public EnemyHealth m_enemyHealth;
    public DummyHealth m_DummyHealth;

    public Animator ReloadAnimation;

    //gun variables
    public int m_bulletCount;
    public int m_bulletsRemain;
    bool m_canFire;

    private void Start()
    {
        m_bulletCount = magazineSize;
        m_bulletsRemain = usableBullets;
        m_canFire = true;
        IsReloading = false;
        m_AmmoText.text = "Ammo: " + m_bulletCount;
        m_BulletText.text = "Bullet: " + m_bulletsRemain;
    }

    private void Update()
    {
        if(Input.GetButton("Fire1") && m_canFire && m_bulletCount > 0 && IsReloading == false)
        {
            StartCoroutine(FiringGun());
        }
        else if(Input.GetKeyDown(KeyCode.R) && m_bulletsRemain > 0 && m_bulletCount < magazineSize)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        int reloadAmount = magazineSize - m_bulletCount;
        if(reloadAmount >= m_bulletsRemain) //checking if the reload amount is more than the remaining amount of bullets
        {
            IsReloading = true;
            ReloadAnimation.SetBool("Reload", true);
            yield return new WaitForSeconds(ReloadTime - 0.25f);
            ReloadAnimation.SetBool("Reload", false);
            yield return new WaitForSeconds(0.25f);
            
            m_bulletCount += m_bulletsRemain; //reload the gun equal to the remaining amount of bullets
            m_bulletsRemain -= reloadAmount;
            m_AmmoText.text = "Ammo: " + m_bulletCount;
            m_BulletText.text = "Bullet: " + m_bulletsRemain;
            FindObjectOfType<AudioManager>().playAudio("Reload Sound");
            IsReloading = false;
        }
        else 
        {
            IsReloading = true;
            ReloadAnimation.SetBool("Reload", true);
            yield return new WaitForSeconds(ReloadTime - 0.25f);
            ReloadAnimation.SetBool("Reload", false);
            yield return new WaitForSeconds(0.25f);

            m_bulletCount = magazineSize;
            m_bulletsRemain -= reloadAmount;
            m_AmmoText.text = "Ammo: " + m_bulletCount;
            m_BulletText.text = "Bullet: " + m_bulletsRemain;
            FindObjectOfType<AudioManager>().playAudio("Reload Sound");
            IsReloading = false;
        }
    }

    IEnumerator FiringGun()
    {
        StartCoroutine(GunFlashing());

        RaycastHit hitInfo;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hitInfo)) //checking for colliders with raycast
        {
            if (hitInfo.collider.tag == "Wall")
            {
                Debug.Log(m_bulletHoleSprites[Random.Range(0, m_bulletHoleSprites.Length)]); //check if sprite is randomized
                GameObject selectedBulletHole = m_bulletHoleSprites[Random.Range(0, m_bulletHoleSprites.Length)];
                Instantiate(GunSparks, new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z), Quaternion.LookRotation(hitInfo.normal));

                float offset = 0.01f;
                Vector3 Adjust = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
                Adjust = Adjust + (hitInfo.normal * offset);
                Instantiate(selectedBulletHole, Adjust, Quaternion.LookRotation(hitInfo.normal));
                FindObjectOfType<AudioManager>().playAudio("Hit Object");
            }
            if (hitInfo.collider.tag == "Enemy")
            {
                m_enemyHealth = hitInfo.transform.GetComponent<EnemyHealth>();
                Instantiate(GunSparks, new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z), Quaternion.LookRotation(hitInfo.normal));
                m_enemyHealth.Damage(10);
                FindObjectOfType<AudioManager>().playAudio("Hit Object");
            }
            if (hitInfo.collider.tag == "Dummy")
            {
                m_DummyHealth = hitInfo.transform.GetComponent<DummyHealth>();
                Instantiate(GunSparks, new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z), Quaternion.LookRotation(hitInfo.normal));
                m_DummyHealth.Damage(10);
                FindObjectOfType<AudioManager>().playAudio("Hit Object");
            }
        }

        FindObjectOfType<AudioManager>().playAudio("Gun Sound");
        m_canFire = false;
        m_bulletCount--;
        m_AmmoText.text = "Ammo: " + m_bulletCount.ToString();

        yield return new WaitForSeconds(fireRate);
        m_canFire = true;
    }

    IEnumerator GunFlashing()
    {

        flashImage.sprite = gunFlash[Random.Range(0, gunFlash.Length)]; //randomly choose a sprite
        flashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        flashImage.sprite = null;
        flashImage.color = new Color(0, 0, 0, 0);
    }  
}
