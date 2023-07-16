using UnityEngine;

/// <summary>
/// The EnemyControllerBattle class handles the behavior of the enemy during battles.
/// It controls the enemy's ability to shoot catching balls and manages the timing for shooting.
/// </summary>
public class EnemyControllerBattle : MonoBehaviour {

    /// <summary>
    /// The static instance of the EnemyControllerBattle, ensuring only one instance exists in the game.
    /// </summary>
    public static EnemyControllerBattle instance;

    public bool canShoot;

    [SerializeField] private GameObject catchingBall;
    [SerializeField] private float timer = 3f;

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

    /// <summary>
    /// Attempts to shoot a catching ball at the player.
    /// If the enemy has thrown enough balls, triggers the level state change for the loading scene.
    /// </summary>
    private void tryToCatch() {
        if (throwBallCounter > 5) {
            LevelManager.s_instance.ChangeLevelState(LevelState.LoadingScene);
            TransitionManager.instance.StartTransition();
        }
        float catchingBallSpeed = 10f * Time.deltaTime;
        GameObject ball = Instantiate(catchingBall, transform.position, Quaternion.identity);
    }
}