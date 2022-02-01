using UnityEngine;
using UnityEngine.EventSystems;


public enum ButtonPosition { TopLeft, TopRight, BottomLeft, BottomRight };
public enum ButtonClickStatus { OnPointerDown, OnPointerUp };

public class TouchButtonEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Event
    public delegate void ButtonClick(ButtonPosition buttonPosition, ButtonClickStatus buttonClickStatus);
    public static event ButtonClick OnButtonClick;

    // Data
    [SerializeField] private ButtonPosition buttonPosition;


    // called just one
    public void OnPointerDown(PointerEventData eventData)
    {
        SendClickEvent(buttonPosition, ButtonClickStatus.OnPointerDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SendClickEvent(buttonPosition, ButtonClickStatus.OnPointerUp);
    }

    private void SendClickEvent(ButtonPosition buttonPosition, ButtonClickStatus buttonClickStatus)
    {
        OnButtonClick?.Invoke(buttonPosition, buttonClickStatus);
    }

}
