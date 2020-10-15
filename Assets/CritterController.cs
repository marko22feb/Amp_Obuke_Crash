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

        //  Quaternion tempRotation = Quaternion.LookRotation(GameController.control.Player.transform.position, Vector3.up);
        // Debug.Log(tempRotation);
        Quaternion lookAwayRotation = new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, transform.rotation.w);
      transform.rotation = lookAwayRotation;
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
}
