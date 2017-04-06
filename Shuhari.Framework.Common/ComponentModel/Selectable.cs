namespace Shuhari.Framework.ComponentModel
{
    /// <summary>
    /// Base selectable implementation
    /// </summary>
    public abstract class Selectable : Observable, ISelectable
    {
        private bool _selected = false;

        /// <inheritdoc />
        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(nameof(Selected), ref _selected, value); }
        }
    }
}
