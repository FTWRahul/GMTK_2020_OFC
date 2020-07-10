[System.Serializable]
public class CameraShakeSettings
{
    public float traumaMult = 16; //the power of the shake
    public float traumaMag = 0.8f; //the range of movment
    public float traumaRotMag = 17f; //the rotational power
    public float traumaDepthMag = 0.6f; //the depth multiplier
    public float traumaDecay = 1.3f; //how quickly the shake falls off

    public CameraShakeSettings(float tMult, float tMag, float tRotMag, float tDepthMag, float tDecay)
    {
        traumaMult = tMult;
        traumaMag = tMag;
        traumaRotMag = tRotMag;
        traumaDepthMag = tDepthMag;
        traumaDecay = tDecay;
    }
}