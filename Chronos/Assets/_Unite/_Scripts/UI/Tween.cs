using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public enum TweenAnimationType
{
    Move,
    Scale,
    ScaleX,
    ScaleY,
    ScaleZ,
    Fade,
    Rotate
}

public class Tween : MonoBehaviour
{
    [SerializeField] private GameObject objectToAnimate;
    [SerializeField] private TweenAnimationType animationType;
    [SerializeField] private Ease easeType;
    [SerializeField] private float duration;
    [SerializeField] private float delay;
    [SerializeField] private bool showOnEnable;
    [SerializeField] private bool animateOnDisable;
    [SerializeField] private bool loop;
    [SerializeField] private bool pingPong;
    [SerializeField] private Vector3 from;
    [SerializeField] private Vector3 to;

    [Header("Tween Events")]
    public UnityEvent OnCycle;

    private void OnEnable()
    {
        if (showOnEnable)
        {
            Show();
        }
    }

    public void Show()
    {
        HandleTween();
    }

    public void HandleTween()
    {
        if (objectToAnimate == null)
        {
            objectToAnimate = gameObject;
        }

        switch (animationType)
        {
            case TweenAnimationType.Fade:
                Fade();
                break;
            case TweenAnimationType.Move:
                MoveToPosition();
                break;
            case TweenAnimationType.Scale:
                Scale();
                break;
            case TweenAnimationType.ScaleX:
                ScaleX();
                break;
            case TweenAnimationType.ScaleY:
                ScaleY();
                break;
            case TweenAnimationType.ScaleZ:
                ScaleZ();
                break;
            case TweenAnimationType.Rotate:
                Rotate();
                break;
        }
    }

    private void Fade()
    {
        if (objectToAnimate.GetComponent<CanvasGroup>() == null)
        {
            objectToAnimate.AddComponent<CanvasGroup>();
        }
        float canvasAlpha = objectToAnimate.GetComponent<CanvasGroup>().alpha;
        CanvasGroup canvasGroup = objectToAnimate.GetComponent<CanvasGroup>();
        canvasAlpha = from.x;
        canvasGroup.DOFade(to.x, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .OnComplete(() => OnCycle?.Invoke());
    }

    private void ScaleZ()
    {
        objectToAnimate.transform.DOScaleZ(to.z, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    private void ScaleY()
    {
        objectToAnimate.transform.DOScaleY(to.y, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    private void ScaleX()
    {
        objectToAnimate.transform.DOScaleX(to.x, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    private void Scale()
    {
        objectToAnimate.transform.DOScale(to, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    public void MoveToPosition()
    {
        Vector3 targetPosition = gameObject.transform.position + to;
        objectToAnimate.transform.DOMove(targetPosition, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .OnComplete(() => MoveFromPosition());
    }

    public void MoveFromPosition()
    {
        Vector3 targetPosition = gameObject.transform.position - from;
        objectToAnimate.transform.DOMove(targetPosition, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .OnComplete(() => OnCycle?.Invoke());
    }


    private void Rotate()
    {
        objectToAnimate.transform.DORotate(to, duration, RotateMode.FastBeyond360)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    public void ScaleTo()
    {
        objectToAnimate.transform.DOScale(to, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .OnComplete(() => ScaleFrom());
    }

    private void ScaleFrom()
    {
        objectToAnimate.transform.DOScale(from, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }
}
