using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 LocationOffset;
    private Transform m_Player; // Reference to the player's transform.
    
    private void Awake()
    {
        // Setting up the reference.
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.rotation = m_Player.rotation;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (m_Player.GetComponent<PlayerController>().inputType != PlayerController.InputType.Game) return;
        Vector3 newPosition = new Vector3(m_Player.transform.position.x + LocationOffset.x, m_Player.transform.position.y + LocationOffset.y, m_Player.transform.position.z + LocationOffset.z);
        transform.position = newPosition;
    }

    private void FixedUpdate()
    {
        if (m_Player.GetComponent<PlayerController>().inputType != PlayerController.InputType.Game) return;
        Vector3 offset = new Vector3(1, 1, 1);
        Vector3 newRotation = new Vector3(transform.eulerAngles.x - Input.GetAxis("Mouse Y") * 25, transform.eulerAngles.y + (Input.GetAxis("Mouse X") * 50), 0);
        transform.rotation = Quaternion.Euler(newRotation);
    }
}