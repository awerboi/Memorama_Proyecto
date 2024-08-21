using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Memorama.Pablo_y_Héctor
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "w", "w", "d", "d", "b", "b",
            "p", "p", "v", "v", "ñ", "ñ", "j", "j", "f", "f", "o", "o",
            "l", "l", "z", "z", "k","k", "a", "a", "q", "q", "h", "h"
        };

        Label firstClicked, secondClicked;
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        
        private void label_Click(object sender, EventArgs e)
        {
            //when the player click two cards and they dont match, both will hide again
            if (firstClicked != null && secondClicked != null)
                return;


            //the as keyword is trying to convert the center into a label, but if it cannot do that, "clickedLabel" just will be null
            Label clickedLabel = sender as Label;

            if (clickedLabel == null)
                return;

            //if an already pressed button, is pressed again, it will be ignored
            if (clickedLabel.ForeColor == Color.Black)
                return;
            if (firstClicked == null)
            {
                //if a label is first time pressed, the color will turn to black, so it will be visible 
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            CheckForWinner();

            //when two images are the same, both will be freezed in place
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else 
                timer1.Start();
        }

        //check for winner and type a message
        private void CheckForWinner ()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            MessageBox.Show("Felicidades, te ganaste un cantonés");
        }

        //set a time to click the pair of images, and then, restart everything to the backround color
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void AssignIconsToSquares()
        {
            Label label;
            int randomNumber;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                    label = (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;

                // Elige un número aleatorio para asingarle un ícono en la lista, y luego se elimina el número aleatorio de la lista de íconos
                randomNumber = random.Next(0, icons.Count);
                label.Text = icons[randomNumber];

                icons.RemoveAt(randomNumber);
            }
        }
    }
}
