using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace AgentsApp.Commands
{
    class DelegateCommand : ICommand
    {

        private EventHandler Handler;
        private EventHandlerWithParam HandlerWithParam;
        private bool isEnabled = true;

        public event System.EventHandler CanExecuteChanged;
        public delegate void EventHandler();
        public delegate void EventHandlerWithParam(object param);

        public DelegateCommand(EventHandler handler)
        {
            this.Handler = handler;
        }

        public DelegateCommand(EventHandlerWithParam handlerWithParam)
        {
            this.HandlerWithParam = handlerWithParam;
        }

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
        }

        bool ICommand.CanExecute(object param)
        {
            return this.IsEnabled;
        }

        void ICommand.Execute(object param)
        {
            if (HandlerWithParam != null && param != null)
            {
                this.HandlerWithParam(param);
            }
            else
            {
                this.Handler();
            }
        }

        private void OnCanExecudeChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
