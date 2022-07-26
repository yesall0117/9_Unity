using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    Animator anim;

    public float MoveSpeed = 7f;

    CharacterController cc;

    float gravity = -20f;
    public float YVelocity = 0;

    public float jumpPower = 10f;
    public bool isJumping = false;

    public int hp = 20;

    int maxHp = 20;
    public Slider hpSlider;

    public GameObject hitEffect;


    public void DamageAction(int damage)
    {
        hp-= damage;
        print("플레이어 체력: " + hp);

        if (hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        cc=GetComponent<CharacterController>();

        anim=GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir=new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        anim.SetFloat("MoveMotion", dir.magnitude);

        

        transform.position += dir * MoveSpeed * Time.deltaTime;



        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        { 
           isJumping = false;
            YVelocity = 0;
        }
        if (Input.GetButtonDown("Jump")&&!isJumping)
        {
            YVelocity = jumpPower;
            isJumping=true;
        }

        YVelocity +=gravity*Time.deltaTime;
        dir.y = YVelocity;

        cc.Move(dir*MoveSpeed*Time.deltaTime);

        hpSlider.value = (float)hp / (float)maxHp;

    }
}
