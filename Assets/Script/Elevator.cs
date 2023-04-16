using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    public bool MovArriba;
    public bool MovAbajo;
    public bool Esperar = false;
    public bool Move = false;
    public int Decisión;
    public float Speed;
    public float[] ParadasInter = new float[3];
    public bool AltoArriba = false;
    public bool AltoAbajo = false;
    public float Espera;
    public float ValorEspera;

    public GameObject Player;

    void Start ()
    {
        Player = GameObject.Find("Chara");
        ValorEspera = Espera;
    }
	
	void Update ()
    {
        Paradas();
        Movimiento();
        AutomaticMove();
	}

    public void Movimiento()
    {
        if(MovArriba == true)
        {
            transform.position += Vector3.up * Speed * Time.deltaTime;
            Esperar = false;
        }
        if(MovAbajo == true)
        {
            transform.position += Vector3.down * Speed * Time.deltaTime;
            Esperar = false;
        }
    }

    public void Paradas()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            if (Move == true)
            {
                ParadaArriba();
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (Move == true)
            {
                ParadaAbajo();
            }
        }
        for (int i = 0; i < ParadasInter.Length; i++)
        {
            if (Esperar == false)
            {
                if (transform.position.y > ParadasInter[i] - 0.05 && transform.position.y < ParadasInter[i] + 0.05)
                {
                    MovAbajo = false;
                    MovArriba = false;
                    Esperar = true;
                    Espera = ValorEspera;
                }
            }
        }
    }

    public void OnTriggerStay2D(Collider2D player)
    {
        if (player.gameObject.layer == 8)
        {
            Move = true;
            Player.transform.position = new Vector3(Player.transform.position.x, transform.position.y -1.35f, player.transform.position.z);
        }
    }

    public void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.layer == 8)
        {
            Move = false;
        }
    }

    public void AutomaticMove ()
    {
        if (MovAbajo == false && MovArriba == false)
        {
            Espera -= Time.deltaTime;
            if (Espera <= 0)
            {
                if (Decisión == 0)
                {
                    if(transform.position.y >= ParadasInter[0] - 0.5)
                    {
                        Decisión = 2;
                    }
                    else if(transform.position.y <= ParadasInter[ParadasInter.Length - 1] + 0.5)
                    {
                        Decisión = 1;
                    }
                    //Decisión = Random.Range(1, 3);
                }
                if (Decisión == 1)
                {
                    if (AltoArriba == true)
                    {
                        Decisión = 0;
                        Espera = ValorEspera;
                    }
                    else
                    {
                        MovArriba = true;
                    }
                }
                else if(Decisión == 2)
                {
                    if(AltoAbajo == true)
                    {
                        Decisión = 0;
                        Espera = ValorEspera;
                    }
                    else
                    {
                        MovAbajo = true;
                    }
                }
            }
        }
        if(MovArriba == true)
        {
            ParadaArriba();
        }
        if(MovAbajo == true)
        {
            ParadaAbajo();
        }
    }

    public void ParadaArriba()
    {
        if (transform.position.y < ParadasInter[0])
        {
            MovArriba = true;
            MovAbajo = false;
            AltoArriba = false;
            AltoAbajo = false;
        }
        if (MovArriba == true && transform.position.y >= ParadasInter[0])
        {
            MovArriba = false;
            AltoArriba = true;
        }
    }

    public void ParadaAbajo()
    {
        if (transform.position.y > ParadasInter[ParadasInter.Length - 1])
        {
            MovAbajo = true;
            MovArriba = false;
            AltoArriba = false;
            AltoAbajo = false;
        }

        if (MovAbajo == true && transform.position.y <= ParadasInter[ParadasInter.Length - 1])
        {
            MovAbajo = false;
            AltoAbajo = true;
        }
    }
}
