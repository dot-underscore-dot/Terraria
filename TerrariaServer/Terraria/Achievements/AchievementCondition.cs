﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.AchievementCondition
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.Achievements
{
  [JsonObject]
  public abstract class AchievementCondition
  {
    public readonly string Name;
    protected IAchievementTracker _tracker;
    [JsonProperty("Completed")]
    private bool _isCompleted;

    public bool IsCompleted
    {
      get
      {
        return this._isCompleted;
      }
    }

    public event AchievementCondition.AchievementUpdate OnComplete;

    protected AchievementCondition(string name)
    {
      this.Name = name;
    }

    public virtual void Load(JObject state)
    {
      this._isCompleted = JToken.op_Explicit(state.get_Item("Completed"));
    }

    public virtual void Clear()
    {
      this._isCompleted = false;
    }

    public virtual void Complete()
    {
      if (this._isCompleted)
        return;
      this._isCompleted = true;
      // ISSUE: reference to a compiler-generated field
      if (this.OnComplete == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.OnComplete(this);
    }

    protected virtual IAchievementTracker CreateAchievementTracker()
    {
      return (IAchievementTracker) null;
    }

    public IAchievementTracker GetAchievementTracker()
    {
      if (this._tracker == null)
        this._tracker = this.CreateAchievementTracker();
      return this._tracker;
    }

    public delegate void AchievementUpdate(AchievementCondition condition);
  }
}
