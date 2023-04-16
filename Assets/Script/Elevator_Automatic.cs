using UnityEngine;
using System.Collections;

public class Elevator_Automatic : MonoBehaviour
{

    public bool ParaArriba;
    public bool ParaAbajo;
    public bool Movimiento;
    public float ParadaFinal;
    public float Speed;

    public GameObject Chara;
    public BoxCollider2D bc;

	// Use this for initialization
	void Start ()
    {
        Chara = GameObject.Find("Chara");
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        Move();
    }

    public void Move()
    {
        if (Movimiento == true)
        {
            if (ParaArriba == true)
            {
                transform.position += Vector3.up * Speed * Time.deltaTime;

                if (transform.position.y >= ParadaFinal)
                {
                    Movimiento = false;
                    bc.enabled = false;
                }
            }
            if (ParaAbajo == true)
            {
                transform.position += Vector3.down * Speed * Time.deltaTime;

                if (transform.position.y <= ParadaFinal)
                {
                    Movimiento = false;
                    bc.enabled = false;
                }
            }
        }
    }

    public void OnTriggerStay2D(Collider2D player)
    {
        if (player.gameObject.layer == 8)
        {
            Movimiento = true;
            if (bc.enabled == true)
            {
                Chara.transform.position = new Vector3(Chara.transform.position.x, transform.position.y - 1.32f, Chara.transform.position.z);
            }
        }
    }


}
