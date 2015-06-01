using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using RSOOffliner.Base;
using RSOOffliner.Models;
using RSOOffliner.Services;

namespace RSOOffliner.ViewModels
{
    public class RSSManagerViewModel : NotificationObject
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

        private string _rssHTMLContent;

        public string RssHTMLContent
        {
            get { return _rssHTMLContent; }
            set
            {
                _rssHTMLContent = value;
                RaisePropertyChanged();
            }
        }

        private void LoadRssHTMLContent()
        {
            String temp = Regex.Replace(RSSManagerService.readHtml(RssItemSelected.Link), @"<[^>]*>", String.Empty);
            MessageBox.Show(temp);
            this.RssHTMLContent = temp;
        }
    }
}
