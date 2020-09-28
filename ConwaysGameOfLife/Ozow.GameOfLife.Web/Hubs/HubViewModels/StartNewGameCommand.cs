namespace Ozow.GameOfLife.Web.Hubs.HubViewModels
{
    public class StartNewGameCommand
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int GenCount { get; set; }
    }
}