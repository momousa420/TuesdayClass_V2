using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �i�����_��
/// </summary>
public class Collectable : MonoBehaviour
{

    public GameObject pickE;

    //�i���ͭ��� 1�j
    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ι�Ҥ� Instantiate �禡 (��k) �Ӳ��ͤ@�Ӫ���
        Instantiate(pickE, gameObject.transform.position, Quaternion.identity);
        
        GoGoGo rubyGO = collision.GetComponent<GoGoGo>();
        print("�I�쪺�F��O�G" + rubyGO);
        rubyGO.ChangeHealth(1);

        rubyGO.PlaySound(audioClip);

        Destroy(gameObject);
    }
}
