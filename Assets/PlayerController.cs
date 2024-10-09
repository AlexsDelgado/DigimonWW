using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
  private const string IDLE = "idle";
  private const string WALK = "walk";

  NewControls input;
  private Animator animator;
  private Transform player;

  [Header("Movement")] 
  [SerializeField] private ParticleSystem clickEffect;
  [SerializeField] private LayerMask clickableLayers;
  public float speed;
  private bool isMoving;
  [SerializeField] private Vector3 destination;


  private void Awake()
  {
    player = GetComponent<Transform>();
    animator = GetComponent<Animator>();

    input = new NewControls();
    AssignInputs();
  }

  void AssignInputs()
  {
    input.Main.Movement.performed += ctx => ClickToMove();
  }

  void ClickToMove()
  {
    Debug.Log("clickeo");
    RaycastHit hit;
    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100, clickableLayers))
    {
      isMoving = true;
      destination = hit.point;
      // var step = speed * Time.deltaTime;
      // player.position = Vector3.MoveTowards(transform.position, hit.point, step);
      
      if (clickEffect != null)
      {
        Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
      }
    }
  }
  void Movement()
  {
    if (Vector3.Distance(player.position,destination)<0.001f )
    {
      isMoving = false;
    }
    player.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
  }
  void OnEnable()
  {
    input.Enable();
  }

  void OnDisable()
  {
    input.Disable();
  }

  private void Update()
  {
    if (isMoving)
    {
      Movement();
    }
    
    FaceDestiny();
    SetAnimation();
  }

  void FaceDestiny()
  {
    
  }

  void SetAnimation()
  {
    Vector3 directoin = (destination - player.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directoin.x,0,directoin.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
  }
}
