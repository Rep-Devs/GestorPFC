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
    /// Lógica de interacción para ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : INavigableView<ProjectViewModel>
    {
        public ProjectViewModel ViewModel { get; }

        public ProjectPage(ProjectViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            viewModel.OnPageLoaded();
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Verifica si un proyecto ha sido seleccionado
            var proyectoSeleccionado = ViewModel.ProyectoSeleccionado;

            if (proyectoSeleccionado != null)
            {
                // Muestra los detalles del proyecto en un MessageBox
                MessageBox.Show($"Proyecto: {proyectoSeleccionado.Titulo}\n" +
                                $"Descripción: {proyectoSeleccionado.Descripcion}\n" +
                                $"Estado: {proyectoSeleccionado.EstadoProyecto}\n" +
                                $"Fecha de Entrega: {proyectoSeleccionado.FechaEntrega.ToString("dd/MM/yyyy")}\n" +
                                $"Departamento ID: {proyectoSeleccionado.DepartamentoId}\n" +
                                $"Alumno ID: {proyectoSeleccionado.AlumnoId}\n" +
                                $"Tutor ID: {proyectoSeleccionado.TutorProyectoId}");
            }
        }
    }
}
