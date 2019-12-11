﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Radical.Windows.Presentation.ComponentModel.Regions;

namespace Radical.Samples.Presentation.TabControlRegion
{
    /// <summary>
    /// Interaction logic for BetaView.xaml
    /// </summary>
    [InjectViewInRegion(Named = "MainTabRegion")]
    public partial class BetaView : UserControl
    {
        public BetaView()
        {
            InitializeComponent();
        }
    }
}
