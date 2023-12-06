using System;
using System.Collections;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

namespace KabirovTextAnalyzer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Main_Text;
        private List<string> mywordsArray = new List<string>();
        private List<string> mylongestWords = new List<string>();
        private List<string> mymostCommonWords = new List<string>();
        private int mywordscount = 0;
        private int myletterscount = 0;
        private Dictionary<char, int> mylettersDistribution;
        /*private char[] myalphabet = {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и',
        'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т',
        'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь',
        'э', 'ю', 'я', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z'};*/

        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            Main_Text = MainTB.Text;
            // myletterscount = Main_Text.Length;
            letterscount.Text = Main_Text.Length.ToString();

            mywordsArray = GetWords(Main_Text);
            // mylettersDistribution = CountLetters(Main_Text);
            lettersDistribution.Text = string.Join("\n", CountLetters(Main_Text).Select(pair => $"{pair.Key}: {pair.Value}"));
            if (mywordsArray.Any())
            {
                // mywordscount = mywordsArray.Count;
                wordscount.Text = mywordsArray.Count.ToString();
                // var mylongestWords = GetTopNLongestWords(mywordsArray, 5);
                longestwords.Text = string.Join("\n", GetTopNLongestWords(mywordsArray, 5));
                // var mymostCommonWords = GetMostCommonWords(mywordsArray, 5);
                mostCommonWords.Text = string.Join("\n", GetMostCommonWords(mywordsArray, 5));

            }
            else
            {
                MessageBox.Show("Введите текст");
            }
            


        }

        static Dictionary<char, int> CountLetters(string input)
        {
            Dictionary<char, int> letterCount = new Dictionary<char, int>();
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz";

            foreach (char letter in alphabet)
            {
                letterCount[letter] = 0;
            }

            foreach (char letter in input)
            {
                if (letterCount.ContainsKey(char.ToLower(letter)))
                {
                    letterCount[char.ToLower(letter)]++;
                }
            }

            return letterCount;
        }

        private List<string> GetWords(string text)
        {
            char[] charArray = new char[] { '.', ',', ';', '?', '-', '!', '?', '(', ')', '[', ']', '{', '}', '"', '\\', '\'', '/'};
            return text.Split(' ', '\n', '\r', '\t').Select(word => word.Trim(charArray)).Where(word => !string.IsNullOrWhiteSpace(word)).ToList();
        }

        private List<string> GetTopNLongestWords(List<string> words, int n)
        {
            return words.OrderByDescending(word => word.Length).Take(n).ToList();
        }

        private List<KeyValuePair<string, int>> GetMostCommonWords(List<string> words, int n)
        {
            var wordCounts = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
                else
                {
                    wordCounts[word] = 1;
                }
            }

            return wordCounts.OrderByDescending(pair => pair.Value).Take(n).ToList();
        }
    }




}

