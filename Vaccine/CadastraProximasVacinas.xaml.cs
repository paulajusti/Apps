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
    public partial class CadastraProximasVacinas : PhoneApplicationPage
    {
        public Pessoas Pessoa { get; set; }
        public ProximasVacinas Proximasvacinas { get; set; }

        //=================================== construtor com botão desabilitado ===================================
        public CadastraProximasVacinas()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-br");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-br");
            InitializeComponent();
            btnCadastrar.IsEnabled = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Proximasvacinas != null)
            {
                btnCadastrar.IsEnabled = true;
                txbId.Text = Proximasvacinas.Id.ToString();
                txtNome.Text = Proximasvacinas.NomeProximaVacina.ToString();
                Proximasvacinas.dataAgora = DateTime.Now;
                Proximasvacinas.dataProximaVacina = (DateTime)dtpData.Value;

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
            ProximasVacinas proximavacina = new ProximasVacinas();

            if (txbId.Text != "")
            {
                proximavacina.Id = Convert.ToInt32(txbId.Text);
                proximavacina.idPessoa = Pessoa.Id;
                proximavacina.NomeProximaVacina = txtNome.Text;
                proximavacina.dataAgora = DateTime.Now;
                proximavacina.dataProximaVacina = (DateTime)dtpData.Value;
            }
            else
            {
                proximavacina.idPessoa = Pessoa.Id;
                proximavacina.NomeProximaVacina = txtNome.Text;
                proximavacina.dataAgora = DateTime.Now;
                proximavacina.dataProximaVacina = (DateTime)dtpData.Value;
            }

            if (Proximasvacinas != null)
            {
                ProximasVacinasDB.Atualizar(proximavacina);
            }
            else
            {
                ProximasVacinasDB.Salvar(proximavacina);

            }
            MessageBox.Show("\nRegistro salvo com sucesso!", "Concluído", MessageBoxButton.OK);

            //============================================== LIVE TILE ===============================================

            ShellTile tile = ShellTile.ActiveTiles.First();
            if (null != tile)
            {
                StandardTileData data = new StandardTileData();
                data.BackTitle = "Carteira de Vacinação";
                var obj = ProximasVacinasDB.GetProximaDataLiveTile();
                if (obj != null)
                    data.BackContent = "Próxima Vacina:\n" + obj.dataProximaVacina.ToShortDateString();
                else
                    data.BackContent = "Não há vacinas para fazer!";
                tile.Update(data);
            }

            //================================================ ALARME =================================================
            /*    Alarm alarm = new Alarm("Alarm")
                {
                
                   // DateTime date = (DateTime)proximavacina.dataProximaVacina,
                   // DateTime time = (DateTime)beginTimePicker.Value;
                   //  DateTime beginTime = date + time.TimeOfDay;
               
                    BeginTime = proximavacina.dataProximaVacina,
                  //  BeginTime = DateTime.Now.AddMinutes(3),
                    Content = "Hoje é dia de fazer vacina! :)",
                    RecurrenceType = RecurrenceInterval.None,
                };
                ScheduledActionService.Add(alarm); */

            NavigationService.GoBack();
        }
    }
}