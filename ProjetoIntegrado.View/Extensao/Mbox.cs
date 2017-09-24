﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View
{
    public static class Mbox
    {
        #region MBOX DE AFIRMAÇÃOE E CONFIRMAÇÃO

        public static MetroWindow JanelaPrincipal;

        private static void Afirmacao(string titulo, string texto)
        {
            JanelaPrincipal.ShowModalMessageExternal(titulo, texto, MessageDialogStyle.Affirmative);
        }

        private static MessageDialogResult Pergunta(string titulo, string texto)
        {
            return JanelaPrincipal.ShowModalMessageExternal(titulo, texto, MessageDialogStyle.AffirmativeAndNegative);
        }

        #endregion

        public static void SelecioneUmaLinhaDaTabela()
        {
            Afirmacao("Aviso", "Por favor, selecione uma linha da tabela.");
        }

        public static MessageDialogResult DesejaExcluir()
        {
            return Pergunta("Aviso", "Tem certeza que deseja remover esse registro?");
        }

        public static MessageDialogResult DesejaSair()
        {
            return Pergunta("Aviso", "Tem certeza que deseja sair?");
        }
    }
}
