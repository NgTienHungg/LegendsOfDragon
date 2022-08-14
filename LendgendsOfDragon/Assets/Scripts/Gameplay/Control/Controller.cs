using System.Collections;
using UnityEngine;

public class Controller : Singleton<Controller>
{
    [SerializeField] private UIGamePlay uiGamePlay;
    [SerializeField] private TileMatrix mtTiles;
    [SerializeField] private DragonMatrix mtDragons;

    [HideInInspector] public Vector2Int firstPick, secondPick;
    [HideInInspector] public bool isPlaying, isSelecting, isMerging, isRefreshing, isGameOver;
    [HideInInspector] public int score, scoreAdd, highestLevel;
    [HideInInspector] public float timeRemaining;

    public float distanceHighlight;
    public float highlightSpeed;
    public float mergeSpeed;
    public float timeOneTurn;

    private BFSTool BFS;
    private int[,] mt_point = new int[5, 5]; // tinh diem
    private bool[,] mt_2light = new bool[5, 5]; // dang highlight

    private new void Awake()
    {
        base.Awake();
        BFS = GetComponent<BFSTool>();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                mtDragons.At(i, j).SetPosition(mtTiles.At(i, j).transform.position + new Vector3(0.8f, 1f));
                mt_point[i, j] = mtDragons.At(i, j).level;
                highestLevel = Mathf.Max(highestLevel, mt_point[i, j] + 1);
                mt_2light[i, j] = false;
            }
        }

        firstPick = new Vector2Int(-1, -1);
        secondPick = new Vector2Int(-1, -1);
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                mtDragons.At(i, j).SetPosition(mtTiles.At(i, j).transform.position + new Vector3(0.8f, 1f));
                mt_point[i, j] = mtDragons.At(i, j).level;
                highestLevel = Mathf.Max(highestLevel, mt_point[i, j] + 1);
                mt_2light[i, j] = false;
            }
        }

        // make all dragon falling, then start playing game
        AudioManager.Instance.PlaySound("Notification");
        StartCoroutine(mtDragons.MakeIntro());
        timeRemaining = timeOneTurn;
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        TimeHandling();

        if (isGameOver)
        {
            StartCoroutine(GameOver());
            isPlaying = false;
            isGameOver = false;
            return;
        }

        if (isSelecting)
        {
            // moi click lan dau
            if (firstPick.x != -1 && firstPick.y != -1)
            {
                AudioManager.Instance.PlaySound("Select");

                if (CanMatch())
                    Highlight();
                else
                    isSelecting = false; // pick vao tile khong the match voi tile nao xung quanh

                firstPick = new Vector2Int(-1, -1); // update only once
            }
            else if (secondPick.x != -1 && secondPick.y != -1)
            {
                if (CanMerge())
                    MergeDragons();
                else
                    UnHighlight();

                isSelecting = false;
                secondPick = new Vector2Int(-1, -1);
            }
        }

        // vua moi merge xong
        if (isMerging && isRefreshing)
        {
            isMerging = false;
            StartCoroutine(Refresh());
        }
    }

    #region Time handling
    private void TimeHandling()
    {
        if (isMerging || isRefreshing)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0f;
            isGameOver = true;
        }
    }
    #endregion

    #region Highlight
    private bool CanMatch()
    {
        int r = firstPick.x;
        int c = firstPick.y;
        int level = mt_point[r, c];

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (Mathf.Abs(i) == Mathf.Abs(j)) continue;
                int _r = r + i; int _c = c + j;
                if (_r >= 0 && _r < 5 && _c >= 0 && _c < 5 && mt_point[_r, _c] == level)
                    return true;
            }
        }
        return false;
    }

    private void Highlight()
    {
        mt_2light = BFS.FlagSameValue(mt_point, firstPick.x, firstPick.y); // using bfs to flag tiles is highlight

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (mt_2light[i, j])
                {
                    mtTiles.At(i, j).Highlight();
                    mtDragons.At(i, j).Highlight();
                }
            }
        }
    }
    #endregion

    #region Merge
    private bool CanMerge()
    {
        return mt_2light[secondPick.x, secondPick.y];
    }

    private void MergeDragons()
    {
        AudioManager.Instance.PlaySound("AddScore");
        isMerging = true;

        int[,] mtPath = BFS.CalculatePath(mt_2light, secondPick.x, secondPick.y);
        Vector2Int[,] mtTrace = BFS.GetTrace(); // xu ly dong thoi cung voi Path
        CalculateScoreAdd();
        StartCoroutine(mtDragons.MergeDragons(mtPath, mtTrace));
    }

    private void CalculateScoreAdd()
    {
        int baseScore = mtDragons.At(secondPick.x, secondPick.y).level + 1;
        int count = 0;
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (mt_2light[i, j])
                    count++;
        scoreAdd = count * baseScore;
    }
    #endregion

    #region Refresh and Update score
    private void UnHighlight()
    {
        AudioManager.Instance.PlaySound("Select");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (mt_2light[i, j])
                {
                    mtTiles.At(i, j).UnHighlight();
                    mtDragons.At(i, j).UnHighlight();
                }
            }
        }
    }

    private IEnumerator Refresh()
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (mt_2light[i, j])
                    mtTiles.At(i, j).UnHighlight();

        mtDragons.Refresh();
        UpdateScoreAndLevel();

        // wait all dragon go to correct position
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (!mtDragons.At(i, j).IsReady())
                    yield return null;

        isRefreshing = false;

        CheckGameOver();
    }

    private void UpdateScoreAndLevel()
    {
        score += scoreAdd;

        // dragon da o dung coordinates cua no, nen co the tinh level duoc roi
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
            {
                mt_point[i, j] = mtDragons.At(i, j).level;
                highestLevel = Mathf.Max(highestLevel, mt_point[i, j] + 1);
            }

        if (score >= PlayerPrefs.GetInt("BestScore"))
            PlayerPrefs.SetInt("BestScore", score);

        if (highestLevel >= PlayerPrefs.GetInt("HighestLevel"))
            PlayerPrefs.SetInt("HighestLevel", highestLevel);
    }

    private void CheckGameOver()
    {
        isGameOver = true;

        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                if (mt_point[i, j] == mt_point[i + 1, j] || mt_point[i, j] == mt_point[i, j + 1])
                {
                    isGameOver = false;
                    timeRemaining = timeOneTurn;
                    return;
                }
    }
    #endregion

    private IEnumerator GameOver()
    {
        AudioManager.Instance.PlayMusic("GameOver");
        if (timeRemaining == 0f)
            uiGamePlay.notification.Notify("Time out !!!");
        else
            uiGamePlay.notification.Notify("Can't move anymore !!!");

        yield return new WaitForSecondsRealtime(1.6f);
        FindObjectOfType<UIPlayController>().OnGameOver();
    }
}