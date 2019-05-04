//***************
// name : ISingelton
// createtime: 5/1/2019
// desc: 
// create by asvo
//***************

namespace Asvo.Common
{
	public abstract class ISingelton<T> where T : class, new() {
		private readonly static object s_lockObj = new object();
		private static T s_intance = null;

		public static T Instance{
			get{				
				if (s_intance == null)
				{
					lock (s_lockObj)	
					{
						if (s_intance == null)
							s_intance = new T();
					}
				}
				return s_intance;
			}
		}
	}
}
