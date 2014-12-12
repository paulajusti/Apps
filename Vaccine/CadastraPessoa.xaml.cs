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

namespace Vaccine
{
    public partial class CadastraPessoa : PhoneApplicationPage
    {
        public Pessoas Pessoa { get; set; }

        //=================================== construtor com botão desabilitado ===================================
        public CadastraPessoa()
        {
            InitializeComponent();
            btnCadastrar.IsEnabled = false;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Pessoa != null)
            {
                btnCadastrar.IsEnabled = true;
                txbId.Text = Pessoa.Id.ToString();
                txtNome.Text = Pessoa.Nome;
            }
        }

        //===================== método para ativar botão somente quando algum dado for digitado =====================
        private void tbNome_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text != string.Empty)
            {
                btnCadastrar.IsEnabled = true;
            }
            else
                btnCadastrar.IsEnabled = false;
        }

        //======================================= método do botão de cadastro =======================================
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            Pessoas pessoa = new Pessoas();

            if (txbId.Text != "")
            {
                pessoa.Id = Convert.ToInt32(txbId.Text);
                pessoa.Nome = txtNome.Text;
            }
            else
            {
                pessoa.Nome = txtNome.Text;
            }

            if (Pessoa != null)
            {
                PessoasDB.Atualizar(pessoa);
            }
            else
            {
                PessoasDB.Salvar(pessoa);
            }
            MessageBox.Show("Cadastro " + pessoa.Nome + " salvo com sucesso!");

            NavigationService.GoBack();
        }
    }
}