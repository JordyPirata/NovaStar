using System.Collections;
using UnityEngine;
using Unity.Mathematics;
using System.Threading.Tasks;

//TODO: Change name to ChunkFacade and implement 
public class ChunkManager : MonoBehaviour
{
    private static ChunkManager instance;
    public static ChunkManager Instance { get { return instance; } private set { instance = value; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public const int octaves = 8;
    public const float persistance = Mathf.PI / 2;
    public const float lacunarity = .5f;
    public static int width = 257;
    public static int depth = 257;
    public static int length = width *depth;
    public static int height = 50;
    public const float offset = 0.01f;
    public static int seed = 0;
    public Transform viewer;
    public const float maxViewDst = 450;
    public static Vector2 viewerPosition;
    public float2 viewerCoordinate;

    public async void Start()
    {
        StartCoroutine(WeldChunks());
        await UpdateChunks();
    }
    // Update the chunks with the viewer position
    public async Task UpdateChunks()
    {
        while (true)
        {   // get the viewer position and coordinate
            viewerPosition = new float2(viewer.position.x, viewer.position.z);
            viewerCoordinate = new float2(Mathf.RoundToInt(viewerPosition.x / width), Mathf.RoundToInt(viewerPosition.y / depth));
            // update the visible chunks
            await ChunkVisibility.UpdateVisibleChunks(viewerCoordinate);
            // delay the update of the chunks by system time
            await Task.Delay(2000);
        }
    }
    
    // Weld the chunks together
    public IEnumerator WeldChunks()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            Weld.SetNeighborsAll();
        }
    }
}
