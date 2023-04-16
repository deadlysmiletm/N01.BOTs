using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Vector3 PosiciónInicial;
    public Vector3 Mouse;
    public bool ShieldActive = false;
    public bool Salto;
    public bool end = false;
    public bool ObjDes = false;
    public bool Tutopausa = false;
    public float FuerzaSalto;
    public float Speed;
    public int Vidas;

    public GameObject Ojo;
    public GameObject Cabeza;
    public GameObject Brillo;
    public GameObject Arma;
    public GameObject BulletPrefab;
    public GameObject Mira;
    public GameObject Escudo;
    public GameObject Cámara;

    public Main CámaraScript;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public BoxCollider2D Escudobc;

    public SpriteRenderer Brillosr;
    public SpriteRenderer Cabezasr;
    public SpriteRenderer Armasr;
    public SpriteRenderer Escudosr;

    public Sprite ArmaDer;
    public Sprite ArmaIzq;
    public Sprite ShieldDer;
    public Sprite ShieldIzq;



    // Use this for initialization
    void Start()
    {
        transform.position = PosiciónInicial;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        Cabezasr = Cabeza.GetComponent<SpriteRenderer>();
        Brillosr = Brillo.GetComponent<SpriteRenderer>();
        Armasr = Arma.GetComponent<SpriteRenderer>();
        Escudosr = Escudo.GetComponent<SpriteRenderer>();
        Escudobc = Escudo.GetComponent<BoxCollider2D>();
        Cámara = GameObject.Find("Main Camera");
        CámaraScript = Cámara.GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CámaraScript.pausa == false)
        {
            if (end == false)
            {
                MousePosition();
                Move();
                Weapon();
                Shoot();
                Shield();
                Jump();
                Objetivos();
            }
            else
            {
                if (CámaraScript.Nivel == 1)
                {
                    transform.position += Vector3.right * Speed * Time.deltaTime;
                    Armasr.sprite = ArmaDer;
                    Arma.transform.localRotation = new Quaternion(0, 0, 0, 0);
                    Cabezasr.flipX = false;
                    Brillosr.flipX = false;
                    Cabeza.transform.localPosition = new Vector3(0, 0, 0);
                    Brillo.transform.localPosition = new Vector3(0, 0, 0);
                    Ojo.transform.localPosition = new Vector3(0.39f, Ojo.transform.localPosition.y, 0);
                }
                else if(CámaraScript.Nivel == 2)
                {
                }
            }
        }
    }

    public void Move()
    {
        transform.position += Vector3.right * Speed * Time.deltaTime * Input.GetAxis("Horizontal");
    }

    public void MousePosition()
    {
        Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Mouse.z = 0;
    }

    public void Weapon()
    {
        if (ShieldActive == false)
        {
            Arma.transform.right = Mouse - transform.position;

            if (Mouse.x > Arma.transform.position.x)
            {
                Armasr.sprite = ArmaDer;
                Cabezasr.flipX = false;
                Brillosr.flipX = false;
                Cabeza.transform.localPosition = new Vector3(0, 0, 0);
                Brillo.transform.localPosition = new Vector3(0, 0, 0);
                Ojo.transform.localPosition = new Vector3(0.39f, Ojo.transform.localPosition.y, 0);

            }
            if (Mouse.x < Arma.transform.position.x)
            {
                Armasr.sprite = ArmaIzq;
                Cabezasr.flipX = true;
                Brillosr.flipX = true;
                Cabeza.transform.localPosition = new Vector3(-0.162f, 0, 0);
                Brillo.transform.localPosition = new Vector3(-0.162f, 0, 0);
                Ojo.transform.localPosition = new Vector3(-0.55f, Ojo.transform.localPosition.y, 0);
            }
        }
    }

    public void Shoot()
    {
        if (ShieldActive == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject BulletRespawn = GameObject.Instantiate(BulletPrefab);
                BulletRespawn.transform.rotation = Arma.transform.rotation;
                BulletRespawn.transform.position = Mira.transform.position;
            }
        }
    }

    public void Shield()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(1))
        {
            ShieldActive = true;
            Escudo.gameObject.SetActive(true);
            Arma.gameObject.SetActive(false);
            if (Mouse.x > transform.position.x)
            {
                Escudosr.sprite = ShieldDer;
                Escudobc.offset = new Vector2(2.64f, Escudobc.offset.y);
                Cabezasr.flipX = false;
                Brillosr.flipX = false;
                Cabeza.transform.localPosition = new Vector3(0, 0, 0);
                Brillo.transform.localPosition = new Vector3(0, 0, 0);
                Ojo.transform.localPosition = new Vector3(0.39f, Ojo.transform.localPosition.y, 0);
            }
            else
            {
                Escudosr.sprite = ShieldIzq;
                Escudobc.offset = new Vector2(-2.64f, Escudobc.offset.y);
                Cabezasr.flipX = true;
                Brillosr.flipX = true;
                Cabeza.transform.localPosition = new Vector3(-0.162f, 0, 0);
                Brillo.transform.localPosition = new Vector3(-0.162f, 0, 0);
                Ojo.transform.localPosition = new Vector3(-0.55f, Ojo.transform.localPosition.y, 0);
            }
        }
        else
        {
            ShieldActive = false;
            Escudo.gameObject.SetActive(false);
            Arma.gameObject.SetActive(true);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Salto == true)
        {
            rb.AddForce(Vector3.up * FuerzaSalto, ForceMode2D.Impulse);
            Salto = false;
        }
    }

    public void Objetivos()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            CámaraScript.Objetivos.gameObject.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            CámaraScript.Objetivos.gameObject.SetActive(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D colisión)
    {
        if(colisión.gameObject.layer == 14 || colisión.gameObject.layer == 21)
        {
            Salto = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D bala)
    {
        if(bala.gameObject.layer == 12 || bala.gameObject.layer == 22)
        {
            if (bala.IsTouching(Escudobc))
            {
                bala.gameObject.layer = 8;
                Destroy(bala.gameObject);
            }
            else
            {
                Vidas--;
                if (Vidas == 0)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        if(bala.gameObject.layer == 18)
        {
            end = true;
        }
        if(bala.gameObject.layer == 19)
        {
            ObjDes = true;
        }
        if(bala.gameObject.layer == 20)
        {
            Tutopausa = true;
        }

        if (bala.gameObject.layer == 6)
        {
            bala.GetComponent<SoundController>().ExecuteBehaviour();
        }
    }

    public void OnTriggerExit2D(Collider2D Tutos)
    {
        if(Tutos.gameObject.layer == 19)
        {
            ObjDes = false;
        }
        if(Tutos.gameObject.layer == 20)
        {
            Tutopausa = false;
        }
    }


}