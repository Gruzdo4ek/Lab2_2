namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            grades.Text = Properties.Settings.Default.input.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string input = this.grades.Text;

                Properties.Settings.Default.input = input;
                MessageBox.Show(Logic.Results(input));
            }
            catch (FormatException)
            {
                MessageBox.Show("Неккоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
    public class Logic
    {
        public static int[] GetGrades(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new int[0];
            }

            string[] gradesStr = input.Split(' ');

            int[] grades = new int[gradesStr.Length];
            for (int i = 0; i < gradesStr.Length; i++)
            {
                if (int.TryParse(gradesStr[i], out int grade))
                {
                    if (grade < 2 || grade > 5)
                    {
                        return new int[0];
                    }
                    grades[i] = grade;
                }
                else
                {
                    return new int[0];
                }
            }
            return grades;
        }

        public static int[] CountGrades(int[] grades)
        {
            int[] counts = new int[4];
            foreach (int grade in grades)
            {
                counts[grade - 2]++;
            }
            return counts;
        }

        public static string Results(string input)
        {
            string outMessage = "";
            int[] grades = Logic.GetGrades(input);
            if (!(grades.Length == 0))
            {
                int[] counts = Logic.CountGrades(grades);
                outMessage = "Количество двоек: " + counts[0] +
                "\nКоличество троек: " + counts[1] +
                "\nКоличество четверок: " + counts[2] +
                "\nКоличество пятерок: " + counts[3];

            }
            else
                outMessage = "Отсутствуют оценки учеников или Неверный формат оценки";


            return outMessage;
        }
    }
}