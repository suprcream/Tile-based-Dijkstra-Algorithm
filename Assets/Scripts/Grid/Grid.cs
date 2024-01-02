using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject gridCellPrefab; // Prefab dari objek grid yang ingin kamu buat
    public int totalCellsX = 5; // Jumlah sel grid pada sumbu X
    public int totalCellsZ = 5; // Jumlah sel grid pada sumbu Z
    private Vector3 planeSize; // Ukuran plane
    public float heightOffset = 0.5f; // Ketinggian offset dari permukaan plane

    void Start() {
        GenerateGridOnPlane();
    }

    void GenerateGridOnPlane() {
        if (gridCellPrefab == null) {
            Debug.LogError("Grid cell prefab belum di-assign.");
            return;
        }

        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null) {
            Debug.LogError("Tidak dapat menemukan Mesh Collider pada objek ini.");
            return;
        }

        planeSize = meshCollider.bounds.size;

        // Hitung ukuran cell agar grid menutupi seluruh bagian plane
        float cellSizeX = planeSize.x / totalCellsX;
        float cellSizeZ = planeSize.z / totalCellsZ;

        // Buat empty GameObject untuk menjadi parent dari semua grid yang di-generate
        GameObject gridParent = new GameObject("GridParent");

        for (int x = 0; x < totalCellsX; x++) {
            for (int z = 0; z < totalCellsZ; z++) {
                // Hitung posisi grid berdasarkan jarak antar grid
                Vector3 spawnPosition = new Vector3(-planeSize.x / 2 + cellSizeX * x + cellSizeX / 2, -heightOffset, -planeSize.z / 2 + cellSizeZ * z + cellSizeZ / 2);

                // Buat instance dari prefab gridCellPrefab pada posisi yang dihitung
                GameObject newGridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
                newGridCell.transform.localScale = new Vector3(cellSizeX, 1f, cellSizeZ); // Set ukuran grid
                newGridCell.transform.parent = gridParent.transform; // Set parent dari grid agar berada di dalam objek plane
            }
        }

        // Menonaktifkan GameObject plane setelah grid selesai di-generate
        gameObject.SetActive(false); // Nonaktifkan plane
    }
}
