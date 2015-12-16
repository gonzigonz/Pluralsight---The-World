using System.Diagnostics;

namespace TheWorld.Services
{
	public class DebugMailService : IMailService
	{
		public bool SendMail(string to, string from, string subject, string body)
		{
			Debug.WriteLine($"Sending mail: to {to}, Subject: {subject}");
			return true;
		}
	}
}
