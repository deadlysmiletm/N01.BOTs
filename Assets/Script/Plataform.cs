using UnityEngine;
using System.Collections;

public class Plataform : MonoBehaviour
{
    public TargetJoint2D tj;

    public float Speed;
    public bool Movimiento;
    public bool MovDer;
    public bool MovIzq;
    public float[] Paradas = new float[2];

	// Use this for initialization
	void Start ()
    {
        tj = GetComponent<TargetJoint2D>();
        Movimiento = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Movimiento == true)
        {
            Move();
        }
	}

    public void Move()
    {
        if (tj.target.x >= Paradas[0] -0.05 && MovDer == true)
        {
            tj.target += new Vector2(1, 0) * Speed * Time.deltaTime;
            if (tj.target.x > Paradas[1])
            {
                Movimiento = false;
                MovDer = false;
                MovIzq = true;
            }
        }
        if (tj.target.x <= Paradas[1] +0.05 && MovIzq == true)
        {
            tj.target += new Vector2(-1, 0) * Speed * Time.deltaTime;
            if (tj.target.x < Paradas[0])
            {
                Movimiento = false;
                MovDer = true;
                MovIzq = false;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D interruptor)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Movimiento = true;
        }
    }


}
