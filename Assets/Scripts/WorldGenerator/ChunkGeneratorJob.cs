// Create factory
using Unity.Jobs;
using Unity.Collections;
using Generator;
using Unity.Mathematics;
using UnityEngine;
using System.IO;
using Repository;
// TODO: test and finish this class or change the Instantiate method to another class
public class ChunkGeneratorJob : MonoBehaviour, IJobParallelFor
{
    string message;
    public NativeArray<int2> chunksCoords;
    public NativeArray<Chunk> chunks;
    public void Execute(int index)
    {
        chunks[index] = ChunkFactory.CreateChunk(chunksCoords[index].x,chunksCoords[index].y);
        SetAttributes(index);
        GenerateTerrain(index);
        SaveChunk(index);
    }
    //set position of the chunks
    public void SetAttributes(int index)
    {
        gameObject.transform.position = chunks[index].position;
        gameObject.name = chunks[index].ChunkName;
    }
    public void GenerateTerrain(int index)
    {
        /*
        terrain = gameObject.AddComponent<Terrain>();
        terrain = TerrainSettings.ApplySettings(terrain, chunks[index]);
        gameObject.AddComponent<TerrainCollider>().terrainData = terrain.terrainData;
        */
    }
    public async void SaveChunk(int index)
    {
        message = await JsonRepository.Instance.CreateAsync(chunks, Path.Combine(Application.persistentDataPath, string.Concat(chunks[index].ChunkName, ".json")));
        Debug.Log(message);
    }
}
