using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletImpact;
    public Transform explosion;
    ParticleSystem bulletps;
    ParticleSystem explosionPs;

    public Transform crossHair;
    Vector3 originSize;
    public float maxDistance = 10f;

    bool isFiring = false; // 마우스 버튼이 눌려져 있는지 여부를 저장하는 변수
    RaycastHit hitInfo; // 레이캐스트의 충돌 정보를 저장하는 변수

    void Start()
    {
        originSize = crossHair.localScale * 3.2f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (bulletImpact)
            bulletps = bulletImpact.GetComponent<ParticleSystem>();
        if (explosion)
            explosionPs = explosion.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, maxDistance))
        {
            crossHair.position = hitInfo.point;
            crossHair.forward = -ray.direction;
            crossHair.localScale = originSize * hitInfo.distance;

            if (Input.GetMouseButtonDown(0))
            {
                isFiring = true; // 마우스 버튼이 눌렸음을 표시
            }
        }
        else
        {
            crossHair.position = ray.GetPoint(maxDistance);
            crossHair.forward = -ray.direction;
            crossHair.localScale = originSize * maxDistance;

            if (Input.GetMouseButtonDown(0))
            {
                isFiring = true; // 마우스 버튼이 눌렸음을 표시
            }
        }

        if (isFiring)
        {
            Fire(); // 연속적인 동작 처리
        }
    }

    void Fire()
    {
        // 총알 발사 동작 처리

        if (bulletImpact)
        {
            bulletImpact.up = hitInfo.normal;
            bulletImpact.position = hitInfo.point + hitInfo.normal * -1f;
            bulletps.Stop();
            bulletps.Play();
        }

        if (hitInfo.transform.name.Contains("Drone"))
        {
            if (explosion)
            {
                explosion.position = hitInfo.transform.position;
                explosionPs.Stop();
                explosionPs.Play();
                explosion.GetComponent<AudioSource>().Stop();
                explosion.GetComponent<AudioSource>().Play();
            }
            Destroy(hitInfo.transform.gameObject);
        }
        else
        {
            if (bulletImpact)
            {
                bulletImpact.GetComponent<AudioSource>().Stop();
                bulletImpact.GetComponent<AudioSource>().Play();
            }
        }

        isFiring = false; // 동작 처리 후 마우스 버튼 상태 초기화
    }
}
