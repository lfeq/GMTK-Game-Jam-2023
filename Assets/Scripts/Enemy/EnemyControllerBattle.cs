using Unity.VisualScripting;
using UnityEngine;

public class EnemyControllerBattle : MonoBehaviour
{
    public static EnemyControllerBattle instance;
    [SerializeField] GameObject catchingBall;
    public bool canShoot;
    [SerializeField] float timer = 3f;
    private float setTime;
    private int throwBallCounter = 0;

   
    private void Awake() {
        instance = this;
    }

    private void Start() {
        canShoot = true;
        setTime = timer;
        GetComponent<SpriteRenderer>().sprite = LevelManager.s_instance.getSprite();
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer < 0 && canShoot) {
            throwBallCounter++;
            timer = setTime;
            tryToCatch();
        }
    }

    void tryToCatch() {
        if(throwBallCounter > 5) {
            LevelManager.s_instance.ChangeLevelState(LevelState.LoadingScene);
            TransitionManager.instance.StartTransition();
        }
        float catchingBallSpeed = 10f * Time.deltaTime;
        GameObject ball = Instantiate(catchingBall, transform.position, Quaternion.identity);
    }
}

