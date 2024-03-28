using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public enum TweenAnimationType
{
    Fade,
    MoveTo,
    MoveFrom,
    Rotate,
    Scale,
    ScaleX,
    ScaleY,
    ScaleZ,
    Sequence
}

public class Tweening : MonoBehaviour
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

    void Start()
    {
        
    }

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
            case TweenAnimationType.MoveTo:
                MoveToPosition();
                break;
            case TweenAnimationType.MoveFrom:
                MoveFromPosition();
                break;
            case TweenAnimationType.Rotate:
                Rotate();
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
            case TweenAnimationType.Sequence:
                Sequence();
                break;
        }
    }

    public void Fade()
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
        Vector3 targetPosition = gameObject.transform.position + (from * 2);
        objectToAnimate.transform.DOMove(targetPosition, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .OnComplete(() => OnCycle?.Invoke());
    }


    public void Rotate()
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

    public void ScaleFrom()
    {
        objectToAnimate.transform.DOScale(from, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    public void ScaleZ()
    {
        objectToAnimate.transform.DOScaleZ(to.z, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    public void ScaleY()
    {
        objectToAnimate.transform.DOScaleY(to.y, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    public void ScaleX()
    {
        objectToAnimate.transform.DOScaleX(to.x, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    public void Scale()
    {
        objectToAnimate.transform.DOScale(to, duration)
            .SetLoops(loop ? -1 : 0, loop ? LoopType.Yoyo : LoopType.Restart)
            .SetEase(easeType)
            .SetDelay(delay)
            .OnComplete(() => OnCycle?.Invoke());
    }

    public void Sequence()
    {

        // Create a new Sequence.
        // We will set it so that the whole duration is 6
        Sequence s = DOTween.Sequence();
        // Add an horizontal relative move tween that will last the whole Sequence's duration
        s.Append(objectToAnimate.transform.DOMove(gameObject.transform.position, duration).SetRelative().SetEase(Ease.InOutQuad));
        // Insert a rotation tween which will last half the duration
        // and will loop forward and backward twice
        s.Insert(0, objectToAnimate.transform.DORotate(new Vector3(0, 45, 0), duration / 2).SetEase(Ease.InQuad).SetLoops(2, LoopType.Yoyo));
        // Add a color tween that will start at half the duration and last until the end
        s.Insert(duration / 2, objectToAnimate.GetComponent<Renderer>().material.DOColor(Color.yellow, duration / 2));
        // Set the whole Sequence to loop infinitely forward and backwards
        s.SetLoops(-1, LoopType.Yoyo);
    }
}