using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour 
{

	public float speed;
	public float maxLaunchVelocity;
	public Rigidbody2D rb2d;

	[SerializeField] private float gravityScale = 0.5f;

	private bool isGrounded = true;
	private	bool canAirJump;
	private bool directionChosen;
	private Vector2 startPos;
	private Vector2 direction;
	private Animator anim;
	private SpriteRenderer spriteR;

	public void GetKilled()
	{
		GetComponent<Collider2D>().enabled = false;
		rb2d.velocity = new Vector2(-rb2d.velocity.x, 0).normalized;
		this.enabled = false;
		StartCoroutine(GameplayManager.ResetLevel(4, 0));
	}

	void Start() 
    {
		anim = GetComponent<Animator>();
		spriteR = GetComponent<SpriteRenderer>();
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.gravityScale = gravityScale;
		tag = "Neko";
	}

	private void OnCollisionEnter2D()
	{
		isGrounded = true;
		anim.SetBool("isGrounded", isGrounded);
		rb2d.velocity = Vector2.zero;
		rb2d.gravityScale = 0f;
		spriteR.flipX = !spriteR.flipX;
	}

	private void OnCollisionExit2D()
	{
		isGrounded = false;
		anim.SetBool("isGrounded", isGrounded);
		rb2d.gravityScale = gravityScale;
	}

	private void Update()
	{
		HandleMovement();
	}

	private void LateUpdate()
	{
		//freeze Neko if still attached to collider (prevents wall-sliding)
		if (isGrounded && rb2d.velocity.x != 0)
		{
			if (Mathf.Abs(rb2d.velocity.x) < 0.01f) OnCollisionEnter2D();
		}
	}

	void HandleMovement()
	{
		//Handle movement if in mid-air
		if (!isGrounded)
		{
			if (Input.touchCount > 0 && canAirJump)
			{
				GetTouchDirection();
			}
			if (directionChosen)
			{
				LeapTowardDirection();
				directionChosen = false;
				canAirJump = false;
				anim.SetTrigger("doubleJump");
			}
		}

		//Handle movement when grounded or on wall
		if (isGrounded)
		{
			if (Input.touchCount > 0)
			{
				GetTouchDirection();
			}
			if (directionChosen)
			{
				LeapTowardDirection();
				directionChosen = false;
				canAirJump = true;
			}
		}

	}

	private void LeapTowardDirection()
	{
		if (direction.magnitude > maxLaunchVelocity)
		{
			direction.Normalize();
			direction *= maxLaunchVelocity;
		}
		rb2d.velocity = direction * speed;
		if (rb2d.velocity.x < 0)
		{
			spriteR.flipX = true;
		}
		else
		{
			spriteR.flipX = false;
		}
		SetSlowMotion(false);
	}

	void GetTouchDirection()
	{
		Touch touch = Input.GetTouch(0);
		
		// Handle finger movements based on touch phase.
		switch (touch.phase)
		{
			// Record initial touch position.
			case TouchPhase.Began:
				//slow mo when touch input received mid-air
				if (!isGrounded)
				{
					SetSlowMotion(true);
				}
				startPos = touch.position;
				directionChosen = false;
				break;

			// Determine direction by comparing the current touch position with the initial one.
			case TouchPhase.Moved:
				direction = touch.position - startPos;
				break;

			// Report that a direction has been chosen when the finger is lifted.
			case TouchPhase.Ended:
				direction *= 0.01f;
				directionChosen = true;
				break;
		}
	}

	void SetSlowMotion(bool value)
	{
		if (value)
		{
			Time.timeScale = 0.7f;
			rb2d.velocity *= 0.1f;
			rb2d.gravityScale = 0.07f;
		}
		else
		{
			Time.timeScale = 1;
			rb2d.gravityScale = gravityScale;
		}

	}

	//void GetClickDirection()
	//{
	//	Touch touch = new Touch();
	//	if (Input.GetMouseButtonDown(0))
	//	{
	//		touch.phase = TouchPhase.Began;
	//		startPos = Input.mousePosition;
	//		directionChosen = false;
	//	}
	//	if (touch.phase == TouchPhase.Began)
	//	{
	//		direction = (Vector2)Input.mousePosition - startPos;
	//	}
	//	if (Input.GetMouseButtonUp(0))
	//	{
	//		touch.phase = TouchPhase.Ended;
	//		direction *= 0.01f;
	//		directionChosen = true;
	//	}
	//}

}
