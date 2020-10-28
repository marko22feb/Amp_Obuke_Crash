using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CritterController : PawnController
{
    AICharacterControl aic;
    public GameObject ChickenHut;
    public Vector3 DesiredLocation;
    public bool IsGettingRandomLocation = false;

    public void Awake()
    {
        aic = GetComponent<AICharacterControl>();
    }

    private void Start()
    {
        StartCoroutine(RandomIdleMove());
    }

    public void GetRandomMapLocationInRange()
    {
        DesiredLocation = ChickenHut.transform.position;
        SphereCollider SC = ChickenHut.GetComponent<SphereCollider>();
        DesiredLocation += new Vector3(Random.Range(0f, SC.radius), 0f, Random.Range(0f, SC.radius));
    }

    public void GetFleeLocation()
    {
      DesiredLocation = GameController.control.Player.transform.position;
      transform.LookAt(GameController.control.Player.transform);

      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);
      
      DesiredLocation += transform.forward * 10f;
    }

    IEnumerator RandomIdleMove()
    {
        IsGettingRandomLocation = true;
        yield return new WaitForSeconds(5f);
        GetRandomMapLocationInRange();
        aic.DesiredLocation = DesiredLocation;
        IsGettingRandomLocation = false;
        StartCoroutine(RandomIdleMove());
    }

    public override void OnMessageReceived(PlayerController.MessageType type)
    {
        if (Vector3.Distance(GameController.control.Player.transform.position, transform.position) < 5f)
        {
            StopAllCoroutines();
            IsGettingRandomLocation = false;
            GetFleeLocation();
            aic.DesiredLocation = DesiredLocation;
        } else {
            Debug.Log("OutOfRange");
            if (!IsGettingRandomLocation) StartCoroutine(RandomIdleMove());
        }

    }

    public void DestroySelf()
    {
        ChickenHut.GetComponent<ChickenSpawner>().Despawn(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce((transform.position - other.transform.position).normalized * 400, ForceMode.Force);
        }
    }
}
