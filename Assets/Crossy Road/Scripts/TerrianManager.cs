using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrianManager : MonoBehaviour
{
    // vector3 variable to store 3d position of an object
    private Vector3 currentPosition = new Vector3(0, 0, 0);
    private int randGen = 0;

    float platformSizeZ = 0;
    float terrianSizeZ = 0;

    private const int MAX_TERRIAN = 10;

    [SerializeField] private GameObject grassOne;
    // an List of terrians or terrian generator
    [SerializeField] List<GameObject> groundGenerator = new List<GameObject>();

    private List<GameObject> currentTerrians = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(grassOne, currentPosition, Quaternion.identity);
        platformSizeZ = groundGenerator[randGen].transform.position.z +
            (groundGenerator[randGen].GetComponent<Renderer>().bounds.size.z / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            GenerateTerrian();
        }
    }

    private void GenerateTerrian()
    {
        randGen = Random.Range(0, groundGenerator.Count);
        
        terrianSizeZ = groundGenerator[randGen].GetComponent<Renderer>().bounds.size.z / 2;    
        currentPosition.z = platformSizeZ + terrianSizeZ;

        GameObject terrian = Instantiate(groundGenerator[randGen], currentPosition, Quaternion.identity);

        currentTerrians.Add(terrian);

        if(currentTerrians.Count > MAX_TERRIAN)
        {
            Destroy(currentTerrians[0]);
            currentTerrians.RemoveAt(0);
        }

        platformSizeZ = platformSizeZ + groundGenerator[randGen].transform.position.z +
            groundGenerator[randGen].GetComponent<Renderer>().bounds.size.z;
    }
}
