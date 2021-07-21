using System.IO;
using System.Text;

namespace Sandbox.System
{
	public static class Console
	{
		public static void WriteLine(object o)
		{
			Log.Info(o);
		}

		public static TextWriter Error => new ErrorWriter();
	}

	class ErrorWriter : TextWriter
	{
		public override Encoding Encoding => Encoding.UTF8;

		public override void WriteLine(object obj)
		{
			Log.Error(obj);
		}
	}
}
