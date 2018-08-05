using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using S_Constants;


namespace New_Snake
{
    public partial class S_Main_Wnd : Form
    {
        
        S_Game Game;
        public S_Main_Wnd()
        {
            InitializeComponent();
            P_Game_board.Width = S_Constants.S_Constants.c_n_width_of_game_board;
            P_Game_board.Height =S_Constants.S_Constants.c_n_height_of_game_board;
            Game = null;
        }
   
  
        private void S_Main_Wnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (Game == null)
                return;
            if (!Game.Is_Replace_Over)
                return;
            if (!Game.Is_Delay_Over)
                return;
            switch (e.KeyCode)
            {
                case Keys.A:
                    Game.Set_New_Direction(E_Direction.Left);
                    break;

                case Keys.D:
                    Game.Set_New_Direction(E_Direction.Right);
                    break;
                case Keys.W:
                    Game.Set_New_Direction(E_Direction.Up);
                    break;
                case Keys.S:
                    Game.Set_New_Direction(E_Direction.Down);
                    break;
                default:
                    break;
            }
        }

        private void btn_Start_Game_Click(object sender, EventArgs e)
        {
            // Начинаем игру
            if (Game != null)
            {
                Game.Stop_Game();
            }

           
            Game = new S_Game(this);
            
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            Game.Stop_Game();
        }

        private void chkb_Check_speed_mode_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
