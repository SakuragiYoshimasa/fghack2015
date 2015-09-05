using System.Collections.Generic;

public class CondCheck : CondBase
{
    public class EnemyInfo
    {
        public string id;
        public int type;
        public double lat;
        public double lon;
    }

    public class TeamInfo
    {
        public int id;
        public int point;
    }

    public class R : CondBase.Response
    {
        public List<EnemyInfo> EnemyList;

        public List<TeamInfo> TeamList;

        public R(List<EnemyInfo> enemyList, List<TeamInfo> teamList, CondBase cond)
            : base(cond)
        {
            EnemyList = enemyList;
            TeamList = teamList;
        }
    }

    public bool Hit { get; private set; }

    public int EnemyID { get; private set; }

    public CondCheck(bool hit, int enemyID)
        : base(true, true)
    {
        Hit = hit;
        EnemyID = enemyID;
    }

    protected override Dictionary<string, object> CreateParams()
    {
        return  new Dictionary<string, object>()
        {
            { "hit", true },
            { "enemyID", EnemyID }
        };
    }
}
