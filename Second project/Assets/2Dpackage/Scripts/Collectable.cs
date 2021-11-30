using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 可收集寶物
/// </summary>
public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoGoGo rubyGO = collision.GetComponent<GoGoGo>();
        print("碰到的東西是：" + rubyGO);
        rubyGO.ChangeHealth(1);
        Destroy(gameObject);
    }
}
