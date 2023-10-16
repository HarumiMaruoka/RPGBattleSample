using System;
using System.Collections.Generic;
using Sky.DataBase;

namespace Sky
{
    namespace Actor
    {
        /// <summary>
        /// Actor�̗̑͂�MP���A���\�[�X�Ɋւ���X�e�[�^�X���Ǘ�����N���X�B
        /// </summary>
        public class ActorResourceStatus
        {
            public ActorResourceStatus(int id, ActorLevelStatus level)
            {
                _id = id;
                _level = level;
            }

            private readonly int _id;
            private readonly ActorLevelStatus _level;

            private int _hp;
            private int _mp;

            public int Hp => _hp;
            public int Mp => _mp;
            public int MaxHP
            {
                get
                {
                    try
                    {
                        var maxHP = GameDataBase.Instance.LevelStatusDataStore.StatusData[new LevelData(_id, _level.Level)].MaxHP;
                        return maxHP;
                    }
                    catch (KeyNotFoundException)
                    {
                        UnityEngine.Debug.Log($"ActorID: {_id}, Level: {_level.Level}���f�[�^�ɑ��݂��܂���B");
                        return 0;
                    }
                }
            }
            public int MaxMP => GameDataBase.Instance.LevelStatusDataStore.StatusData[new LevelData(_id, _level.Level)].MaxMP;

            public bool IsDead => _hp <= 0; // �̗͂�0�ȉ��̎�����actor�͎��S���Ă��鎖��\������B

            public event Action<int, int> OnDamaged; // Damage���ɔ��΂���A�N�V�����B�������ɌÂ�Hp�A�������ɐV����Hp���n�����B
            public event Action<int, int> OnHealed; // Heal���ɔ��΂���A�N�V�����B�������ɌÂ�Hp�A�������ɐV����Hp���n�����B

            // �ő�l�ɉ񕜂���B
            public void RestoreToMaxValues()
            {
                _hp = MaxHP; _mp = MaxMP;
            }

            public bool Damage(int value)
            {
                var oldHp = _hp;
                _hp -= value;

                if (_hp > MaxHP) _hp = MaxHP;
                else if (_hp <= 0) _hp = 0;
                OnDamaged?.Invoke(oldHp, _hp);
                return true;
            }
            public bool Heal(int value)
            {
                var oldHp = _hp;
                _hp += value;

                if (_hp > MaxHP) _hp = MaxHP;
                else if (_hp <= 0) _hp = 0;
                OnHealed?.Invoke(oldHp, _hp);
                return true;
            }
            public bool TryRevive(ReviveMode mode)
            {
                if (!IsDead) return false;

                var value = 0;
                if (mode == ReviveMode.Leave1) value = 1;
                else if (mode == ReviveMode.Leave100Percent) value = MaxHP;
                else
                {
                    var percent = (int)mode;
                    value = (int)((percent / 100f) * MaxHP);
                }

                _hp = value;
                if (_mp > MaxMP) _mp = MaxMP;
                else if (_mp <= 0) _mp = 0;
                return true;
            }

            public bool TryIncreaseMP(int value)
            {
                if (IsDead) return false;

                _mp += value;

                if (_mp > MaxMP) _mp = MaxMP;
                else if (_mp <= 0) _mp = 0;
                return true;
            }
            /// <summary> MP�������B����ʂɑ΂��A���݂̏��L�ʂ�����Ȃ����false��Ԃ��B </summary>
            /// <param name="value"> ����� </param>
            /// <returns> ����ɐ��������Ƃ�true��Ԃ��B </returns>
            public bool TryDecreaseMP(int value)
            {
                if (IsDead) return false;
                if (_mp - value < 0) return false;

                _mp -= value;

                if (_mp > MaxMP) _mp = MaxMP;
                else if (_mp <= 0) _mp = 0;
                return true;
            }

            public ActorResourceStatus Clone(ActorLevelStatus clonedLevel)
            {
                // ActorResourceStatus�N���X��V�����C���X�^���X�ɃR�s�[
                ActorResourceStatus newStatus = new ActorResourceStatus(_id, clonedLevel); // ActorLevelStatus�̓f�B�[�v�R�s�[

                // �t�B�[���h���R�s�[
                newStatus._hp = _hp;
                newStatus._mp = _mp;

                // �C�x���g�n���h���[��null�ɐݒ�
                newStatus.OnDamaged = null;
                newStatus.OnHealed = null;

                return newStatus;
            }
        }

        public enum ReviveMode
        {
            // 1�ŕ���, 1�� 2�� ... 10�� �ŕ���
            Leave1,
            Leave10Percent = 10,
            Leave20Percent = 20,
            Leave30Percent = 30,
            Leave40Percent = 40,
            Leave50Percent = 50,
            Leave60Percent = 60,
            Leave70Percent = 70,
            Leave80Percent = 80,
            Leave90Percent = 90,
            Leave100Percent = 100,
        }
    }
}