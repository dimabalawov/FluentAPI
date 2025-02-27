namespace FluentAPI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            LoginFORM loginForm = new LoginFORM();
            loginForm.ShowDialog();

            // ���� ���� ������� � ��������� �������� ����
            if (loginForm.IsAuthenticated)
            {
                Application.Run(new Form1());
            }

        }
    }
}