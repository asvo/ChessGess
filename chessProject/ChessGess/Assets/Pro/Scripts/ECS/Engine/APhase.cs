//***************
// name : APhase
// createtime: 5/2/2019
// desc: 
// create by asvo
//***************
using System.Collections.Generic;

namespace Asvo.ECS
{
	public class APhase {
		private List<ASystem> m_systems = new List<ASystem>();
		public List<ASystem> Systems{
			get{
				return m_systems;
			}
		}

		public void AddSystem(ASystem system)
		{
			m_systems.Add(system);
		}

		public void RemoveSystem(ASystem system)
		{
			m_systems.Remove(system);
		}

		public T FindSystem<T>() where T : ASystem
		{		
			T t = null;
			foreach(var sys in m_systems)
			{
				t = sys as T;
				if (null != t)
				{
					break;
				}
			}
			return t;
		}

		public bool InsertBefore(ASystem system, ASystem beforeSys)
		{
			bool hasFind = false;
			for(int i = 0; i < m_systems.Count; ++i)
			{
				if (m_systems[i].Name == beforeSys.Name)
				{
					hasFind = true;
					m_systems.Insert(i, system);					
					break;
				}
			}
			if (!hasFind)
			{
				throw new NotFindSystemException(beforeSys.Name);
			}
			return hasFind;
		}

		public bool InsertAfter(ASystem system, ASystem afterSys)
		{
			bool hasFind = false;
			for(int i = 0; i < m_systems.Count; ++i)
			{
				if (m_systems[i].Name == afterSys.Name)
				{
					hasFind = true;
					m_systems.Insert(i+1, system);
					break;
				}
			}
			if (!hasFind)
			{
				throw new NotFindSystemException(afterSys.Name);
			}
			return hasFind;
		}

		public void OnAddEntity(CEntity entity)
		{
			foreach(var system in m_systems)
			{
				system.Process.MatchAdd(entity);
			}
		}

		public void OnRemoveEntity(CEntity entity)
		{
			foreach(var system in m_systems)
			{
				system.Process.MatchRemove(entity);
			}
		}
		
		public void Update(float deltaT)
		{
			//TODO,注意该循环里面可能有system被移除的逻辑，会导致迭代器失效。
			//TODO，以后改成自定义的collection.
			foreach(var system in m_systems)
			{
				system.Process.Process(deltaT);
			}
		}
	}
}
