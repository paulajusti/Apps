using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Vaccine.Classes;
using System.Globalization;

namespace Vaccine
{
    public partial class PivotVacinas : PhoneApplicationPage
    {
        public Pessoas pessoa;
        public VacinasFeitas vacinasFeitas;
        public ProximasVacinas proximasVacinas;

        public PivotVacinas()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-br");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-br");
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            AtualizarListaVacinasFeitas();
            AtualizarListaProximasVacinas();
        }

        //============================= método que adiciona nova vacina feita =============================
        private void onClickAddVacinaFeita(object sender, EventArgs e)
        {
            this.lstVacinasFeitas.SelectedIndex = -1;
            CadastraVacinaFeita();
        }

        private void CadastraVacinaFeita()
        {
            NavigationService.Navigate(new Uri("/cadastraVacinasFeitas.xaml", UriKind.Relative));
        }

        //================================== método que atualiza a lista ==================================
        private void AtualizarListaVacinasFeitas()
        {
            List<VacinasFeitas> lista = VacinasFeitasDB.GetVacinasFeitas(pessoa.Id);
            lstVacinasFeitas.ItemsSource = lista;
        }

        //============================= método que edita a vacina selecionada =============================
        private void OnClick_EditarVacinaFeita(object sender, RoutedEventArgs e)
        {
            if (vacinasFeitas != null)
            {
                NavigationService.Navigate(new Uri("/cadastraVacinasFeitas.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma vacina para editar!");
            }
        }

        //============================= método que deleta a vacina selecionada ============================
        private void OnClick_ExcluirVacinaFeita(object sender, RoutedEventArgs e)
        {
            if (vacinasFeitas != null)
            {
                if (MessageBox.Show("Deletar registro?", "Atenção!",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    VacinasFeitasDB.Deletar(vacinasFeitas);
                    AtualizarListaVacinasFeitas();
                }
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma vacina para deletar!");
            }
        }

        private void OnSelectionChangeVacinasFeitas(object sender, SelectionChangedEventArgs e)
        {
            vacinasFeitas = (sender as ListBox).SelectedItem as VacinasFeitas;
        }

        //========================== método para desmarcar vacina selecionada ===========================
        private void OnTap_DesmcarVacinaFeita(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.lstVacinasFeitas.SelectedIndex = -1;
        }

        private void txbPesquisarVacinaFeita_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<VacinasFeitas> lista = VacinasFeitasDB.GetSearchVacinasFeitas(txbPesquisarVacinaFeita.Text);
            lstVacinasFeitas.ItemsSource = lista;
        }


        //================================================================ PRÓXIMAS VACINAS ==========================================================

        //============================= método que adiciona nova vacina feita =============================
        private void onClickAddProximaVacina(object sender, EventArgs e)
        {
            this.lstProximasVacinas.SelectedIndex = -1;
            CadastraProximaVacina();
        }

        private void CadastraProximaVacina()
        {
            NavigationService.Navigate(new Uri("/cadastraProximasVacinas.xaml", UriKind.Relative));
        }

        //================================== método que atualiza a lista ==================================
        private void AtualizarListaProximasVacinas()
        {
            List<ProximasVacinas> lista = ProximasVacinasDB.GetProximasVacinas(pessoa.Id);
            lstProximasVacinas.ItemsSource = lista;
        }

        //============================= método que edita a vacina selecionada =============================
        private void OnClick_EditarProximaVacina(object sender, RoutedEventArgs e)
        {
            if (proximasVacinas != null)
            {
                NavigationService.Navigate(new Uri("/cadastraProximasVacinas.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma vacina para editar!");
            }
        }

        //============================= método que deleta a vacina selecionada ============================
        private void OnClick_ExcluirProximaVacina(object sender, RoutedEventArgs e)
        {
            if (proximasVacinas != null)
            {
                if (MessageBox.Show("Deletar registro?", "Atenção!",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    ProximasVacinasDB.Deletar(proximasVacinas);
                    AtualizarListaProximasVacinas();
                }
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma vacina para deletar!");
            }
        }

        private void OnSelectionChangeProximasVacinas(object sender, SelectionChangedEventArgs e)
        {
            proximasVacinas = (sender as ListBox).SelectedItem as ProximasVacinas;
        }

        //========================== método para desmarcar vacina selecionada ===========================
        private void OnTap_DesmcarProximaVacina(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.lstProximasVacinas.SelectedIndex = -1;
        }

        //================================= movimentar vacina de page ====================================
        private void OnClick_MoverParaVacinasFeitas(object sender, RoutedEventArgs e)
        {
            if (proximasVacinas != null)
            {
                if (MessageBox.Show("Mover registro?", "Atenção!",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    VacinasFeitas vac = new VacinasFeitas
                    {
                        Id = proximasVacinas.Id,
                        idPessoa = proximasVacinas.idPessoa,
                        NomeVacinaFeita = proximasVacinas.NomeProximaVacina,
                        dataVacinaFeita = proximasVacinas.dataProximaVacina,
                        loteVacinaFeita = "",
                        localVacinaFeita = "",
                        reacaoVacinaFeita = "",
                        dataAgora = proximasVacinas.dataAgora
                    };
                  //  NavigationService.Navigate(new Uri("/cadastraVacinasFeitas.xaml?NomeVacinaFeita=" + proximasVacinas.NomeProximaVacina + "&dataVacinaFeita=" + proximasVacinas.dataProximaVacina, UriKind.Relative));
                    VacinasFeitasDB.Salvar(vac);
                    AtualizarListaVacinasFeitas();
                    ProximasVacinasDB.Deletar(proximasVacinas);
                    AtualizarListaProximasVacinas();
                    MessageBox.Show("Vacina transferida com sucesso! Não se esqueça de atualizar os campos!");
                }
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma vacina para mover!");
            }
        }

        private void txbPesquisarProximaVacina_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ProximasVacinas> lista = ProximasVacinasDB.GetSearchProximasVacinas(txbPesquisarProximaVacina.Text);
            lstProximasVacinas.ItemsSource = lista;
        }


        //================================================================ COMUM NAS PAGES ==========================================================

        //====================================== application bar =========================================
        private void PivotMain_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as Pivot).SelectedIndex)
            {
                case 0:
                    this.ApplicationBar = this.Resources["AppBar1"] as ApplicationBar;
                    break;
                case 1:
                    this.ApplicationBar = this.Resources["AppBar2"] as ApplicationBar;
                    break;
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            CadastraVacinasFeitas page = e.Content as CadastraVacinasFeitas;
            CadastraProximasVacinas page2 = e.Content as CadastraProximasVacinas;
            if (page != null)
            {
                page.Vacinasfeitas = vacinasFeitas;
                page.Pessoa = pessoa;
            }
            if (page2 != null)
            {
                page2.Proximasvacinas = proximasVacinas;
                page2.Pessoa = pessoa;
            }
        }
    }
}