using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB_Kurs.Mysql;

namespace DB_Kurs
{
  public partial class Login : Form
  {
    public Login()
    {
      InitializeComponent();
		}

		private void ButtonClick(object sender, EventArgs e)
    {
			bool login = false;
			using (ConnectionString db = new ConnectionString())
			{
				var users = db.users.ToList();
				foreach (var user in users)
				{
					if (user.login == LoginBox.Text && user.password == PasswordBox.Text)
					{
						login = true;
						MessageBox.Show("Вы успешно авторизовались!");
						Hide();
						Формы.Main main = new Формы.Main();
						main.Show();
						break;
					}
					else
						continue;
				}
				if (!login)
					MessageBox.Show("Пользователь не найден!");
			}
		}

    private void TextBoxKeyUp(object sender, KeyEventArgs e)
    {
			if (Button.Enabled == false && PasswordBox.Text.Length != 0 && LoginBox.Text.Length != 0)
        Button.Enabled = true;
			else if (Button.Enabled == true && (PasswordBox.Text.Length == 0 || LoginBox.Text.Length == 0))
        Button.Enabled = false;
		}
  }
}
