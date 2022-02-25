using System;

namespace Enemy
{
    namespace Enemy3State
    {
        public interface IEnemy3State
        {
            // ステート処理のインターフェース

            event Action<Enemy3StateType> ChangeStateEvent;

            Enemy3StateType StateType { get; }

            void OnStart(Enemy3StateType beforeState, Enemy3Core player);

            void OnUpdate(Enemy3Core player);

            void OnFixedUpdate(Enemy3Core player);

            void OnEnd(Enemy3StateType nextState, Enemy3Core player);
        }
    }

}
