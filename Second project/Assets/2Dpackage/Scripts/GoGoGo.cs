using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ϥβV�X����ruby���ʵ{��
/// </summary>
public class GoGoGo : MonoBehaviour
{
    private Vector2 lookDirection;  //�w�q�ݪ���V
    private Vector2 rubyPosition;   //�w�q��m
    private Vector2 rubyMove;       //�w�q���ʶq

    public Animator rubyAnimator;   //�w�q�Ӹ� ruby ���ʵe����ܼƽc�l
    public Rigidbody2D rb;          //�w�q���� (���ʥ�)

    public float speed = 3;

    //�i��q���� 1/4�j
    [Header("�̰���q"), Range(0, 10)]             //�b�ˬd���������U���
    public int maxHealth = 5;       //�w�q�̰���q��

    [Header("��e��q"), Range(0, 10)]   //�b�ˬd���������U���+�i�հ�
    //private int currentHealth;       //�w�q��e��q (�����)
    public int currentHealth;          //�w�q��e��q (��ܦb�ˬd��)

    private void Start()
    {
        rubyAnimator = GetComponent<Animator>();  //�C���Ұʨ��o �ʵe�������
        rb = GetComponent<Rigidbody2D>();         //�C���Ұʨ��o ���餸��
        
        //�i��q���� 2/4�j
        currentHealth = maxHealth;
        print("Ruby��e��q�G" + currentHealth);

    }

    private void FixedUpdate()
    {
        rubyPosition = transform.position;   //��ثe���󪺦�m���� ruby

        float horizontal = Input.GetAxis("Horizontal");  //�^�����k���䪺�ƭ�
        float vertical = Input.GetAxis("Vertical");      //�^���W�U���䪺�ƭ�
        //print("Horizontal is:" + horizontal);            //�ˬd�� (��ܫ���ƭ�)
        //print("Vertical is: " + vertical);                //�ˬd�� (��ܫ���ƭ�)

        rubyMove = new Vector2(horizontal, vertical);    //����䪺�ƭȽᤩ�� rubyMove

        //������J���� 0 ��
        if (!Mathf.Approximately(rubyMove.x, 0) || !Mathf.Approximately(rubyMove.y, 0))
        {
            lookDirection = rubyMove;   //���a���U���ʫ���� (����0)�A
            lookDirection.Normalize();  //�зǤơA�ϫ���ƭȧ�ֱ���ƭȡG1
        }

        //�i����V�X�𪺰ʵe�j
        rubyAnimator.SetFloat("Look X", lookDirection.x);     //�����¦V���ƭ�
        rubyAnimator.SetFloat("Look Y", lookDirection.y);     //�����¦V���ƭ�
        rubyAnimator.SetFloat("Speed", rubyMove.magnitude);   //�� rubyMove �����ʦV�q���� Speed

        //���� ruby ��m
        rubyPosition = rubyPosition + speed * rubyMove * Time.deltaTime;
        rb.MovePosition(rubyPosition);   //�ϥέ���i�沾��

    }

    //�i��q���� 3/4�j
    public void ChangeHealth(int amount)
    {
        //currentHealth = currentHealth + amount;      //�[�����-1
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);      //�[�����-2  ��}��
        print("Ruby��e��q�G" + currentHealth);     //�ˬd�O�_���[��

    }


}
