using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateTime;
    [SerializeField] float jumpPower;
    [SerializeField] float jumpTime;

    void OnEnable()
    {
        transform.DORotate(new Vector3(0, 360, 0), rotateTime, RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental).Play();
        transform.DOJump(Vector3.zero, jumpPower, 1, jumpTime).SetRelative().SetEase(Ease.InOutSine).SetLoops(-1).Play();
    }
}
