using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    Animator anim;

    public GameObject firePosition;
    public GameObject bombFactory;
    public float throwPower = 15f;

    public GameObject bulletEffect;
    ParticleSystem ps;

    public int weaponPower = 5;
    public Text wModeText;

    public GameObject[] eff_Flash;

    enum WeaponMode
    { 
       Normal,
       Sniper,
       Bazooka
    }
    WeaponMode wMode;

    bool ZoomMode = false;



    public GameObject bazookaFactory;

    private int currentMissile;
    public int maxMissile = 5;

    public Text BulletCount;


    // Start is called before the first frame update
    void Start()
    {
        ps=bulletEffect.GetComponent<ParticleSystem>();

        anim=GetComponentInChildren<Animator>();

        wMode = WeaponMode.Normal;

        currentMissile = maxMissile;

    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            switch (wMode)
            { 
               case WeaponMode.Normal:
                    GameObject bomb = Instantiate(bombFactory);
                    bomb.transform.position=firePosition.transform.position;

                    Rigidbody rb = bomb.GetComponent<Rigidbody>();

                    rb.AddForce(Camera.main.transform.forward*throwPower,ForceMode.Impulse);
                    break;
                case WeaponMode.Sniper:
                    if (!ZoomMode)
                    {
                        Camera.main.fieldOfView = 15f;
                        ZoomMode = true;
                    }
                    else 
                    {
                        Camera.main.fieldOfView = 60f;
                        ZoomMode=false;
                    }
                    break;
                case WeaponMode.Bazooka:

                    if (currentMissile > 0)
                    {
                        currentMissile -= 1;

                        GameObject missile = Instantiate(bazookaFactory);
                        missile.transform.position = firePosition.transform.position;
                        Rigidbody rb2 = missile.GetComponent<Rigidbody>();

                        rb2.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

                        BulletCount.text = "" + currentMissile;
                    }
                    else
                    {
                        Debug.Log("더이상 사용할 수 없습니다.");
                    }
                    


                    break;

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (anim.GetFloat("MoveMotion") <0.2f)
            {
                anim.SetTrigger("Attack");

                StartCoroutine(ShootEffectOn(0.05f));

            }
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                bulletEffect.transform.position = hitInfo.point;
                bulletEffect.transform.forward = hitInfo.normal;

                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }

                
             ps.Play();
  
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;
            Camera.main.fieldOfView = 60f;
            ZoomMode = false;

            wModeText.text = "Normal Mode";

            BulletCount.text = "∞";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;

            wModeText.text = "Sniper Mode";

            BulletCount.text = "∞";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            wMode = WeaponMode.Bazooka;

            wModeText.text = "Bazooka Mode";

            BulletCount.text = "" + currentMissile;

        }
    }


    IEnumerator ShootEffectOn(float duration)
    {
        int num = Random.Range(0, eff_Flash.Length);
        eff_Flash[num].SetActive(true);

        yield return new WaitForSeconds(duration);

        eff_Flash[num].SetActive(false);

    }
}
