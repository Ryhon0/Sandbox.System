namespace Sandbox.System.Diagnostics
{
	public sealed class Trace
	{
		public static void WriteLine(string s, params object[] os)
		{
			Log.Trace(string.Format(s, os));
		}
	}
}
