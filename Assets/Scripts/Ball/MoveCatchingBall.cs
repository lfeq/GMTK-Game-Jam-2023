using System.Collections;
using UnityEngine;

public class MoveCatchingBall : MonoBehaviour {
    public static MoveCatchingBall instance;

    [SerializeField] private float ballSpeed = 6f;
    private int timesCaught = 0;

    //private Vector2 playerPosition;
    private bool isBallLaunched = false;

    private Vector2[] positionsArray = new Vector2[3];
    private Vector2 randomPlayerPosition;

    private void Awake() {
        instance = this;
        positionsArray[0] = new Vector2(-5f, 2.5f);
        positionsArray[1] = new Vector2(0, 2.5f);
        positionsArray[2] = new Vector2(5f, 2.5f);
    }

    

    private void Start() {
        int randomPos = Random.Range(0, positionsArray.Length);
        randomPlayerPosition = positionsArray[randomPos];
        setTimeCaught(LevelManager.s_instance.getTimesCaught());
        ballSpeed += timesCaught;
        Debug.Log("Times caught: " +  timesCaught);
    }

    private void FixedUpdate() {
        setBallSpeedAndLaunch();

        if (Vector2.Distance(transform.position, randomPlayerPosition) < 0.1f) {
            isBallLaunched = false;
            Destroy(gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        isBallLaunched = false;
        Destroy(gameObject);
    }

    private void setTimeCaught(int t_timeCaught) {
        if (t_timeCaught == 0) {
            return;
        }
        timesCaught = t_timeCaught;
    }

    private void setBallSpeedAndLaunch() {
        //setTimeCaught(LevelManager.s_instance.getTimesCaught());
        StartCoroutine(lastChanceToDodgeBall());
        transform.position = Vector2.MoveTowards(transform.position, randomPlayerPosition, ballSpeed * Time.deltaTime);
        Debug.Log("ball speed: " + ballSpeed);
    }

    public bool getIsBallLaunched() {
        return isBallLaunched;
    }

    public void setIsBallLaunched(bool t_isBallLaunched) {
        isBallLaunched = t_isBallLaunched;
    }

    private IEnumerator lastChanceToDodgeBall() {
        yield return new WaitForSeconds(.4f);
        isBallLaunched = true;
    }
}