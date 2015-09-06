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
        public bool Running;

        public List<EnemyInfo> EnemyList;

        public List<TeamInfo> TeamList;

        public int Time;

        public R(bool running, int time, List<EnemyInfo> enemyList, List<TeamInfo> teamList, CondBase cond)
            : base(cond)
        {
            Running = running;
            Time = time;
            EnemyList = enemyList;
            TeamList = teamList;
        }
    }

    public string HitEnemyID { get; private set; }

    public bool IsSuccessGesture { get; private set; }

    public int EnemyID { get; private set; }

    public CondCheck(string hitEnemyID, bool isSuccessGesture)
        : base(true, true)
    {
        HitEnemyID = hitEnemyID;
        IsSuccessGesture = isSuccessGesture;
    }

    protected override Dictionary<string, object> CreateParams()
    {
        return  new Dictionary<string, object>()
        {
            { "hitEnemyID", HitEnemyID },
            { "isSuccessGesture", IsSuccessGesture },
        };
    }
}
