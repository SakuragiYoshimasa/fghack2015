using System.Collections.Generic;

public abstract class CondBase
{
    public class Response
    {
        public CondBase Cond { get; private set; }

        public Response(CondBase cond)
        {
            Cond = cond;
        }
    }

    public CondBase(bool reliable = false, bool retryable = true)
    {
    }

    public Dictionary<string, object> GetParams()
    {
        Dictionary<string, object> p = CreateParams();
        if (p == null)
        {
            p = new Dictionary<string, object>();
        }

        return p;
    }

    protected virtual Dictionary<string, object> CreateParams()
    {
        return null;
    }
}
