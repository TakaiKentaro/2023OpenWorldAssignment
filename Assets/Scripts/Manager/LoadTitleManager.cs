using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTitleManager : MonoBehaviour
{
    private string sceneName; // リロードするシーンの名前

    public void LoadScene()
    {
        sceneName = SceneManager.GetActiveScene().name; // 現在のシーンの名前を取得
        SceneManager.LoadScene(sceneName); // シーンをリロード
    }
}