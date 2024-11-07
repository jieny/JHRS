﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Shell.ViewModels.Dialogs
{
	public class SuccessDialogViewModel : BindableBase, IDialogAware
	{
		private string _title = "未設置標題";

        public DialogCloseListener RequestClose { get; } = new DialogCloseListener();

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private DelegateCommand _closeDialogCommand;
        public DelegateCommand CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand(ExecuteCloseDialogCommand));

        async void ExecuteCloseDialogCommand()
        {
            ButtonResult result = ButtonResult.No;
            await RaiseRequestClose(new DialogResult(result));
        }

        public async virtual Task RaiseRequestClose(IDialogResult dialogResult)
        {
            await Task.Delay(1000);
            RequestClose.Invoke(dialogResult);
        }


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }

}
