using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GlobalFireVFX1 : MonoBehaviour
{
    [HideInInspector]
    public static GlobalFireVFX Instance;


    [SerializeField]
    int resoX;
    [SerializeField]
    int resoY;

    [SerializeField]
    Color innerColor;
    [SerializeField]
    Color middleColor;
    [SerializeField]
    Color outerColor;

    [SerializeField]
    AnimationCurve innerLengthProb;
    [SerializeField]
    AnimationCurve innerThicknessSpawn;
    [SerializeField]
    AnimationCurve innerThicknessDespawn;
    [SerializeField]
    AnimationCurve middleLengthProb;
    [SerializeField]
    AnimationCurve middleThicknessSpawn;
    [SerializeField]
    AnimationCurve middleThicknessDespawn;
    [SerializeField]
    AnimationCurve outerLengthProb;
    [SerializeField]
    AnimationCurve outerThicknessSpawn;
    [SerializeField]
    AnimationCurve outerThicknessDespawn;

    [SerializeField]
    int innerRange;
    [SerializeField]
    int middleRange;
    [SerializeField]
    int outerRange;

    [SerializeField]
    int spawnLimit;


    float[] innerLength;
    float[] innerThicknessSpawnData;
    float[] innerThicknessDespawnData;
    Float4S[] lengthData;

    float[] middleLength;
    float[] middleThicknessSpawnData;
    float[] middleThicknessDespawnData;
    Float4S[] thicknessSpawnData;

    float[] outerLength;
    float[] outerThicknessSpawnData;
    float[] outerThicknessDespawnData;
    Float4S[] thicknessDespawnData;

    int SetKernel;
    int SpawnKernel;
    int UpdateKernel;
    int InitializeKernel;

    int threadGroupsX;
    int threadGroupsY;

    ComputeShader computeShader;

    ComputeBuffer lengthBuffer;
    ComputeBuffer thicknessSpawnBuffer;
    ComputeBuffer thicknessDespawnBuffer;
    ComputeBuffer valueBuffer;
    ComputeBuffer tempValueBuffer;

    public RenderTexture renderTexture { get; private set; }
    MaterialPropertyBlock materialPropertyBlock;

    ComputeBuffer test;
    Float4S[] t;

    bool executeFrame;


    private void Awake()
    {
        //Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateShader();
        //materialPropertyBlock.SetTexture("_FireTex", renderTexture);
        //GetComponent<SpriteRenderer>().SetPropertyBlock(materialPropertyBlock);
        //test.GetData(t);
        //Debug.Log("spawnLimit: " + t[0].innerValue + ". on cpu: " + spawnLimit);
        //Debug.Log("length prop: " + t[0].middleValue + ". on cpu: " + lengthData[7].middleValue);
        //Debug.Log("stay alive prob: " + t[0].outerValue + ". on cpu: " + thicknessDespawnData[7].outerValue);
    }

    private void Initialize()
    {
        executeFrame = true;

        threadGroupsX = resoX / 8;
        threadGroupsY = resoY / 8;

        renderTexture = new RenderTexture(resoX, resoY, 0);
        renderTexture.enableRandomWrite = true;
        renderTexture.format = RenderTextureFormat.ARGBFloat;
        renderTexture.graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R32G32B32A32_SFloat;
        renderTexture.filterMode = FilterMode.Point;
        renderTexture.Create();

        //materialPropertyBlock = new MaterialPropertyBlock();
        //materialPropertyBlock.SetTexture("_FireTex", renderTexture);
        //GetComponent<SpriteRenderer>().SetPropertyBlock(materialPropertyBlock);
        //GetComponent<SpriteRenderer>().sharedMaterial.SetTexture("_FireTex", renderTexture);

        innerLength = CurveToLengthArray(innerLengthProb, resoY);
        middleLength = CurveToLengthArray(middleLengthProb, resoY);
        outerLength = CurveToLengthArray(outerLengthProb, resoY);
        InitializeLengthData();
        innerThicknessSpawnData = CurveToThicknessArray(innerThicknessSpawn, resoX, innerRange);

        middleThicknessSpawnData = CurveToThicknessArray(middleThicknessSpawn, resoX, middleRange);
        outerThicknessSpawnData = CurveToThicknessArray(outerThicknessSpawn, resoX, outerRange);
        InitializeThicknessSpawnData();

        innerThicknessDespawnData = CurveToThicknessArray(innerThicknessDespawn, resoX, innerRange);
        middleThicknessDespawnData = CurveToThicknessArray(middleThicknessDespawn, resoX, middleRange);
        outerThicknessDespawnData = CurveToThicknessArray(outerThicknessDespawn, resoX, outerRange);
        InitializeThicknessDespawnData();

        int size = resoY * resoX;
        int4[] valueData = new int4[size];
        int4[] tempValueData = new int4[size];
        for (int i = 0; i < size; i++)
        {
            valueData[i] = 0;
            tempValueData[i] = 0;
        }

        computeShader = Resources.Load<ComputeShader>("Shader/VFX/FireVFX");

        SetKernel = computeShader.FindKernel("CSSet");
        SpawnKernel = computeShader.FindKernel("CSSpawn");
        UpdateKernel = computeShader.FindKernel("CSUpdate");
        InitializeKernel = computeShader.FindKernel("CSInitialize");

        valueBuffer = new ComputeBuffer(resoX * resoY, sizeof(int) * 4);
        tempValueBuffer = new ComputeBuffer(resoX * resoY, sizeof(int) * 4);
        lengthBuffer = new ComputeBuffer(resoY, sizeof(float) * 4);
        thicknessSpawnBuffer = new ComputeBuffer(resoX, sizeof(float) * 4);
        thicknessDespawnBuffer = new ComputeBuffer(resoX, sizeof(float) * 4);

        valueBuffer.SetData(valueData);
        tempValueBuffer.SetData(tempValueData);

        lengthBuffer.SetData(lengthData);
        thicknessSpawnBuffer.SetData(thicknessSpawnData);
        thicknessDespawnBuffer.SetData(thicknessDespawnData);

        computeShader.SetBuffer(SetKernel, "valueBuffer", valueBuffer);
        computeShader.SetBuffer(SetKernel, "tempValueBuffer", tempValueBuffer);
        computeShader.SetBuffer(SetKernel, "lengthBuffer", lengthBuffer);
        computeShader.SetBuffer(SetKernel, "thicknessDespawnBuffer", thicknessDespawnBuffer);

        computeShader.SetBuffer(SpawnKernel, "tempValueBuffer", tempValueBuffer);
        computeShader.SetBuffer(SpawnKernel, "thicknessSpawnBuffer", thicknessSpawnBuffer);

        computeShader.SetBuffer(UpdateKernel, "tempValueBuffer", tempValueBuffer);
        computeShader.SetBuffer(UpdateKernel, "valueBuffer", valueBuffer);
        computeShader.SetTexture(UpdateKernel, "renderTexture", renderTexture);

        computeShader.SetTexture(InitializeKernel, "renderTexture", renderTexture);

        computeShader.SetFloat("time", Time.time);

        computeShader.SetVector("innerColor", innerColor);
        computeShader.SetVector("middleColor", middleColor);
        computeShader.SetVector("outerColor", outerColor);

        computeShader.SetInt("resoX", resoX);
        computeShader.SetInt("resoY", resoY);
        computeShader.SetInt("spawnLimit", spawnLimit);

        test = new ComputeBuffer(1, sizeof(float) * 4);
        t = new Float4S[1];
        t[0] = new Float4S(0, 0, 0);
        test.SetData(t);
        computeShader.SetBuffer(SetKernel, "test", test);

        computeShader.Dispatch(InitializeKernel, threadGroupsX, threadGroupsY, 1);
    }

    private void UpdateShader()
    {
        if (executeFrame)
        {
            executeFrame = true;
            computeShader.SetFloat("time", Time.time);
            computeShader.Dispatch(SetKernel, threadGroupsX, threadGroupsY, 1);
            computeShader.Dispatch(SpawnKernel, threadGroupsX, threadGroupsY, 1);
            computeShader.Dispatch(UpdateKernel, threadGroupsX, threadGroupsY, 1);
        }
        else
        {
            executeFrame = true;
        }
    }

    private float[] CurveToLengthArray(AnimationCurve curve, int arrayLength)
    {
        float[] result = new float[arrayLength];

        float stepSize = 1f / arrayLength;
        for (int i = 0; i < arrayLength; i++)
        {
            result[i] = Mathf.Clamp01(curve.Evaluate(i * stepSize));
        }

        return result;
    }

    private float[] CurveToThicknessArray(AnimationCurve curve, int arrayLength, int range)
    {
        float[] result = new float[arrayLength];

        int start = arrayLength / 2 - range;
        int end = arrayLength / 2 + range;
        float stepSize = 1f / (2f * range - 1f);

        for (int j = 0; j < start; j++)
        {
            result[j] = 0;
        }
        for (int i = start; i < end; i++)
        {
            result[i] = Mathf.Clamp01(curve.Evaluate((i - start) * stepSize));
        }
        for (int k = end; k < arrayLength; k++)
        {
            result[k] = 0;
        }
        return result;
    }

    private void InitializeLengthData()
    {
        lengthData = new Float4S[resoY];

        for (int i = 0; i < lengthData.Length; i++)
        {
            lengthData[i] = new Float4S(innerLength[i], middleLength[i], outerLength[i]);
        }
    }

    private void InitializeThicknessSpawnData()
    {
        thicknessSpawnData = new Float4S[resoX];
        for (int i = 0; i < thicknessSpawnData.Length; i++)
        {
            thicknessSpawnData[i] = new Float4S(innerThicknessSpawnData[i], middleThicknessSpawnData[i], outerThicknessSpawnData[i]);
        }
    }

    private void InitializeThicknessDespawnData()
    {
        thicknessDespawnData = new Float4S[resoX];
        for (int i = 0; i < thicknessDespawnData.Length; i++)
        {
            thicknessDespawnData[i] = new Float4S(innerThicknessDespawnData[i], middleThicknessDespawnData[i], outerThicknessDespawnData[i]);
        }
    }

    private void OnDestroy()
    {
        valueBuffer.Release();
        tempValueBuffer.Release();
        lengthBuffer.Release();
        thicknessDespawnBuffer.Release();
        thicknessSpawnBuffer.Release();
        test.Release();
        renderTexture.Release();
    }
}

//public struct Float4S
//{
//    public float innerValue;
//    public float middleValue;
//    public float outerValue;
//    public float paddingDummy;

//    public Float4S(float innerValue, float middleValue, float outerValue)
//    {
//        this.innerValue = innerValue;
//        this.middleValue = middleValue;
//        this.outerValue = outerValue;
//        paddingDummy = -1;
//    }
//}

