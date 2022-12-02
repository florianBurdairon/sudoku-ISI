using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace sudoku
{
    /*
     * Fenêtre permettant d'afficher le score et de demander à l'utilisateur de saisir son nom pour l'enregistrer dans le tableau de scores
     */
    public partial class ChoiceUsername : Form
    {
        Jeu jeu;

        public ChoiceUsername(Jeu jeu)
        {
            InitializeComponent();
            this.jeu = jeu;

            int time = this.jeu.time;
            lbTime.Text = "Votre temps : ";
            if (time / 3600 > 0)
            {
                lbTime.Text += (time / 3600).ToString() + ":";
            }
            string m = ((time % 3600) / 60).ToString();
            if (m.Length == 1)
                m = "0" + m;
            string s = (time % 60).ToString();
            if (s.Length == 1)
                s = "0" + s;

            lbTime.Text += m + ":" + s;
        }

        /*
         * Gère le clique sur le bouton btnSave
         */
        private void btnSave_Click(object sender, EventArgs e)
        {
            jeu.SaveScore(tbUsername.Text != "" ? tbUsername.Text : "Invité");
            this.Close();
        }

        /*
         * Gère le clique sur le bouton btnNotSave
         */
        private void btnNotSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
