using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Vaccine.Resources;
using Microsoft.Phone.Tasks;
using Vaccine.Classes;
using System.IO.IsolatedStorage;

namespace Vaccine
{
    public partial class MainPage : PhoneApplicationPage
    {
        Pessoas pessoa;
        public MainPage()
        {
            InitializeComponent();
        }

        private IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            AtualizarLista();
        }

        //================================ método que adiciona nova pessoa ================================
        private void onClickAdd(object sender, EventArgs e)
        {
            this.lstPessoas.SelectedIndex = -1;
            CadastraPessoa();
        }

        private void CadastraPessoa()
        {
            NavigationService.Navigate(new Uri("/CadastraPessoa.xaml", UriKind.Relative));
        }

        //================================== método que atualiza a lista ==================================
        private void AtualizarLista()
        {
            List<Pessoas> lista = PessoasDB.GetPessoas(null);
            lstPessoas.ItemsSource = lista;
        }

        //============================= método que edita a pessoa selecionada =============================
        private void OnClick_Editar(object sender, RoutedEventArgs e)
        {
            if (pessoa != null)
            {
                NavigationService.Navigate(new Uri("/CadastraPessoa.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma pessoa para editar!");
            }
        }

        //============================= método que deleta a pessoa selecionada ============================
        private void OnClick_Excluir(object sender, RoutedEventArgs e)
        {
            if (pessoa != null)
            {
                if (MessageBox.Show("Deletar " + pessoa.Nome + "?", "Atenção!",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    PessoasDB.Deletar(pessoa);
                    AtualizarLista();
                }
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma pessoa para deletar!");
            }
        }

        //======================= método que mostra as vacinas da pessoa selecionada ======================
        private void OnClickMostrar(object sender, EventArgs e)
        {
            if (pessoa != null)
            {
                NavigationService.Navigate(new Uri("/PivotVacinas.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Você deve selecionar uma pessoa para exibir as vacinas!");
            }
        }

        private void OnSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            pessoa = (sender as ListBox).SelectedItem as Pessoas;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            CadastraPessoa page = e.Content as CadastraPessoa;
            if (page != null)
                page.Pessoa = pessoa;

            PivotVacinas page2 = e.Content as PivotVacinas;
            if (page2 != null)
                page2.pessoa = pessoa;
        }

        //========================== método para desmarcar pessoa selecionada ===========================
        private void OnTap_Desmcar(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.lstPessoas.SelectedIndex = -1;
        }


        //=============================== para pedir para usuário avaliar ===============================
        int acessos = 0;
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (this.iso.Contains("login.Acesso"))
            {
                int acesso = 0;
                if (this.iso.TryGetValue<int>("login.Acesso", out acesso))
                {
                    this.acessos = acesso + 1;
                    this.iso["login.Acesso"] = acessos;

                    if (this.acessos == 3)
                    {
                        if (MessageBox.Show("Por favor, avalie nosso aplicativo, é rapidinho! =) \nSe possível deixe um comentário também!\n\n\nAgradecemos desde já por tudo!", "EQUIPE CARTEIRA DE VACINAÇÃO", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            MarketplaceReviewTask like = new MarketplaceReviewTask();
                            like.Show();
                        }
                        else
                        {
                            this.acessos = 2;
                            this.iso["login.Acesso"] = acessos;
                            iso.Save();
                        }
                    }
                }
            }
            else
            {
                this.iso.Add("login.Acesso", 1);
            }
            this.iso.Save();
        }

        private void txbPesquisarNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Pessoas> lista = PessoasDB.GetPessoas(txbPesquisarNome.Text);
            lstPessoas.ItemsSource = lista;
        }

    }
}