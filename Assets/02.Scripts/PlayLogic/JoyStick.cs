using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private float _backRadius;

    private RectTransform _joyZoneRect;
    private RectTransform _joyBackRect;
    private RectTransform _joyHandelRect;
    private Vector2 _firstRectPos;

    public Vector2 JoyDirection { get; private set; }
    public Vector2 LastJoyDirection { get; private set; }

    private void Awake()
    {
        RectTransform[] rectTrans = GetComponentsInChildren<RectTransform>();
        _joyZoneRect = rectTrans[0];
        _joyBackRect = rectTrans[1];
        _joyHandelRect = rectTrans[2];

        // Canvas Size에 맞춰 Handle이 Back을 벗어나지 않도록 제한
        _backRadius = _joyBackRect.sizeDelta.x * 0.5f;
        _backRadius *= transform.parent.GetComponent<RectTransform>().localScale.x;
    }

    private void Start()
    {
        Initialize();
        LastJoyDirection = Vector2.up;
    }

    private void Initialize()
    {
        _joyHandelRect.localPosition = Vector2.zero;
        _joyBackRect.localPosition = new Vector2(0, _joyZoneRect.sizeDelta.y * 0.25f);
        JoyDirection = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position;
        Vector2 backPos = _firstRectPos;
        JoyDirection = (pos - backPos).normalized;

        float distance = Vector2.Distance(pos, backPos);
        if (distance < _backRadius)
        {
            _joyHandelRect.position = backPos + JoyDirection * distance;
        }
        else
        {
            _joyHandelRect.position = backPos + JoyDirection * _backRadius;
        }
        LastJoyDirection = JoyDirection;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _firstRectPos = eventData.position;
        _joyBackRect.position = _firstRectPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Initialize();
    }
}