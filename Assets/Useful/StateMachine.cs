// 日本語対応

using System;

namespace Useful
{
    public class StateMachine
    {
        private State _currentState = null;

        public State CurrentState => _currentState;

        public event Action<State> OnStateChanged;

        // 最初のステートを決める。
        public virtual void Initialize(State initialState)
        {
            TransitionTo(initialState);
        }
        // 現在のステートを毎フレーム実行する。
        public void Update()
        {
            _currentState?.Update();
        }
        // ステート遷移処理。
        public void TransitionTo(State nextState)
        {
            _currentState?.Exit();
            _currentState = nextState;

            _currentState?.Enter();

            OnStateChanged?.Invoke(_currentState);
        }
    }
}