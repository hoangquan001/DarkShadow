using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void callback();
    class Task
    {
        callback cb;
        public float timer;
        public float timeOut;
        float Token;
        public Task(float timeOut, callback cb)
        {
            this.cb = cb;
            this.timer = 0;
            this.timeOut = timeOut;
        }
        public void call()
        {
            cb();
        }
    }
    static TimeManager _instance = null;
    static public TimeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("GameManager");
                _instance = go.GetComponent<TimeManager>();

            }
            return _instance;
        }

    }
    int index = 0;
    Dictionary<string, Task> taskMgr = new Dictionary<string, Task>();
    Queue<string> expiredToken = new Queue<string>();
    // @params return token 
    public string setTimeOut(float timeOut, callback cb)
    {
        string random = RandomStringGenerator(10);
        Task task = new Task(timeOut, cb);
        taskMgr[random] = task;
        return random;
    }

    string RandomStringGenerator(int lenght)
    {
        string result = "";
        for (int i = 0; i < lenght; i++)
        {
            char c = (char)(Random.Range(48, 90));
            result += c;
        }
        return result;
    }
    public bool killTask(string taskToken)
    {
        if (taskMgr.ContainsKey(taskToken))
        {
            taskMgr.Remove(taskToken);
            return true;
        }
        return false;
    }
    public bool isExpiredToken(string token)
    {
        return taskMgr.ContainsKey(token);
    }

    void Start()
    {
        _instance = this;
    }
    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var item in taskMgr)
        {
            string token = item.Key;
            Task task = item.Value;
            task.timer += Time.deltaTime;
            if (task.timer >= task.timeOut)
            {
                task.call();
                expiredToken.Enqueue(token);
            }
        }

        while (expiredToken.Count != 0)
        {
            string token = expiredToken.Dequeue();
            taskMgr.Remove(token);
        };



    }

}
