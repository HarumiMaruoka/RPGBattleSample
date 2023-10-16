// 日本語対応
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sky
{
    namespace Battle
    {
        public class Lose : BattleStateBase
        {
            public override void Enter()
            {
                BattleDebugger.Current?.ClearText();
                BattleDebugger.Current?.AppendText("CurrentState is Lose");
            }
            public override void Update()
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Field Test Scene");
                }
            }
        }
    }
}