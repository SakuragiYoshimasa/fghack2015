using System.Collections.Generic;

public class CondStart : CondBase
{
    public class R : CondBase.Response
    {
        public R(CondBase cond)
            : base(cond)
        {
        }
    }

    public CondStart()
        : base(true, true)
    {
    }
}
