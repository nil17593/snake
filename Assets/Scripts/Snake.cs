using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 dir = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    private bool upInput, DownInput, leftInput, rightInput;
    public int intialSize = 4;
    public BoxCollider2D wallArea;
    private float maxX, maxY, minX, minY;
    void Start()
    {
        Vector3 newPosition = transform.position;
        Bounds bounds = this.wallArea.bounds;
        maxX = bounds.max.x;
        minX = bounds.min.x;
        maxY = bounds.max.y;
        minY = bounds.min.y;

        // InvokeRepeating("Move", 0.3f, 0.3f);

        ResetState();
    }

    private void Update()
    {
        // upInput = Input.GetKeyDown(KeyCode.UpArrow);
        // DownInput = Input.GetKeyDown(KeyCode.DownArrow);
        // leftInput = Input.GetKeyDown(KeyCode.LeftArrow);
        // rightInput = Input.GetKeyDown(KeyCode.RightArrow);

        changePos();

        // for (int i = _segments.Count - 1; i > 0; i--)
        // {
        //     _segments[i].position = _segments[i - 1].position * Time.deltaTime;
        // }

        ScreenWrap();
    }

    // void Move()
    // {
    //     this.transform.Translate(dir);
    // }

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

    private void changePos()
    {
        // if (upInput && (dir != Vector2.down))
        // {
        //     dir = Vector2.up;
        // }
        // else if (DownInput && (dir != Vector2.up))
        // {
        //     dir = Vector2.down;
        // }
        // else if (leftInput && (dir != Vector2.right))
        // {
        //     dir = Vector2.left;
        // }
        // else if (rightInput && (dir != Vector2.left))
        // {
        //     dir = Vector2.right;
        // }

        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + dir.x,
                                               Mathf.Round(this.transform.position.y) + dir.y,
                                               0f);
        ScreenWrap();
    }

    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.intialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
