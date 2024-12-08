using System.Collections.Generic;
using UnityEngine;

public class BubbleSortCubes : MonoBehaviour
{
    public GameObject[] cubes; // Массив кубов

    void Start()
    {
        // Сортируем кубы по размеру
        BubbleSort(cubes);
        
        // Определяем самый большой куб
        GameObject biggestCube = FindBiggestCube(cubes);
        
        // Выстраиваем кубы относительно самого большого
        PlaceCubesRelativeToBiggest(biggestCube.transform.position);
    }

    private void BubbleSort(GameObject[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j].transform.localScale.magnitude > array[j + 1].transform.localScale.magnitude)
                {
                    // Меняем местами объекты
                    GameObject temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    private GameObject FindBiggestCube(GameObject[] cubes)
    {
        GameObject biggestCube = cubes[0];
        foreach (GameObject cube in cubes)
        {
            if (cube.transform.localScale.magnitude > biggestCube.transform.localScale.magnitude)
            {
                biggestCube = cube;
            }
        }
        return biggestCube;
    }

    private void PlaceCubesRelativeToBiggest(Vector3 referencePosition)
    {
        float offset = 2f; // Смещение между кубами

        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].transform.position = referencePosition + Vector3.right * (i * offset);
        }
    }
}