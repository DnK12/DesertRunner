using UnityEngine;
using System.Collections;

public class CController : MonoBehaviour
{
    
    public ParticleSystem PsBump;
    public AnimationClip fallAnimation;
    public AnimationClip idleAnimation;
    public int lane = 1;
    ParticleSystem PsDust;    
    CharacterController cc;
    Animator anima;
    RaycastHit hitleftOrRight;
    RaycastHit hitDown;
    
    float gravity = -9.8f;
    float playerGravity;
    float Jump = 8f;
    float distanceBump = 3f;
    float distanceX = 3f;    
    bool isMove = false;    
    bool isbump = false;
    
    bool letMove = true;   
    int currentLane = 0;       
    IEnumerator Move(float direction)
    {        
        float duration = anima.GetCurrentAnimatorStateInfo(0).length;
        float speed = distanceX / duration;
        while (duration > 0 && !isbump)
        {
            yield return new WaitForEndOfFrame();
            isMove = true;
            cc.Move(Vector3.right * direction * speed * Time.deltaTime);
            duration -= Time.deltaTime;            
        }
        isMove = false;        
    }
    IEnumerator Bump()
    {
        float duration = anima.GetCurrentAnimatorStateInfo(0).length;
        float speed = distanceBump / duration;
        cc.center = new Vector3(0, 1.7f, 0);
        while (duration > 0 && !isbump)
        {
            yield return new WaitForEndOfFrame();
            
            cc.Move(new Vector3(0, -0.09f, -1.4f)  * speed * Time.deltaTime);
            duration -= Time.deltaTime;
        }
    }

    
    IEnumerator  Lose()
    {        
        levelMove.levelSpeed = 0f;
        anima.SetTrigger("Bump");
        StartCoroutine(Bump());
        letMove = false;
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anima = GetComponent<Animator>();
        
       
        PsDust = gameObject.GetComponentInChildren<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        Ray rayGround = new Ray(transform.position, Vector3.down);
        Physics.Raycast(rayGround, out hitDown);
        
        
        if (hitDown.collider != null && hitDown.distance < 0.1)
        {
            anima.SetBool("Fall", false);
            playerGravity = 0;
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                anima.SetTrigger("Jump");
                playerGravity = Jump;
            }
           
        }        
        else anima.SetBool("Fall", true);
        playerGravity += gravity * Time.deltaTime;
      
        Vector3 dirJump = new Vector3(0, playerGravity * Time.deltaTime, 0);
        cc.Move(dirJump);
             
        Ray rayRight = new Ray(transform.position, new Vector3(2,0,2));
        Ray rayLeft = new Ray(transform.position, new Vector3(-2, 0, 2));        
        float dirX = Input.GetAxisRaw("Horizontal");
        
        if(dirX != 0 && letMove && !isMove && !anima.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {            
            if (dirX > 0)
            {
                Physics.Raycast(rayRight, out hitleftOrRight, 3f);
                if (currentLane < lane && hitleftOrRight.collider == null)
                {                   
                    anima.SetTrigger("Right");
                    currentLane++;
                    StartCoroutine(Move(dirX));
                }                
            }
            if (dirX < 0)
            {
                Physics.Raycast(rayLeft, out hitleftOrRight);
                if (currentLane > -lane && hitleftOrRight.collider == null)
                {                    
                    anima.SetTrigger("Left");
                    currentLane--;
                    StartCoroutine(Move(dirX));
                }                
            }
        }               
        if (isbump) Bump();       
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger"))
        {
            GameObject.Instantiate(PsBump, transform.position + new Vector3(0,1.7f,-1), Quaternion.identity);
            StartCoroutine(Lose());
        }
    }    
}