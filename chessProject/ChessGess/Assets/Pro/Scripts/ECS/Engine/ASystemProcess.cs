//***************
// name : ASystemProcess
// createtime: 5/2/2019
// desc: 
// create by asvo
//***************
using System;
using System.Collections.Generic;

namespace Asvo.ECS
{
	public class ASystemProcess {
		
		protected ASystem m_system;
		protected List<Type> m_compoTypes;
		public CEntityList EntityList {get; protected set;}

		public ASystemProcess(ASystem system, params Type[] components)
		{
			m_system = system;
			m_compoTypes = new List<Type>();
			m_compoTypes.AddRange(components);
			EntityList = new CEntityList();
		}
		
		public virtual void AddEntity(CEntity entity){}
		public virtual void RemoveEntity(CEntity entity){}		
		public void Process(float deltaT){
            m_system.m_deltaTime = deltaT;
            m_system.PreUpdate();
            for(var node = EntityList.head;node != null;node = node.next)
            {
                m_system.Update(node.Entity);
            }
            m_system.PostUpdate();
        }
        
		public void MatchAdd(CEntity entity)
		{
            if (EntityList.HasEntity(entity))
            {
                bool isEntityDeled = EntityList.RemoveEntity(entity);
                if (entity.HasComponents(m_compoTypes))
                {
                    EntityList.AddEntity(entity);
                }
                if (m_system != null)
                {
                    m_system.UpdateRemoveEntity(entity);
                }
            }
            if (entity.HasComponents(m_compoTypes))
            {
                EntityList.AddEntity(entity);

                if (m_system != null)
                {
                    m_system.UpdateAddEntity(entity);
                }
            }
        }

		public void MatchRemove(CEntity entity)
		{
            if (EntityList.HasEntity(entity))
            {
                bool isEntityDeled = EntityList.RemoveEntity(entity);
                if (m_system != null)
                {
                    m_system.UpdateRemoveEntity(entity);
                }
            }
		}
	}
}
