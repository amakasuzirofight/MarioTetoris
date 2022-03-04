using System;

namespace Enemy
{
    namespace EnemyBossState
    {
        public interface IEnemyBossState
        {
            // �X�e�[�g�����̃C���^�[�t�F�[�X

            event Action<EnemyBossStateType> ChangeStateEvent;

            EnemyBossStateType StateType { get; }

            void OnStart(EnemyBossStateType beforeState, EnemyBossCore player);

            void OnUpdate(EnemyBossCore player);

            void OnFixedUpdate(EnemyBossCore player);

            void OnEnd(EnemyBossStateType nextState, EnemyBossCore player);
        }
    }

}
