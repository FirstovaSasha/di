﻿using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private KochPainter kochPainter;

        public KochFractalAction(KochPainter kochPainter)
        {
            this.kochPainter = kochPainter;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            //var container = new StandardKernel();
            //container.Bind<IImageHolder>().ToConstant(imageHolder);
            //container.Bind<Palette>().ToConstant(palette);
            //container.Get<KochPainter>().Paint();
            kochPainter.Paint();
        }
    }
}