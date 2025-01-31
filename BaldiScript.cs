
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BaldiScript : MonoBehaviour
{
	public bool db;

	public float baseTime;

	public float speed;

	public float timeToMove;

	public float baldiAnger;

	public float baldiTempAnger;

	public float baldiWait;

	public float baldiSpeedScale;

	private float moveFrames;

	private float currentPriority;

	public bool antiHearing;

	public float antiHearingTime;

	public float vibrationDistance;

	public float angerRate;

	public float angerRateRate;

	public float angerFrequency;

	public float timeToAnger;

	public bool endless;

	public Transform player;

	public Transform wanderTarget;

	public AILocationSelectorScript wanderer;

	private AudioSource baldiAudio;

	public AudioClip slap;

	public AudioClip[] speech = new AudioClip[3];

	public Animator baldiAnimator;

	public float coolDown;

	private Vector3 previous;

	private bool rumble;

	private NavMeshAgent agent;

	public Animator BaldiCator;

	private void Start()
	{
		baldiAudio = GetComponent<AudioSource>();
		agent = GetComponent<NavMeshAgent>();
		timeToMove = baseTime;
		Wander();
		if (PlayerPrefs.GetInt("Rumble") == 1)
		{
			rumble = true;
		}
	}

	private void Update()
	{
		if (timeToMove > 0f)
		{
			timeToMove -= 1f * Time.deltaTime;
		}
		else
		{
			Move();
		}
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
		if (baldiTempAnger > 0f)
		{
			baldiTempAnger -= 0.02f * Time.deltaTime;
		}
		else
		{
			baldiTempAnger = 0f;
		}
		if (antiHearingTime > 0f)
		{
			antiHearingTime -= Time.deltaTime;
		}
		else
		{
			antiHearing = false;
		}
		if (endless)
		{
			if (timeToAnger > 0f)
			{
				timeToAnger -= 1f * Time.deltaTime;
				return;
			}
			timeToAnger = angerFrequency;
			GetAngry(angerRate);
			angerRate += angerRateRate;
		}
	}

	private void FixedUpdate()
	{
		if (moveFrames > 0f)
		{
			moveFrames -= 1f;
			agent.speed = speed;
		}
		else
		{
			agent.speed = 0f;
		}
		Vector3 direction = player.position - base.transform.position;
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out hitInfo, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player"))
		{
			db = true;
			TargetPlayer();
		}
		else
		{
			db = false;
		}
	}

	private void Wander()
	{
		wanderer.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		coolDown = 1f;
		currentPriority = 0f;
	}

	public void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 1f;
		currentPriority = 0f;
	}

	private void Move()
	{
		if ((base.transform.position == previous) & (coolDown < 0f))
		{
			Wander();
		}
		moveFrames = 10f;
		timeToMove = baldiWait - baldiTempAnger;
		previous = base.transform.position;
		baldiAudio.PlayOneShot(slap);
		baldiAnimator.SetTrigger("slap");
		if (rumble)
		{
			float num = Vector3.Distance(base.transform.position, player.position);
			if (num < vibrationDistance)
			{
				float motorLevel = 1f - num / vibrationDistance;
			}
		}
	}

	public void GetAngry(float value)
	{
		baldiAnger += value;
		if (baldiAnger < 0.5f)
		{
			baldiAnger = 0.5f;
		}
		baldiWait = -6f * baldiAnger / (baldiAnger + 2f / baldiSpeedScale) + 6f;
	}

	public void GetTempAngry(float value)
	{
		baldiTempAnger += value;
	}

	public void Hear(Vector3 soundLocation, float priority)
	{
		if (!antiHearing && priority >= currentPriority)
		{
			BaldiCator.SetTrigger("Hear");
			agent.SetDestination(soundLocation);
			currentPriority = priority;
			return;
		}
		BaldiCator.SetTrigger("Think");
	}

	public void ActivateAntiHearing(float t)
	{
		Wander();
		antiHearing = true;
		antiHearingTime = t;
	}
	public IEnumerator EatApple()
	{
		EatingApple = true;
		baldiAnimator.SetBool("Apple Intro", true);
		baldiAudio.PlayOneShot(aud_Apple);
		float t = aud_Apple.length;
		while(t>0)
		{
			timeToMove = 1f;
			if(Time.timeScale==0)
			{
				baldiAudio.Pause();
			}
			t -= Time.deltaTime*Time.timeScale;
			yield return null;
		}
        baldiAnimator.SetBool("Apple Intro",false);
        baldiAnimator.SetBool("Eating Apple", true);
        float Delay = EatingDelay;
		float SoundDelay = 0.05f;
		while(Delay > 0)
		{
			if(SoundDelay<0)
			{
				SoundDelay = 0.05f;
				baldiAudio.PlayOneShot(WeightedSelection<AudioClip>.RandomSelection(aud_EatingAppleSounds));
			}
		SoundDelay-=Time.deltaTime*Time.timeScale;
			timeToMove = 1f;
			Delay -= Time.deltaTime*Time.timeScale;
			yield return null;
		}
		baldiAnimator.SetBool("Eating Apple", false);
		EatingApple=false;
    }
	public bool EatingApple;
	public AudioClip aud_Apple;
    public WeightedSelection<AudioClip>[] aud_EatingAppleSounds=new WeightedSelection<AudioClip>[3];
	public float EatingDelay;
}
