using UnityEngine;
using System.Collections;

public class Enemy_Ground : MonoBehaviour
{
    public Vector3 PosiciónInicial;

    public GameObject Brillo;
    public GameObject Ojo;
    public GameObject Arma;
    public GameObject Player;
    public GameObject Visión;
    public GameObject BulletPrefab;
    public GameObject Camara;
    public GameObject Mira;
    public BoxCollider2D Playerbc;
    public BoxCollider2D bc;
    public BoxCollider2D Visiónbc;
    public SpriteRenderer sr;
    public SpriteRenderer Brillosr;
    public SpriteRenderer Ojosr;
    public SpriteRenderer Armasr;
    public Sprite CuerpoDer;
    public Sprite CuerpoIzq;
    public Sprite OjoNormal;
    public Sprite BrilloNormal;
    public Sprite ArmaDer;
    public Sprite ArmaIzq;
    public Sprite CuerpoAlertaDer;
    public Sprite CuerpoAlertaIzq;
    public Sprite OjoAlerta;
    public Sprite BrilloAlerta;

    public Color Normal;
    public Color Daño;
    public Color MuchoDaño;

    public int Vida;
    public float Speed;
    public float TiempoTiro;
    public float RangoTiro;
    public float RespawnTime;
    public float RespawnDefault;
    public bool Der;
    public bool Izq;
    public bool ArmaDir;
    public bool Alerta = false;
    public bool Disparo = false;
    public bool Dead = false;


    // Use this for initialization
    void Start ()
    {
        PosiciónInicial = transform.position;
        Player = GameObject.Find("Chara");
        Camara = GameObject.Find("Main Camera");
        RespawnDefault = RespawnTime;

        bc = GetComponent<BoxCollider2D>();
        Visiónbc = Visión.GetComponent<BoxCollider2D>();
        Playerbc = Player.GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        Brillosr = Brillo.GetComponent<SpriteRenderer>();
        Ojosr = Ojo.GetComponent<SpriteRenderer>();
        Armasr = Arma.GetComponent<SpriteRenderer>();
        Normal = sr.color;

        int DirecciónRandom = Random.Range(0, 2);
        if(DirecciónRandom == 0)
        {
            Der = true;
            Izq = false;
            ArmaDir = false;
        }
        else
        {
            Izq = true;
            Der = false;
            ArmaDir = true;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        Sprite();
        if (Dead == false)
        {
            PlayerDetection();
            Move();
        }
        else
        {
            bc.enabled = false;
            RespawnTime -= Time.deltaTime;
            if(RespawnTime <= 0)
            {
                RespawnTime = RespawnDefault;
                bc.enabled = true;
                transform.position = PosiciónInicial;
                Dead = false;
            }
        }
	}

    public void Sprite()
    {
        if (Dead == false)
        {
            if (Der == true)
            {
                sr.sprite = CuerpoDer;
                Armasr.sprite = ArmaDer;
                if (Alerta == true)
                {
                    sr.sprite = CuerpoAlertaDer;
                }
                else
                {
                    sr.sprite = CuerpoDer;
                }
            }
            if (Izq == true)
            {
                sr.sprite = CuerpoIzq;
                Armasr.sprite = ArmaIzq;
                if (Alerta == true)
                {
                    sr.sprite = CuerpoAlertaIzq;
                }
                else
                {
                    sr.sprite = CuerpoIzq;
                }
            }

            if (Alerta == true)
            {
                Brillosr.sprite = BrilloAlerta;
                Ojosr.sprite = OjoAlerta;
            }
            else
            {
                Brillosr.sprite = BrilloNormal;
                Ojosr.sprite = OjoNormal;
            }
        }
        else
        {
            sr.sprite = null;
            Armasr.sprite = null;
            Ojosr.sprite = null;
            Brillosr.sprite = null;
            Vida = 3;
            sr.color = Normal;
            Armasr.color = Normal;
            Ojosr.color = Normal;
            Brillosr.color = Normal;
            bc.enabled = false;
        }
    }

    public void Move()
    {
        if(Alerta == false)
        {
            if(Der == true)
            {
                transform.position += Vector3.right * Speed * Time.deltaTime;
                Brillosr.flipX = true;
                Ojosr.flipX = true;
                Ojo.transform.localPosition = new Vector3(0.916f, Ojo.transform.localPosition.y, 0);
                Visión.transform.localPosition = new Vector3(14.96f, Visión.transform.localPosition.y, 0);
                Mira.transform.localPosition = new Vector3(2.404f, Mira.transform.localPosition.y, 0);
            }
            if(Izq == true)
            {
                transform.position += Vector3.left * Speed * Time.deltaTime;
                Brillosr.flipX = false;
                Ojosr.flipX = false;
                Ojo.transform.localPosition = new Vector3(-0.916f, Ojo.transform.localPosition.y, 0);
                Visión.transform.localPosition = new Vector3(-14.96f, Visión.transform.localPosition.y, 0);
                Mira.transform.localPosition = new Vector3(-2.404f, Mira.transform.localPosition.y, 0);
            }
        }
        else
        {
            Shoot();
        }
    }

    public void OnTriggerEnter2D(Collider2D límite)
    {
        if(límite.gameObject.layer == 11)
        {
            if(Der == true)
            {
                Der = false;
                Izq = true;
            }
            else if(Izq == true)
            {
                Der = true;
                Izq = false;
            }
        }
        if (límite.gameObject.layer == 10)
        {
            Vida--;
            if (Vida == 2)
            {
                sr.color = Daño;
                Brillosr.color = Daño;
                Ojosr.color = Daño;
                Armasr.color = Daño;
            }
            if (Vida == 1)
            {
                sr.color = MuchoDaño;
                Brillosr.color = MuchoDaño;
                Ojosr.color = MuchoDaño;
                Armasr.color = MuchoDaño;
            }
            else if (Vida <= 0)
            {
                Main CamaraScript = Camara.GetComponent<Main>();
                CamaraScript.Puntaje += 100;
                Dead = true;
            }
        }

    }

    public void PlayerDetection()
    {
        if (Visiónbc.IsTouching(Playerbc) == true)
        {
            Alerta = true;
        }
        else
        {
            Alerta = false;
        }
    }

    public void Shoot()
    {
        if (Time.time > TiempoTiro)
        {
            TiempoTiro = Time.time + RangoTiro;
            GameObject BulletRespawn = GameObject.Instantiate(BulletPrefab);
            BulletRespawn.transform.rotation = Arma.transform.rotation;
            BulletRespawn.transform.position = Mira.transform.position;
            Bullet BulletScript = BulletRespawn.GetComponent<Bullet>();
            BulletRespawn.layer = 12;
            BulletScript.Speed /= 2;

            if (Armasr.sprite == ArmaIzq)
            {
                BulletScript.dir = -1;
            }
        }
    }
}
