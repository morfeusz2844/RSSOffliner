using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using RSOOffliner.Base;
using RSOOffliner.Models;

namespace RSOOffliner.ViewModels
{
    public class RSSManagerViewModel : NotificationObject,IRSSManagerViewModel
    {
        private ObservableCollection<RSS> _rssItems = new ObservableCollection<RSS>();

        public ObservableCollection<RSS> RssItem
        {
            get { return _rssItems; }
            set
            {
                _rssItems = value;
                RaisePropertyChanged();
            }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        private string _url;

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                RaisePropertyChanged();
            }
        }

        private string _feedTitle;

        public string FeedTitle
        {
            get
            {
                return _feedTitle;
            }
            set
            {
                _feedTitle = value;
                RaisePropertyChanged();
            }
        }

        private string _feedDescription;

        public string FeedDescription
        {
            get
            {
                return _feedDescription;
            }
            set
            {
                _feedDescription = value;
                RaisePropertyChanged();
            }
        }

        private RSS _rssItemSelected;

        public RSS RssItemSelected
        {
            get
            {
                return _rssItemSelected;
            }
            set
            {
                _rssItemSelected = value;
                String temp = Regex.Replace(_rssItemSelected.Description, @"<[^>]*>", String.Empty);
                _rssItemSelected.Description = temp;
                RaisePropertyChanged();
            }
        }

        public Manager ToManager()
        {
            return new Manager
            {
                Id = this.Id,
                FeedTitle = this.FeedTitle,
                FeedDescription = this.FeedDescription,
                Url = this.Url,
                RssItem = this.RssItem
            };
        }

        public static RSSManagerViewModel FromManager(Manager manager)
        {
            return new RSSManagerViewModel
            {
                _id = manager.Id,
                _url = manager.Url,
                _feedTitle = manager.FeedTitle,
                _feedDescription = manager.FeedDescription,
                _rssItems = (ObservableCollection<RSS>)manager.RssItem
            };
        }
    }
}
