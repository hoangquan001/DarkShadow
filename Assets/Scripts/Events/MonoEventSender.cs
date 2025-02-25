using UnityEngine;

public class MonoEventSender : MonoBehaviour
{
    public MonoEventReceiver receiver;
    public EventDefine eventType;
    public void SendEvent()
    {
        if(eventType!= EventDefine.None)
        receiver?.Send(eventType);
    }
    
}