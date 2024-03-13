using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public Scene currentScene;
    public Scene miniGameScene;

    [SerializeField]
    static public int _sceneCount = 0;
    static public bool _changeScene = false;

    private void Update()
    {
        ChangeScene();
    }

    public void ChangeScene()
    {
        if (_changeScene)
        {
            switch (_sceneCount)
            {
                case 0:
                    Debug.Log("MainMenu");
                    break;
                case 1:
                    Debug.Log("HowToPlay");
                    break;
                case 2:
                    Debug.Log("Minigame");
                    break;
                case 3:
                    Debug.Log("Story1");
                    break;
                case 4:
                    Debug.Log("Story2");
                    break;
                case 5:
                    Debug.Log("Story3");
                    break;
                case 6:
                    Debug.Log("Story4");
                    break;
                case 7:
                    Debug.Log("Story5");
                    break;
                case 8:
                    Debug.Log("Story6");
                    break;
                case 9:
                    Debug.Log("Story7");
                    break;
                case 10:
                    Debug.Log("Story8");
                    break;
                case 11:
                    Debug.Log("Story9");
                    break;
                default:
                    break;
            }
            _changeScene = !_changeScene;
        }
    }

}
