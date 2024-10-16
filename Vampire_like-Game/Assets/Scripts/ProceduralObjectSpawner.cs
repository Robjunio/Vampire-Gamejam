using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralObjectSpawner : MonoBehaviour
{
    [SerializeField] int chunkWidth = 10;
    [SerializeField] int chunkHeight = 10;
    [SerializeField] float scale;
    [SerializeField] Transform player;
    [SerializeField] int loadRadius;

    // Array de prefabs para os objetos (árvores, lanternas, etc.)
    [SerializeField] GameObject[] objectPrefabs; // Array de prefabs
    [SerializeField] float spawnThreshold = 0.8f; // Threshold para spawnar os objetos
    [SerializeField] float minDistanceBetweenObjects = 5f; // Distância mínima entre objetos

    private HashSet<Vector2Int> generatedChunks = new HashSet<Vector2Int>();
    private List<Vector3> spawnedObjectPositions = new List<Vector3>(); // Lista para armazenar as posições dos objetos já spawnados

    void Start()
    {
        StartCoroutine(GenerateChunksAroundPlayer());
    }

    IEnumerator GenerateChunksAroundPlayer()
    {
        while (true)
        {
            Vector2Int playerChunkCoord = new Vector2Int(
                Mathf.FloorToInt(player.position.x / chunkWidth),
                Mathf.FloorToInt(player.position.y / chunkHeight)
            );

            for (int x = -loadRadius; x <= loadRadius; x++)
            {
                for (int y = -loadRadius; y <= loadRadius; y++)
                {
                    Vector2Int chunkCoord = new Vector2Int(playerChunkCoord.x + x, playerChunkCoord.y + y);
                    if (!generatedChunks.Contains(chunkCoord)) // Confirma se o chunk já foi gerado
                    {
                        GenerateObjectsInChunk(chunkCoord.x, chunkCoord.y);
                        generatedChunks.Add(chunkCoord);
                        yield return null; // Espera um frame antes de continuar
                    }
                }
            }
            yield return new WaitForSeconds(0.5f); // Espera 0,5 segundos para não sobrecarregar
        }
    }

    void GenerateObjectsInChunk(int chunkX, int chunkY)
    {
        float offsetX = chunkX * 100f;
        float offsetY = chunkY * 100f;

        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                float xCoord = ((float)x / chunkWidth * scale) + offsetX;
                float yCoord = ((float)y / chunkHeight * scale) + offsetY;
                float sample = Mathf.PerlinNoise(xCoord, yCoord); // Calcula o valor de Perlin Noise

                Vector3 spawnPosition = new Vector3(x + chunkX * chunkWidth, y + chunkY * chunkHeight, 0);

                // Verifica se o valor de Perlin Noise passa do threshold e se a distância é suficiente
                if (sample > spawnThreshold && CanPlaceObject(spawnPosition))
                {
                    // Escolhe aleatoriamente um objeto do pool (árvore ou lanterna)
                    GameObject prefabToSpawn = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
                    Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                    spawnedObjectPositions.Add(spawnPosition); // Armazena a posição do objeto spawnado
                }
            }
        }
    }

    // Função que verifica se podemos spawnar um objeto sem que fique muito perto de outro
    bool CanPlaceObject(Vector3 position)
    {
        foreach (Vector3 objectPos in spawnedObjectPositions)
        {
            if (Vector3.Distance(position, objectPos) < minDistanceBetweenObjects)
            {
                return false; // Se a distância for menor que a mínima, não pode spawnar
            }
        }
        return true; // Se não houver objetos próximos, pode spawnar
    }
}
