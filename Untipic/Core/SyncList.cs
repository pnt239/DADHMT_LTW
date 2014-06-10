namespace Untipic.Core
{
    public class SyncList<T> : System.ComponentModel.BindingList<T>
    {

        private readonly System.ComponentModel.ISynchronizeInvoke _syncObject;
        private readonly System.Action<System.ComponentModel.ListChangedEventArgs> _fireEventAction;

        public SyncList()
            : this(null)
        {
        }

        public SyncList(System.ComponentModel.ISynchronizeInvoke syncObject)
        {

            _syncObject = syncObject;
            _fireEventAction = FireEvent;
        }

        protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs args)
        {
            if (_syncObject == null)
            {
                FireEvent(args);
            }
            else
            {
                _syncObject.Invoke(_fireEventAction, new object[] { args });
            }
        }

        private void FireEvent(System.ComponentModel.ListChangedEventArgs args)
        {
            base.OnListChanged(args);
        }
    }
}
