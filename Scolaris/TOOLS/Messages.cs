using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Scolaris.TOOLS
{
    public class Messages
    {
        static public DialogResult Show(string message)
        {
            DialogResult reponse = MessageBox.Show(message, Constantes.APP_NAME, MessageBoxButtons.OK);
            return reponse;
        }

        static public DialogResult Message(String text, String titre, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult reponse;
            if (buttons == null && icon == null)
                reponse = MessageBox.Show(text, titre);
            else if (icon == null)
                reponse = MessageBox.Show(text, titre, buttons);
            else
                reponse = MessageBox.Show(text, titre, buttons, icon);
            return reponse;
        }

        static public DialogResult Error(string message)
        {
            return Message(message, Constantes.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static public DialogResult ErrorRetryCancel(string message)
        {
            return Message(message, Constantes.APP_NAME, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }

        static public DialogResult ErrorYesNo(string message)
        {
            return Message(message, Constantes.APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        static public DialogResult Warning(string message)
        {
            return Message(message, Constantes.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static public DialogResult Information(string message)
        {
            return Message(message, Constantes.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static public DialogResult Hand(string message)
        {
            return Message(message, Constantes.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        static public DialogResult Stop(string message)
        {
            return Message(message, Constantes.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        static public DialogResult Question(string message)
        {
            return Message(message, Constantes.APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        static public DialogResult ChampsVide()
        {
            return Error(Mots.Msg_ChampsVides);
        }

        static public DialogResult ChampsVide(string element)
        {
            return Hand(Mots.Msg_ChampsVide + " '" + element + "'");
        }

        static public DialogResult Confirmation(string action)
        {
            return Question(Mots.Msg_Confirmation + " " + action + "?");
        }

        static public DialogResult Confirmation_Infos(string action)
        {
            return Question(Mots.Msg_Confirmation + " " + action + " " + Mots.Les_Informations + "?");
        }

        static public DialogResult Annulation()
        {
            return Question(Mots.Msg_Annulation);
        }

        static public DialogResult FermerApplication()
        {
            return Question(Mots.Msg_FermerApplication);
        }

        static public DialogResult Exception(Exception ex)
        {
            return Stop(Mots.Msg_Exception + " : " + ex.Message);
        }

        static public DialogResult Exception(NpgsqlException ex)
        {
            return Stop(Mots.Msg_Exception + " : " + ex.Message);
        }

        static public DialogResult Exception(string place, Exception ex)
        {
            return Stop("L'erreur suivante a été detectée : " + ex.Message + "----- Place : " + place);
        }

        static public DialogResult Inexistant(string element)
        {
            return Hand(element + " " + Mots.Msg_Inexistant);
        }

        static public DialogResult Succes()
        {
            return Information(Mots.Msg_Succes);
        }

        static public DialogResult Succes(string element, string action)
        {
            return Information(element + " " + action + " " + Mots.Msg_Succes);
        }
    }
}
