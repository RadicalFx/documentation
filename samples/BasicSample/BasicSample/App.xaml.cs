﻿using System.Windows;

namespace BasicSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            this.AddRadicalApplication<Presentation.MainView>();
        }
    }
}
