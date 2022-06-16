using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    Vector2 moveInput;
    internal Rigidbody2D rigidBody;
    public float knockBackForce;
    CapsuleCollider2D myCapsuleCollider;
    public float climbSpeed = 5f;

    //Colliderlar
    BoxCollider2D box2D;
    CircleCollider2D cir2D;
    
    [Tooltip("karakterin ne kadar hızlı gideceğini belirler")]
    [Range(0, 20)]
    public float PlayerSpeed;
   
    //zıplama
    [Tooltip("karakterin ne kadar yükseğe zıplayacağını belirler")]
    [Range(0,1500)]
    public float jumpPower;

    //zıplama
    [Tooltip("karakterin 2. zıplamada ne kadar yükseğe zıplayacağını belirler")]
    [Range(0, 30)]
    public float doubleJumpPower;

    internal bool canDoubleJump;
    internal bool canDamage;

    [Tooltip("karakterin yere değip değmediğini belirler")]
    //karakteri dondurme
    bool facingRight = true;


    //yeri bulma
    public bool isGrounded;
    Transform groundCheck;
    public float graundCheckRadius = 0.2f;
    [Tooltip("yerin ne olduğunu belirler")]
    public LayerMask groundLayer;


    //Animator controlleri
    Animator playerAnimController;

    //Oyuncu Canı.
    internal int maxPlayerHealth =100;
    public int currentPlayerHealth;
    internal bool isHurt;


    //Oyuncuyu Öldür
    internal bool isDead;
    public float deadForce;

    //Oyuncunun Puanları
    public int currentPoints;
    public int yedek_para;
    public int point = 1;


    //GameManager
    GameManager gameManager;

    //CheckPoint
    public GameObject startPosion;
    GameObject checkPoint;

    //Sound

    AudioSource auSource;
    AudioClip au_Jump;
    AudioClip au_Hit;
    AudioClip au_PickupCoin;
    AudioClip ac_Dead;
    AudioClip ac_Shoot;

    //Ateş etmek
    Transform firePoint;
    GameObject bullet;
   
   
    void Start()
    {
        


        transform.position = startPosion.transform.position;
        // rigidBody ayarları
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 5;
        rigidBody.freezeRotation = true;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        //Colliderları al
        box2D = GetComponent<BoxCollider2D>();
        cir2D = GetComponent<CircleCollider2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();




        // GroundCheck'i bul
        groundCheck = transform.Find("GroundCheck");


        // Animatoru al
        playerAnimController = GetComponent<Animator>();

        //Canı max Cana Eşitle
       
        currentPlayerHealth = maxPlayerHealth;

        //GameManager
        gameManager = FindObjectOfType<GameManager>();


        // Sesleri yükleme
        auSource = GetComponent<AudioSource>();
        au_Jump = Resources.Load("SoundEffects/Jump") as AudioClip;
        au_Hit = Resources.Load("SoundEffects/Hit") as AudioClip;
        au_PickupCoin = Resources.Load("SoundEffects/Coin") as AudioClip;
        ac_Dead = Resources.Load("SoundEffects/Dead") as AudioClip;
        ac_Shoot = Resources.Load("SoundEffects/Shoot") as AudioClip;

        //Ateş etme
        firePoint = transform.Find("FirePoint");
        bullet = Resources.Load("Bullet") as GameObject;

        //YedekPara
        yedek_para = PlayerPrefs.GetInt("yedekpara");


    }
     void Update()
    {
        Run();
        updateAnimations();
        ReducedHealth();
        isDead = currentPlayerHealth <= 0;
        if (isDead)
         KillPlayer();

        if(transform.position.y <= -15)
        {
            isDead = true;
        }

        //Eğer canımız maxCanımızdan yüksekse canımızı MaxCana Eşitle.
        if (currentPlayerHealth > maxPlayerHealth)
            currentPlayerHealth = maxPlayerHealth;


        //Tırmanma
        ClimbLadder();
        
        
    }
   
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
       
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * PlayerSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;
    }

    private void FixedUpdate()
    {
        //yere değiyor mu diye bak
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, graundCheckRadius, groundLayer);


        //hareket etme
        float h = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(h * PlayerSpeed, rigidBody.velocity.y);

        flip(h);

        if (isGrounded)
            canDamage = false;

    }
    public void jump()
    {
        //rigidbody'ye dikey yönünde (y yönü) güç ekle
        rigidBody.AddForce(new Vector2 (0, jumpPower));
        auSource.PlayOneShot(au_Jump);
        auSource.pitch =Random.Range(0.8f, 1.1f);
       
       

    }

    public void DoubleJump()
    {
        //rigidbody'ye dikey yönünde (y yönü) ani bir güç güç ekler
        rigidBody.AddForce(new Vector2(0, doubleJumpPower),ForceMode2D.Impulse);
        auSource.PlayOneShot(au_Jump);
        auSource.pitch = Random.Range(0.8f, 1.1f);
        canDamage = true;
    }
    //Karakteri dondurme fonksiyonu.
    public void flip(float h)
    {
        if(h>0 && !facingRight || h <0 && facingRight)
        {
            facingRight = !facingRight;
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        } 
    }
    //Tırmanma fonksiyonu
    void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            return;
        }
        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x,moveInput.y * climbSpeed);
        rigidBody.velocity = climbVelocity;
        
    }

    //Animator'u yenileme fonksiyonu
    void updateAnimations()
    {
        playerAnimController.SetFloat("VelocityX", Mathf.Abs(rigidBody.velocity.x));
        playerAnimController.SetBool("isGrounded", isGrounded);
        playerAnimController.SetFloat("VelocityY", rigidBody.velocity.y);
        playerAnimController.SetBool("isDead", isDead);
        if(isHurt && isDead)
        playerAnimController.SetTrigger("isHurt");
    }
    //Can azaltma fonksiyonu.
    void ReducedHealth()
    {
        if (isHurt)
        {
            //eğer canımız     o zaman canımızdan zarar kadar çıkart
            // 100 ise         - zarar
            //eğer bu kondisyon doğru ise can - zarar = yeniCan
            Debug.Log(currentPlayerHealth);
            isHurt = false;

            //Eğer havadaysak sol veya sag dikey yonde güc uygula.
            if (facingRight && !isGrounded)
                rigidBody.AddForce(new Vector2(1000, 0), ForceMode2D.Force);
            else if (!facingRight && isGrounded)
                rigidBody.AddForce(new Vector2(1000, 0), ForceMode2D.Force);
            //Eğer yerdeyek sol veya sağ yönde güç uygula
            if (facingRight && isGrounded)
                rigidBody.AddForce(new Vector2(-knockBackForce, 0), ForceMode2D.Force);
            else if (!facingRight && isGrounded)
                rigidBody.AddForce(new Vector2(knockBackForce, 0), ForceMode2D.Force);

            if (!isDead) {
            auSource.PlayOneShot(au_Hit);
            auSource.pitch = Random.Range(0.8f, 1.1f);
            }
        }
    }

    //Oyuncuyu oldurme Fonksiyonu
    void KillPlayer()
    {
       
        
      isHurt = false;
      rigidBody.AddForce(new Vector2(0, deadForce),ForceMode2D.Impulse);
        rigidBody.drag += Time.deltaTime * 50;
        deadForce -= Time.deltaTime * 20;
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
       box2D.enabled = false;
       cir2D.enabled = false;
        
    }
   public void RecoverPlayer()
    {
        if (checkPoint != null)
            transform.position = checkPoint.transform.position;
        else transform.position = startPosion.transform.position;

        deadForce = 5;
        rigidBody.gravityScale = 5;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        currentPlayerHealth = maxPlayerHealth;
        box2D.enabled = true;
        cir2D.enabled = true;
        rigidBody.constraints = RigidbodyConstraints2D.None;
        rigidBody.freezeRotation = true;
        rigidBody.simulated = true;
        rigidBody.drag = 0;


    }

    public void ShootProjecttile()
    {
        GameObject b = Instantiate(bullet) as GameObject;
        b.transform.position = firePoint.transform.position;
        b.transform.rotation = firePoint.transform.rotation;

        auSource.PlayOneShot(ac_Shoot);
        auSource.pitch = Random.Range(.8f, 1.1f); 

        if (transform.localScale.x < 0)
        {
            b.GetComponent<Projecttile>().bulletSpeed *= -1;
            b.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            b.GetComponent<Projecttile>().bulletSpeed *= 1;
            b.GetComponent<SpriteRenderer>().flipX = false;
        }
         

      
            
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            currentPoints += point;
            if(currentPoints >= yedek_para)
            {
                yedek_para = currentPoints;
            }
            PlayerPrefs.SetInt("yedekpara", yedek_para);
            Destroy(collision.gameObject);
            auSource.PlayOneShot(au_PickupCoin);
            auSource.pitch = Random.Range(0.8f, 1.1f);
        }
        if(collision.tag == "CheckPoint")
        {
            checkPoint = collision.gameObject;
        }
        if(collision.tag == "Enemy" && isDead)
        {

            auSource.PlayOneShot(ac_Dead);
        }
    }
}
