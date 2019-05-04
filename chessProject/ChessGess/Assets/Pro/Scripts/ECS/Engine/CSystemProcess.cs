//***************
// name : CSystemProcess
// createtime: 5/2/2019
// desc: 
// create by asvo
//***************

using System;

namespace Asvo.ECS
{
    public class CSystemProcess : ASystemProcess
    {
        public CSystemProcess(ASystem system, params Type[] components) : base(system, components)
        {
        }

		
    }
}
