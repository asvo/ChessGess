//***************
// name : ASystem
// createtime: 5/1/2019
// desc: 
// create by asvo
//***************
using System;
using System.Collections.Generic;

namespace Asvo.ECS
{
	public class ASystem {
		
		//TODO, str compare -> change to -> int compare, for effection
		protected int m_hashCode;
		public string Name {get; private set;}
		public Type Type {get; private set;}
		public float m_deltaTime;

		protected ASystemProcess m_process;
		public ASystemProcess Process{
			get{return m_process;}
		}

		public ASystem()
		{
			Type = GetType();
			Name = Type.Name;
            RegisterSignals();
        }

		protected virtual void RegisterSignals(){}
		protected virtual void UnregisterSignals(){}
		
		public virtual void UpdateAddEntity(CEntity entity){}
		public virtual void UpdateRemoveEntity(CEntity entity){}

		public virtual void PreUpdate(){}
		public virtual void Update(CEntity entity){}
		public virtual void PostUpdate(){}

		public virtual void Destroy()
		{
			UnregisterSignals();
			m_process = null;
		}
	}
}
