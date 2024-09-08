using System.Collections;
using UnityEngine;
using AudioManager = UnityEngine.AudioSource;
using SoundObject = UnityEngine.AudioClip;
public class ITM_GrapplingHook : MonoBehaviour
{
	[SerializeField]
	private LineRenderer lineRenderer;

	[SerializeField]
	private Rigidbody rigidbody;

	[SerializeField]
	private MovementModifier moveMod;

	[SerializeField]
	private AudioManager audMan;

	[SerializeField]
	private AudioSource motorAudio;

	[SerializeField]
	private SoundObject audLaunch;

	[SerializeField]
	private SoundObject audClang;

	[SerializeField]
	private SoundObject audSnap;

	[SerializeField]
	private Transform cracks;

	private Vector3[] positions = new Vector3[2];

	[SerializeField]
	private float speed = 100f;

	[SerializeField]
	private float maxPressure = 100f;

	[SerializeField]
	private float initialForce = 20f;

	[SerializeField]
	private float forceIncrease = 5f;

	[SerializeField]
	private float stopDistance = 5f;

	public float force;

	public float pressure;

	public float initialDistance;

	public float time;

	public int uses;

	private bool locked;

	private bool snapped;

	public PlayerScript pm;

	public bool Use(PlayerScript Pm)
	{
	pm = Pm;
		if (pm.cc.enabled&&pm.gc.playerCollider.enabled)
		{
			base.transform.position = pm.transform.position+pm.transform.forward*2.5f;
			base.transform.rotation = Camera.main.transform.rotation;
			pm.Am.moveMods.Add(moveMod);
			audMan.PlayOneShot(audLaunch);
			return true;
		}
		Object.Destroy(base.gameObject);
		return false;
	}

	private void Update()
	{
		if (!locked)
		{
			rigidbody.velocity = base.transform.forward * speed * Time.timeScale;
			time += Time.deltaTime * Time.timeScale;
			if (time > 60f)
			{
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			if ((base.transform.position - pm.transform.position).magnitude <= stopDistance)
			{
				StartCoroutine(EndDelay());
			}
			moveMod.movementAddend = (base.transform.position - pm.transform.position).normalized * force;
			if (!snapped)
			{
				motorAudio.pitch = (force - initialForce) / 100f + 1f;
			}
			force += forceIncrease * Time.deltaTime;
			pressure = (base.transform.position - pm.transform.position).magnitude - (initialDistance - force);
			if (pressure > maxPressure && !snapped)
			{
				snapped = true;
				audMan.loop=false;
				audMan.PlayOneShot(audSnap);
				motorAudio.Stop();
				lineRenderer.enabled = false;
				pm.Am.moveMods.Remove(moveMod);
				StartCoroutine(WaitForAudio());
			}
		}
		positions[0] = base.transform.position;
		positions[1] = pm.transform.position - Vector3.up * 1f;
		lineRenderer.SetPositions(positions);
	}

	private IEnumerator EndDelay()
	{
		float time = 0.25f;
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		End();
	}

	private void End()
	{
		pm.Am.moveMods.Remove(moveMod);
		Object.Destroy(base.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!locked && collision.gameObject.tag != "Player" && collision.gameObject.tag != "NPC")
		{
			locked = true;
			force = initialForce;
			initialDistance = (base.transform.position - pm.transform.position).magnitude;
			rigidbody.velocity = Vector3.zero;
			audMan.PlayOneShot(audClang);
			motorAudio.Play();
			cracks.rotation = Quaternion.LookRotation(collision.contacts[0].normal * -1f, Vector3.up);
			cracks.gameObject.SetActive(true);
		}
	}

	private IEnumerator WaitForAudio()
	{
		while (audMan.isPlaying)
		{
			yield return null;
		}
		End();
	}
}
