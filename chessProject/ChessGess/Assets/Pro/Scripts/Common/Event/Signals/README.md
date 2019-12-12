# Retromono.Signal

A generic library without any dependencies intended for use with [Monogame](http://www.monogame.net/). Retromono.Signal provides Signal classes which behave almost like C#'s native events with a couple notable exceptions:
 - You don't have to check for null
 - A priority can be assigned to control the order in which the listeners are called regardless of the order they were added
 - Adding listeners with a context so that later all of the listeners with that context can be removed easily
 - Adding listeners without any params to signals which take params when you know you don't need them

## How to use
```csharp
var signal = new Signal<int>();
signal.AddListener(param => Debug.WriteLine(param));
signal.Dispatch(50);
```

```csharp
int roleID = 1002;
CSkill skill = role.skill;
var signal = new Signal<int, CSkill>();
signal.AddListener(OnReleaseSkill);
signal.Dispatch(roleID,skill);


private void OnReleaseSkill(int id,CSkill skill)
{
	print("角色{0}释放技能{1}",id,skill.name);
}
```