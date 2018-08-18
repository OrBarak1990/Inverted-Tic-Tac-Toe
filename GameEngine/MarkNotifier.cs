using System.ComponentModel;
using System.Windows.Forms;

namespace GameEngine
{
    public class MarkNotifier : INotifyPropertyChanged
    {
        private const string k_Blank = " ", k_Mark = "Mark";
        private string m_Mark = k_Blank;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Mark
        {
            get
            {
                return m_Mark;
            }

            set
            {
                m_Mark = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(k_Mark));
        }
    }
}
