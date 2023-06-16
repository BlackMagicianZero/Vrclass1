using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tower_New : MonoBehaviour
{
    public static Tower_New Instance;

    public Slider hpslider;

    public int MAX_HP = 10;
    int hp = 0;

    public GameObject die;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        hp = MAX_HP;
    }

    public void Damage()
    {
        hp--;
        Debug.Log("아파요");
        hpslider.value = hp;

        if (hp <= 0)
        {
            if (die)
            {
                die.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drone"))
        {
            Drone drone = other.GetComponent<Drone>();
            if (drone != null)
            {
                Destroy(drone.gameObject);
                Damage();
            }
        }
    }
}
