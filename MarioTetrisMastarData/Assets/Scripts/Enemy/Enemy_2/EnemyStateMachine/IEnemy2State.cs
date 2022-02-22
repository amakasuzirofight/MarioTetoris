using System;

namespace Enemy
{
    namespace Enemy2State
    {
        public interface IEnemy2State
        {
            // ステート処理のインターフェース

            event Action<Enemy2StateType> ChangeStateEvent;

            Enemy2StateType StateType { get; }

            void OnStart(Enemy2StateType beforeState, Enemy2Core player);

            void OnUpdate(Enemy2Core player);

            void OnFixedUpdate(Enemy2Core player);

            void OnEnd(Enemy2StateType nextState, Enemy2Core player);
        }
    }

}
