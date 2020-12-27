﻿using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;
using Microsoft.AspNetCore.Components;
using Rectangle = Aptacode.Geometry.Primitives.Polygons.Rectangle;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class IndexBase : ComponentBase
    {
        public DemoSceneController SceneController { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var sceneBuilder = new SceneBuilder();
            var componentBuilder = new ComponentBuilder();

            sceneBuilder.SetWidth(1000).SetHeight(1000);

            var polygon = componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(1).Build();

            polygon.Scale(new Vector2(2.0f));
            polygon.Rotate(1.7f);
            polygon.Translate(new Vector2(100, 0));


            sceneBuilder.AddComponent(polygon);

            sceneBuilder.AddComponent(componentBuilder.SetPrimitive(Rectangle.Create(400, 100, 75, 75))
                .SetFillColor(Color.Blue).SetBorderThickness(1).Build());

            sceneBuilder.AddComponent(componentBuilder.SetPrimitive(Rectangle.Create(100, 100, 100, 100))
                .SetFillColor(Color.Red).SetBorderThickness(1).Build());

            sceneBuilder.AddComponent(componentBuilder.SetPrimitive(PolyLine.Create(80, 40, 70, 80, 60, 20))
                .SetFillColor(Color.Blue).SetBorderThickness(1).Build());
            sceneBuilder.AddComponent(componentBuilder.SetPrimitive(Ellipse.Create(80, 80, 15, 10, (float) Math.PI))
                .SetFillColor(Color.Orange).SetBorderThickness(1).Build());
            sceneBuilder.AddComponent(componentBuilder.SetPrimitive(Ellipse.Create(180, 180, 15, 10, (float) Math.PI))
                .SetFillColor(Color.Orange).SetBorderThickness(1).Build());
            var scene = sceneBuilder.Build();

            SceneController = new DemoSceneController(scene);

            await base.OnInitializedAsync();
        }
    }
}