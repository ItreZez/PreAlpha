using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VOL : MonoBehaviour
{//START CLASS VOL


    public Slider sliderMusic;
    public float sliderValueMusic;
    public Image imagenMuteMusic;

    private void Start()
    {
        //guardar la configuracion de volumen 
        sliderMusic.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = sliderMusic.value;
        RevisarSiEstoyMuteMusic();
    }
    
    public void ChangeSlider(float valor)
    {
        sliderValueMusic = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValueMusic);
        AudioListener.volume = sliderMusic.value;
        RevisarSiEstoyMuteMusic();

    }

    public void RevisarSiEstoyMuteMusic()
    {
        if (sliderValueMusic == 0)
        {
            imagenMuteMusic.enabled = true;
        }
        else
        {
            imagenMuteMusic.enabled = false;
        }
    }

   
}//END CLASS VOL
