using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAction : MonoBehaviour
{
    public float speed = 30;


    public GameObject missileEffect;

    public int attackPower = 10;

    public float explosionRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].GetComponent<EnemyFSM>().HitEnemy(attackPower);
        }

        GameObject eff = Instantiate(missileEffect);
        eff.transform.position = transform.position;


        Destroy(gameObject);
    }
}
