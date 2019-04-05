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
            //¿QUE HACER CUANDO HAY MUESRTAS DISPONIBLES?
            waveIn.DataAvailable += WaveIn_DataAvailable;

            waveIn.StartRecording();
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;
            int bytesGrabados = e.BytesRecorded;
            float acumulador = 0.0f;
            for (int i = 0; i < bytesGrabados; i += 2)
            {
                //Transformando 2 bytes separados en una muestra de 16 bits
                //1.- Toma el segundo byte y el antepone 8 0's L PRINCIPIO
                //2.- Hace un "OR" con el primer byte, al cual automaticamente se le llenan 8 0's al final
                short muestra = (short)(buffer[i + 1] << 8 | buffer[i]);
                float muestra32Bits = (float)muestra / 32768.0f;
                acumulador += Math.Abs(muestra32Bits);

            }
            float promedio = acumulador / (bytesGrabados / 2.0f);
            sld_Microfono.Value = (double)promedio;

        }

        private void Btn_Detener_Click(object sender, RoutedEventArgs e)
        {
            waveIn.StopRecording();
        }
    }
}
