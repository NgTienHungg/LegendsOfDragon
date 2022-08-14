using UnityEngine;
using Sirenix.OdinInspector;

public class TileMatrix : SerializedMonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private SpriteRenderer customBound;

    [TableMatrix(SquareCells = true)]
    [ShowInInspector] private Tile[,] tiles = new Tile[5, 5];

    private void Awake()
    {
        Vector3 grassTargetBound = customBound.bounds.size / 5f;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                tiles[i, j] = Instantiate(tilePrefab, transform).GetComponent<Tile>();
                tiles[i, j].SetUp(i, j, grassTargetBound, customBound.transform.position);
            }
        }
    }

    public Tile At(int i, int j)
    {
        return tiles[i, j];
    }
}