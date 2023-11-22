using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class TitleMovement : MonoBehaviour
{
    public Transform[] letters;

    [Header("Wave")]
    [Tooltip("Défénie le temps a attendre entre chaque nouvelle vague")]
    public float waitUntilNewWave;
    [Tooltip("Défénie l'interval entre chaque lettre")]
    public float interval;
    [Tooltip("Définie la hauteur de saut de la vague")]
    public float jumpPower;
    [Tooltip("Définie le temps de saut de la vague")]
    public float jumpTime;
    [Tooltip("Définie le temps de rotation de la vague")]
    public float rotateTime;

    [Header("Shacking")]
    [Tooltip("Définie le temps de monté/descente")]
    public float shackingTime;
    [Tooltip("Définie la hauteur de monté/descente")]
    public float shackingHeight;

    void Start()
    {
        DOTween.SetTweensCapacity(200, 125);
        StartCoroutine(Wave());
        for (int i = 0; i < letters.Length; i++)
        {
            float rand = UnityEngine.Random.Range(0f, 2f);
            StartShacking(i, rand);
        }
    }

    void StartShacking(int index, float rand)
    {
        float y = shackingHeight / (rand - 1);
        float time = shackingTime / 4 / (rand - 1);
        letters[index].position += new Vector3(0, y, 0);
        letters[index].DOMoveY(shackingHeight - y, (shackingTime / 4) - time).SetRelative().SetEase(Ease.InOutSine).OnComplete(() => ShackingDown(index));
    }

    void ShackingUp(int index)
    {
        letters[index].DOMoveY(shackingHeight * 2, shackingTime / 2).SetRelative().SetEase(Ease.InOutSine).OnComplete(() => ShackingDown(index));
    }

    void ShackingDown(int index)
    {
        letters[index].DOMoveY(-shackingHeight * 2, shackingTime / 2).SetRelative().SetEase(Ease.InOutSine).OnComplete(() => ShackingUp(index));
    }

    IEnumerator Wave(int index = 0)
    {
        DOTween.Sequence().Append(
        letters[index].GetChild(0).DORotate(new Vector3(0, 360, 0), rotateTime, RotateMode.FastBeyond360)
        .SetRelative()
        .SetEase(Ease.InOutSine))
        .AppendInterval(waitUntilNewWave)
        .SetLoops(-1, LoopType.Incremental)
        .Play();

        DOTween.Sequence().Append(
        letters[index].GetChild(0).DOJump(Vector3.zero, jumpPower, 1, jumpTime)
        .SetRelative()
        .SetEase(Ease.InOutSine))
        .AppendInterval(waitUntilNewWave)
        .SetLoops(-1, LoopType.Incremental)
        .Play();

        yield return new WaitForSeconds(interval);
        if (index < letters.Length - 1)
        {
            index++;
            StartCoroutine(Wave(index));
        }
    }
}
