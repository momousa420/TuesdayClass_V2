using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使用剛體進行角色移動、動畫版本-1：【使用動畫 Animator】
/// </summary>

public class rubyRun : MonoBehaviour
{
    public float moveSpeed; //宣告 moveSpeed (float 型態)變數，
                            //利用 public 可在 #Inspector 設定移動速度

    public Rigidbody2D rb;  //宣告 rb (剛體型態)變數，作為可乘載此物件的剛體變數箱子

    //【使用動畫 Animator 1/3】
    public Animator rubyAnimator; //宣告 rubyAnimator (Animator)變數，作為動畫控制變數箱子

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //遊戲啟動後，自動掛載本物件的剛體元件
                                          //在此物件的 #Inspector 中記得附加剛體元件

        //【使用動畫 Animator 2/3】
        rubyAnimator = GetComponent<Animator>(); //取得本身物件的動畫控制師元件


    }

    // 使用 FixedUpdate 取代 Update() 可得到更好的物理效果
    void FixedUpdate()
    {
        Vector2 rubyPosition = transform.position; //宣告一個 rubyPosition (向量 型態)變數，
                                                   //乘載此物件的座標位置
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //【使用動畫 Animator 3/3】告訴電腦 moveX, moveY的值
        rubyAnimator.SetFloat("moveX", horizontal);
        rubyAnimator.SetFloat("moveY", vertical);
        //變更 rubyPosition 座標，分別針對 x座標＆y座標 的位置與上下左右按鍵配合的位移計算
        //乘上 Time.deltaTime 將取得精準的時間速度
        rubyPosition.x += moveSpeed * horizontal * Time.deltaTime;
        rubyPosition.y += moveSpeed * vertical * Time.deltaTime;
        //print(Input.GetAxis("Horizontal")); //在 #Console 視窗顯示左右按鍵的數值(±0~1)
        //transform.position = rubyPostion;  //舊方法不使用，因為會產生抖動行為

        rb.MovePosition(rubyPosition);  //使用剛體的 MovePosition()方法來移動角色
    }
}
