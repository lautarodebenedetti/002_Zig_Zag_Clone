using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPartPrefab;
    public float offset = 0.71132f; //distance between the starts of parts of roads
    public Vector3 lastPosOfRoadPart;

    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 1f, 0.4f);
    }

    public void CreateNewRoadPart()
    {
        //First we calculate the position of the new road part.
        Vector3 spawnPos = Vector3.zero;

        float chance = Random.Range(0, 100);
        if(chance < 50)
        {
            spawnPos = new Vector3(lastPosOfRoadPart.x + offset, lastPosOfRoadPart.y, lastPosOfRoadPart.z + offset);
        }
        else
        {
            spawnPos = new Vector3(lastPosOfRoadPart.x - offset, lastPosOfRoadPart.y, lastPosOfRoadPart.z + offset);
        }
        //Now we create the instantiate of the object in the next position.
        GameObject g = Instantiate(roadPartPrefab, spawnPos, Quaternion.Euler(0, 45, 0));
        lastPosOfRoadPart = g.transform.position;
        //Finally activate some crystals in the road.
        float chanceOfCrystal = Random.Range(0, 10);
        if (chanceOfCrystal % 5 == 0)
        {
            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
