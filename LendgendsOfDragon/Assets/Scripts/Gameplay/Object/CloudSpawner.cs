using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [Header("Prepare")]
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private Sprite[] cloudSprites;

    [Header("Spawn")]
    [SerializeField] private Vector2 horizontalLimit;
    [SerializeField] private Vector2 speedRange;
    [SerializeField] private Vector2 alphaRange;

    private GameObject[] clouds = new GameObject[5];
    private SpriteRenderer[] sprRenders = new SpriteRenderer[5];
    private float[] speeds = new float[5];

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            clouds[i] = Instantiate(cloudPrefab, transform);
            sprRenders[i] = clouds[i].GetComponent<SpriteRenderer>();
            RespawnCloud(i);
        }

        // random position in screen when start game
        clouds[0].transform.position = new Vector3(Random.Range(2.5f, 3.5f), clouds[0].transform.position.y);
        clouds[1].transform.position = new Vector3(Random.Range(-2.9f, -1.8f), clouds[1].transform.position.y);
        clouds[2].transform.position = new Vector3(Random.Range(0.2f, 1.2f), clouds[2].transform.position.y);
        clouds[3].transform.position = new Vector3(Random.Range(2.9f, 3.8f), clouds[3].transform.position.y);
        clouds[4].transform.position = new Vector3(Random.Range(-3f, -1.8f), clouds[4].transform.position.y);

    }

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            clouds[i].transform.Translate(Vector3.left * speeds[i] * Time.deltaTime);
            if (clouds[i].transform.position.x <= horizontalLimit.x)
                RespawnCloud(i);
        }
    }

    private void RespawnCloud(int i)
    {
        if (i == 0)
            clouds[i].transform.position = new Vector3(horizontalLimit.y, Random.Range(2.2f, 3.8f));
        else if (i == 1)
            clouds[i].transform.position = new Vector3(horizontalLimit.y, Random.Range(1.8f, 0.6f));
        else if (i == 2)
            clouds[i].transform.position = new Vector3(horizontalLimit.y, Random.Range(-0.5f, -1.8f));
        else if (i == 3)
            clouds[i].transform.position = new Vector3(horizontalLimit.y, Random.Range(-3f, -4f));
        else if (i == 4)
            clouds[i].transform.position = new Vector3(horizontalLimit.y, Random.Range(-5.6f, -6.4f));

        sprRenders[i].sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
        sprRenders[i].color = new Color(1f, 1f, 1f, Random.Range(alphaRange.x, alphaRange.y));
        speeds[i] = Random.Range(speedRange.x, speedRange.y);
    }
}