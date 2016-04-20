using UnityEngine;
using System.Collections;

public interface DialogReceiverInterface
{
    void OnDialogOk(string message, System.Object workingObject = null);
    void OnDialogCanceled(System.Object workingObject = null);
}
