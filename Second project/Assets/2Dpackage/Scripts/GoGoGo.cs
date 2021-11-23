using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使用混合樹機制的ruby移動程式
/// </summary>
public class GoGoGo : MonoBehaviour
{
    private Vector2 lookDirection;  //定義看的方向
    private Vector2 rubyPosition;   //定義位置
    private Vector2 rubyMove;       //定義移動量

    public Animator rubyAnimator;   //定義承載 ruby 的動畫控制器變數箱子
    public Rigidbody2D rb;          //定義剛體 (移動用)

    public float speed = 3;

    private void Start()
    {
        rubyAnimator = GetComponent<Animator>();  //遊戲啟動取得 動畫控制器元件
        rb = GetComponent<Rigidbody2D>();         //遊戲啟動取得 剛體元件
    }

    private void FixedUpdate()
    {
        rubyPosition = transform.position;   //把目前物件的位置給予 ruby

        float horizontal = Input.GetAxis("Horizontal");  //擷取左右按鍵的數值
        float vertical = Input.GetAxis("Vertical");      //擷取上下按鍵的數值
        print("Horizontal is: " + horizontal);            //檢查用 (顯示按鍵數值)
        print("Vertical is: " + vertical);                //檢查用 (顯示按鍵數值)

        rubyMove = new Vector2(horizontal, vertical);    //把按鍵的數值賦予給 rubyMove

        //當按鍵輸入不為 0 時
        if (!Mathf.Approximately(rubyMove.x, 0) || !Mathf.Approximately(rubyMove.y, 0))
        {
            lookDirection = rubyMove;   //當玩家按下移動按鍵時 (不為0)，
            lookDirection.Normalize();  //標準化，使按鍵數值更快接近數值：1
        }

        //【控制混合樹的動畫】
        rubyAnimator.SetFloat("Look X", lookDirection.x);     //給予朝向的數值
        rubyAnimator.SetFloat("Look Y", lookDirection.y);     //給予朝向的數值
        rubyAnimator.SetFloat("Speed", rubyMove.magnitude);   //把 rubyMove 的移動向量給予 Speed

        //移動 ruby 位置
        rubyPosition = rubyPosition + speed * rubyMove * Time.deltaTime;
        rb.MovePosition(rubyPosition);   //使用剛體進行移動
    }
}
