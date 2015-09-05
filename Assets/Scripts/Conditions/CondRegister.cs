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

	public double Latitde{get; private set;}

	public double Longitude{get; private set;}

    public CondRegister(int roomID, int team,double lat,double longi)
        : base(true, true)
    {
        RoomID = roomID;
        Team = team;
		Latitde = lat;
		Longitude = longi;

    }

    protected override Dictionary<string, object> CreateParams()
    {
        return  new Dictionary<string, object>()
        {
            { "roomID", RoomID },
            { "team", Team },
			{"lat",Latitde},
			{"lon",Longitude}
        };
    }
}
