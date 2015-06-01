using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSOOffliner.Base;
using RSOOffliner.Models;

namespace RSOOffliner.ViewModels
{
    public class ChannelViewModel : ValidationObject
    {
        public static ChannelViewModel FromChannel(Channel channel)
        {
            return new ChannelViewModel
            {
                _id = channel.Id,
                _name = channel.Name,
                _url = channel.Url
            };
        }

        public Channel ToChannel()
        {
            return new Channel
            {
                Id = this.Id,
                Name = this.Name,
                Url = this.Url
            };
        }

        public ChannelViewModel()
        {
            Validators.Add("Name",p=>string.IsNullOrWhiteSpace(Name)?"Nazwa nie może być pusta":null);
            Validators.Add("Url",p=>string.IsNullOrWhiteSpace(Url)?"Adres nie może być pusty":null);
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
                RaiseErrorsChanged();
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
                RaiseErrorsChanged();
            }
        }

        private string _url;

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                RaisePropertyChanged();
                RaiseErrorsChanged();
            }
        }
    }
}
