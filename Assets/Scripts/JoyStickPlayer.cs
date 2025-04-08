using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickPlayer : MonoBehaviour
{
    public float speed;
    public VariableJoystick fixedJoystick;
    public Rigidbody2D rb;

    private void Update()
    {
        Vector2 direction = new Vector2(fixedJoystick.Horizontal, fixedJoystick.Vertical);

        if (direction.magnitude > 0.01f)
        {
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
