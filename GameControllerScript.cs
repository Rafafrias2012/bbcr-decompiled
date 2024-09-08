using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
	public CursorControllerScript cursorController;

	public PlayerScript player;

	// Token: 0x04000623 RID: 1571
	public CharacterController playerCharacter;

	// Token: 0x04000624 RID: 1572
	public Collider playerCollider;

	// Token: 0x04000625 RID: 1573
	public AILocationSelectorScript AILocationSelector;

	public Transform playerTransform;

	public Transform cameraTransform;

	public Camera camera;

	public EntranceScript entrance_0;

	public EntranceScript entrance_1;

	public EntranceScript entrance_2;

	public EntranceScript entrance_3;

	public GameObject baldiTutor;

	public GameObject baldi;

	public BaldiScript baldiScrpt;

	public AudioClip aud_Prize;

	public AudioClip aud_PrizeMobile;

	public AudioClip aud_AllNotebooks;

	// Token: 0x04000634 RID: 1588
	public AudioClip aud_Teleport;

	// Token: 0x04000635 RID: 1589
	private bool flipped;

	public GameObject principal;

	public GameObject crafters;

	public GameObject playtime;

	public PlaytimeScript playtimeScript;

	public GameObject gottaSweep;

	public GameObject bully;

	public GameObject firstPrize;

	public FirstPrizeScript firstPrizeScript;

	public GameObject quarter;

	public AudioSource tutorBaldi;

	public RectTransform boots;

	public string mode;

	public int notebooks;

	public GameObject[] notebookPickups;

	public int failedNotebooks;

	public bool spoopMode;

	public bool finaleMode;

	public bool debugMode;

	public bool mouseLocked;

	public int exitsReached;

	public int itemSelected;

	public int[] item = new int[5];

	public RawImage[] itemSlot = new RawImage[5];

	private string[] itemNames = new string[18]
	{
		"Air", "Energy flavored Zesty Bar", "Yellow Door Lock", "Principal's Keys", "BSODA", "Quarter", "Baldi Anti Hearing and Disorienting Tape", "Alarm Clock", "WD-NoSquee (Door Type)", "Safety Scissors",
		"Big Ol' Boots","Dirty Chalk Eraser","Dangerous Teleporter","Grappling Hook","Faculty Nametag","Principal Whistle","An Apple For Baldi","Water Bottle"
	};

	public TMP_Text itemText;

	public Object[] items = new Object[11];

	public Texture[] itemTextures = new Texture[11];

	public GameObject bsodaSpray;

	public GameObject alarmClock;

	public TMP_Text notebookCount;

	public GameObject pauseMenu;

	public GameObject highScoreText;

	public GameObject reticle;

	public RectTransform itemSelect;

	private int[] itemSelectOffset = new int[5] { -157, -119, -80, -40, 0 };

	private bool gamePaused;

	private bool learningActive;

	private float gameOverDelay;

	private AudioSource audioDevice;

	public AudioClip aud_Soda;

	public AudioClip aud_Spray;

	public AudioClip aud_buzz;

	public AudioClip aud_Hang;

	public AudioClip aud_MachineQuiet;

	public AudioClip aud_MachineStart;

	public AudioClip aud_MachineRev;

	public AudioClip aud_MachineLoop;

	public AudioClip aud_Switch;

	public AudioSource schoolMusic;

	public AudioSource learnMusic;

	public BoxCollider ChalkCloud;

	public GameObject CursorImage;

	private void Start()
	{
		audioDevice = GetComponent<AudioSource>();
		mode = PlayerPrefs.GetString("CurrentMode");
		if (mode == "endless")
		{
			baldiScrpt.endless = true;
		}
		schoolMusic.Play();
		LockMouse();
		UpdateNotebookCount();
		gameOverDelay = 0.5f;
	}

	private void Update()
	{
		if (!learningActive)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (Vector3.Distance(playerTransform.position, hitInfo.point) < 10)
				{
					if ((hitInfo.transform.tag == "Notebook") || (hitInfo.transform.tag == "Item") || (hitInfo.transform.tag == "Door") || (hitInfo.collider.name == "PayPhone") || (hitInfo.collider.name == "ZestyMachine") || (hitInfo.collider.name == "BSODAMachine"))
					{
						CursorImage.SetActive(true && !gamePaused);
					}
					else
					{
						CursorImage.SetActive(false);
					}
				}
				else
				{
					CursorImage.SetActive(false);
				}
			}
			else
			{
				CursorImage.SetActive(false);
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (!gamePaused)
				{
					PauseGame();
				}
				else
				{
					UnpauseGame();
				}
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Y) & gamePaused)
			{
				ExitGame();
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.N) & gamePaused)
			{
				UnpauseGame();
			}
			if (!gamePaused & (Time.timeScale != 1f))
			{
				Time.timeScale = 1f;
			}
			if (Input.GetMouseButtonDown(1) && Time.timeScale != 0f)
            {
                UseItem();
            }
			else if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale != 0f)
            {
                UseItem();
            }
			if ((Input.GetAxis("Mouse ScrollWheel") > 0f) && Time.timeScale != 0f)
			{
				DecreaseItemSelection();
			}
			else if ((UnityEngine.Input.GetAxis("Mouse ScrollWheel") < 0f) && Time.timeScale != 0f)
			{
				IncreaseItemSelection();
			}
			if (Time.timeScale != 0f)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					itemSelected = 0;
					UpdateItemSelection();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					itemSelected = 1;
					UpdateItemSelection();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					itemSelected = 2;
					UpdateItemSelection();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					itemSelected = 3;
					UpdateItemSelection();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					itemSelected = 4;
					UpdateItemSelection();
				}
			}
		}
		else
		{
			cursorController.LockCursor();
			if (Time.timeScale != 0f)
			{
				Time.timeScale = 0f;
			}
			else
			{
				cursorController.UnlockCursor();
			}
		}
		if (player.gameOver)
		{
			if (mode == "endless" && notebooks > PlayerPrefs.GetInt("HighBooks") && !highScoreText.activeSelf)
			{
				highScoreText.SetActive(true);
			}
			Time.timeScale = 0f;
			gameOverDelay -= Time.unscaledDeltaTime * 0.5f;
			camera.farClipPlane = gameOverDelay * 400f;
			audioDevice.PlayOneShot(aud_buzz);
			if (PlayerPrefs.GetInt("Rumble") == 1)
			{
			}
			if (gameOverDelay <= 0f)
			{
				if (mode == "endless")
				{
					if (notebooks > PlayerPrefs.GetInt("HighBooks"))
					{
						PlayerPrefs.SetInt("HighBooks", notebooks);
					}
					PlayerPrefs.SetInt("CurrentBooks", notebooks);
				}
				Time.timeScale = 1f;
				SceneManager.LoadScene("GameOver");
			}
		}
		if (finaleMode && !audioDevice.isPlaying && exitsReached == 3)
		{
			audioDevice.clip = aud_MachineLoop;
			audioDevice.loop = true;
			audioDevice.Play();
		}
		if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
	}

	private void UpdateNotebookCount()
	{
		if (mode == "story")
		{
			notebookCount.text = notebooks + "/7 Notebooks";
		}
		else
		{
			notebookCount.text = notebooks + " Notebooks";
		}
		if ((notebooks == 7) & (mode == "story"))
		{
			ActivateFinaleMode();
		}
	}

	public void CollectNotebook()
	{
		notebooks++;
		UpdateNotebookCount();
	}

	public void LockMouse()
	{
		if (!learningActive)
		{
			cursorController.LockCursor();
			mouseLocked = true;
			reticle.SetActive(true);
		}
	}

	public void UnlockMouse()
	{
		cursorController.UnlockCursor();
		mouseLocked = false;
		reticle.SetActive(false);
	}

	public void PauseGame()
	{
		if (!learningActive)
		{
			UnlockMouse();
			Time.timeScale = 0f;
			gamePaused = true;
			pauseMenu.SetActive(true);
		}
	}

	public void ExitGame()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void UnpauseGame()
	{
		Time.timeScale = 1f;
		gamePaused = false;
		pauseMenu.SetActive(false);
		LockMouse();
	}

	public void ActivateSpoopMode(bool PlayMusic)
	{
		spoopMode = true;
		entrance_0.Lower();
		entrance_1.Lower();
		entrance_2.Lower();
		entrance_3.Lower();
		baldiTutor.SetActive(false);
		baldi.SetActive(true);
		principal.SetActive(true);
		crafters.SetActive(true);
		playtime.SetActive(true);
		gottaSweep.SetActive(true);
		bully.SetActive(true);
		firstPrize.SetActive(true);
		if (PlayMusic)
		{
			audioDevice.PlayOneShot(aud_Hang);
		}
		learnMusic.Stop();
		schoolMusic.Stop();
	}

	private void ActivateFinaleMode()
	{
		finaleMode = true;
		entrance_0.Raise();
		entrance_1.Raise();
		entrance_2.Raise();
		entrance_3.Raise();
	}

	public void GetAngry(float value)
	{
		if (!spoopMode)
		{
			ActivateSpoopMode(false);
		}
		baldiScrpt.GetAngry(value);
	}

	public void ActivateLearningGame()
	{
		learningActive = true;
		UnlockMouse();
		tutorBaldi.Stop();
		if (!spoopMode)
		{
			schoolMusic.Stop();
			learnMusic.Play();
		}
	}

	public void DeactivateLearningGame(GameObject subject)
	{
		learningActive = false;
		Object.Destroy(subject);
		LockMouse();
		if (player.stamina < 100f)
		{
			player.stamina = 100f;
		}
		if (!spoopMode)
		{
			schoolMusic.Play();
			learnMusic.Stop();
		}
		if ((notebooks == 1) & !spoopMode)
		{
			quarter.SetActive(true);
			tutorBaldi.PlayOneShot(aud_Prize);
		}
		else if ((notebooks == 7) & (mode == "story"))
		{
			audioDevice.PlayOneShot(aud_AllNotebooks, 0.8f);
		}
	}

	private void IncreaseItemSelection()
	{
		itemSelected++;
		if (itemSelected > 4)
		{
			itemSelected = 0;
		}
		itemSelect.anchoredPosition = new Vector3(itemSelectOffset[itemSelected], 0f, 0f);
		UpdateItemName();
	}

	private void DecreaseItemSelection()
	{
		itemSelected--;
		if (itemSelected < 0)
		{
			itemSelected = 4;
		}
		itemSelect.anchoredPosition = new Vector3(itemSelectOffset[itemSelected], 0f, 0f);
		UpdateItemName();
	}

	private void UpdateItemSelection()
	{
		itemSelect.anchoredPosition = new Vector3(itemSelectOffset[itemSelected], 0f, 0f);
		UpdateItemName();
	}

	public void CollectItem(int item_ID)
	{
		bool took = false;
		for (int i = 0; i < item.Length; i++)
		{
			if (item[i] == 0 && !took)
			{
				took = true;
				item[i] = item_ID;
				if (item[i] == 13)
				{
					GrappleHookCount[i] = 5;
				}
				itemSlot[i].texture = itemTextures[item_ID];
			}
			else if (i == item.Length - 1 && !took)
			{
				took = true;
				item[itemSelected] = item_ID;
				if (item[itemSelected] == 13)
				{
					GrappleHookCount[itemSelected] = 5;
				}
				itemSlot[itemSelected].texture = itemTextures[item_ID];
			}
		}
		UpdateItemName();
	}

	private void UseItem()
	{
		if (item[itemSelected] == 0)
		{
			return;
		}
		if (item[itemSelected] == 1)
		{
			player.stamina = player.maxStamina * 2f;
			ResetItem();
		}
		else if (item[itemSelected] == 2)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && ((hitInfo.collider.tag == "SwingingDoor") & (Vector3.Distance(playerTransform.position, hitInfo.transform.position) <= 10f)))
			{
				hitInfo.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f);
				ResetItem();
			}
		}
		else if (item[itemSelected] == 3)
		{
			Ray ray2 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo2;
			if (Physics.Raycast(ray2, out hitInfo2) && ((hitInfo2.collider.tag == "Door") & (Vector3.Distance(playerTransform.position, hitInfo2.transform.position) <= 10f)))
			{
				DoorScript component = hitInfo2.collider.gameObject.GetComponent<DoorScript>();
				if (component.DoorLocked)
				{
					component.UnlockDoor();
					component.OpenDoor();
					ResetItem();
				}
			}
		}
		else if (item[itemSelected] == 4)
		{
			Object.Instantiate(bsodaSpray, playerTransform.position, cameraTransform.rotation);
			ResetItem();
			player.ResetGuilt("drink", 1f);
			audioDevice.PlayOneShot(aud_Soda);
		}
		else if (item[itemSelected] == 5)
		{
			Ray ray3 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo3;
			if (Physics.Raycast(ray3, out hitInfo3))
			{
				if ((hitInfo3.collider.name == "BSODAMachine") & (Vector3.Distance(playerTransform.position, hitInfo3.transform.position) <= 10f))
				{
					ResetItem();
					CollectItem(4);
					audioDevice.PlayOneShot(aud_CoinDrop);
				}
				else if ((hitInfo3.collider.name == "ZestyMachine") & (Vector3.Distance(playerTransform.position, hitInfo3.transform.position) <= 10f))
				{
					ResetItem();
					CollectItem(1);
					audioDevice.PlayOneShot(aud_CoinDrop);
				}
				else if ((hitInfo3.collider.name == "PayPhone") & (Vector3.Distance(playerTransform.position, hitInfo3.transform.position) <= 10f))
				{
					hitInfo3.collider.gameObject.GetComponent<TapePlayerScript>().Play();
					ResetItem();
					audioDevice.PlayOneShot(aud_CoinDrop);
				}
			}
		}
		else if (item[itemSelected] == 6)
		{
			Ray ray4 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo4;
			if (Physics.Raycast(ray4, out hitInfo4) && ((hitInfo4.collider.name == "TapePlayer") & (Vector3.Distance(playerTransform.position, hitInfo4.transform.position) <= 10f)))
			{
				hitInfo4.collider.gameObject.GetComponent<TapePlayerScript>().Play();
				ResetItem();
			}
		}
		else if (item[itemSelected] == 7)
		{
			GameObject gameObject = Object.Instantiate(alarmClock, playerTransform.position, cameraTransform.rotation);
			gameObject.GetComponent<ITM_AlarmClock>().Use(player);
			ResetItem();
		}
		else if (item[itemSelected] == 8)
		{
			Ray ray5 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo5;
			if (Physics.Raycast(ray5, out hitInfo5) && ((hitInfo5.collider.tag == "Door") & (Vector3.Distance(playerTransform.position, hitInfo5.transform.position) <= 10f)))
			{
				hitInfo5.collider.gameObject.GetComponent<DoorScript>().SilenceDoor();
				ResetItem();
				audioDevice.PlayOneShot(aud_Spray);
			}
		}
		else if (item[itemSelected] == 9)
		{
			Ray ray6 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo6;
			if (player.jumpRope)
			{
				player.DeactivateJumpRope();
				playtimeScript.Disappoint();
				ResetItem();
			}
			else if (Physics.Raycast(ray6, out hitInfo6) && hitInfo6.collider.name == "1st Prize")
			{
				firstPrizeScript.GoCrazy();
				ResetItem();
			}
		}
		else if (item[itemSelected] == 10)
		{
			player.ActivateBoots();
			StartCoroutine(BootAnimation());
			ResetItem();
		}
		else if (item[itemSelected] == 11)
		{
			Physics.IgnoreCollision(playerCharacter, Object.Instantiate<BoxCollider>(ChalkCloud, playerTransform.position, Quaternion.identity));
			ResetItem();
		}
		else if (item[itemSelected] == 12)
		{
			base.StartCoroutine(this.Teleporter());
			ResetItem();
		}
		else if (item[itemSelected] == 13)
		{
			Object.Instantiate<ITM_GrapplingHook>(GrapplingHookPre, playerTransform.position, cameraTransform.rotation).Use(player);
			GrappleHookCount[itemSelected]--;
			UpdateItemName();
			if (GrappleHookCount[itemSelected] == 0)
			{
				ResetItem();
			}
		}
		else if (item[itemSelected] == 14)
		{
			base.StartCoroutine(FacultyNametag());
			ResetItem();
		}
		else if (item[itemSelected] == 15)
		{
			audioDevice.PlayOneShot(aud_PrincipalWhistle);
			principal.GetComponent<PrincipalScript>().HearWhistle();
			ResetItem();
		}
		else if (item[itemSelected] == 16)
        {
            player.stamina = player.maxStamina;
            ResetItem();
        }
	}
	[Header("Grappling Hook Settings")]
	[Tooltip("Grappling Hook Settings")]
	public ITM_GrapplingHook GrapplingHookPre;
	public List<int> GrappleHookCount = new List<int>();
	private IEnumerator FacultyNametag()
	{
		NametagAnim.SetBool("Spin", true);
		NametagActive = true;
		NametagCount++;
		float t = NametagTime;
		while (t > 0)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		NametagCount--;
		if (NametagCount <= 0)
		{
			NametagActive = false;
			NametagAnim.SetBool("Spin", false);
		}
	}
	[Header("Faculty Nametag Settings")]
	[Tooltip("Faculty Nametag Settings")]
	public Animator NametagAnim;
	public bool NametagActive;
	private int NametagCount;
	public float NametagTime;
	[Header("Principal Whistle Settings")]
	[Tooltip("Principal Whistle Settings")]
	public AudioClip aud_PrincipalWhistle;
	[Header("Machine Settings")]
	[Tooltip("Machine Settings")]
	public AudioClip aud_CoinDrop;
	private IEnumerator BootAnimation()
	{
		float time = 15f;
		float height = 375f;
		Vector3 position5 = default(Vector3);
		boots.gameObject.SetActive(true);
		while (height > -375f)
		{
			height -= 375f * Time.deltaTime;
			time -= Time.deltaTime;
			position5 = boots.localPosition;
			position5.y = height;
			boots.localPosition = position5;
			yield return null;
		}
		position5 = boots.localPosition;
		position5.y = -375f;
		boots.localPosition = position5;
		boots.gameObject.SetActive(false);
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		boots.gameObject.SetActive(true);
		while (height < 375f)
		{
			height += 375f * Time.deltaTime;
			position5 = boots.localPosition;
			position5.y = height;
			boots.localPosition = position5;
			yield return null;
		}
		position5 = boots.localPosition;
		position5.y = 375f;
		boots.localPosition = position5;
		boots.gameObject.SetActive(false);
	}
	// Token: 0x06000991 RID: 2449 RVA: 0x000236A0 File Offset: 0x00021AA0
	private IEnumerator Teleporter()
	{
		this.playerCharacter.enabled = false;
		this.playerCollider.enabled = false;
		int teleports = UnityEngine.Random.Range(12, 16);
		int teleportCount = 0;
		float baseTime = 0.2f;
		float currentTime = baseTime;
		float increaseFactor = 1.1f;
		while (teleportCount < teleports)
		{
			currentTime -= Time.deltaTime;
			if (currentTime < 0f)
			{
				this.Teleport();
				teleportCount++;
				baseTime *= increaseFactor;
				currentTime = baseTime;
			}
			if (this.flipped)
			{
				this.player.height = 6f;
			}
			else
			{
				this.player.height = 4f;
			}
			yield return null;
		}
		this.playerCharacter.enabled = true;
		this.playerCollider.enabled = true;
		yield break;
	}
	// Token: 0x06000992 RID: 2450 RVA: 0x000236BC File Offset: 0x00021ABC
	private void Teleport()
	{
		this.AILocationSelector.GetNewTarget();
		this.player.transform.position = this.AILocationSelector.transform.position + Vector3.up * this.player.height;
		this.audioDevice.PlayOneShot(this.aud_Teleport);
	}
	private void ResetItem()
	{
		item[itemSelected] = 0;
		itemSlot[itemSelected].texture = itemTextures[0];
		UpdateItemName();
	}

	public void LoseItem(int id)
	{
		item[id] = 0;
		itemSlot[id].texture = itemTextures[0];
		UpdateItemName();
	}

	private void UpdateItemName()
	{
		if (item[itemSelected] != 13)
		{
			itemText.text = itemNames[item[itemSelected]];
		}
		if (item[itemSelected] == 13)
		{
			itemText.text = "(" + GrappleHookCount[itemSelected] + ")" + itemNames[item[itemSelected]];
		}
	}

	public void ExitReached()
	{
		exitsReached++;
		if (exitsReached == 1)
		{
			RenderSettings.ambientLight = Color.red;
			RenderSettings.fog = true;
			audioDevice.PlayOneShot(aud_Switch, 0.8f);
			audioDevice.clip = aud_MachineQuiet;
			audioDevice.loop = true;
			audioDevice.Play();
		}
		if (exitsReached == 2)
		{
			audioDevice.volume = 0.8f;
			audioDevice.clip = aud_MachineStart;
			audioDevice.loop = true;
			audioDevice.Play();
		}
		if (exitsReached == 3)
		{
			audioDevice.clip = aud_MachineRev;
			audioDevice.loop = false;
			audioDevice.Play();
		}
	}

	public void DespawnCrafters()
	{
		crafters.SetActive(false);
	}

	public void Fliparoo()
	{
		this.flipped = true;
		player.height = 6f;
		player.fliparoo = 180f;
		player.flipaturn = -1f;
		Camera.main.GetComponent<CameraScript>().offset = new Vector3(0f, -1f, 0f);
	}
	
	private void ResetGame()
{
    // Reset player position and rotation
    player.transform.position = Vector3.zero;
    player.transform.rotation = Quaternion.identity;

    // Reset player's game over state
    player.gameOver = false;

    // Reset notebook count
    notebooks = 0;
    UpdateNotebookCount();

    // Reset item selection
    itemSelected = 0;
    UpdateItemSelection();

    // Reset items
    for (int i = 0; i < item.Length; i++)
    {
        item[i] = 0;
        itemSlot[i].texture = itemTextures[0];
    }

    // Reset exits reached
    exitsReached = 0;

    // Reset game mode
    mode = PlayerPrefs.GetString("CurrentMode");

    // Reset game paused state
    gamePaused = false;

    // Reset time scale
    Time.timeScale = 1f;

    // Reset audio
    audioDevice.Stop();
    schoolMusic.Play();

    // Reset other game objects
    baldiTutor.SetActive(true);
    baldi.SetActive(false);
    principal.SetActive(false);
    crafters.SetActive(true);
    playtime.SetActive(true);
    gottaSweep.SetActive(true);
    bully.SetActive(true);
    firstPrize.SetActive(true);
    quarter.SetActive(false);
}
}
