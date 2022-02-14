using System;

namespace Enemy
{
    namespace Enemy1State
    {
        public interface IEnemy1State
        {
            // ステート処理のインターフェース

            event Action<Enemy1StateType> ChangeStateEvent;

            Enemy1StateType StateType { get; }

            void OnStart(Enemy1StateType beforeState, Enemy1Core player);

            void OnUpdate(Enemy1Core player);

            void OnFixedUpdate(Enemy1Core player);

            void OnEnd(Enemy1StateType nextState, Enemy1Core player);
        }
    }

}
