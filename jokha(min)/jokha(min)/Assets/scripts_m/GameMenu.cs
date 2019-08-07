using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void GameStart()
    {
        //화면전환 효과 추가
        SceneManager.LoadScene("Opening");
    }

    public void ExitGame()
    {
        Application.Quit();
        //뒤로가기를 눌렀을 때 게임이 종료되거나
        //게임종료 버튼 만들기
    }
}
