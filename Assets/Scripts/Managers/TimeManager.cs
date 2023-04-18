using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void callback();
    class Timer
    {
        callback cb;
        public float interval;
        public float timeOut;
        ulong Token;
        public Timer(float timeOut, callback cb, ulong Token)
        {
            Reset(timeOut, cb, Token);
        }
        public void Reset(float timeOut, callback cb, ulong Token)
        {
            this.cb = cb;
            this.interval = 0;
            this.timeOut = timeOut;
        }
        public void call()
        {
            cb();
        }
    }
    ulong curtoken = 0;
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
    Queue<Timer> _pool = new Queue<Timer>();
    Dictionary<ulong, Timer> curTimers = new Dictionary<ulong, Timer>();
    Queue<ulong> expiredToken = new Queue<ulong>();
    // @params return token 
    public ulong setTimeOut(float timeOut, callback cb)
    {
        curtoken += 1;

        Timer timer = _pool.Count != 0 ? _pool.Dequeue() : null;
        if (timer != null)
            timer.Reset(timeOut, cb, curtoken);
        else
            timer = new Timer(timeOut, cb, curtoken);

        curTimers[curtoken] = timer;
        return curtoken;
    }

    public bool killTask(ulong taskToken)
    {
        if (curTimers.ContainsKey(taskToken))
        {
            curTimers.Remove(taskToken);

            return true;
        }
        return false;
    }
    public bool isExpiredToken(ulong token)
    {
        return curTimers.ContainsKey(token);
    }
    public float getIntervalTime(ulong taskToken)
    {
        if (isExpiredToken(taskToken))
            return curTimers[curtoken].interval;
        return -1;
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

        foreach (var item in curTimers)
        {
            Timer timer = item.Value;
            timer.interval += Time.deltaTime;
            if (timer.interval >= timer.timeOut)
            {
                timer.call();
                expiredToken.Enqueue(item.Key);
                _pool.Enqueue(timer);

            }
        }

        while (expiredToken.Count != 0)
        {
            ulong token = expiredToken.Dequeue();
            curTimers.Remove(token);
        };



    }

}
