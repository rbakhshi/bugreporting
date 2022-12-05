using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClassLibrary1;

[assembly:InternalsVisibleTo("MauiAppNew")]

namespace MauiAppNew.Library
{
    class ClickViewModel : INotifyPropertyChanged
    {
        private readonly Func<ContentHolder> _contentBuilder;
        private int _count;

        public ClickViewModel(Func<Action, ICommand> commandBuilder, Func<ContentHolder> contentBuilder)
        {
            _contentBuilder = contentBuilder;
            ClickCommand = commandBuilder(() => Count++);
            Content = _contentBuilder();;
        }

        public ICommand ClickCommand { get; }

        public ContentHolder? Content { get; set; }

        public int Count
        {
            get => _count;

            set
            {
                SetField(ref _count, value);
                Content = _contentBuilder();
                OnPropertyChanged("");
            }
        }

        public string Text
        {
            get
            {
                if (_count == 1)
                {
                    return $"Clicked {_count} time";
                }

                return $"Clicked {_count} times";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}