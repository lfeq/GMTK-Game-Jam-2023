using Unity.VisualScripting;
using UnityEngine;

public class EnemyControllerBattle : MonoBehaviour
{
    public static EnemyControllerBattle instance;
    [SerializeField] GameObject catchingBall;
    public bool canShoot;
    [SerializeField] float timer = 3f;
    float setTime;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        setTime = timer;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            tryToCatch();
            timer = setTime;
        }
    }

    void tryToCatch() {
        float catchingBallSpeed = 10f * Time.deltaTime;
        GameObject ball = Instantiate(catchingBall, transform.position, Quaternion.identity);
    }
}

