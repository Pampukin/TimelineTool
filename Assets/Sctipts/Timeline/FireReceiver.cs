using UnityEngine;
using UnityEngine.Playables;

public class FireReceiver : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is AbstractFireMarker fireMarker)
        {
            fireMarker.Fire();
        }
    }
}
