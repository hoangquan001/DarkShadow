
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    static CameraShake _instance;
    public static CameraShake Instance
    {
        get { return _instance; }
    }
    Camera cam;
    float timer = 0;
    float counter = 0;

    Vector3 OriginPosition;
    bool isShake = false;
    void Awake()
    {
        _instance = this;
        cam = Camera.main;
    }

    public void OnStartShake()
    {
        isShake = true;
        counter = 0;
        OriginPosition = cam.transform.position;
    }

    public void OnDuringShake()
    {
        cam.transform.position += new Vector3(Mathf.Sin(counter) * 0.5f, Mathf.Cos(counter) * 0.25f, 0);
        counter += 5;

    }

    public void OnStopShake()
    {
        cam.transform.position = OriginPosition;
        isShake = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.025f && isShake)
        {
            timer = 0;
            OnDuringShake();
            if (counter >= 90)
                OnStopShake();

        }
    }

}


































// static CameraShake instance;
//     public static CameraShake Instance { get { return instance; } }
//     Camera cam;
//     float timer = 0;
//     void Awake()
//     {
//         instance = this;
//         cam = Camera.main;
//     }
//     bool isShake = false;
//     float counter = 0;

//     Vector3 originPos;
//     public void OnStartShake()
//     {
//         originPos = cam.transform.position;
//     }
//     public void OnStopShake()
//     {
//         counter = 0;
//         cam.transform.position = originPos;
//         isShake = false;
//     }
//     public void Shake()
//     {
//         isShake = true;
//         OnStartShake();
//     }
//     void Earthquake()
//     {
//         cam.transform.position += new Vector3(Mathf.Sin(counter) * 0.5f, Mathf.Cos(counter) * 0.25f, 0);
//         counter += 5;
//         if (counter >= 90)
//         {
//             OnStopShake();
//         }
//     }
//     void Update()
//     {
//         timer += Time.deltaTime;
//         if (timer >= 0.025 && isShake)
//         {
//             Earthquake();
//             timer = 0;
//         }
//     }
