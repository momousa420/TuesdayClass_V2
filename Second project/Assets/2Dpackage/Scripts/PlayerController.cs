using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 RubyMove = new Vector2();
        RubyMove = transform.position;
        RubyMove.x = RubyMove.x + Input.GetAxis("Horizontal") * moveSpeed;
        RubyMove.y = RubyMove.y + Input.GetAxis("Vertical") * moveSpeed;
        transform.position = RubyMove;
    }
}
