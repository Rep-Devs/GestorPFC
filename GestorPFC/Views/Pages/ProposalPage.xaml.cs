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

namespace GestorPFC.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para ProposalPage.xaml
    /// </summary>
    public partial class ProposalPage : INavigableView<ProposalViewModel>
    {
        public ProposalViewModel ViewModel { get; }

        public ProposalPage(ProposalViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            viewModel.OnPageLoaded();
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            var propuestaSeleccionada = ViewModel.PropuestaSeleccionada;

            if (propuestaSeleccionada != null)
            {

                MessageBox.Show($"Título: {propuestaSeleccionada.Titulo}\n" +
                                $"Descripción: {propuestaSeleccionada.Descripcion}\n" +
                                $"Departamento: {propuestaSeleccionada.Departamento}\n" +
                                $"Es Proyecto: {propuestaSeleccionada.BooleanProyecto}\n" +
                                $"Alumno ID: {propuestaSeleccionada.AlumnoId}");
            }
        }
    }
    
}
