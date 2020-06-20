namespace Domain.Entities.FilterModels
{
    public abstract class PagingModel
    {
        private int _page;
        private int _size;
        public int Page
        {
            get => _page;
            set
            {
                if (value != 0)
                    _page = value;
                else
                    _page = 1;
            }
        }

        public int Size
        {
            get => _size;
            set
            {
                if (value != 0)
                    _size = value;
                else
                    _size = 15;
            }
        }
    }
}
