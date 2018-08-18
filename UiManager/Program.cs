using System.Windows.Forms;

namespace UiManager
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            Manager manager = new Manager();
            manager.StartGame();
        }
    }
}
