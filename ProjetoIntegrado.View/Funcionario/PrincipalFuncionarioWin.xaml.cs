﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.Funcionario
{
    using Model;
    using Funcoes;
    using Mensagens;
    using ViewUtil;

    public partial class PrincipalFuncionarioWin
    {
        public bool AlterouOftal;
        private List<FuncionarioModel> lFuncionarios;

        public PrincipalFuncionarioWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarFuncionarios();
        }

        #region MANTEM FUNCIONARIO

        private FiltroPessoa GetFiltro() =>
           (FiltroPessoa)Enum.Parse(typeof(FiltroPessoa), Mascara.Remover(cbFiltro.Text.ToLower()));

        private void CarregarFuncionarios()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                lFuncionarios = FuncionarioModel.CarregarTodos();
            else
                lFuncionarios = FuncionarioModel.Pesquisar(GetFiltro(), tbPesquisa.Text);

            lvwFuncionarios.ItemsSource = lFuncionarios;
            lbTotalRegistro.Content = lFuncionarios.Count.ToString("D3");
            tbPesquisa.Focus();
        }

        private void Novo()
        {
            var cadFuncionario = new CadFuncionarioWin();
            cadFuncionario.ShowDialog();

            if (cadFuncionario.cadastrou)
                CarregarFuncionarios();

            AlterouOftal = cadFuncionario.AlterouOftal;
        }

        private void Editar()
        {
            if (lvwFuncionarios.SelectedIndex >= 0)
            {
                var funcionario = lvwFuncionarios.SelectedItems[0] as FuncionarioModel;
                var cadFuncionario = new CadFuncionarioWin(funcionario);

                cadFuncionario.ShowDialog();

                if (cadFuncionario.cadastrou)
                    CarregarFuncionarios();

                AlterouOftal = cadFuncionario.AlterouOftal;
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void Remover()
        {
            if (lvwFuncionarios.SelectedIndex >= 0)
            {
                var funcionario = lvwFuncionarios.SelectedItems[0] as FuncionarioModel;
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    AlterouOftal = funcionario?.cargo?.id == 1;
                    funcionario?.Remover();
                    lFuncionarios.Remove(funcionario);
                    lvwFuncionarios.Items.Refresh();
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion

        #region EVENTOS

        private void BtnNovo_OnClick(object sender, RoutedEventArgs e)
        {
            Novo();
        }

        private void lvwFuncionarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Editar();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarFuncionarios();

            if (e.Key == Key.Down)
                lvwFuncionarios.SelecionarPrimeiraLinha();
        }

        private void lvwFuncionarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Editar();
            else if (e.Key == Key.Delete)
                Remover();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
