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

using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.Dsp;

namespace Entrada
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WaveIn waveIn;

        public MainWindow()
        {
            InitializeComponent();
            LlenrComboDispositivo();
        }
        public void LlenrComboDispositivo()
        {
            for (int i=0; i<WaveIn.DeviceCount; i++)
            {
                WaveInCapabilities capacidades = WaveIn.GetCapabilities(i);
                cb_dispositivos.Items.Add(capacidades.ProductName);
            }
            cb_dispositivos.SelectedIndex = 0;
        }

        private void Btn_Iniciar_Click(object sender, RoutedEventArgs e)
        {
            waveIn = new WaveIn();
            //FORMATO DE AUDIO
            waveIn.WaveFormat = new WaveFormat(44100, 16, 1);
            //BUFFER
            waveIn.BufferMilliseconds = 250;
            //
        }
    }
}
