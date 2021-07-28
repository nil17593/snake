using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// movement of snake and body parts
/// game pause, resume, gameover logics
/// trigger events after triggering with other gameobjects snake will grow or reduce its size
/// also increase or decrese the scores
/// </summary>
public class SnakeController : MonoBehaviour
{
    [Header("Referance of score script")]
    [SerializeField] private ScoreController scoreController;

    [Header("list for snake body parts")]
    private List<Transform> segments;

    [Header("Positions")]
    [SerializeField] private Transform segmentPrefab;
    [SerializeField] private int initialSize;
    [SerializeField] private BoxCollider2D wallArea;
    private Vector2 dir = Vector2.right;

    [Header("Score variables")]
    [SerializeField] private int score;
    [SerializeField] private int speed;
    [SerializeField] private int decreasScore;
   
    [Header("Buttons")]
    //[SerializeField] Button buttonRestart;
    [SerializeField] Button buttonMenu;
    [SerializeField] Button buttonResume;

    [Header("Scenes to load")]
    [SerializeField] string scenename;
    [SerializeField] string lobby;

    [Header("Created gameobjects")]
    [SerializeField] GameObject gameOver;

    [Header("Input Keys")]
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode up;

    [Header("Bounds for screen wrapping")]
    private float maxX, maxY, minX, minY;

    private float snakeFaceAngle;

    private void Awake()
    {
        //buttonRestart.onClick.AddListener(ReloadCurrentScene);
        buttonMenu.onClick.AddListener(LobbyScene);
    }
    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
        Bounds bounds = this.wallArea.bounds;
        snakeFaceAngle = -90f;
        maxX = bounds.max.x;
        minX = bounds.min.x;
        maxY = bounds.max.y;
        minY = bounds.min.y;
    }
    private void Update()
    {
        SnakeChangePos();
        ScreenWrap();

       
        //upInput = Input.GetKeyDown(KeyCode.UpArrow);
        //downInput = Input.GetKeyDown(KeyCode.DownArrow);
        //rightInput = Input.GetKeyDown(KeyCode.RightArrow);
        //leftInput = Input.GetKeyDown(KeyCode.LeftArrow);
        //if (moving == true)
        //{
        //    this.transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
        //}
        //if (boosting)
        //{
        //    boostTimer += Time.deltaTime;
        //    if (boostTimer >= 3)
        //    {
        //        speed = 5;
        //        boostTimer = 0;
        //        boosting = false;
        //    }
        //}
    }

    //game pause
    

    //load lobby scene
    private void LobbyScene()
    {
        SceneManager.LoadScene("lobby");
    }
    
    //snake directions handled 
    private void SnakeChangePos()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    direction = Vector2.up;
        //    //this.transform=
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    direction = Vector2.down;
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    direction = Vector2.right;
        //}
        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    direction = Vector2.left;
        //}
        if (Input.GetKey(right) && dir != Vector2.left)
        {
            dir = Vector2.right;
            snakeFaceAngle = -90f;
        }
        else if (Input.GetKey(down) && dir != Vector2.up)
        {
            dir = -Vector2.up;    // '-up' means 'down'
            snakeFaceAngle = 180f;
        }
        else if (Input.GetKey(left) && dir != Vector2.right)
        {
            dir = -Vector2.right; // '-right' means 'left'
            snakeFaceAngle = 90f;
        }
        else if (Input.GetKey(up) && dir != Vector2.down)
        {
            dir = Vector2.up;
            snakeFaceAngle = 0f;
        }
        this.transform.eulerAngles = new Vector3(0, 0, snakeFaceAngle);
    }
    private void FixedUpdate()
    {
        Movement();
    }

    //snake movement 
    private void Movement()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        transform.Translate(dir * speed);
        ScreenWrap();

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + dir.x,
            Mathf.Round(this.transform.position.y) + dir.y,
            0f
            );
    }

    //snake will grow in size
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    //snake size will reduce
    public void ReduceSize()
    {
        Destroy(segments[segments.Count - 1].gameObject);
        segments.RemoveAt(segments.Count - 1);
        if (segments.Count < 1)
        {
            gameOver.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    //screen wrapping 
    void ScreenWrap()
    {
        Vector3 newPosition = transform.position;
        if (newPosition.x >= maxX)
        {
            newPosition.x = -newPosition.x + 1f;
        }
        else if (newPosition.x <= minX)
        {
            newPosition.x = -newPosition.x - 1f;
        }
        if (newPosition.y >= maxY)
        {
            newPosition.y = -newPosition.y + 1f;
        }
        else if (newPosition.y <= minY)
        {
            newPosition.y = -newPosition.y - 1f;
        }
        transform.position = newPosition;
    }

    //snakes reset state
    private void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }

    //all triggered events
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        if (other.tag == "MassBurner")
        {
            ReduceSize();
            scoreController.DecreaseScore(decreasScore);
        }
        else if (other.tag == "Obstacle")
        {
            //ResetState();
            this.gameObject.SetActive(false);
            gameOver.SetActive(true);
        }
    }
}
