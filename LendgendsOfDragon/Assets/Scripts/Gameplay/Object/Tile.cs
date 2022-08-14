using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] SpriteRenderer customBound;
    [SerializeField] private Sprite greenGrass;
    [SerializeField] private Sprite yellowGrass;
    private SpriteRenderer spriteRenderer;

    private float speed;
    private float distance;

    private Vector2Int coordinate;
    private Vector3 startPosition;
    private bool isMovingUp, isMovingDown;
    private bool isHighlight;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        speed = Controller.Instance.highlightSpeed;
        distance = Controller.Instance.distanceHighlight;
    }

    private void Update()
    {
        if (isHighlight && isMovingUp)
            MoveUp();

        if (isMovingDown)
            MoveDown();
    }

    public void SetUp(int row, int col, Vector3 targetBound, Vector3 boardPosition)
    {
        coordinate = new Vector2Int(row, col);

        // set sprite
        spriteRenderer.sortingOrder = row;
        if ((row + col) % 2 == 0)
            spriteRenderer.sprite = yellowGrass;
        else
            spriteRenderer.sprite = greenGrass;

        //set scale
        Vector3 currentBound = customBound.bounds.size;
        transform.localScale = new Vector3(targetBound.x / currentBound.x, targetBound.y / currentBound.y);

        // set position
        transform.position = boardPosition + new Vector3((coordinate.y - 2) * targetBound.x, (2 - coordinate.x) * targetBound.y) - targetBound / 2f;
        startPosition = transform.localPosition;
    }

    private void MoveUp()
    {
        float _speed = Mathf.Max(speed * (((startPosition.y + distance) - transform.localPosition.y) / distance), 0.1f); // make decending speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.localPosition.y >= startPosition.y + distance)
        {
            transform.localPosition = startPosition + new Vector3(0f, distance);
            isMovingUp = false;
        }
    }

    private void MoveDown()
    {
        float _speed = Mathf.Max(speed * ((transform.localPosition.y - startPosition.y) / distance), 0.1f); // make decending speed
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.localPosition.y <= startPosition.y)
        {
            transform.localPosition = startPosition;
            isMovingDown = false;
        }
    }

    public void Highlight()
    {
        isHighlight = true;
        spriteRenderer.color = Color.green;
        isMovingDown = false;
        isMovingUp = true;
    }

    public void UnHighlight()
    {
        isHighlight = false;
        spriteRenderer.color = Color.white;
        isMovingUp = false;
        isMovingDown = true;
    }

    private void OnMouseDown()
    {
        if (!Controller.Instance.isPlaying || Controller.Instance.isMerging || Controller.Instance.isRefreshing)
            return;

        if (!Controller.Instance.isSelecting)
        {
            Controller.Instance.isSelecting = true;
            Controller.Instance.firstPick = coordinate;
            //Debug.Log($"First pick: {Controller.Instance.firstPick.x} {Controller.Instance.firstPick.y}");
        }
        else
        {
            Controller.Instance.secondPick = coordinate;
            //Debug.Log($"Second pick: {Controller.Instance.secondPick.x} {Controller.Instance.secondPick.y}");
        }
    }

    public bool IsReady()
    {
        return !isMovingDown && transform.localPosition == startPosition;
    }

    public bool IsOnTop()
    {
        return !isMovingUp && transform.localPosition == (startPosition + new Vector3(0f, distance));
    }
}