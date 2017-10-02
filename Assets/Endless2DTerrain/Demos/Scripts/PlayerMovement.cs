using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour

{
	public Text countText;
	public Text HeadlineText;
	public Text loseText;

	[HideInInspector] public bool facingRight = true;

    public Camera playerCamera;
	
    public float speed = 12.0F;
	public float maxSpeed = 24.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    
	private Vector3 moveDirection = Vector3.zero;
	public Animator anim;



	private int coinCount;


	void Awake()
	{
		anim = GetComponent<Animator> ();

	}

    void Start()
    {
		coinCount = 30;
		SetCountText ();
		HeadlineText.text = "STOCK MARKET IN PERIL";
		loseText.text = "";
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

	

        playerCamera.transparencySortMode = TransparencySortMode.Orthographic;
    }

	void  SetCountText(){
		countText.text = "Stock Value: " + coinCount.ToString () + "%";
	}


	/*void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}*/


    void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("EndCollider")) {
			loseText.text = "YOU COULDN'T KEEP UP, YOUR RESEARCH IS FUTILE";
			this.gameObject.SetActive (false);

		}

		//green
		if (other.gameObject.CompareTag ("GreenCollect")) {
			Debug.Log ("Green Collected");
			other.gameObject.SetActive (false);
			coinCount = coinCount + 5;
			SetCountText ();
			if (coinCount == 100)
			{
				HeadlineText.text = "THE ECONOMY HAS BEEN SAVED!";
				loseText.text = "YOU SAVED US!";
				this.gameObject.SetActive (false);

			}
			else if (coinCount >= 60 && coinCount <= 80)
			{
				HeadlineText.text = "COUNTRY SHOWING ECONOMIC RECOVERY";
			}

			else if (coinCount > 20 && coinCount < 60)
			{
				HeadlineText.text = "STOCK MARKET IN PERIL";
			}

		} else if (other.gameObject.CompareTag ("RedCollect")) {
			Debug.Log ("Red Collected");
			other.gameObject.SetActive (false);

			coinCount = coinCount - 5;
			SetCountText ();
			if (coinCount == 0)
			{
				HeadlineText.text = "GLOBAL ECONOMIC DEPRESSION";
				loseText.text = "YOU COULDN'T SAVE US";
				this.gameObject.SetActive (false);
			}
			else if (coinCount < 30 && coinCount >= 10)
			{
				HeadlineText.text = "ON THE VERGE OF ECONOMIC COLLAPSE";
			}
			else if (coinCount < 60 && coinCount >= 30)
			{
				HeadlineText.text = "STOCK MARKET IN PERIL";
			}
		}
	}

	void Update()
    {    
        CharacterController controller = GetComponent<CharacterController>();




        if (controller.isGrounded) {
			//move right
			if (Input.GetKey (KeyCode.RightArrow)) {
				moveDirection += Vector3.right;
				anim.SetTrigger ("move");
				facingRight = true;
			} 


			//move left
			else if (Input.GetKey (KeyCode.LeftArrow)) {
				moveDirection -= Vector3.right;
				anim.SetTrigger ("move");

			} else {
				moveDirection = Vector3.zero;
				anim.SetTrigger ("idle");
				
				
				
				
			}


		
			
			if (Input.GetButton("Jump"))
			{
				moveDirection.y = jumpSpeed;
				anim.SetTrigger ("jump");
			}

			if (Input.touchCount > 0)
            {
                moveDirection.y = jumpSpeed;
            }


			 
		}
		
		moveDirection.y -= gravity * Time.smoothDeltaTime;
		controller.Move(moveDirection * Time.smoothDeltaTime);
		
		//After we move, adjust the camera to follow the player
       //playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
    }
}
