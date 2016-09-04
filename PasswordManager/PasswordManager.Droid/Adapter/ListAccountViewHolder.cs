using System;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace PasswordManager.Droid.Adapter
{
    public class ListAccountViewHolder : RecyclerView.ViewHolder
    {
        Action<int> listener;

        public TextView txtName { get; set; }
        public TextView txtAccount { get; set; }
        public TextView txtCount { get; set; }

        public ListAccountViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            txtName = itemView.FindViewById<TextView>(Resource.Id.textView);
            txtAccount = itemView.FindViewById<TextView>(Resource.Id.txtAccount);
            txtCount = itemView.FindViewById<TextView>(Resource.Id.textViewCount);
            
            this.listener = listener;

            itemView.Click += OnClick;
        }

        void OnClick(object sender, EventArgs e)
        {
            int position = base.AdapterPosition;

            if (position == RecyclerView.NoPosition)
                return;

            listener(position);
        }

    }
}