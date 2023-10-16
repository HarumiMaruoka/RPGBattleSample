namespace Sky
{
    namespace Actor
    {
        /// <summary>
        /// �U���͂�h��͓��̃X�e�[�^�X��\������\���́B
        /// </summary>
        public struct ActorAttributeStatus
        {
            public ActorAttributeStatus(int maxHP, int maxMP, int attack, int defense, int magicPower, int magicDefense, int speed, int accuracy, int evasion, int criticalRate)
            {
                _maxHP = maxHP;
                _maxMP = maxMP;
                _attack = attack;
                _defense = defense;
                _magicPower = magicPower;
                _magicDefense = magicDefense;
                _speed = speed;
                _accuracy = accuracy;
                _evasion = evasion;
                _criticalRate = criticalRate;
            }

            /// <summary> �ő�̗� </summary>
            public readonly int _maxHP;
            /// <summary> �ő�MP </summary>
            public readonly int _maxMP;
            /// <summary> �U���� </summary>
            public readonly int _attack;
            /// <summary> �h��� </summary>
            public readonly int _defense;
            /// <summary> ���@�� </summary>
            public readonly int _magicPower;
            /// <summary> ���@�h��� </summary>
            public readonly int _magicDefense;
            /// <summary> ���� </summary>
            public readonly int _speed;
            /// <summary> ������ </summary>
            public readonly int _accuracy;
            /// <summary> ��� </summary>
            public readonly int _evasion;
            /// <summary> �N���e�B�J���� </summary>
            public readonly int _criticalRate;

            /// <summary> �ő�̗� </summary>
            public int MaxHP => _maxHP;
            /// <summary> �ő�MP </summary>
            public int MaxMP => _maxMP;
            /// <summary> �U���� </summary>
            public int Attack => _attack;
            /// <summary> �h��� </summary>
            public int Defense => _defense;
            /// <summary> ���@�� </summary>
            public int MagicPower => _magicPower;
            /// <summary> ���@�h��� </summary>
            public int MagicDefense => _magicDefense;
            /// <summary> ���� </summary>
            public int Speed => _speed;
            /// <summary> ������ </summary>
            public int Accuracy => _accuracy;
            /// <summary> ��� </summary>
            public int Evasion => _evasion;
            /// <summary> �N���e�B�J���� </summary>
            public int CriticalRate => _criticalRate;

            public static ActorAttributeStatus operator +(ActorAttributeStatus left, ActorAttributeStatus right)
            {
                return new ActorAttributeStatus(
                    left._maxHP + right._maxHP,
                    left._maxMP + right._maxMP,
                    left._attack + right._attack,
                    left._defense + right._defense,
                    left._magicPower + right._magicPower,
                    left._magicDefense + right._magicDefense,
                    left._speed + right._speed,
                    left._accuracy + right._accuracy,
                    left._evasion + right._evasion,
                    left._criticalRate + right._criticalRate
                );
            }
        }
    }
}