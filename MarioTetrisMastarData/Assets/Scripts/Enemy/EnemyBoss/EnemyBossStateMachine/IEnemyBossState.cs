using System;

namespace Enemy
{
    namespace EnemyBossState
    {
        public interface IEnemyBossState
        {
            // ステート処理のインターフェース

            event Action<EnemyBossStateType> ChangeStateEvent;

            EnemyBossStateType StateType { get; }

            void OnStart(EnemyBossStateType beforeState, EnemyBossCore player);

            void OnUpdate(EnemyBossCore player);

            void OnFixedUpdate(EnemyBossCore player);

            void OnEnd(EnemyBossStateType nextState, EnemyBossCore player);
        }
    }

}
