using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right, Jump }
    public ButtonType buttonType;

    private PlayerController player;
    private bool isPressed = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // Controle por teclado
        switch (buttonType)
        {
            case ButtonType.Left:
                if (Input.GetKeyDown(KeyCode.A))
                    player.StartMovingLeft();
                if (Input.GetKeyUp(KeyCode.A))
                    player.StopMovingLeft();
                break;

            case ButtonType.Right:
                if (Input.GetKeyDown(KeyCode.D))
                    player.StartMovingRight();
                if (Input.GetKeyUp(KeyCode.D))
                    player.StopMovingRight();
                break;

            case ButtonType.Jump:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
                    player.Jump();
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;

        switch (buttonType)
        {
            case ButtonType.Left:
                player.StartMovingLeft();
                break;
            case ButtonType.Right:
                player.StartMovingRight();
                break;
            case ButtonType.Jump:
                player.Jump();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;

        switch (buttonType)
        {
            case ButtonType.Left:
                player.StopMovingLeft();
                break;
            case ButtonType.Right:
                player.StopMovingRight();
                break;
        }
    }
}
