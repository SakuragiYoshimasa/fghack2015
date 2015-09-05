using System.Collections.Generic;

public class CondRegister : CondBase
{
    public class R : CondBase.Response
    {
        public R(CondBase cond)
            : base(cond)
        {
        }
    }

    public int RoomID { get; private set; }

    public int Team { get; private set; }

    public CondRegister(int roomID, int team)
        : base(true, true)
    {
        RoomID = roomID;
        Team = team;
    }

    protected override Dictionary<string, object> CreateParams()
    {
        return  new Dictionary<string, object>()
        {
            { "roomID", RoomID },
            { "team", Team },
        };
    }
}
