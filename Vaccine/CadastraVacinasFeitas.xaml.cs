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
    public partial class CadastraVacinasFeitas : PhoneApplicationPage
    {
        #region propriedades
        public Pessoas Pessoa { get; set; }
        public VacinasFeitas Vacinasfeitas { get; set; }

        #endregion

        #region Metodos

        //=================================== construtor com botão desabilitado ===================================
        public CadastraVacinasFeitas()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-br");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-br");
            InitializeComponent();
            btnCadastrar.IsEnabled = false;
        }

        #endregion

        #region Eventos
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Vacinasfeitas != null)
            {
                btnCadastrar.IsEnabled = true;
                txbId.Text = Vacinasfeitas.Id.ToString();
                txtNome.Text = Vacinasfeitas.NomeVacinaFeita.ToString();
                Vacinasfeitas.dataAgora = DateTime.Now;
                Vacinasfeitas.dataVacinaFeita = (DateTime)dtpData.Value;
                txtLote.Text = Vacinasfeitas.loteVacinaFeita.ToString();
                txtLocal.Text = Vacinasfeitas.localVacinaFeita.ToString();
                txtSintomas.Text = Vacinasfeitas.reacaoVacinaFeita.ToString();
            }

        }

        //=================== método para ativar botão somente quando os dois botões tiverem dados ===================
        private void tbNome_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text != string.Empty)
            {
                btnCadastrar.IsEnabled = true;
            }
            else
                btnCadastrar.IsEnabled = false;
        }

        //======================================== método do botão de cadastro ========================================
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            VacinasFeitas vacinafeita = new VacinasFeitas();

            if (txbId.Text != "")
            {
                vacinafeita.Id = Convert.ToInt32(txbId.Text);
                vacinafeita.idPessoa = Pessoa.Id;
                vacinafeita.NomeVacinaFeita = txtNome.Text;
                vacinafeita.dataAgora = DateTime.Now;
                vacinafeita.dataVacinaFeita = (DateTime)dtpData.Value;
                vacinafeita.loteVacinaFeita = txtLote.Text;
                vacinafeita.localVacinaFeita = txtLocal.Text;
                vacinafeita.reacaoVacinaFeita = txtSintomas.Text;
            }
            else
            {
                vacinafeita.idPessoa = Pessoa.Id;
                vacinafeita.NomeVacinaFeita = txtNome.Text;
                vacinafeita.dataAgora = DateTime.Now;
                vacinafeita.dataVacinaFeita = (DateTime)dtpData.Value;
                vacinafeita.loteVacinaFeita = txtLote.Text;
                vacinafeita.localVacinaFeita = txtLocal.Text;
                vacinafeita.reacaoVacinaFeita = txtSintomas.Text;
            }

            if (Vacinasfeitas != null)
            {
                VacinasFeitasDB.Atualizar(vacinafeita);
            }
            else
            {
                VacinasFeitasDB.Salvar(vacinafeita);

            }
            MessageBox.Show("\nRegistro salvo com sucesso!", "Concluído", MessageBoxButton.OK);
            NavigationService.GoBack();
        }

        #endregion

    }
}