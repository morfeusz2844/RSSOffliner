using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RSOOffliner.Base;
using RSOOffliner.Models;
using RSOOffliner.Services;
using RSOOffliner.ViewModels;

namespace RSOOffliner
{
    public class MainWindowViewModel : NotificationObject
    {
        private ObservableCollection<ChannelViewModel> _channels = new ObservableCollection<ChannelViewModel>();
        private ObservableCollection<RSSManagerViewModel> _channelViewModels = new ObservableCollection<RSSManagerViewModel>();


        public ObservableCollection<RSSManagerViewModel> ChannelViewModels
        {
            get { return _channelViewModels; }
            set
            {
                _channelViewModels = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ChannelViewModel> Channels
        {
            get { return _channels; }
            set
            {
                _channels = value;
                RaisePropertyChanged();
            }
        }

        private ChannelViewModel _selectedChannel;

        public ChannelViewModel SelectedChannel
        {
            get
            {
                return _selectedChannel;
            }
            set
            {
                _selectedChannel = value;
                //if (_selectedChannel.Url != null) {
                //    RssManager = new RSSManagerViewModel { Url = _selectedChannel.Url, Id = _selectedChannel.Id };
                //}
                RaisePropertyChanged();
            }
        }

        private RSSManagerViewModel _rssManager;

        public RSSManagerViewModel RssManager
        {
            get { return _rssManager; }
            set
            {
                _rssManager = value;
                RaisePropertyChanged();
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
                LoadCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isSaving;

        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                _isSaving = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private BasicCommand _loadCommand;

        public BasicCommand LoadCommand
        {
            get { return _loadCommand ?? (_loadCommand = new BasicCommand(OnLoad, CanLoad)); }
        }

        private BasicCommand _newChannelCommand;

        public BasicCommand NewChannelCommand
        {
            get { return _newChannelCommand ?? (_newChannelCommand = new BasicCommand(OnNewChannel)); }
        }

        private BasicCommand _saveCommand;

        public BasicCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new BasicCommand(OnSave, CanSave)); }
        }

        private BasicCommand _saveChannelCommand;

        public BasicCommand SaveChannelCommand
        {
            get { return _saveChannelCommand ?? (_saveChannelCommand = new BasicCommand(OnSaveChannel)); }
        }

        private void OnLoad(object _)
        {
            IsLoading = true;
            Task<List<Channel>>.Factory.StartNew(
                () =>
                {
                    return ChannelService.GetChannels();
                },
                TaskCreationOptions.LongRunning)
                .ContinueWith(
                    task =>
                    {
                        foreach (var channel in task.Result)
                        {
                            Channels.Add(ChannelViewModel.FromChannel(channel));
                        }
                        IsLoading = false;
                    }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnSave(object _)
        {
            IsSaving = true;
            List<Channel> temp = Channels.Select(channelViewModel => channelViewModel.ToChannel()).ToList();
            ChannelService.SaveChannels(temp);
            List<Manager> tempManagers = ChannelViewModels.Select(rssManagerViewModel => rssManagerViewModel.ToManager()).ToList();
            RSSManagerService.SaveRssInfo(tempManagers);
            IsSaving = false;
        }
        private bool CanLoad(object _)
        {
            return !IsLoading;
        }

        private bool CanSave(object _)
        {
            return !IsSaving;
        }
        private void OnNewChannel(object _)
        {
            var channel = new ChannelViewModel {Id = Channels.Count > 0 ? Channels.Max(e => e.Id) + 1 : 1};
            Channels.Add(channel);
            SelectedChannel = channel;
        }

        private void OnSaveChannel(object obj)
        {
            var channel = (ChannelViewModel) obj;
            var rss = new RSSManagerViewModel {Id = channel.Id,Url=channel.Url};
            RSSManagerService.GetFeed(rss);
            ChannelViewModels.Add(rss);
            RssManager = rss;
            
            MessageBox.Show(String.Format("Zapisałem kanał: {0}", channel.Name));
            MessageBox.Show(ChannelViewModels.Count+" "+rss.RssItem.Count);
        }
    }
}
