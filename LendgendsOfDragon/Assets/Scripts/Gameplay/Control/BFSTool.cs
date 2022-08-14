using System.Collections.Generic;
using UnityEngine;

public class BFSTool : MonoBehaviour
{
    private int[] dR = { -1, 0, 1, 0 };
    private int[] dC = { 0, 1, 0, -1 };

    private int[,] arr = new int[5, 5];
    private bool[,] flag = new bool[5, 5];
    private Vector2Int[,] trace = new Vector2Int[5, 5];

    public bool[,] FlagSameValue(int[,] a, int r, int c)
    {
        // tra ve 1 ma tran bool, danh dau cac tile duoc highlight - dung de highlight cac tile //
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                arr[i, j] = a[i, j];
                flag[i, j] = false;
            }
        }

        int value = arr[r, c];
        flag[r, c] = true;
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        q.Enqueue(new Vector2Int(r, c));

        while (q.Count != 0)
        {
            Vector2Int u = q.Dequeue();
            for (int k = 0; k < 4; k++)
            {
                int _r = u.x + dR[k];
                int _c = u.y + dC[k];

                if (_r >= 0 && _r < 5 && _c >= 0 && _c < 5 && arr[_r, _c] == value && !flag[_r, _c])
                {
                    flag[_r, _c] = true;
                    q.Enqueue(new Vector2Int(_r, _c));
                }
            }
        }
        return flag;
    }

    public int[,] CalculatePath(bool[,] dd, int r, int c)
    {
        // tra ve 1 ma tran the hien thu tu loang ra tu dinh duoc chon (secondPick) - dung de merge dragons //
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                arr[i, j] = 0;
                trace[i, j] = new Vector2Int(-1, -1);
                flag[i, j] = dd[i, j];
            }
        }

        arr[r, c] = 1;
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        q.Enqueue(new Vector2Int(r, c));

        while (q.Count != 0)
        {
            Vector2Int u = q.Dequeue();
            for (int k = 0; k < 4; k++)
            {
                int _r = u.x + dR[k];
                int _c = u.y + dC[k];

                if (_r >= 0 && _r < 5 && _c >= 0 && _c < 5 && flag[_r, _c] && arr[_r, _c] == 0)
                {
                    arr[_r, _c] = arr[u.x, u.y] + 1;
                    trace[_r, _c] = new Vector2Int(u.x, u.y);
                    q.Enqueue(new Vector2Int(_r, _c));
                }
            }
        }
        return arr;
    }

    public Vector2Int[,] GetTrace()
    {
        return trace;
    }
}