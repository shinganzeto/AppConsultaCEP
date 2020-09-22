using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppConsultaCEP.Servico.Modelo;
using AppConsultaCEP.Servico;
using System.Net.Http;

namespace AppConsultaCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;

        }

        private void BuscarCEP(object sender, EventArgs arg)
        {
            string cep = CEP.Text.Trim();
            if (isValidCEP(cep))
            {
                try {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if(end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} de {0} {1} {3}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: "+cep, "OK");
                    }
    
                } catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }    
        }

        private bool isValidCEP(string cep)
        {

            bool valido = true;
            
            //if(cep.Length !=8)
            //{
            //    DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter 8 caracteres", "OK");
            //    valido = false;
            //}
            int NovoCEP = 0;
            if(!int.TryParse(cep, out NovoCEP))
            {

                DisplayAlert("ERRO", "CEP Inválido! O CEP deve composto apenas por numeros", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
