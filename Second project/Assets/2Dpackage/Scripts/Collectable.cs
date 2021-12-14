using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 可收集寶物
/// </summary>
public class Collectable : MonoBehaviour
{

    public GameObject pickE;

    //【產生音效 1】
    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //用實例化 Instantiate 函式 (方法) 來產生一個物件
        Instantiate(pickE, gameObject.transform.position, Quaternion.identity);
        
        GoGoGo rubyGO = collision.GetComponent<GoGoGo>();
        print("碰到的東西是：" + rubyGO);
        rubyGO.ChangeHealth(1);

        rubyGO.PlaySound(audioClip);

        Destroy(gameObject);
    }
}
