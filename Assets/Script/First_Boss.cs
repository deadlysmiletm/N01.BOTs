using UnityEngine;
using System.Collections;

public class First_Boss : MonoBehaviour
{
    public GameObject Arma;
    public GameObject Player;
    public GameObject BulletPrefab;
    public GameObject Mira1;
    public SpriteRenderer sr;
    public Sprite FullVida;
    public Sprite MitadVida;
    public Sprite PocaVida;

    public Vector3 Invervido;
    public Vector3 Normal;
    public float Vida = 100;
    public float Speed;
    public float SpeedMedium;
    public float SpeedUltra;
    public bool Movimiento;
    public bool MovDer;
    public bool MovIzq;
    public bool MovArriba;
    public bool MovAbajo;
    public bool patrón;
    public bool GiroDer;
    public bool GiroIzq;
    public float TiempoTiro;
    public float RangoTiro;
    public float RangoNormal;
    public float RangoLento;
    public int Disparos = 0;
    public bool Dead;

    public float[] MovVertical = new float[2];
    public float[] MovHorizontal = new float[3];

    // Use this for initialization
    void Start ()
    {
        SpeedMedium = Speed + 10;
        SpeedUltra = SpeedMedium + 10;
        Invervido = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Normal = transform.localScale;
        RangoNormal = RangoTiro;
        RangoLento = RangoTiro * 2;
        int DecisiónInicial = Random.Range(0, 2);
        sr = GetComponent<SpriteRenderer>();

        Movimiento = true;

        if(DecisiónInicial == 0)
        {
            MovDer = true;
        }
        else
        {
            MovIzq = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        VidaManager();
        Weapon();

        if(Movimiento == true)
        {
            Move();
        }

        if(Movimiento == true)
        {
            RangoTiro = RangoLento;
        }
        else
        {
            RangoTiro = RangoNormal;
        }

        Shoot();
        Giros();
	}

    public void VidaManager()
    {
        if(Vida <= 50)
        {
            sr.sprite = MitadVida;
            Speed = SpeedMedium;
        }
        if(Vida <= 25)
        {
            sr.sprite = PocaVida;
            Speed = SpeedUltra;
        }
        if(Vida <= 0)
        {
            this.gameObject.SetActive(false);
        }

    }

    public void Move()
    {
        if(MovArriba == true)
        {
            transform.position += Vector3.up * Speed * Time.deltaTime;
            LímiteArriba();
        }
        if(MovAbajo == true)
        {
            transform.position += Vector3.down * Speed * Time.deltaTime;
            LímiteAbajo();
        }
        if(MovIzq == true)
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
            GiroIzq = true;
            GiroDer = false;
            LímiteIzq(); 
        }
        if(MovDer == true)
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
            GiroDer = true;
            GiroIzq = false;
            LímiteDer();
        }
    }

    public void Weapon()
    {
        Arma.transform.right = transform.position - Player.transform.position;
    }

    public void LímiteDer()
    {
        if(transform.position.x >= MovHorizontal[2])
        {
            MovDer = false;
            MovAbajo = true;
            GiroDer = false;
            GiroIzq = true;
        }
    }

    public void LímiteIzq()
    {
        if(transform.position.x <= MovHorizontal[0])
        {
            MovIzq = false;
            MovAbajo = true;
            GiroDer = true;
            GiroIzq = false;
        }

    }

    public void LímiteArriba()
    {
        if(transform.position.y >= MovVertical[0])
        {
            Movimiento = false;
            if(patrón == true)
            {
                if(GiroDer == true)
                {
                    MovDer = true;
                    MovArriba = false;
                }
                else if(GiroIzq == true)
                {
                    MovIzq = true;
                    MovArriba = false;
                }

                Movimiento = true;
            }
        }
    }
    
    public void LímiteAbajo()
    {
        if(transform.position.y <= MovVertical[1])
        {
            Movimiento = false;
            patrón = true;
        }
    }

    public void Giros()
    {
        if (GiroDer == true)
        {
            transform.localScale = Invervido;
            Arma.transform.localScale = new Vector3(-1, -1, 1);
        }
        else if (GiroIzq == true)
        {
            transform.localScale = Normal;
            Arma.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Shoot()
    {
        if (Time.time > TiempoTiro)
        {
            TiempoTiro = Time.time + RangoTiro;
            GameObject BulletRespawn1 = GameObject.Instantiate(BulletPrefab);
            BulletRespawn1.transform.rotation = Arma.transform.rotation;
            BulletRespawn1.transform.position = Mira1.transform.position;
            Bullet BulletScript1 = BulletRespawn1.GetComponent<Bullet>();
            BulletRespawn1.layer = 12;
            BulletScript1.dir = -1;

            if(Movimiento == false)
            {
                Disparos++;
                if (Disparos == 5 && MovAbajo == true)
                {
                    MovAbajo = false;
                    MovArriba = true;
                    Movimiento = true;
                    Disparos = 0;
                }
                else if (Disparos == 5 && MovArriba == true)
                {
                    MovAbajo = true;
                    MovArriba = false;
                    Movimiento = true;
                    Disparos = 0;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D bala)
    {
        if(bala.gameObject.layer == 10)
        {
            Vida--;
        }
    }
}
