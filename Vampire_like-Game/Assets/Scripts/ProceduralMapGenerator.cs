using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
public class ProceduralMapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] private TileBase[] tiles;
    [SerializeField] int chunkWidth = 10;
    [SerializeField] int chunkHeight = 10;
    [SerializeField] float scale;
    [SerializeField] Transform player;
    [SerializeField] int loadRadius;
    [SerializeField] float tileThreshold;  
    private HashSet<Vector2Int> generatedChunks = new HashSet<Vector2Int>(); 

    void Start() {
        StartCoroutine(GenerateChunksAroundPlayer());
    }

    IEnumerator GenerateChunksAroundPlayer() {
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
                    if (!generatedChunks.Contains(chunkCoord)) // Para confirmar que não existe. Nunca mexi com HashSet então é na base da fé.
                    {
                        GenerateChunk(chunkCoord.x, chunkCoord.y);
                        generatedChunks.Add(chunkCoord);
                        yield return null; // Espera um frame antes de continuar
                    }
                }
            }
            yield return new WaitForSeconds(0.5f); // Espera ,5seg pra não forçar a carga.
        }
    }

    void GenerateChunk(int chunkX, int chunkY) {
        float offsetX = chunkX * 100f;
        float offsetY = chunkY * 100f;

        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                float xCoord = ((float)x / chunkWidth * scale) + (chunkX * scale) + offsetX;
                float yCoord = ((float)y / chunkHeight * scale) + (chunkY * scale) + offsetY;
                float sample = Mathf.PerlinNoise(xCoord, yCoord); // Perlin Noise, aqui que a mágica acontece
                if (sample > tileThreshold)
                {
                TileBase tileToPlace = GetTileByPerlinValue(sample); // Só para selecionar o item
                tilemap.SetTile(new Vector3Int(x + chunkX * chunkWidth, y + chunkY * chunkHeight, 0), tileToPlace); 
                }
            }
        }
    }

    TileBase GetTileByPerlinValue(float value) { 
           float remappedValue = Mathf.InverseLerp(tileThreshold, 1f, value);

    // Usa o valor remapeado para calcular o índice do tile
    int index = Mathf.FloorToInt(remappedValue * tiles.Length); // Só para poder ter mais de um tile
        index = Mathf.Clamp(index,0, tiles.Length - 1);
        return tiles[index];
    }

}
