using System;
using System.Windows.Forms;

namespace DB_Kurs
{
  static class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
      Application.Run(new Формы.Main());
#else
      Application.Run(new Login());
#endif
    }
  }
}
