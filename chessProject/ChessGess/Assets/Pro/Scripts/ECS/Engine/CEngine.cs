//***************
// name : CEngine
// createtime: 5/1/2019
// desc: 
// create by asvo
//***************
using System.Collections.Generic;
using Asvo.Common;

namespace Asvo.ECS
{
	public class CEngine : ISingelton<CEngine>{

		private Dictionary<ulong, CEntity> m_entityDict;
		private APhase m_logicPhase;
		private APhase m_physicPhase;
		private List<APhase> m_phases;
		
		public CEngine(){			
			m_entityDict = new Dictionary<ulong, CEntity>();
			m_phases = new List<APhase>();
			m_logicPhase = new APhase();
			m_phases.Add(m_logicPhase);
			m_physicPhase = new APhase();
			m_phases.Add(m_physicPhase);
		}

		public CEntity CreateEntity(List<AComponent> componentList)
		{
			var entity = new CEntity();
			entity.InitComponents(componentList);
			return entity;
		}

		public void AddEntity(CEntity entity)
		{
			foreach(var phase in m_phases)
			{
				phase.OnAddEntity(entity);
			}		
			m_entityDict.Add(entity.BaseId, entity);
		}
		
		public void RemoveEntity(CEntity entity)
		{
			foreach(var phase in m_phases)
			{
				phase.OnRemoveEntity(entity);
			}
			m_entityDict.Remove(entity.BaseId);
            entity.Destroy();
			//entity.rest() to pool
		}

		public void RemoveAllEntitys()
		{
			List<ulong> deleteList = new List<ulong>(m_entityDict.Keys);
            foreach (var pair in m_entityDict)
            {
                deleteList.Add(pair.Key);
            }
            for (int i = 0; i < deleteList.Count; ++i)
            {
                CEntity entity = null;
                if (m_entityDict.TryGetValue(deleteList[i], out entity))
                    RemoveEntity(entity);
            }
		}

		internal void MatchSystemsAddComponent(CEntity entity)
        {
            for (int i = 0; i < m_phases.Count; ++i)
            {
                m_phases[i].OnAddEntity(entity);
            }
        }

		internal void MatchSystemsRemoveComponent(CEntity entity)
        {
            for (int i = 0; i < m_phases.Count; ++i)
            {
                m_phases[i].OnRemoveEntity(entity);
            }
        }
		
		public void AddLogicSys(ASystem system)
		{
			m_logicPhase.AddSystem(system);
			InternalAddSystem(system);
		}

		public void AddPhysicSys(ASystem system)
		{
			m_physicPhase.AddSystem(system);
			InternalAddSystem(system);
		}
		
		protected void InternalAddSystem(ASystem system)
		{
			foreach(var entityPair in m_entityDict)
			{
				system.Process.MatchAdd(entityPair.Value);
			}
		}

		public void Update(float deltaT)
		{
			m_logicPhase.Update(deltaT);
		}
		
		public void FixUpdate(float deltaT)
		{
			m_physicPhase.Update(deltaT);
		}

		public CEntity FindEntityByVaseID(uint entityBaseId)
		{
			CEntity entity = null;
			m_entityDict.TryGetValue(entityBaseId, out entity);
			return entity;
		}

		public T GetSystem<T>() where T : ASystem
		{
			foreach(var phase in m_phases)
			{
				foreach(var sys in phase.Systems)
				{
					var t = sys as T;
					if (null != t)
						return t;
				}
			}
			return null;
		}
	}
}



#if XXXXX_YYYY
public class Pool<T> where T : class, new() {
		public readonly int max;
		readonly Stack<T> freeObjects;

		public int Count { get { return freeObjects.Count; } }
		public int Peak { get; private set; }

		public Pool (int initialCapacity = 16, int max = int.MaxValue) {
			freeObjects = new Stack<T>(initialCapacity);
			this.max = max;
		}

		public T Obtain () {
			return freeObjects.Count == 0 ? new T() : freeObjects.Pop();
		}

		public void Free (T obj) {
			if (obj == null) throw new ArgumentNullException("obj", "obj cannot be null");
			if (freeObjects.Count < max) {
				freeObjects.Push(obj);
				Peak = Math.Max(Peak, freeObjects.Count);
			}
			Reset(obj);
		}

//		protected void FreeAll (List<T> objects) {
//			if (objects == null) throw new ArgumentNullException("objects", "objects cannot be null.");
//			var freeObjects = this.freeObjects;
//			int max = this.max;
//			for (int i = 0; i < objects.Count; i++) {
//				T obj = objects[i];
//				if (obj == null) continue;
//				if (freeObjects.Count < max) freeObjects.Push(obj);
//				Reset(obj);
//			}
//			Peak = Math.Max(Peak, freeObjects.Count);
//		}

		public void Clear () {
			freeObjects.Clear();
		}

		protected void Reset (T obj) {
			var poolable = obj as IPoolable;
			if (poolable != null) poolable.Reset();
		}

		public interface IPoolable {
			void Reset ();
		}
	}

#endif