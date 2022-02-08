using System.Collections;
using System.Collections.Generic;
using topdownGame.Controller;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    private Controller2D m_controller;
    public float Speed;
    
    private void Start() {
        m_controller = GetComponent<Controller2D>();
    }

    
    private void FixedUpdate() {
        if (m_controller.collisionsInfo.HasCollision()) {
            DestroyImmediate(gameObject);
            return;
        }
        
        var velocity = Vector2.left * Speed;
        m_controller.Move(velocity * Time.deltaTime);
    }
}
