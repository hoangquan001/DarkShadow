using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    // Start is called before the first frame update
    VideoPlayer video;
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!video.isPlaying)
        {
            nextScene();
        }
    }
    public void nextScene()
    {
        SceneController.Instance.NextMap();
        DataPersistanceManager.Instance.nextMap();

    }
}
