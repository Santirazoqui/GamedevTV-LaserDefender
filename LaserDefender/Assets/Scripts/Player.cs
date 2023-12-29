using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    Vector2 rawInput;
    [SerializeField] float moveSpeed = 10f;

    Vector2 minBounds;
    Vector2 maxbounds;


    void Start()
    {
        InitBounds();
    }
    // Update is called once per frame
    void Update()
    {
        this.Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxbounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }
 
    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();

        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxbounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxbounds.y);

        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>(); 
        Debug.Log(rawInput);
    }
}
