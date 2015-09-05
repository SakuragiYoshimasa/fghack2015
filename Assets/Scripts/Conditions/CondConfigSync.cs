public class CondConfigSync : CondBase
{
	public class R : CondBase.Response
	{
		public int GameTime { get; private set; }

		public R (
			int gameTime,
			CondBase cond
		)
			: base (cond)
		{
			GameTime = gameTime;
		}
	}

	public CondConfigSync ()
	{
	}
}
