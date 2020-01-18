using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeluruController : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;
    int scoreP1;
    int scoreP2;
    Text scoreUIP1;
    Text scoreUIP2;
    GameObject panelSelesai;
    Text txPemenang;
    AudioSource audio;
    public AudioClip hitSound;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2, 0).normalized;
        rigid.AddForce(arah * force);
        scoreP1 = 0;
        scoreP2 = 0;
        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();     
        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive (false);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        audio.PlayOneShot(hitSound);
        if (coll.gameObject.name == "TepiKanan")
        {
            scoreP1 += 1;
            TampilkanScore();
            if (scoreP1 == 5) {
                panelSelesai.SetActive (true);
                txPemenang = GameObject.Find ("Pemenang").GetComponent<Text>();
                txPemenang.text = "Pesawat Satu Menang!";
                Destroy (gameObject);
                return;
            }
            ResetPeluru();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * force);
        }
        if (coll.gameObject.name == "TepiKiri")
        {
            scoreP2 += 1;
            TampilkanScore();
            if (scoreP2 == 5) {
                panelSelesai.SetActive (true);
                txPemenang = GameObject.Find ("Pemenang").GetComponent<Text>();
                txPemenang.text = "Pesawat Dua Menang!";
                Destroy (gameObject);
                return;
            }
            ResetPeluru();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rigid.AddForce(arah * force);
        }
        if (coll.gameObject.name == "Pesawat1" || coll.gameObject.name == "Pesawat2")
        {
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(arah * force * 2);
        }
    }
    void ResetPeluru()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
    }
    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }
}
