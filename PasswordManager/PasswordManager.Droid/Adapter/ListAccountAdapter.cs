using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using PasswordManager.Model;

namespace PasswordManager.Droid.Adapter
{
   public class ListAccountAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        List<ListAccountItem> lstAccount;

        public ListAccountAdapter(List<ListAccountItem> restaurants)
        {
            this.lstAccount = restaurants;
        }

        public override int ItemCount
        {
            get
            {
                return lstAccount.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = (ListAccountViewHolder)holder;
            vh.txtName.Text = lstAccount[position].Name;
            vh.txtCount.Text = lstAccount[position].Count.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_row_cats, parent, false);

            return new ListAccountViewHolder(layout, OnItemClick);
        }

        void OnItemClick(int position)
        {
            if (ItemClick != null)
            {
                ItemClick(this, position);
            }
        }
    }
}