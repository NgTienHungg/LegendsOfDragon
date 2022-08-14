using UnityEngine;

public class Dragon : MonoBehaviour
{
    [HideInInspector] public int level;
    [HideInInspector] public Vector2Int coordinates;
    [HideInInspector] public Vector3 startPosition;

    // highlight
    private float distance;
    private float speedMoving;
    private bool isHighlight;
    private bool isMovingUp, isMovingDown;

    // merge
    private float speedMerging;
    private bool needMoveToMerge;
    private Vector3 targetPos;
    private Vector3 direction;

    // animation
    private Animator animator;
    private float cooldownJump, cooldownFly, cooldownBreak;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        speedMoving = Controller.Instance.highlightSpeed;
        distance = Controller.Instance.distanceHighlight;

        if (level != 0)
            cooldownJump = Random.Range(1f, 4f);
    }

    private void Update()
    {
        if (isHighlight && isMovingUp)
            MoveUp();

        if (isMovingDown)
            MoveDown();

        if (needMoveToMerge)
            MoveMerge();

        if (!isMovingUp && !isMovingDown && !needMoveToMerge)
        {
            if (level == Mathf.Clamp(level, 1, 4))
                CheckJump();
            else if (level == Mathf.Clamp(level, 5, 6))
                CheckFly();
            else if (level == 7)
                CheckBreak();
        }
    }

    public void LevelUp()
    {
        level = Mathf.Min(level + 1, 13);
        SetUpAnim();
        ShowEffect();
    }

    public void SetUp(int level, int row, int col)
    {
        this.level = level;
        coordinates = new Vector2Int(row, col);
        SetUpAnim(); // call when DragonMatrix.Awake, Dragon.LevelUp
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
        startPosition = position;
    }

    private void MoveUp()
    {
        float _speed = Mathf.Max(speedMoving * (((startPosition.y + distance) - transform.localPosition.y) / distance), 0.1f); // make decending speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.localPosition.y >= startPosition.y + distance)
        {
            transform.localPosition = startPosition + new Vector3(0f, distance);
            isMovingUp = false;
        }
    }

    private void MoveDown()
    {
        float _speed = Mathf.Max(speedMoving * ((transform.localPosition.y - startPosition.y) / distance), 0.1f); // make decending speed
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
        isMovingDown = false;
        isMovingUp = true;
    }

    public void UnHighlight()
    {
        isHighlight = false;
        isMovingUp = false;
        isMovingDown = true;
    }

    public void MoveToMerge(Vector3 targetPos, float speedMerging)
    {
        needMoveToMerge = true;
        this.targetPos = targetPos;
        this.speedMerging = speedMerging;
        animator.SetTrigger("Idle");

        if (transform.position.x > targetPos.x)
            direction = Vector3.left;
        else if (transform.position.x < targetPos.x)
            direction = Vector3.right;
        else if (transform.position.y > targetPos.y)
            direction = Vector3.down;
        else if (transform.position.y < targetPos.y)
            direction = Vector3.up;
    }

    private void MoveMerge()
    {
        transform.Translate(direction * speedMerging * Time.deltaTime);
        if ((direction == Vector3.left && transform.position.x < targetPos.x)
            || (direction == Vector3.right && transform.position.x > targetPos.x)
            || (direction == Vector3.down && transform.position.y < targetPos.y)
            || (direction == Vector3.up && transform.position.y > targetPos.y))
        {
            transform.localPosition = startPosition + new Vector3(0f, distance); // dang highlight
            animator.SetBool("Chilling", true);
            gameObject.SetActive(false);
            needMoveToMerge = false;
        }
    }

    public bool IsReady()
    {
        return !isMovingUp && transform.localPosition == startPosition;
    }

    public bool IsOnTop()
    {
        return !isMovingDown && transform.localPosition == startPosition + new Vector3(0f, distance);
    }

    /*----------------------------------------------------------------------------------------------------------------------------------*/

    public void SetUpAnim()
    {
        animator.SetInteger("Level", level);
        animator.SetBool("Chilling", false);

        if (level == Mathf.Clamp(level, 1, 4))
            cooldownJump = Random.Range(5f, 8f);
        else if (level == Mathf.Clamp(level, 5, 6))
            cooldownFly = Random.Range(6f, 8f);
        else if (level == 7)
            cooldownBreak = Random.Range(3f, 5f);
    }

    private void ShowEffect()
    {
        if (level == Mathf.Clamp(level, 1, 4))
        {
            EffectHolder.Instance.Play(EffectType.Smoke, transform.position, transform);
            animator.SetTrigger("Jump");
        }
        else if (level == Mathf.Clamp(level, 5, 6))
        {
            EffectHolder.Instance.Play(EffectType.Blink, transform.position + new Vector3(0f, 0.4f), transform);
            AudioManager.Instance.PlaySound("Blink");
            animator.SetTrigger("Fly");
        }
        else if (level == Mathf.Clamp(level, 7, 8))
        {
            EffectHolder.Instance.Play(EffectType.Smoke, transform.position, transform);
            AudioManager.Instance.PlaySound("EggBreak");
        }
        else if (level == Mathf.Clamp(level, 9, 11))
        {
            EffectHolder.Instance.Play(EffectType.Blink, transform.position, transform);
            AudioManager.Instance.PlaySound("Whistle");
        }
        else if (level == Mathf.Clamp(level, 12, 13))
        {
            EffectHolder.Instance.Play(EffectType.Blink, transform.position, transform);
            AudioManager.Instance.PlaySound("SuperWhistle");
        }
    }

    private void CheckJump()
    {
        cooldownJump -= Time.deltaTime;
        if (cooldownJump <= 0f)
        {
            cooldownJump = Random.Range(4f, 8f);
            animator.SetTrigger("Jump");
        }
    }

    private void CheckFly()
    {
        cooldownFly -= Time.deltaTime;
        if (cooldownFly <= 0f)
        {
            cooldownFly = Random.Range(4f, 6f);
            animator.SetTrigger("Fly");
        }
    }

    private void CheckBreak()
    {
        cooldownBreak -= Time.deltaTime;
        if (cooldownBreak <= 0f)
        {
            cooldownBreak = Random.Range(3f, 5f);
            animator.SetTrigger("Break");
        }
    }
}