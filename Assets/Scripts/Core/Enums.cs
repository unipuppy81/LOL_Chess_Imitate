// è�Ǿ� �Ӽ� - �迭
public enum ChampionLine
{
    None = 0,
    SweetMagician, // ���޼���
    Druid,
    Witch, 
    Meadist, // ���ܼ���
    Frost,
    EldritchPowers, // ������ ��
    SpaceAndTime,
    Arcana,
    Fairy,
    Dragon,
    Portal,
    Hunger,
    Flame
}


// è�Ǿ� �Ӽ� - ����

public enum ChampionJob
{
    None = 0,
    Pal, // ��¦
    Mage, // ������
    Batqueen, // ���㿩��
    Shelter, // ��ȣ����
    Hunter, // ��ɲ�
    Vanguard, // ������
    Rusher, // �⵵��
    Bastion, // ���
    Enchantress, // �����
    Warrior, // ����
    Overmind, // �ʿ�ü
    Demolition, // ���Ĵ�
    Scholar, // ����
    Transmogrifier // ����ȯ��
}


// è�Ǿ� �ڽ�Ʈ
public enum ChampionCost
{
    None = 0,
    OneCost,
    TwoCost,
    ThreeCost,
    FourCost,
    FiveCost
}


// ������ Ÿ��

public enum ItemType
{
    None = 0,
    Normal,
    Using, // �Ҹ� ������ (����, ������, ������)
    Symbol, // Ư�� ������
    Relics, // ����
    Special, // ����
    Support // ����
}

public enum ItemAttributeType
{
    None = 0,
    AD_Damage,
    AD_Speed,
    AD_Defense,
    AP_Damage,
    AP_Defense,
    Mana,
    HP,
    CriticalPercent,
    Special
}

// ��ų Ÿ��
public enum SkillType
{
    None = 0,
    Active,
    Passive
}

public enum DamageType
{
    Normal,
    Critical,
    Player
}