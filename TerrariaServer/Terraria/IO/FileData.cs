﻿// Decompiled with JetBrains decompiler
// Type: Terraria.IO.FileData
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Terraria.Utilities;

namespace Terraria.IO
{
  public abstract class FileData
  {
    protected string _path;
    protected bool _isCloudSave;
    public FileMetadata Metadata;
    public string Name;
    public readonly string Type;
    protected bool _isFavorite;

    public string Path
    {
      get
      {
        return this._path;
      }
    }

    public bool IsCloudSave
    {
      get
      {
        return this._isCloudSave;
      }
    }

    public bool IsFavorite
    {
      get
      {
        return this._isFavorite;
      }
    }

    protected FileData(string type)
    {
      this.Type = type;
    }

    protected FileData(string type, string path, bool isCloud)
    {
      this.Type = type;
      this._path = path;
      this._isCloudSave = isCloud;
      this._isFavorite = (isCloud ? Main.CloudFavoritesData : Main.LocalFavoriteData).IsFavorite(this);
    }

    public void ToggleFavorite()
    {
      this.SetFavorite(!this.IsFavorite, true);
    }

    public string GetFileName(bool includeExtension = true)
    {
      return FileUtilities.GetFileName(this.Path, includeExtension);
    }

    public void SetFavorite(bool favorite, bool saveChanges = true)
    {
      this._isFavorite = favorite;
      if (!saveChanges)
        return;
      (this.IsCloudSave ? Main.CloudFavoritesData : Main.LocalFavoriteData).SaveFavorite(this);
    }

    public abstract void SetAsActive();

    public abstract void MoveToCloud();

    public abstract void MoveToLocal();
  }
}
