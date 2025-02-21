﻿using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<KochPainter>().ToSelf().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<Form>().To<MainForm>();
                container.Bind<IUiAction>().To<SaveImageAction>();
                container.Bind<IUiAction>().To<DragonFractalAction>();
                container.Bind<IUiAction>().To<KochFractalAction>();
                container.Bind<IUiAction>().To<ImageSettingsAction>();
                container.Bind<IUiAction>().To<PaletteSettingsAction>();
                //container.Bind<IImageSettingsProvider>().To<>();
                //container.Bind<IDragonPainterFactory>().ToFactory();
                Application.Run(container.Get<Form>());

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}