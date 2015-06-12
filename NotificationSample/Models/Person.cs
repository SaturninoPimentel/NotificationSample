using NotificationSample.Models.Base;

namespace NotificationSample.Models
{
    public class Person : BindableBase
    {

        private int _id=10;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value; 
                RaisePropertyChanged();
            }
        }
        

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }
        

    }
}
