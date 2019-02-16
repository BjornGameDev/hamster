using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Python.Runtime;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class QuantumManager : MonoBehaviour
{
    public Vector3[] plotPositions;
    public Vector3[] plotEcho1;
    public Vector3[] plotEcho2;

    private float leftWell = 0;
    private float rightWell = 0;
    private float leftInput = 0;
    private float rightInput = 0;


    public float getWellPosition(string handName)
    {
        return (handName == "leftHand" ? leftWell + 0.75f : rightWell - 0.75f);
    }

    private static QuantumManager m_instance;
    public static QuantumManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<QuantumManager>();
            }

            return m_instance;
        }
    }
    
    private StirapEnv m_env;

    [Tooltip("Steps per second")]
    [SerializeField] private float m_updateFrequency = 60;
    public float UpdateIntervalSeconds => 1f / m_updateFrequency;
    [SerializeField] private float MaxGradientThreshold = 10f;
    [SerializeField] private int m_growthBuffer = 10;
    [SerializeField] private LineRenderer m_plotRenderer;
    [SerializeField] private PlotAccuracy m_plotAccuracy = PlotAccuracy.Max;
    [SerializeField] private VisualizeMode m_plotVisualizeMode;
    [Range(-0.25f, 0.25f)]
    [SerializeField] private double m_noise = 0;
    [SerializeField] private bool m_randomizeNoise = false;

    public enum VisualizeMode
    {
        ComplexAsVector,
        ComplexMagnitude
    }

    public enum PlotAccuracy
    {
        Max = 1,
        High = 2,
        Medium = 4,
        Low = 8
    }

    
    public enum State
    {
        Initialized,
        Started,
        Stopped
    }

    private State m_state;

    private RingBuffer m_growthRight;
    private RingBuffer m_growthMid;
    private RingBuffer m_growthLeft;
    
    public int TimeSteps = 400;

    // Start is called before the first frame update
    void Start()
    {
        ResetUI();
        if (m_state != State.Started)
            StartGame();
    }

    private void ResetGrowthMeasures()
    {
        m_growthRight = new RingBuffer(0, m_growthBuffer);
        m_growthLeft = new RingBuffer(0, m_growthBuffer);
        m_growthMid = new RingBuffer(0, m_growthBuffer);
    }

    private void ResetUI()
    {

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
 
        }

        leftInput = (Input.GetAxis("LeftTrigger")  - 0.5f)*2;
        rightInput = (Input.GetAxis("RightTrigger") - 0.5f)*2;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
            Application.Quit();
        }

    }

    public void StartGame()
    {
       
        Double noise = m_noise;
        if (m_randomizeNoise)
        {
            noise = UnityEngine.Random.value * m_noise;
        }

        InitEnv(noise);

        StartCoroutine(GameCoroutine());
    }

    private void InitEnv(double displacement)
    {
        // To be safe, add a using (Py.Gil()) block every time you interact with any python wrapper class like StirapEnv
        using (Python.Runtime.Py.GIL())
        {
            
            m_env = new StirapEnv(-1, displacement);
            Debug.Log("Noise: "+m_env.Noise);
        }
    }
    
    /// <summary>
    /// Main game loop as a coroutine. You could easily just use use the Update method.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameCoroutine()
    {
        
        float time, delta;
        int step = 0;
        
        ResetGrowthMeasures();
        // Run the stirap env step at intervals determined by UpdateFrequency, until the result states Done.
        yield return new WaitForSeconds(1f);
        do
        {
            time = Time.time;
            if (UpdateIntervalSeconds > 0)
                yield return new WaitForSeconds(UpdateIntervalSeconds);
            else
                yield return null;
            delta = 10*(Time.time - time);
            
            step++;
        } while (!RunStep(step, delta));

        EndGame();
    }

    private void EndGame()
    {
        m_env.Reset();    
    }


    private bool canMove(float value, float input)
    {
        bool canMove = false;

        value = Mathf.Abs(value);
        if(value + input < 0.1f)
        {
            canMove = true;
        }

        if(value + input > 1.49f)
        {
            canMove = true;
        }

        return canMove;
    }

    /// <summary>
    /// Runs a single step of the simulation and handles the results
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    private bool RunStep(int step, float deltaTime)
    {
        StirapEnv.StepResult result;
        // Add a using Py.GIL() block whenever interacting with Python wrapper classes such as StirapEnv
        using (Py.GIL())
        {

            float leftClamp = (canMove(leftWell, leftInput) ? leftInput : 0);
            float rightClamp = (canMove(rightWell, rightInput) ? rightInput * -1 : 0);

            float left = leftClamp * deltaTime;
            float right = rightClamp * deltaTime;
            result = m_env.Step(left, right);
        }


        float leftPop = result.LeftPopulation;
        float rightPop = result.RightPopulation;

        leftWell = result.LeftWellPosition;
        rightWell = result.RightWellPosition;

        float midPop = 1f - (leftPop + rightPop);
        
        
        RenderPlot(result);
        return result.Done;
    }

    private void RenderPlot(StirapEnv.StepResult result)
    {
        if (m_plotRenderer  == null)
            return;

        int len = result.WavePoints.Length;
        
        List<Vector3> v = new List<Vector3>();
        float xx = 40f;
        float x_step = xx / len;
        int step = (int) m_plotAccuracy;
        
        for (int i=0; i<len-step+1; i+=step)
        {
            Complex c = result.WavePoints[i];
            v.Add(new Vector3(i * x_step - xx/2f, (float)c.Magnitude*3, 0f));
        }

        Vector3[] res = v.ToArray();
        m_plotRenderer.SetPositions(res);
        plotPositions = res;
        StartCoroutine(echo1(res));
        StartCoroutine(echo2(res));
    }
    IEnumerator echo1(Vector3[] res)
    {
        yield return new WaitForSeconds(0.33f);
        plotEcho1 = res;
    }
    IEnumerator echo2(Vector3[] res)
    {
        yield return new WaitForSeconds(0.66f);
        plotEcho2 = res;
    }
    public class RingBuffer
    {
        private List<float> m_buffer = new List<float>();
        private int m_bufferSize = 10;

        public float Average
        {
            get
            {
                float v = 0f;
                foreach (float f in m_buffer)
                {
                    v += f;
                }

                return v / m_buffer.Count;
            }
        }

        public float Last
        {
            get
            {
                if (m_buffer.Count > 0)
                    return m_buffer[m_buffer.Count - 1];
                return 0f;
            }
        }

        public RingBuffer(float startMeasure, int bufferSize)
        {
            m_buffer.Add(startMeasure);
            m_bufferSize = bufferSize;
        }

        public RingBuffer Update(float measure)
        {
            m_buffer.Add(measure);
            if (m_buffer.Count > m_bufferSize)
                m_buffer.RemoveAt(0);

            return this;
        }

        public RingBuffer Clear()
        {
            m_buffer.Clear();
            return this;
        }


    }
}