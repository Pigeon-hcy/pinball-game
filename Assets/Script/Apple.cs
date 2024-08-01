using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour,IInteractable
{
    public bool canInteract => throw new System.NotImplementedException();
    public MMF_Player onHitFeedBack;
    public GameObject AppleBall;



    public void InteractWith()
    {
        //What happens after collision
        onHitFeedBack.PlayFeedbacks();
        Instantiate(AppleBall, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }

}
