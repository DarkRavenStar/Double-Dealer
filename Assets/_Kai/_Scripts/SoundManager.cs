using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource efxSource;
    public AudioSource bombSource;
    public AudioSource walkSource;
    public AudioSource DoorOpenSource;
    public AudioSource DoorNotOpenSource;
    public AudioSource HidingSource;
    public AudioSource GameOverSource;
    public AudioSource DoorCloseSource;
    public AudioSource PickPocketSource;
    public AudioSource AlertSource;
    public AudioSource PowerDownSource;
    public AudioSource PipeGameRotateSource;
    public AudioSource PuzzleSolveSource;
    public AudioSource PuzzleResetSource;
    public AudioSource OpenMenuSource;
    public AudioSource CloseMenuSource;
    public AudioSource MenuUpScrollSource;
    public AudioSource MenuDownScrollSource;
    public AudioSource HumanGuardWalkSource;
    public AudioSource RobotGuardWalkSource;
    public AudioSource MainMenuSource;
    public AudioSource ClickSource;
    public AudioSource CorrectSource;
    public AudioSource WrongSource;
    public AudioSource WinSource;
    public AudioSource PressureSolveSource;
    public AudioSource PressureSource;
    public AudioSource PressureResetSource;
    public Slider VolumeBGM;
    public Slider VolumeSFX;

    public static float bgmSliderValue = 0.5f;
    public static float sfxSliderValue = 0.5f;

    public float bgmSliderValueTemp;
    public float sfxSliderValueTemp;

    public bool runOnce = false;

    
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {

            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        bgmSliderValueTemp = bgmSliderValue;
        sfxSliderValueTemp = sfxSliderValue;
        /*
        if (VolumeBGM == null)
        {
            if(GameObject.Find("BGM_Slider").activeInHierarchy == true)
            {
                VolumeBGM = GameObject.Find("BGM_Slider").GetComponent<Slider>();
            }
            else
            {
                //VolumeBGM.value = bgmSliderValue;
            }
        }

        if (VolumeSFX == null)
        {
            if (GameObject.Find("SFX_Slider").activeInHierarchy == true)
            {
                VolumeSFX = GameObject.Find("SFX_Slider").GetComponent<Slider>();
            }
            else
            {
                //VolumeSFX.value = sfxSliderValue;
            }
        }*/

        if(VolumeBGM != null)
        {
            musicSource.volume = VolumeBGM.value;
        }

        if (VolumeSFX != null)
        {
            bombSource.volume = VolumeSFX.value;
            walkSource.volume = VolumeSFX.value;
            DoorOpenSource.volume = VolumeSFX.value;
            DoorNotOpenSource.volume = VolumeSFX.value;
            GameOverSource.volume = VolumeSFX.value;
            DoorCloseSource.volume = VolumeSFX.value;
            PickPocketSource.volume = VolumeSFX.value;
            AlertSource.volume = VolumeSFX.value;
            PowerDownSource.volume = VolumeSFX.value;
            PipeGameRotateSource.volume = VolumeSFX.value;
            OpenMenuSource.volume = VolumeSFX.value;
            CloseMenuSource.volume = VolumeSFX.value;
            MenuUpScrollSource.volume = VolumeSFX.value;
            MenuDownScrollSource.volume = VolumeSFX.value;
            HumanGuardWalkSource.volume = VolumeSFX.value;
            RobotGuardWalkSource.volume = VolumeSFX.value;
            MainMenuSource.volume = VolumeSFX.value;
            ClickSource.volume = VolumeSFX.value;
            CorrectSource.volume = VolumeSFX.value;
            WrongSource.volume = VolumeSFX.value;
            PressureSolveSource.volume = VolumeSFX.value;
            PressureSource.volume = VolumeSFX.value;
            PressureResetSource.volume = VolumeSFX.value;
        }

        if (VolumeBGM == null)
        {
            musicSource.volume = bgmSliderValue;
        }

        if (VolumeSFX == null)
        {
            bombSource.volume = sfxSliderValue;
            walkSource.volume = sfxSliderValue;
            DoorOpenSource.volume = sfxSliderValue;
            DoorNotOpenSource.volume = sfxSliderValue;
            GameOverSource.volume = sfxSliderValue;
            DoorCloseSource.volume = sfxSliderValue;
            PickPocketSource.volume = sfxSliderValue;
            AlertSource.volume = sfxSliderValue;
            PowerDownSource.volume = sfxSliderValue;
            PipeGameRotateSource.volume = sfxSliderValue;
            OpenMenuSource.volume = sfxSliderValue;
            CloseMenuSource.volume = sfxSliderValue;
            MenuUpScrollSource.volume = sfxSliderValue;
            MenuDownScrollSource.volume = sfxSliderValue;
            HumanGuardWalkSource.volume = sfxSliderValue;
            RobotGuardWalkSource.volume = sfxSliderValue;
            MainMenuSource.volume = sfxSliderValue;
            ClickSource.volume = sfxSliderValue;
            CorrectSource.volume = sfxSliderValue;
            WrongSource.volume = sfxSliderValue;
            WinSource.volume = sfxSliderValue;
            PressureSolveSource.volume = sfxSliderValue;
            PressureSource.volume = sfxSliderValue;
            PressureResetSource.volume = sfxSliderValue;
        }

}
    
    public void BGMVolumeInput()
    {
        bgmSliderValue = VolumeBGM.value;
    }

    public void SFXVolumeInput()
    {
        sfxSliderValue = VolumeSFX.value;
    }

    public void BGMVolumeUpdate()
    {
        VolumeBGM.value = bgmSliderValue;
    }

    public void SFXVolumeUpdate()
    {
        VolumeSFX.value = sfxSliderValue;
    }

    /*
    public void PlayBgm(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    */
    public void PlaySound(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }
    public void PlayBomb(AudioClip clip)
    {
        bombSource.clip = clip;
        bombSource.Play();
    }
    public void Walking(AudioClip clip)
    {
        walkSource.clip = clip;
        walkSource.Play();
    }
    public void DoorOpen(AudioClip clip)
    {
        DoorOpenSource.clip = clip;
        DoorOpenSource.Play();
    }
    public void DoorOpen1(AudioClip clip)
    {
        DoorNotOpenSource.clip = clip;
        DoorNotOpenSource.Play();
    }
    public void Hide(AudioClip clip)
    {
        HidingSource.clip = clip;
        HidingSource.Play();
    }
    public void GameOver(AudioClip clip)
    {
        GameOverSource.clip = clip;
        GameOverSource.Play();
    }
    public void DoorClose(AudioClip clip)
    {
        DoorCloseSource.clip = clip;
        DoorCloseSource.Play();
    }
    public void PickPocket(AudioClip clip)
    {
        PickPocketSource.clip = clip;
        PickPocketSource.Play();
    }
    public void EnemySpotted(AudioClip clip)
    {
        AlertSource.clip = clip;
        AlertSource.Play();
    }
    public void EnemyDown(AudioClip clip)
    {
        PowerDownSource.clip = clip;
        PowerDownSource.Play();
    }
    public void PipeRotate(AudioClip clip)
    {
        PipeGameRotateSource.clip = clip;
        PipeGameRotateSource.Play();
    }
    public void PuzzleSolve(AudioClip clip)
    {
        PuzzleSolveSource.clip = clip;
        PuzzleSolveSource.Play();
    }
    public void PuzzleReset(AudioClip clip)
    {
        PuzzleResetSource.clip = clip;
        PuzzleResetSource.Play();
    }
    public void OpenMenu(AudioClip clip)
    {
        OpenMenuSource.clip = clip;
        OpenMenuSource.Play();
    }
    public void CloseMenu(AudioClip clip)
    {
        CloseMenuSource.clip = clip;
        CloseMenuSource.Play();
    }
    public void MenuUpScroll(AudioClip clip)
    {
        MenuUpScrollSource.clip = clip;
        MenuUpScrollSource.Play();
    }
    public void MenuDownScroll(AudioClip clip)
    {
        MenuDownScrollSource.clip = clip;
        MenuDownScrollSource.Play();
    }
    public void HumanGuardWalk(AudioClip clip)
    {
        HumanGuardWalkSource.clip = clip;
        HumanGuardWalkSource.Play();
    }
    public void RobotGuardWalk(AudioClip clip)
    {
        RobotGuardWalkSource.clip = clip;
        RobotGuardWalkSource.Play();
    }
    public void MainMenuSound(AudioClip clip)
    {
        MainMenuSource.clip = clip;
        MainMenuSource.Play();
    }
    public void ClickSound(AudioClip clip)
    {
        ClickSource.clip = clip;
        ClickSource.Play();
    }
    public void CorrectSound(AudioClip clip)
    {
        CorrectSource.clip = clip;
        CorrectSource.Play();
    }
    public void WrongSound(AudioClip clip)
    {
        WrongSource.clip = clip;
        WrongSource.Play();
    }
    public void WinSound(AudioClip clip)
    {
        WinSource.clip = clip;
        WinSource.Play();
    }
    public void PressureSound(AudioClip clip)
    {
        PressureSource.clip = clip;
        PressureSource.Play();
    }
    public void PressureResetSound(AudioClip clip)
    {
        PressureResetSource.clip = clip;
        PressureResetSource.Play();
    }
    public void PressureSolveSound(AudioClip clip)
    {
        PressureSolveSource.clip = clip;
        PressureSolveSource.Play();
    }
}
