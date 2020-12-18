﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Utilities.Mvvm;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class SceneViewModel : BindableBase
    {

        public SceneViewModel()
        {
            Components = new List<ComponentViewModel>();
            Size = new Vector2(100, 100);
        }

        #region Properties

        public List<ComponentViewModel> Components { get; set; }

        public Vector2 Size { get; set; }

        #endregion
    }
}
