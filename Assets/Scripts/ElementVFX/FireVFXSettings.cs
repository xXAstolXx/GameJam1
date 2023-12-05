using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "FireVFX", menuName = "VFX/Fire VFX settings")]
public class FireVFXSettings : ScriptableObject
{
    public int ResoX { get { return resoX; }}
    [SerializeField]
    private int resoX;
    public int ResoY { get { return resoY; } }
    [SerializeField]
    private int resoY;

    public Color InnerColor { get { return innerColor; } }
    [SerializeField]
    private Color innerColor;
    public Color MiddleColor { get { return middleColor; } }
    [SerializeField]
    private Color middleColor;
    public Color OuterColor { get { return OuterColor; } }
    [SerializeField]
    private Color outerColor;

    public AnimationCurve InnerLengthProb { get { return innerLengthProb; } }
    [SerializeField]
    private AnimationCurve innerLengthProb;
    public AnimationCurve InnerThicknessSpawn { get { return innerThicknessSpawn; } }
    [SerializeField]
    private AnimationCurve innerThicknessSpawn;
    public AnimationCurve InnerThicknessDespawn { get { return innerThicknessDespawn; } }
    [SerializeField]
    private AnimationCurve innerThicknessDespawn;
    public AnimationCurve MiddleLengthProb { get { return middleLengthProb; } }
    [SerializeField]
    private AnimationCurve middleLengthProb;
    public AnimationCurve MiddleThicknessSpawn { get { return middleThicknessSpawn; } }
    [SerializeField]
    private AnimationCurve middleThicknessSpawn;
    public AnimationCurve MiddleThicknessDespawn { get { return MiddleThicknessDespawn; } }
    [SerializeField]
    private AnimationCurve middleThicknessDespawn;
    public AnimationCurve OuterLengthProb { get { return outerLengthProb; } }
    [SerializeField]
    private AnimationCurve outerLengthProb;
    public AnimationCurve OuterThicknessSpawn { get { return outerThicknessSpawn; } }
    [SerializeField]
    private AnimationCurve outerThicknessSpawn;
    public AnimationCurve OuterThicknessDespawn { get { return outerThicknessDespawn; } }
    [SerializeField]
    private AnimationCurve outerThicknessDespawn;

    public int InnerRange { get { return innerRange; } }
    [SerializeField]
    private int innerRange;
    public int MiddleRange { get { return middleRange; } }
    [SerializeField]
    private int middleRange;
    public int OuterRange { get { return outerRange; } }
    [SerializeField]
    private int outerRange;
    public int SpawnLimit { get { return spawnLimit; } }
    [SerializeField]
    private int spawnLimit;

}
