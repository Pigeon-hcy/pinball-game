using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool canInteract { get; }

    void InteractWith();
}
