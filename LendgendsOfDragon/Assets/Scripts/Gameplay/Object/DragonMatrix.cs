using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class DragonMatrix : SerializedMonoBehaviour
{
    [SerializeField] private GameObject dragonPrefab;

    [TableMatrix(SquareCells = true)]
    [ShowInInspector] private Dragon[,] dragons = new Dragon[5, 5];

    private Vector3[,] startPos = new Vector3[5, 5];

    private float mergeSpeed;
    private float timeMoving; // thoi gian di chuyen 1 step
    private float height; // khoang cach giua 2 dragon theo truc y

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; ++j)
            {
                // instantiate and set level for dragon
                int level = Random.Range(0, 3);
                dragons[i, j] = Instantiate(dragonPrefab, transform).GetComponent<Dragon>();
                dragons[i, j].SetUp(level, i, j);
            }
        }
    }

    private void Start()
    {
        // after Control.Awake
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                startPos[i, j] = dragons[i, j].transform.localPosition;

        mergeSpeed = Controller.Instance.mergeSpeed;
        height = startPos[0, 0].y - startPos[1, 0].y;
    }

    public IEnumerator MakeIntro()
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
            {
                dragons[i, j].transform.localPosition = startPos[i, j] + new Vector3(0f, 3f);
                LeanTween.moveLocal(dragons[i, j].gameObject, startPos[i, j], 0.4f).setEase(LeanTweenType.easeOutBack);
            }

        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (!dragons[i, j].IsReady())
                    yield return null;

        Controller.Instance.isPlaying = true;
    }

    public Dragon At(int i, int j)
    {
        return dragons[i, j];
    }

    public IEnumerator MergeDragons(int[,] path, Vector2Int[,] trace)
    {
        //wait all dragons move down completely
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (path[i, j] != 0 && !dragons[i, j].IsOnTop())
                    yield return new WaitForSeconds(0.05f);

        int length = 0; // quang duong dai nhat
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                length = Mathf.Max(length, path[i, j]);

        timeMoving = mergeSpeed / Mathf.Max(length - 1, 4); // tranh TH length = 1, 2, 3 merge cham qua

        int[] dR = { -1, 0, 1, 0 };
        int[] dC = { 0, 1, 0, -1 };

        while (length > 1)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // tim cac dragon xa nhat
                    if (path[i, j] == length)
                    {
                        // tim 1 dragon xung quanh no de merge vao
                        for (int k = 0; k < 4; k++)
                        {
                            int r = i + dR[k];
                            int c = j + dC[k];
                            if (r >= 0 & r < 5 && c >= 0 && c < 5 && path[r, c] == length - 1 && trace[i, j].x == r && trace[i, j].y == c)
                            {
                                MoveNextStep(i, j, r, c);
                                break;
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(timeMoving); // wait dragon moving
            length--;
        }

        /* --- */
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (path[i, j] == 1)
                    dragons[i, j].GetComponent<Animator>().SetBool("Chilling", true);
        yield return new WaitForEndOfFrame();
        /* --- */

        // upgrade new dragon
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (path[i, j] == 1)
                    dragons[i, j].LevelUp();

        Controller.Instance.isRefreshing = true;
    }

    private void MoveNextStep(int curRow, int curCol, int tarRow, int tarCol)
    {
        float dx = dragons[curRow, curCol].transform.position.x - dragons[tarRow, tarCol].transform.position.x;
        float dy = dragons[curRow, curCol].transform.position.y - dragons[tarRow, tarCol].transform.position.y;
        float S = Mathf.Abs(dx) + Mathf.Abs(dy);
        float v = S / timeMoving;
        dragons[curRow, curCol].MoveToMerge(dragons[tarRow, tarCol].transform.position, v);
    }

    public void Refresh()
    {
        for (int i = 4; i >= 0; i--)
        {
            for (int j = 0; j < 5; j++)
            {
                if (!dragons[i, j].gameObject.activeInHierarchy)
                {
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (dragons[k, j].gameObject.activeInHierarchy)
                        {
                            SwapDragon(ref dragons[i, j], ref dragons[k, j]);
                            break;
                        }
                    }
                }
            }
        }

        // set lai pos len dau cot j, de cho roi xuong
        for (int j = 0; j < 5; j++)
            for (int i = 4, k = 0; i >= 0; i--)
                if (!dragons[i, j].gameObject.activeInHierarchy)
                {
                    dragons[i, j].gameObject.SetActive(true);
                    dragons[i, j].transform.localPosition = startPos[0, j] + new Vector3(0f, (++k) * height);
                    dragons[i, j].SetUp(Random.Range(Mathf.Max(0, Controller.Instance.highestLevel - 10), Mathf.Max(3, Controller.Instance.highestLevel - 4)), i, j);
                }

        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                dragons[i, j].UnHighlight();
    }

    private void SwapDragon(ref Dragon a, ref Dragon b)
    {
        Dragon _c = a;
        a = b;
        b = _c;

        Vector2Int _pos = a.coordinates;
        a.coordinates = b.coordinates;
        b.coordinates = _pos;

        dragons[a.coordinates.x, a.coordinates.y].startPosition = startPos[a.coordinates.x, a.coordinates.y];
        dragons[b.coordinates.x, b.coordinates.y].startPosition = startPos[b.coordinates.x, b.coordinates.y];
    }
}