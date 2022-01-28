using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public Animator animator;

	[SerializeField] float runSpeed = 40f;
	[SerializeField] float birdSpeed = 10f;

	float horizontalMove = 0f;

	bool jump = false;
	bool jumpboost = false;
	float timer = 360f; // for countdown timer
						//bool crouch = false;
	bool firstCollision;
	public bool isGrounded;
	string Level = "";
	string reduceLifeCount = "reduceLifeCount";
	string increaseLifeCount = "increaseLifeCount";

	public string QuizCharacter = " ";

	private float runSpeedTimer; //for keeping track of reduced speed time
	private bool isRunSpeedReduced; //to check if speed is reduced

	[SerializeField] GameObject GameOver;
	[SerializeField] GameObject PauseMenu;
	[SerializeField] GameObject GameComplete;
	[SerializeField] GameObject GameFailed;
	[SerializeField] GameObject GameStart;

	[SerializeField] GameObject ButtonPause;
    [SerializeField] GameObject ButtonJump;
	[SerializeField] GameObject ButtonAttack;

	[SerializeField] AudioSource backgroundMusic;
	[SerializeField] AudioSource TimerTick;
	[SerializeField] AudioSource UIClick;

	[SerializeField] GameObject player;
	[SerializeField] GameObject firePoint; //for firing bullets and also serves as an attack point for the melee combat
	[SerializeField] GameObject ButtonLeft;
	[SerializeField] GameObject ButtonRight;
	[SerializeField] GameObject Joystick;
	[SerializeField] GameObject LoadingScreen;
	[SerializeField] GameObject HiddenAreaText;
	[SerializeField] GameObject TimerText;

	[SerializeField] GameObject UICanvas;
	[SerializeField] GameObject QuizCanvas;
	[SerializeField] Hud hud;
	[SerializeField] Text scoreHolder;
	[SerializeField] Text scoreText;
	[SerializeField] TextMeshProUGUI time;


	[SerializeField] Sprite filledHeart;
	[SerializeField] Sprite emptyHeart;

	[SerializeField] GameObject FallProof;
	[SerializeField] PositionKeeper PositionKeeper;

	[SerializeField] Image[] lives;

    [SerializeField] Image DamageOverlay;

    [SerializeField] HealthBar healthBar;


	bool reduceLifeOnce = false;

	private int location;
	private int storage;
	private int mic;
	private int camera;
	private int sensors;
	private int contacts;
	private int phone;
	private int calendar;
	private int activity_tracking;
	private int sms;
	private int hiddenareacount;

	private int maxLives = 3;
	private int currentLives;

	private float currentHealth;

	public bool isFacingRight;

	private int L1P;
	private int L2P;
	private int L3P;

	private string moveBird;
	[SerializeField] private Rigidbody2D Bird;
	[SerializeField] RectTransform timer_rect_transform;	

	private string BaseURL = "Base URL";

    private void Start()
	{
		Level = SceneManager.GetActiveScene().name;
		//getting the count of number of perms in game and subtracting 1 since we check if the perms is less than 0
		if (Level == "Traps")
		{
            location = GameObject.FindGameObjectsWithTag("location").Length - 1;
            storage = GameObject.FindGameObjectsWithTag("storage").Length - 1;
            mic = GameObject.FindGameObjectsWithTag("mic").Length - 1;
            camera = GameObject.FindGameObjectsWithTag("camera").Length - 1;
            phone = GameObject.FindGameObjectsWithTag("phone").Length - 1;
            contacts = GameObject.FindGameObjectsWithTag("contacts").Length - 1;
            sms = GameObject.FindGameObjectsWithTag("sms").Length - 1;

            /*location = 0;
            storage = 0;
            mic = 0;
            camera = 0;
            phone = 0;
            contacts = 0;
            sms = 0;
            activity_tracking = 0;
            sensors = 0;
            calendar = 0;*/
        }

		else if(SceneManager.GetActiveScene().name == "Level2")
        {
            location = GameObject.FindGameObjectsWithTag("location").Length - 1;
            storage = GameObject.FindGameObjectsWithTag("storage").Length - 1;
            camera = GameObject.FindGameObjectsWithTag("camera").Length - 1;
            activity_tracking = GameObject.FindGameObjectsWithTag("activity").Length - 1;
            sensors = GameObject.FindGameObjectsWithTag("sensors").Length - 1;

            /*location = 0;
            storage = 0;
            camera = 0;
            activity_tracking = 0;
            sensors = 0;*/
        }

		else if (SceneManager.GetActiveScene().name == "Level3")
		{
            location = GameObject.FindGameObjectsWithTag("location").Length - 1;
            storage = GameObject.FindGameObjectsWithTag("storage").Length - 1;
            calendar = GameObject.FindGameObjectsWithTag("calendar").Length - 1;

            /*location = 0;
			storage = 0;
			calendar = 0;*/
        }

		healthBar.setMaxHealth(100);
		currentHealth = 100;
		isRunSpeedReduced = false;
		runSpeedTimer = 0;
		currentLives = maxLives;
		firstCollision = true;
		scoreHolder.color = Color.black;
		scoreText.color = Color.black;
		isFacingRight = controller.m_FacingRight;
		isGrounded = controller.m_Grounded;
		backgroundMusic.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
		TimerTick.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
		UIClick.volume = PlayerPrefs.GetFloat("masterVolume", 5f);
		moveBird = "";
		backgroundMusic.Play();
		hiddenareacount = int.Parse(PlayerPrefs.GetString("HiddenAreaCount","0"));

		//UICanvas.GetComponent<Image>().enabled = false;

		string ControllerType = PlayerPrefs.GetString("controller");

        if (ControllerType == "buttons")
        {
            Joystick.SetActive(false);
            ButtonLeft.SetActive(true);
            ButtonRight.SetActive(true);
        }

        else
        {
            ButtonLeft.SetActive(false);
            ButtonRight.SetActive(false);
            Joystick.SetActive(true);
        }

		//For Loading score from previous level
        if (PlayerPrefs.GetInt("PrevLevel") == 1)
        {
            scoreText.text = PlayerPrefs.GetString("LevelScore");
        }

		L1P = int.Parse(PlayerPrefs.GetString("L1P", "0"));
		L2P = int.Parse(PlayerPrefs.GetString("L2P", "0"));
		L3P = int.Parse(PlayerPrefs.GetString("L3P", "0"));

		Screen.sleepTimeout = SleepTimeout.NeverSleep; //To keep the screen awake when the player is playing the game
		GameStart.SetActive(true);
	}

    void Update () {
		isGrounded = controller.m_Grounded;

#if UNITY_ANDROID
		horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal") * runSpeed; //for mobile input
#endif

#if UNITY_EDITOR
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // for pc keys
#endif
		if (GameStart.activeSelf)
		{
			Time.timeScale = 0f;
			UICanvas.SetActive(false);
			if (PlayerPrefs.GetString("controller") != "buttons")
			{
				Joystick.GetComponentInChildren<Image>().enabled = false;
				Joystick.SetActive(false);
			}
		}
            
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // for pc keys
        //horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal") * runSpeed; //for mobile input

        if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!Pause.IsGamePaused)
			{
				Pause.IsGamePaused = true;
				PauseMenu.SetActive(true);
				UICanvas.SetActive(false);
				Joystick.GetComponentInChildren<Image>().enabled = false;
				Time.timeScale = 0f;
			}
		}

		timer -= 1 * Time.deltaTime;
		time.text = "Time Left: "+ timer.ToString("0");

		if(timer <= 0)
        {
			timer = 0;
			player.SetActive(false);
			DisplayGameOverDialog();
			TimerTick.Stop();
        }
		
		if (timer <= 60)
		{
			time.color = Color.red;
			PlayTimerSound();
			backgroundMusic.Stop();
		}

		// localRotation:https://docs.unity3d.com/ScriptReference/Transform-localRotation.html
		// rotation : https://docs.unity3d.com/ScriptReference/Transform-rotation.html

		//To rotate the healthbar as the player moves left or right. 

		if (!controller.m_FacingRight)
        {
            healthBar.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 360, transform.eulerAngles.z);
		}
        else
        {
            healthBar.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 360, transform.eulerAngles.z);
		}

		//For checking in enemy script so that enemy attacks only when player is infront of them
		isFacingRight = controller.m_FacingRight;

		//Lock player Movement after death
		if (animator.GetBool("IsDead"))
		{
			LockPlayerMovement();
		}

		//Pause Menu
		if (CrossPlatformInputManager.GetButtonDown("Pause"))
		{
            UIClick.Play();

			if (!Pause.IsGamePaused)
			{
				Pause.IsGamePaused = true;
				PauseMenu.SetActive(true);
				UICanvas.SetActive(false);

				Joystick.GetComponentInChildren<Image>().enabled = false;
				Joystick.SetActive(false);

				Time.timeScale = 0f;
			}
		}

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		//Jump and Crouch

		if (Input.GetButtonDown("Jump") || CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

		if(jump == false && !controller.m_Grounded && animator.GetBool("IsDead") == false)
        {
			animator.SetBool("IsFalling", true);
        }

        if (controller.m_Grounded)
        {
            animator.SetBool("IsFalling", false);
        }

        /*if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;

		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}*/

        //Reduced Movement Speed
        if (isRunSpeedReduced)
		{
			runSpeedTimer += Time.deltaTime;
			if (runSpeedTimer >= 3)
			{
				runSpeed = 40f;
				runSpeedTimer = 0;
				isRunSpeedReduced = false;
			}
		}


        if (DamageOverlay.color.a > 0)
        {
            var color = DamageOverlay.color;
            color.a -= 0.01f;
            DamageOverlay.color = color;
        }

        //For moving bird towards player
        /*if(moveBird == "left")
			Bird.transform.Translate(Vector2.left * birdSpeed * Time.deltaTime);
		else if(moveBird == "right")
			Bird.transform.Translate(Vector2.right * birdSpeed * Time.deltaTime);*/

    }

	public void onLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	public void onCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move the character
		//controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);

		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, jumpboost);
		jump = false;
		jumpboost = false;
	}

	//For destroying coin object on collision with player
	private void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.CompareTag("uicolor"))
		{
			if (scoreHolder.color == Color.black)
			{
				scoreHolder.color = Color.white;
				scoreText.color = Color.white;
				time.color = Color.white;
			}

            else
            {
                scoreHolder.color = Color.black;
                scoreText.color = Color.black;
				time.color = Color.black;
			}
        }

		if (col.gameObject.CompareTag("life"))
        {
			Destroy(col.gameObject);
			LivesManager(increaseLifeCount);
		}

		if (col.gameObject.CompareTag("clock"))
		{
			Destroy(col.gameObject);
			StartCoroutine(PlayTimerAnim());
			StartCoroutine(ShowTimerText());
			timer += 30;
		}

		if (col.gameObject.CompareTag("storage"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
            CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (storage > 0)
				storage--;
            
			/*else
            {
				hud.removeStorage();
            }*/

			/*if (Level == "Traps" || Level == "Level2" || Level == "Level3")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("location"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (location > 0)
				location--;
			/*else
			{
				hud.removeLocation();
			}*/

			/*if (Level == "Traps" || Level == "Level2" || Level == "Level3")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("mic"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (mic > 0)
				mic--;
			/*else
			{
				hud.removeMic();
			}*/

			/*if (Level == "Traps")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("camera"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (camera > 0)
				camera--;
			/*if (Level == "Level2" || Level == "Traps")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("contacts"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (contacts > 0)
				contacts--;

			/*if (Level == "Traps")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("activity"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (activity_tracking > 0)
				activity_tracking--;
			/*if (Level == "Level2")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("sensors"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (sensors > 0)
				sensors--;
			/*if (Level == "Level2")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("sms"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (sms > 0)
				sms--;

			/*if (Level == "Traps")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("phone"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (phone > 0)
				phone--;

			/*if (Level == "Traps")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("calendar"))
		{
			//For destroying game object and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			if (calendar > 0)
				calendar--;

			/*if (Level == "Level3")
				IncreaseHealth(10);*/
		}

		if (col.gameObject.CompareTag("red"))
		{
			//For destroying coin and to recude player movement speed.
			Destroy(col.gameObject);
			CrossPlatformInputManager.SetAxisZero("Horizontal");

			isRunSpeedReduced = true;
			runSpeed = 10f;

			TakeDamage(10);
		}

		if (col.gameObject.CompareTag("Quiz"))
		{
			CrossPlatformInputManager.SetAxisZero("Horizontal");
			QuizCharacter = col.gameObject.name;
			Time.timeScale = 0f;
			FindObjectOfType<Quiz>().DisplayQuizCanvas();
		}

		if (col.gameObject.CompareTag("fall") && firstCollision)
        {
            firstCollision = false;
            SetHealth(0);
            animator.SetBool("IsDead", true);
            animator.SetBool("IsFalling", false);
            Handheld.Vibrate();

            if (!reduceLifeOnce)
            {
                reduceLifeOnce = true;
                LivesManager(reduceLifeCount);
            }

        }
        
        if (col.gameObject.CompareTag("spikes") && firstCollision)
        {
            firstCollision = false;
            SetHealth(0);
            animator.SetBool("IsDead", true);
            animator.SetBool("IsFalling", false);
            Handheld.Vibrate();
            if (!reduceLifeOnce)
            {
                reduceLifeOnce = true;
                LivesManager(reduceLifeCount);
            }
        }

        if (col.gameObject.CompareTag("gem") || col.gameObject.CompareTag("Hidden"))
		{
			if (col.gameObject.name == "gem" && PlayerPrefs.GetInt("l1h1") == 0)
			{
				hiddenareacount += 1;
				PlayerPrefs.SetString("HiddenAreaCount", hiddenareacount.ToString());
				PlayerPrefs.SetInt("l1h1", 1);
				Score.instance.UploadBadges();
			}

			else if (col.gameObject.name == "gem-1" && PlayerPrefs.GetInt("l1h2") == 0)
            {
				hiddenareacount += 1;
				PlayerPrefs.SetString("HiddenAreaCount", hiddenareacount.ToString());
				PlayerPrefs.SetInt("l1h2", 1);
				Score.instance.UploadBadges();
			}

			else if (col.gameObject.name == "gem-2" && PlayerPrefs.GetInt("l2h1") == 0)
			{
				hiddenareacount += 1;
				PlayerPrefs.SetString("HiddenAreaCount", hiddenareacount.ToString());
				PlayerPrefs.SetInt("l2h1", 1);
				Score.instance.UploadBadges();
			}

			else if (col.gameObject.name == "gem-3" && PlayerPrefs.GetInt("l2h2") == 0)
			{
				hiddenareacount += 1;
				PlayerPrefs.SetString("HiddenAreaCount", hiddenareacount.ToString());
				PlayerPrefs.SetInt("l2h2", 1);
				Score.instance.UploadBadges();
			}

			else if (col.gameObject.name == "HiddenArea" && PlayerPrefs.GetInt("l2h3") == 0)
			{
				hiddenareacount += 1;
				PlayerPrefs.SetString("HiddenAreaCount", hiddenareacount.ToString());
				PlayerPrefs.SetInt("l2h3", 1);
				Score.instance.UploadBadges();
			}

			else if (col.gameObject.name == "HiddenArea2" && PlayerPrefs.GetInt("l3h1") == 0)
			{
				hiddenareacount += 1;
				PlayerPrefs.SetString("HiddenAreaCount", hiddenareacount.ToString());
				PlayerPrefs.SetInt("l3h1", 1);
				Score.instance.UploadBadges();
			}

			else if (col.gameObject.name == "gem-4" && PlayerPrefs.GetInt("l3h2") == 0)
			{
				hiddenareacount += 1;
				PlayerPrefs.SetString("HiddenAreaCount", hiddenareacount.ToString());
				PlayerPrefs.SetInt("l3h2", 1);
				Score.instance.UploadBadges();
			}

			if (hiddenareacount >= 5 && PlayerPrefs.GetInt("HA") == 0)
			{
				PlayerPrefs.SetInt("HA", 1);
				Score.instance.UploadBadges();
			}

			StartCoroutine(ShowHiddenAreaText());
			Destroy(col.gameObject);
		}

		if (col.gameObject.CompareTag("cherry"))
		{
			Destroy(col.gameObject);
			IncreaseHealth(30);
		}

		if (col.gameObject.CompareTag("finish"))
		{
			// check if perms are collected 
			if (Level == "Traps")
			{
				if (location == 0 && storage == 0 && mic == 0 && phone == 0 && contacts == 0 && sms == 0 && camera == 0)
				{
					LevelComplete();
				}

				else
				{
					LevelNotCompleted();
				}
			}

			else if (Level == "Level2")
            {
				if (location == 0 && storage == 0 && camera == 0 && activity_tracking == 0 && sensors == 0)
				{
					LevelComplete();
				}

				else
				{
					LevelNotCompleted();
				}
			}

			else if (Level == "Level3")
            {
				if (location == 0 && storage == 0 && calendar == 0)
				{
					LevelComplete();
				}

				else
				{
					LevelNotCompleted();
				}
			}

			
		}
		//For flipping bird
        if (col.gameObject.CompareTag("Bird"))
        {
			moveBird = "left";
        }

		if (col.gameObject.CompareTag("Eagle"))
		{
			moveBird = "right";
		}
	}

    /*private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.CompareTag("Quiz"))
		{
			collision.gameObject.SetActive(false);
			Score.instance.ChangeScore(10);
		}
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("JumpBoost"))
		{
			jumpboost = true;
			jump = false;
		}
	}

    //For taking hit when enemy attacks
    public void TakeDamage(float damage)
    {
		DisplayDamageOverlay(); //https://www.youtube.com/watch?v=d9FaI28Yf9A
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			SetHealth(0);
			Handheld.Vibrate();
			animator.SetBool("IsDead", true);
			animator.SetBool("IsFalling", false);
			if (!reduceLifeOnce)
			{
				reduceLifeOnce = true;
				LivesManager(reduceLifeCount);
			}

            if (QuizCanvas.activeSelf)
            {
                Time.timeScale = 1f;
                QuizCanvas.SetActive(false);
                UICanvas.SetActive(true);
            }
        }

		else
			SetHealth(currentHealth);

	}

	//Setting Player's health
    private void SetHealth(float health)
    {
        currentHealth = health;
		healthBar.setHealth(health);
		if (currentHealth == 0)
			StartCoroutine(UploadData("dead"));
    }

	//To deactivate Player Object with a delay
	IEnumerator deactivatePlayer()
    {
		yield return new WaitForSeconds(1f); //waiting for death animation to end //animation event trigger is buggy, hence using a coroutine as suggested in https://answers.unity.com/questions/806949/animation-events-not-firing.html
		//player.SetActive(false);
		Time.timeScale = 0f;
		backgroundMusic.Stop();

		ButtonPause.SetActive(false);
		ButtonAttack.SetActive(false);
		ButtonJump.SetActive(false);
		ButtonLeft.SetActive(false);
		ButtonRight.SetActive(false);

		StopCoroutine(deactivatePlayer());
		DisplayGameOverDialog();
	}

	IEnumerator restartFromCheckPoint()
	{
		yield return new WaitForSeconds(1f); //waiting for death animation to end //animation event trigger is buggy, hence using a coroutine as suggested in https://answers.unity.com/questions/806949/animation-events-not-firing.html
		SetHealth(100);
		animator.SetBool("IsDead", false);
		player.transform.position = PositionKeeper.lastCheckPointPosition;
		reduceLifeOnce = false;
		StopCoroutine(restartFromCheckPoint());
		firstCollision = true;
	}

	//GameOver Dialog
	private void DisplayGameOverDialog()
    {
		Joystick.SetActive(false);
        GameOver.SetActive(true);
		GameOver.transform.Find("Restart").GetComponent<Button>().onClick.AddListener(RestartLevel);
		GameOver.transform.Find("MainMenu").GetComponent<Button>().onClick.AddListener(MainMenu);
	}

	//For Restarting Level
	private void RestartLevel()
    {
		StartCoroutine(UploadData("Restart"));

		if (Level == "Traps")
		{
			L1P++;
			PlayerPrefs.SetString("L1P", L1P.ToString());
		}

		else if (Level == "Level2")
		{
			L2P++;
			PlayerPrefs.SetString("L2P", L2P.ToString());
		}

		else if (Level == "Level3")
		{
			L3P++;
			PlayerPrefs.SetString("L3P", L3P.ToString());
		}

		GameOver.SetActive(false);
		LoadingScreen.SetActive(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
	}

	private void MainMenu()
	{
		GameOver.SetActive(false);
		LoadingScreen.SetActive(true);
		SceneManager.LoadScene("MainMenu");
		Time.timeScale = 1f;
	}

	//For Locking Player Movement after death. This does not lock the controller but for a locks movement for a moment until the player is disabled and enabled again. 
	private void LockPlayerMovement()
    {
		horizontalMove = 0;
		jump = false;
		//crouch = false;
	}

	//For checking player health from enemy script so that enemy does not attack when player has 0 health and wouldn't end up losing more than 1 life at a time
	public float CheckHealth()
    {
		return currentHealth;
    }

	IEnumerator restartFromCheckPointForFall()
    {
		SetHealth(100);
		animator.SetBool("IsDead", false);
		StopCoroutine(restartFromCheckPoint());
		yield return null;
	}

	/*IEnumerator ShowDamageOverlay()
    {
		UICanvas.GetComponent<Image>().enabled = true;
		yield return new WaitForSeconds(1f);
		//UICanvas.GetComponent<Image>().enabled = false;
		StartCoroutine(HideImg());
	}

	IEnumerator HideImg()
	{
		yield return new WaitForSeconds(1f);
		UICanvas.GetComponent<Image>().enabled = false;
		
		//UICanvas.GetComponent<Image>().enabled = false;
		//StopCoroutine(ShowDamageOverlay());
		//StopCoroutine(HideImg());
	}*/
	void LivesManager(string option)
    {
        if (option == reduceLifeCount)
        {   //reduce laife
            if (currentLives != 0)
            {
                currentLives -= 1;
                for (int i = 0; i < lives.Length; i++)
                {
                    if (i < currentLives)
                    {
                        lives[i].enabled = true;
                    }
                    else
                    {
                        lives[i].enabled = false;
                    }
                }
                StartCoroutine(restartFromCheckPoint());
            }
            else
                StartCoroutine(deactivatePlayer());
			
			//changing color of ui when player dies inside the cave
			if (scoreHolder.color == Color.white)
			{
				scoreHolder.color = Color.black;
				scoreText.color = Color.black;
				time.color = Color.black;
			}
		}

        else
        {   //increase laife
            if (currentLives != 3)
            {
                currentLives += 1;
                for (int i = 0; i < lives.Length; i++)
                {
                    if (i < currentLives)
                    {
                        lives[i].enabled = true;
                    }
                    else
                    {
                        lives[i].enabled = false;
                    }
                }
            }
			SetHealth(100);
        }
    }

    void IncreaseHealth(float increment)
    {
		currentHealth += increment;

		if (currentHealth >= 100)
		{
			SetHealth(100);
		}

		else
			SetHealth(currentHealth);
    }

	public float getHorizontalMove()
    {
		return horizontalMove;
    }

    void PlayTimerSound()
    {
        if (!TimerTick.isPlaying)
            TimerTick.Play();
	}

	void DisplayDamageOverlay() //https://www.youtube.com/watch?v=d9FaI28Yf9A
	{
		var color = DamageOverlay.color;
		color.a = 1f;
		DamageOverlay.color = color;
	}

	IEnumerator ShowHiddenAreaText()
	{
		HiddenAreaText.SetActive(true);
		yield return new WaitForSeconds(2f);
		HiddenAreaText.SetActive(false);
		StopCoroutine(ShowHiddenAreaText());
	}

	IEnumerator ShowTimerText()
	{
		TimerText.SetActive(true);
		yield return new WaitForSeconds(2f);
		TimerText.SetActive(false);
	}

	IEnumerator UploadData(string Choice) //https://ninest.vercel.app/html/google-forms-embed
	{
        WWWForm form = new WWWForm();
		WWWForm form2 = new WWWForm();
		WWWForm form3 = new WWWForm();

		if (Choice == "dead")
		{
			form.AddField("entry.1780491122", PlayerPrefs.GetString("PlayerName"));
			form.AddField("entry.2050299242", PlayerPrefs.GetString("EmailID"));
			form.AddField("entry.1962013316", PlayerPrefs.GetString("ParticipantID"));
			form.AddField("entry.407520355", gameObject.transform.position.ToString() + " " + Level + " " + (int)timer);

			UnityWebRequest www = UnityWebRequest.Post(BaseURL, form);	
			yield return www.SendWebRequest();

			if (www.isNetworkError)
			{
				Debug.Log(www.error);
			}
			/*else
			{
				Debug.Log("Form upload complete!");
				StopCoroutine(UploadData(""));
			}*/
		}

		else if (Choice == "finish")
        {
			int TimeTakenToFinishLevel = 360 - (int)timer;
			form2.AddField("entry.1780491122", PlayerPrefs.GetString("PlayerName"));
			form2.AddField("entry.2050299242", PlayerPrefs.GetString("EmailID"));
			form2.AddField("entry.1962013316", PlayerPrefs.GetString("ParticipantID"));
			form2.AddField("entry.1631373878", Level + " " + TimeTakenToFinishLevel);

			UnityWebRequest www2 = UnityWebRequest.Post(BaseURL, form2);
			yield return www2.SendWebRequest();

			if (www2.isNetworkError)
			{
				Debug.Log(www2.error);
			}
			/*else
			{
				Debug.Log("Form upload complete!");
				StopCoroutine(UploadData(""));
			}*/
		}

		else if (Choice == "Restart")
		{
			form3.AddField("entry.1780491122", PlayerPrefs.GetString("PlayerName"));
			form3.AddField("entry.2050299242", PlayerPrefs.GetString("EmailID"));
			form3.AddField("entry.1962013316", PlayerPrefs.GetString("ParticipantID"));
			form3.AddField("entry.996285243", Level + " " + (int)timer + " " + gameObject.transform.position.ToString());

			UnityWebRequest www2 = UnityWebRequest.Post(BaseURL, form3);
			yield return www2.SendWebRequest();

			if (www2.isNetworkError)
			{
				Debug.Log(www2.error);
			}
			/*else
			{
				Debug.Log("Form upload complete!");
				StopCoroutine(UploadData(""));
			}*/
		}
	}

	private void LevelComplete()
	{
		Time.timeScale = 0f;
		UICanvas.SetActive(false);
		GameComplete.SetActive(true);
		//timer = 0;
		
		Joystick.SetActive(false);

		backgroundMusic.Stop();
		TimerTick.Stop();



		if (SceneManager.GetActiveScene().name == "Traps")
			PlayerPrefs.SetInt("LoadLevel2", 1);
		else if (SceneManager.GetActiveScene().name == "Level2")
			PlayerPrefs.SetInt("LoadLevel3", 1);

		StartCoroutine(UploadData("finish"));
	}

	private void LevelNotCompleted()
    {
		Time.timeScale = 0f;
		UICanvas.SetActive(false);
		Joystick.SetActive(false);
		GameFailed.SetActive(true);
	}

	IEnumerator PlayTimerAnim() //https://www.youtube.com/watch?v=z5CdXvbTQ2Q&t=49s
	{
		for (float i = 1f; i <= 2f; i += 0.05f)
		{
			timer_rect_transform.localScale = new Vector3(i, i, i);
			yield return new WaitForEndOfFrame();
		}
		timer_rect_transform.localScale = new Vector3(2f, 2f, 2f);
		for (float i = 2f; i >= 1f; i -= 0.05f)
		{
			timer_rect_transform.localScale = new Vector3(i, i, i);
			yield return new WaitForEndOfFrame();
		}
	}

	public void StartGame()
    {
		GameStart.SetActive(false);
		UICanvas.SetActive(true);
		if (PlayerPrefs.GetString("controller") != "buttons")
		{
			Joystick.GetComponentInChildren<Image>().enabled = true;
			Joystick.SetActive(true);
		}
		Time.timeScale = 1f;
	}
}