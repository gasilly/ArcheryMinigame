using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Board_Manager : MonoBehaviour  //Handles the spawn points for the targets and current game conditions such as time
{
    public static float stationaryDespawnTime;
    public static float movingDespawnTime;
    public static float bonusDespawnTime;
    public float spawnTime; //Time it takes a targets to start spawning
    public float spawnDelay; //Delay between each spawn
    private int randBonusSpawnChance; //percent chance of a bonus target spawning
    [SerializeField] private GameObject stationaryPrefab, bonusPrefab,movingPrefab;
    [SerializeField] private float stationaryDespawnUI, movingDespawnUI, bonusDespawnUI;
    private List <GameObject> PrefabList = new List<GameObject>();
    private GameObject[] finishObjects;
    private Transform[] spawnPointArray;
    private bool stopSpawn = false;
    private int randSpawn;

    void Start()
    {
        stationaryDespawnTime = stationaryDespawnUI; //set the statics for the despawn rates
        movingDespawnTime = movingDespawnUI;
        bonusDespawnTime = bonusDespawnUI;
        finishObjects = GameObject.FindGameObjectsWithTag("Show On Finish"); //Get the gameover UI
        hideOnFinished();
        spawnPointArray = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) //Get all the spawn points for the gameboard
        {
            spawnPointArray[i] = transform.GetChild(i);
        }
        InvokeRepeating("Spawn", spawnTime, spawnDelay);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
        if (Timer.timeStart <= 0 && stopSpawn == false) //The timer has hit 0 and the game over function has not been called yet
        {
            GameOver(); //Check for a game over
        }

    }

    void Spawn() //Spawn a random target type at one of the gameboards spawn locations
    {
        for(int i = PrefabList.Count - 1; i >= 0; i--){
            if(PrefabList[i] == null){
                PrefabList.RemoveAt(i);
            }
        }
        if (stopSpawn)
        {
            CancelInvoke("Spawn");
            return;
        }
        randSpawn = Random.Range(0, transform.childCount);
        randBonusSpawnChance = Random.Range(1, 10);
        foreach(GameObject targetPrefab in PrefabList){
            if(targetPrefab == null){
                continue;
            }
            if(spawnPointArray[randSpawn].transform.position == targetPrefab.transform.position && (targetPrefab.tag == "stationary target" 
                || targetPrefab.tag == "bonus target")){
                return;
            }
            else if(spawnPointArray[randSpawn].transform.position.y == targetPrefab.transform.position.y && targetPrefab.tag == "moving target"){
                return;
            }
        }
        if (spawnPointArray[randSpawn].tag == "stationary spawn") //stationary spawn points
        {
            GameObject stationary = Instantiate(stationaryPrefab, spawnPointArray[randSpawn].position, Quaternion.identity);
            stationary.transform.parent = spawnPointArray[randSpawn].transform; //set the spawn point as the objects parent.
            //stationary.GetComponent<BoxCollider2D>().enabled = false;
            PrefabList.Add(stationary); 
        }
        else if(spawnPointArray[randSpawn].tag == "bonus spawn") //bonus spawn points for extra time
        {

            if (randBonusSpawnChance < 4)
            {
                GameObject bonus = Instantiate(bonusPrefab, spawnPointArray[randSpawn].position, Quaternion.identity);
                bonus.transform.parent = spawnPointArray[randSpawn].transform;
                PrefabList.Add(bonus); 
            }
            else{
                GameObject stationary = Instantiate(stationaryPrefab, spawnPointArray[randSpawn].position, Quaternion.identity);
                stationary.transform.parent = spawnPointArray[randSpawn].transform; //set the spawn point as the objects parent.
                PrefabList.Add(stationary); 
            }
        }
        else if(spawnPointArray[randSpawn].tag == "moving left spawnpoint") //spawn points for targets moving left and right
        {
            GameObject move = Instantiate(movingPrefab, spawnPointArray[randSpawn].position, Quaternion.identity);
            FindObjectOfType<Target_Move>().direction = new Vector2(-1, 0);
            move.transform.parent = spawnPointArray[randSpawn].transform;
            PrefabList.Add(move); 
        }
        else if(spawnPointArray[randSpawn].tag == "moving right spawnpoint")
        {
            GameObject move = Instantiate(movingPrefab, spawnPointArray[randSpawn].position, Quaternion.identity);
            FindObjectOfType<Target_Move>().direction = new Vector2(1, 0);
            move.transform.parent = spawnPointArray[randSpawn].transform;
            PrefabList.Add(move); 
        }
    }

    void GameOver()
    {
                            //If the timer has hit 0 the game is done and the score should be displayed
        stopSpawn = true;
        FindObjectOfType<CharacterControllerScript>().speed = 0;
        showOnFinished();
    }
    //Place these in a UI script
    void hideOnFinished() //Hide the game over menu
    {
        foreach(GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }

    void showOnFinished() //Show the game over menu
    {
        foreach(GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    public void Restart()
    {
        Timer.timeStart = 70;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
