using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement1 movement;

    public enum ButtonType { Left, Right, Jump }
    public ButtonType buttonType;

    void Start()
    {
        movement = player.GetComponent<PlayerMovement1>();

        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        // Pointer Down
        EventTrigger.Entry down = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        down.callback.AddListener((data) => { OnButtonDown(); });
        trigger.triggers.Add(down);

        // Pointer Up hanya untuk tombol Left & Right
        if (buttonType != ButtonType.Jump)
        {
            EventTrigger.Entry up = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
            up.callback.AddListener((data) => { OnButtonUp(); });
            trigger.triggers.Add(up);
        }
    }

    void OnButtonDown()
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                movement.MoveLeft(true);
                break;
            case ButtonType.Right:
                movement.MoveRight(true);
                break;
            case ButtonType.Jump:
                movement.JumpButton(true);
                break;
        }
    }

    void OnButtonUp()
    {
        if (buttonType == ButtonType.Left)
            movement.MoveLeft(false);
        else if (buttonType == ButtonType.Right)
            movement.MoveRight(false);
    }
}
