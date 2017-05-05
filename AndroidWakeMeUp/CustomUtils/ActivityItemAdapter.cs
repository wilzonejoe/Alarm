using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CoreWakeMeUp.Entity;

namespace AndroidWakeMeUp.CustomUtils
{
    public class ActivityItemAdapter : RecyclerView.Adapter
    {
        private List<Time> mTimes;

        public ActivityItemAdapter(List<Time>times)
        {
            mTimes = times;
        }

        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            Android.Views.View itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.ActivityItem, parent, false);

            ActivityItemViewHolder vh = new ActivityItemViewHolder(itemView);
            return vh;
        }


        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = (ActivityItemViewHolder)holder;

            if (vh != null) vh.Time.Text = mTimes[position].ToString();
        }

        public override int ItemCount => mTimes.Count;
    }

    public class ActivityItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView Time { get; private set; }

        public ActivityItemViewHolder(Android.Views.View itemView)
            : base(itemView)
        {
            Time = itemView.FindViewById<TextView>(Resource.Id.TimeText);
        }
    }
}