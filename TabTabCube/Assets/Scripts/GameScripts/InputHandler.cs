using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    private Rigidbody2D playerBody;
    private Command buttonA, buttonD, buttonSpace;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        buttonA = new MoveReverse();
        buttonD = new MoveForward();
        buttonSpace = new ChangeGravity();
    }

    // Update is called once per frame
    void Update()
    {
        //HandleInput();
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            buttonA.Execute(playerBody);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            buttonD.Execute(playerBody);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            buttonSpace.Execute(playerBody);
        }
    }
    public abstract class Command
    {
        protected static float gravity = -25.8f;
        protected static float horizantalVel = 15.8f;
        protected static float Vel = 0f;

        public abstract void Execute(Rigidbody2D body);
    }

    public class MoveForward : Command
    {
        public override void Execute(Rigidbody2D body)
        {
            Vel = horizantalVel;
            body.velocity = new Vector2(Vel, gravity);
        }
    }

    public class MoveReverse : Command
    {
        public override void Execute(Rigidbody2D body)
        {
            Vel = -horizantalVel;
            body.velocity = new Vector2(Vel, gravity);
        }
    }

    public class ChangeGravity : Command
    {
        public override void Execute(Rigidbody2D body)
        {
            gravity *= -1;
            body.velocity = new Vector2(Vel, gravity);

        }
    }

}
