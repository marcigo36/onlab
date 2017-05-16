﻿using onlab;
using System;
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

namespace onlabGUI
{
    /// <summary>
    /// Interaction logic for BondOptionInput.xaml
    /// </summary>
    public partial class BondOptionInput : UserControl
    {
        public BondOptionInput()
        {
            InitializeComponent();
        }

        private void typecombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bo = this.DataContext as BondOption;
            //this is very ugly, it should be solved with binding and dataconverter..
            bo.OptionType = (BondOption.Type)typecombobox.SelectedIndex;
        }
    }
}
