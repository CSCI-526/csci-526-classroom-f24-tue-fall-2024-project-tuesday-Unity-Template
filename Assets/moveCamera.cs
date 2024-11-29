using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public int maxX;
    public int maxY;

    public int offsetY = 0;
    public int offsetX = 0;

    // Update is called once per frame
    void Update()
    {
        // move the camera in a boxed region from the center of the screen
        float camX = math.abs(player.transform.position.x) / (player.transform.position.x) * math.min(maxX, math.abs(player.transform.position.x)) + offsetX;
        float camY = math.abs(player.transform.position.y) / (player.transform.position.y) * math.min(maxY, math.abs(player.transform.position.y)) + offsetY;
        transform.position = new Vector3(camX, camY, transform.position.z);
    }
}
