//***************
// name : ECSException
// createtime: 5/2/2019
// desc: 
// create by asvo
//***************

namespace Asvo.ECS
{
	public class NotFindSystemException : System.Exception
	{
		private string m_sysName;
		public NotFindSystemException(string systemName)
		{
			m_sysName = systemName;			
		}
		
		public override string Message { 
			get{
				return string.Format("Not find System {0}", m_sysName);
			}
		}
	}
}
