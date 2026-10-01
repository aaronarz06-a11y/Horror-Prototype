using UnityEngine;

public class FadingLight : MonoBehaviour
{
    public Light mainLight;
    public float fullIntensity = 3f;
    public float fadeSpeed = 0.1f;
    public float rechargeSpeed = 1f;

    void Start()
    {
        if (mainLight == null)
        {
            mainLight = GetComponent<Light>();
        }

        mainLight.intensity = fullIntensity;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            //wind up the flashlight
            mainLight.intensity += rechargeSpeed * Time.deltaTime;
        }
        else
        {
            //or else slowly lose brightness
            mainLight.intensity -= fadeSpeed * Time.deltaTime;
        }

        //keeps brightness between zero and full strength
        mainLight.intensity = Mathf.Clamp(mainLight.intensity, 0f, fullIntensity
        );
    }
}