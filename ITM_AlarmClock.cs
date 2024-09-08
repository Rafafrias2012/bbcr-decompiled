using System.Collections;
using UnityEngine;
using AudioManager = UnityEngine.AudioSource;
using SoundObject = UnityEngine.AudioClip;
public class ITM_AlarmClock : MonoBehaviour
{
    [SerializeField]
    private AudioManager audMan;

    [SerializeField]
    private SoundObject audRing;

    [SerializeField]
    private SoundObject audWind;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] clockSprite = new Sprite[4];

    [SerializeField]
    private float[] setTime = new float[4] { 15f, 30f, 45f, 60f };

    private float time;

    [SerializeField]
    [Range(0f, 3f)]
    private int initSetTime = 1;

    [SerializeField]
    [Range(0f, 127f)]
    private int noiseVal = 112;

    private bool finished;

    public PlayerScript Pm;

    public void Use(PlayerScript pm)
    {
        Pm = pm;
        base.transform.position = pm.transform.position;
        StartCoroutine(Timer(setTime[initSetTime]));
    }

    private IEnumerator Timer(float initTime)
    {
        time = initTime;
        while (time > 0f)
        {
            time -= Time.deltaTime * Time.timeScale;
            if (time <= setTime[0])
            {
                spriteRenderer.sprite = clockSprite[0];
            }
            else if (time <= setTime[1])
            {
                spriteRenderer.sprite = clockSprite[1];
            }
            else if (time <= setTime[2])
            {
                spriteRenderer.sprite = clockSprite[2];
            }
            else
            {
                spriteRenderer.sprite = clockSprite[3];
            }
            yield return null;
        }
    Pm.baldi.Hear(base.transform.position, noiseVal);
        audMan.Pause();
        audMan.PlayOneShot(audRing);
        finished = true;
        spriteRenderer.sprite = clockSprite[3];
        while (audMan.isPlaying)
        {
            yield return null;
        }
        Object.Destroy(base.gameObject);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform==base.transform&&(Vector3.Distance(base.transform.position,Pm.transform.position)<10))
            {
                Clicked();
            }
        }
    }
    public void Clicked()
    {
        if (!finished)
        {
            audMan.PlayOneShot(audWind);
            if (time <= setTime[0])
            {
                time = setTime[1];
            }
            else if (time <= setTime[1])
            {
                time = setTime[2];
            }
            else if (time <= setTime[2])
            {
                time = setTime[3];
            }
            else
            {
                time = setTime[0];
            }
        }
    }
}
