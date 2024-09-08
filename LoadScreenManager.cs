using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreenManager : MonoBehaviour
{
    public RectTransform LoadHeadsParent;
    public RectTransform BaldiHeadPre;
    public float BaldiHeadSize;
    public Vector2 GenerateCount;
    public AsyncOperation Async;
private void Start()
    {
        GenerateHeads();
        LoadHeadsParent.anchoredPosition = new Vector2((-BaldiHeadSize*GenerateCount.x) / 2, (-BaldiHeadSize*GenerateCount.y) / 2);
        base.StartCoroutine(Wait());
    }
   private IEnumerator Wait()
    {
        DontDestroyOnLoad(base.gameObject);
        while(!Async.isDone)
        {
            Debug.Log("Loading:"+ Async.progress);
            yield return null;
            }
        Destroy(base.gameObject);
    }
    private void GenerateHeads()
    {
        float Offect = 0;
        for (int y = 0; y < GenerateCount.y; y++)
        {
            if (y % 2 == 0)
            {
                Offect = 0;
                for (int x = 0; x < GenerateCount.x; x++)
                {
                    if (x % 2 == 0)
                    {
                        RectTransform a = Object.Instantiate<RectTransform>(BaldiHeadPre, LoadHeadsParent);
                        a.anchoredPosition = new Vector2(Offect, y * BaldiHeadSize);
                        Offect += BaldiHeadSize * 2;
                    }
                }
            }
            if (y % 2 != 0)
            {
                Offect = BaldiHeadSize;
                for (int x = 0; x < GenerateCount.x; x++)
                {
                    if (x % 2 == 0)
                    {
                        RectTransform a = Object.Instantiate<RectTransform>(BaldiHeadPre, LoadHeadsParent);
                        a.anchoredPosition = new Vector2(Offect, y * BaldiHeadSize);
                        Offect += BaldiHeadSize * 2;
                    }
                }
            }
        }
    }
}
