using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using ST10090990_Part2;
using ST10090990_PART2_APP.ADO.NET_MODEL;
using System.Threading;

namespace ST10090990_PART2_APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassLibrary CL = new ClassLibrary(); //Declaring a class object here so every component can use the same variables from the Class Library
        List<ClassLibrary> list = ClassLibrary.moduleInfo; //Creating a list of the Library list moduleInfo
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tbTopBar.SelectedIndex + 1; //Code used to move between the tabs when the button is clicked
            if (newIndex >= tbTopBar.Items.Count)
                newIndex = 0;
            tbTopBar.SelectedIndex = newIndex;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername1.Text == "" || txtPassword.Password == "") //Validation to ensure the user has enter data
            {
                MessageBox.Show("Please enter your user info to login", "ALERT", MessageBoxButton.OK, MessageBoxImage.Warning); //Message box to inform the user they have not entered required data
            }
            else
            {
                string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Varsity Work Year 2\SEMESTER 2\PROG 2B\ST10090990Part2\ST10090990_Part2\Database\Part2db.mdf"";Integrated Security=True";//Connection string for SQLConnection to connect to the database
                SqlConnection con = new SqlConnection(constr); //Using the connection string to connect to the database
                try
                {
                    con.Open(); //Opening connection string

                    String userPassword = txtPassword.Password; //A variable to hold what is in the Password Box

                    ClassLibrary cl = new ClassLibrary(); //Creating a class library object

                    userPassword = cl.hashPassword(userPassword); //Calling method from class library to hash password

                    SqlCommand cmd = new SqlCommand("select count(*) from Users where Username='" + txtUsername1.Text + "' and Password='" + userPassword + "'", con); //query to get username and password for validation

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    cmd.ExecuteNonQuery();
                    cl.Name = txtUsername1.Text; //Assigning variable to components

                    string queryStudentNo = "select StudentNo from Users where Username='" + txtUsername1.Text + "' and Password='" + userPassword + "'"; //Query to select StudentNo from the table

                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandText = queryStudentNo;
                    cl.StudentNum = (string)cmd1.ExecuteScalar();//Assigning variables to components

                    lblStudentName1.Content = cl.Name; //Assigning variables to components
                    lblStudentNum.Content = cl.StudentNum; //Assigning variables to components
                    lblStudentName3.Content = cl.Name; //Assigning variables to components
                    lblStudentNum2.Content = cl.StudentNum; //Assigning variables to components

                    if (dt.Rows[0][0].ToString() == "1") //Validating the user information entered compared to what is entered in the database
                    {
                        MessageBox.Show("Login Successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information); //Message box to display that user login was successful

                        int newIndex = tbTopBar.SelectedIndex + 2; //Code used to move between the tabs when the button is clicked

                        if (newIndex >= tbTopBar.Items.Count)
                            newIndex = 0;
                        tbTopBar.SelectedIndex = newIndex;
                    }
                    else
                    {
                        MessageBox.Show("Failed to Login. Please ensure Username and Password is correct, otherwise register!", "ALERT", MessageBoxButton.OK, MessageBoxImage.Warning); //Message box to display that user login failed
                        btnRegister2.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int credit = Convert.ToInt32(txtCredits.Text);
                double hour = Convert.ToDouble(txtHours.Text);
                int numWeek = Convert.ToInt32(txtNumWeeks.Text);

                if (txtModuleCode.Text == "" || txtModuleName.Text == "" || !(credit > 0) || !(hour > 0) || !(numWeek > 0) || dpStartDate.SelectedDate.HasValue == false) //Validation to ensure the user has enter data
                {
                    MessageBox.Show("Please complete the all fields first.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Warning); //Message box to inform the user they have not entered required data
                }
                else
                {
                    int newIndex = tbTopBar.SelectedIndex + 1; //Code used to move between the tabs when the button is clicked

                    if (newIndex >= tbTopBar.Items.Count)
                        newIndex = 0;
                    tbTopBar.SelectedIndex = newIndex;
                }

            }
            catch (Exception x) //Catching any exceptions and displaying it to the user
            {
                MessageBox.Show(x.Message); //Message box to display the exception
            }
        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tbTopBar.SelectedIndex + 1; //Code used to move between the tabs when the button is clicked

            if (newIndex >= tbTopBar.Items.Count)
                newIndex = 0;
            tbTopBar.SelectedIndex = newIndex;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int credit = Convert.ToInt32(txtCredits.Text);
                double hour = Convert.ToDouble(txtHours.Text);
                int numWeek = Convert.ToInt32(txtNumWeeks.Text);
                if (txtModuleCode.Text == "" || txtModuleName.Text == "" || !(credit > 0) || !(hour > 0) || !(numWeek > 0) || dpStartDate.SelectedDate.HasValue == false) //Validation to ensure the user has enter data
                {
                    MessageBox.Show("Please complete the all fields first.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Warning); //Message box to inform the user they have not entered required data
                }
                else
                {
                    CL.Name = (string)lblStudentName1.Content; //Assigning variables to components

                    CL.modCode = txtModuleCode.Text; //Assigning variables to components

                    CL.modName = txtModuleName.Text; //Assigning variables to components

                    CL.credits = credit; //Assigning variables to components

                    CL.hours = hour; //Assigning variables to components

                    CL.numWeeks = numWeek; //Assigning variables to components 

                    CL.startDate = dpStartDate.SelectedDate.Value; //Assigning variables to components

                    ClassLibrary display = new ClassLibrary(CL.modCode, CL.modName, CL.credits, CL.hours, CL.numWeeks, CL.startDate, CL.SelfStudy, CL.Remainder); //Creating object display to capture data

                    display.Calculation(); //calling method to perform calculation
                    display.Calculation2(); //calling method to perform calculation
                    
                    ClassLibrary.moduleInfo.Add(display);//using object display to add data into the list
                    
                    SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Varsity Work Year 2\\SEMESTER 2\\PROG 2B\\ST10090990Part2\\ST10090990_Part2\\Database\\Part2db.mdf\";Integrated Security=True"); //Connection string
                    
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Modules]
                    ([ModCode]
                    ,[ModName]
                    ,[Credits]
                    ,[Hours]
                    ,[NumWeeks]
                    ,[StartDate]
                    ,[SelfStudy]
                    ,[Remainder]
                    ,[Username])
                    VALUES
                    ('" + CL.modCode + "', '" + CL.modName + "', '" + CL.credits + "', '" + CL.hours + "', '" + CL.numWeeks + "', '" + CL.startDate + "', '" + display.SelfStudy + "', '" + display.Remainder + "', '" + CL.Name + "')", con); //Query to insert data that the user has put into the fields
                    
                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Your Data has been added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information); //message to inform user the data has been added

                    con.Close();
                }
            }
            catch (Exception x) //Catching any exceptions and displaying it to the user
            {
                MessageBox.Show(x.Message); //Message box to display the exception
            }

        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            ClassLibrary display = new ClassLibrary(CL.modCode, CL.modName, CL.credits, CL.hours, CL.numWeeks, CL.startDate, CL.SelfStudy, CL.Remainder); //Creating object display to capture data
            var show = from p in ClassLibrary.moduleInfo //LINQ to manipulate data
                       select p;
            foreach (var pp in show) //Loop to display data from the list
            {
                lstModule.Items.Add(pp.modCode);
                display.Calculation();
                display.Calculation2();
                lstHours.Items.Add(pp.SelfStudy);
            }
            Part2dbEntitie dataEntities = new Part2dbEntitie(); //Using the ADO.NET Model to access data in the database

            var query =
            from module in dataEntities.Modules
            where module.Username == CL.Name
            orderby module.SelfStudy descending
            select new { module.ModCode, module.ModName, module.Credits, module.Hours, module.NumWeeks, module.StartDate, module.SelfStudy, module.Remainder }; //Query to get the specified data from the database
          
            dgDisplay.ItemsSource = query.ToList(); //Displaying the data from the database into the datagrid
        }
    

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtModuleCode.Clear(); //Code used to clear components so the user can easily enter multiple module information
            txtModuleName.Clear();
            txtCredits.Clear();
            txtHours.Clear();
            txtNumWeeks.Clear();
            dpStartDate.SelectedDate = DateTime.Now;
        }

        private void btnUpdate2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                const int MAX_HOURS = 24; //setting a constant variable 
                double studyHours = Convert.ToInt32(txtStudyHours.Text); //capturing data inputted into the component into a variable
                DateTime? selectedDate = dpStudyDate.SelectedDate; //capturing data inputted into the component into a variable


                //Valdiates that fields are correct 
                if (studyHours > 0 && studyHours <= MAX_HOURS && dpStudyDate.SelectedDate.HasValue == true && lstModule.SelectedIndex > -1)
                {
                    string moduleName = Convert.ToString(lstModule.SelectedItem); //capturing data inputted into the component into a variable
                    ClassLibrary display = new ClassLibrary(CL.modCode, CL.modName, CL.credits, CL.hours, CL.numWeeks, CL.startDate, CL.SelfStudy, CL.Remainder); //Creating object display to capture data
                    var calc = from t in ClassLibrary.moduleInfo //LINQ to manipulate data
                               where t.modCode == moduleName
                               select t;

                    foreach (var tt in calc) //Loop used to update and calculate data based on user input
                    {
                        display.Calculation();
                        display.Calculation2();
                        double remainder = tt.Remainder - studyHours;
                        txtModName.Text = moduleName;
                        txtRemaining.Text = $"{remainder}";
                    }
                    SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Varsity Work Year 2\\SEMESTER 2\\PROG 2B\\ST10090990Part2\\ST10090990_Part2\\Database\\Part2db.mdf\";Integrated Security=True"); //Connection string
                   
                    SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Modules]
                    SET[Remainder] = '" + txtRemaining.Text + "' WHERE [ModCode] ='" + moduleName + "' ", con); //Query to update the data in the database based on what module code the user selected in the listbox

                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Study session recorded.", "Notice", MessageBoxButton.OK, MessageBoxImage.Information); //Message box to inform the user their study session has been recorded

                    con.Close();

                    Part2dbEntitie dataEntities = new Part2dbEntitie(); //Using the ADO.NET Model to access data in the database

                    var query =
                    from module in dataEntities.Modules
                    where module.Username == CL.Name
                    orderby module.SelfStudy descending
                    select new { module.ModCode, module.ModName, module.Credits, module.Hours, module.NumWeeks, module.StartDate, module.SelfStudy, module.Remainder }; //Query to get the specified data from the database

                    dgDisplay.ItemsSource = query.ToList(); //Displaying the data from the database into the datagrid
                }
                else
                {
                    MessageBox.Show("Please enter the correct values for this study session. Please make sure that you have selected an item in the listbox for module.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Warning); //Message box to inform the user they have not entered required data
                }
            }
            catch (Exception) //Catching any exceptions and displaying it to the user
            {
                MessageBox.Show("An unexpected error occurred. Please try again.", "ALERT", MessageBoxButton.OK, MessageBoxImage.Warning); //Message box to display the exception
            }
        }

        private void btnRegister1_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tbTopBar.SelectedIndex + 2; //Code used to move between the tabs when the button is clicked

            if (newIndex >= tbTopBar.Items.Count)
                newIndex = 0;
            tbTopBar.SelectedIndex = newIndex;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtPassword1.Password == "" || txtStudentNumber.Text == "") //Validation to ensure the user has enter data
            {
                MessageBox.Show("Please complete all fields to register", "ALERT", MessageBoxButton.OK, MessageBoxImage.Warning); //Message box to inform the user they have not entered required data
            }
            else
            {
                ClassLibrary CL = new ClassLibrary();

                String storedPassword = CL.hashPassword(txtPassword1.Password);

                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Varsity Work Year 2\\SEMESTER 2\\PROG 2B\\ST10090990Part2\\ST10090990_Part2\\Database\\Part2db.mdf\";Integrated Security=True");
                
                SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Users]
                ([Username],
                [StudentNo],
                [Password])
                VALUES
                ('" + txtName.Text + "', '" + txtStudentNumber.Text + "', '" + storedPassword + "')", con); //Query to insert data the user has entered into the database 

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("User registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                int newIndex = tbTopBar.SelectedIndex - 1; //Code used to move between the tabs when the button is clicked

                if (newIndex >= tbTopBar.Items.Count)
                    newIndex = 0;
                tbTopBar.SelectedIndex = newIndex;
            }
        }

        private void btnRegister2_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tbTopBar.SelectedIndex + 1; //Code used to move between the tabs when the button is clicked

            if (newIndex >= tbTopBar.Items.Count)
                newIndex = 0;
            tbTopBar.SelectedIndex = newIndex;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string moduleName = Convert.ToString(lstModule.SelectedItem);

            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Varsity Work Year 2\\SEMESTER 2\\PROG 2B\\ST10090990Part2\\ST10090990_Part2\\Database\\Part2db.mdf\";Integrated Security=True");

            SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Modules]
            WHERE [ModCode]='" + moduleName + "'", con); //Query to delete row in the database based on what module the user selected in the list

            con.Open();

            cmd.ExecuteNonQuery();

            MessageBox.Show("Module deleted.", "Notice", MessageBoxButton.OK, MessageBoxImage.Information); //Message box to inform the user there Module has been deleted

            con.Close();

            Part2dbEntitie dataEntities = new Part2dbEntitie();//Using the ADO.NET Model to access data in the database
            var query =
            from module in dataEntities.Modules
            where module.Username == CL.Name
            orderby module.SelfStudy descending
            select new { module.ModCode, module.ModName, module.Credits, module.Hours, module.NumWeeks, module.StartDate, module.SelfStudy, module.Remainder }; //Query to get the specified data from the database

            dgDisplay.ItemsSource = query.ToList();//Displaying the data from the database into the datagrid
        }

        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction(); //Using the function based on a certain user action
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction(); //Using the function based on a certain user action
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction(); //Using the function based on a certain user action

        private void ShowPasswordFunction() //Function to show password when image is clicked
        {
            txtPassword2.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Hidden;
            txtPassword2.Text = txtPassword.Password;
        }

        private void HidePasswordFunction()//Function to hide password when click on image is released
        {
            txtPassword2.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Visible;
        }

        private void ShowPassword1_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction1(); //Using the function based on a certain user action
        private void ShowPassword1_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction1(); //Using the function based on a certain user action
        private void ShowPassword1_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction1(); //Using the function based on a certain user action

        private void ShowPasswordFunction1() //Function to show password when image is clicked
        {
            txtPassword3.Visibility = Visibility.Visible;
            txtPassword1.Visibility = Visibility.Hidden;
            txtPassword3.Text = txtPassword1.Password;
        }

        private void HidePasswordFunction1()//Function to hide password when click on image is released
        {
            txtPassword3.Visibility = Visibility.Hidden;
            txtPassword1.Visibility = Visibility.Visible;
        }
    }
}
