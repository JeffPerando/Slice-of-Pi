
namespace Main.Services.Abstract
{
    public interface ICSVBuilder
    {
        ICSVBuilder setColumns(string[] columns);

        void addRow(object[] data);

    }

}
