/*
 * ****************************************************************************** *
 * Created by Bobby Lapadula                                                      *
 * Last Modified by Bobby Lapadula                                                *
 * Date and Time: 2/21/2024 12:55                                                 *
 *                                                                                *
 * This is the camera controller script. It controls the position of the camera.  *
 * ****************************************************************************** *
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
