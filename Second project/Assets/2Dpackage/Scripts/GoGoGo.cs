using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ϥβV�X����ruby���ʵ{��
/// </summary>
public class GoGoGo : MonoBehaviour
{
    #region �Ѽ�
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
    
    //�i�o�g�l�u 1/3�j
    public GameObject projectilePrefab;

    //�i�s�W���� 1�j
    public AudioSource audioSource;

    //�i���˭��� 1�j
    public AudioClip playerHit;

    //�i�l�u���� 1�j
    public AudioClip launchBullet;

    #endregion

    #region �ƥ�
    private void Start()
    {
        rubyAnimator = GetComponent<Animator>();  //�C���Ұʨ��o �ʵe�������
        rb = GetComponent<Rigidbody2D>();         //�C���Ұʨ��o ���餸��
        
        //�i��q���� 2/4�j
        currentHealth = maxHealth;
        print("Ruby��e��q�G" + currentHealth);

        //�i�s�W���� 2�j
        audioSource = GetComponent<AudioSource>();

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

        //�i��q���� 4/4�j���q�� 0 �ɡA���s�C�����d (Ū�����d)
        if (currentHealth == 0)
        {
            Application.LoadLevel("class_enemyGo");
        }

        //�i�o�g�l�u 3/3�j�]�w�o�g�欰������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }

    #endregion

    #region ��k
    //�i��q���� 3/4�j
    public void ChangeHealth(int amount)
    {
        //currentHealth = currentHealth + amount;      //�[�����-1
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);      //�[�����-2  ��}��
        print("Ruby��e��q�G" + currentHealth);     //�ˬd�O�_���[��

        if (amount < 0)
        {
            //�i���˭��� 2�j
            PlaySound(playerHit);
        }
    }

    //�i�o�g�l�u 2/3�j
    private void Launch()  //�ϥ�private�A�]�u�����{���M��
    {
        //�ϨC��Ruby�o�g�X���l�u (Prefab�Φ�) ���u��Ҥơv�����������C������
        //�o�ӡu��Ҥơv�ʧ@�N�n��O�ڭ̤�ʧ� #Project ��������즲�� #Scene ��
        //���C������L�{���A�ڭ̵L�k�o�򰵡A�ҥH�����z�L�{���������a����o�Ӱʧ@
        //�ͦ����L�{���A�����i���ͦ��� Prefab�B�n�ͦ�����m position�Brotation����
        //Quaternion ��ܵL����
        GameObject projectileOnject = Instantiate(projectilePrefab,
            rb.position, Quaternion.identity);

        //�b Bullet.cs ���A�ڭ̳]�m�F�@�� Launch()��k�A�óz�L�u���O����k�v�Ӳ���
        //�ҥH�o��ݭn�����]�ߤ@�� Bullet ���A���ܶq�A�@���������O���I���e��
        Bullet bullet = projectileOnject.GetComponent<Bullet>();

        //�b�W������������A�K�i�z�L�۱a�� Launch() ��k�ӹ�{�u���O����k�v
        //�ڭ̦b Bullet.cs ���w�q�� Launch() ��k�ݭn����ӰѼơG��V&�O�D
        bullet.Launch(lookDirection, 300);  //300 �ƭȶV�j�A�t�׶V��

        //�o�g��A����ʵe
        rubyAnimator.SetTrigger("Launch");

        PlaySound(launchBullet);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    #endregion

}
