﻿using Cyvasse.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Cyvasse {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            MainViewModel context = new MainViewModel();
            MainView app = new MainView();
            app.DataContext = context;
            app.Show();
        }
    }
}