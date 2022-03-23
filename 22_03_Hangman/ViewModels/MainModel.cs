using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _22_03_Hangman.ViewModels
{
    internal class MainModel : INotifyPropertyChanged
    {
        public MainModel()
        {
            PressCommand = new RelayCommand<string>(
                (value) =>
                {
                    // Add character to list and update CanExecute
                    UsedChars.Add(value);
                    PressCommand.RaiseCanExecuteChanged();

                    // Check if character is in OriginalText
                    if (!OriginalText.ToUpper().Contains(value.ToUpper()))
                    {
                        WrongGuesses++;
                    }

                    if(GuessText == OriginalText)
                    {
                        WinnerText = "You won!";
                        CanEdit = true;
                        PressCommand.RaiseCanExecuteChanged();
                        StartCommand.RaiseCanExecuteChanged();
                    }

                    // Update property
                    NotifyPropertyChanged("GuessText");
                },
                (value) =>
                {
                    // Check if character is in list and if game started
                    return !UsedChars.Contains(value) && !CanEdit;
                }
            );
            StartCommand = new RelayCommand(
                () =>
                {
                    CanEdit = false;
                    WinnerText = "";
                    UsedChars = new ObservableCollection<string>();
                    WrongGuesses = 0;
                    NotifyPropertyChanged("GuessText");
                    PressCommand.RaiseCanExecuteChanged();
                    StartCommand.RaiseCanExecuteChanged();
                },
                () =>
                {
                    return CanEdit;
                }
            );
            UsedChars = new ObservableCollection<string>();
            wrongGuesses = 0;
            originalText = "Lorem Ipsum";
            canEdit = true;
            winnerText = "";
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand<string> PressCommand { get; set; }
        public RelayCommand StartCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool canEdit;
        public bool CanEdit
        {
            get
            {
                return canEdit;
            }
            set
            {
                canEdit = value;
                NotifyPropertyChanged();
            }
        }

        private string originalText;
        public string OriginalText 
        { 
            get 
            {
                return originalText;
            }
            set
            {
                originalText = value;
                NotifyPropertyChanged();
            }
        }

        public string GuessText
        {
            get
            {
                string result = "";
                foreach(char character in OriginalText)
                {
                    result += UsedChars.Contains(character.ToString().ToUpper()) || character == ' ' ? character : "-";
                }
                return result;
            }
        }

        private int wrongGuesses;
        public int WrongGuesses
        {
            get
            {
                return wrongGuesses;
            }
            set
            {
                wrongGuesses = value;
                NotifyPropertyChanged();
            }
        }

        private string winnerText;
        public string WinnerText
        {
            get
            {
                return winnerText;
            }
            set
            {
                winnerText = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> usedChars;
        public ObservableCollection<string> UsedChars
        {
            get
            {
                return usedChars;
            }
            set
            {
                usedChars = value;
                NotifyPropertyChanged();
            }
        }
    }
}
