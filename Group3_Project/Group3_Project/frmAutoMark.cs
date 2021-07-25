using Group3_Project.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Group3_Project
{
    public partial class frmAutoMark : Form
    {
        private string aId;
        public frmAutoMark()
        {
            InitializeComponent();
        }
        public frmAutoMark(string aIdMess) : this()
        {
            aId = aIdMess;
        }

        private void btnInsertTest_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtTest.Text = dialog.SelectedPath;
        }

        private void btnInsertClass_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtClass.Text = dialog.SelectedPath;
        }
        private string[] getListTest(string QNumber,string paperNo)
        {
            string[] fileArray = Directory.GetFiles(txtTest.Text + @"\Paper_No" + paperNo+@"\"+QNumber, "*.txt");
            for (int i = 0; i < fileArray.Length; i++)
            {
                fileArray[i] = fileArray[i].Substring(fileArray[i].Length - 13, 13);
            }
            return fileArray;
        }
        private string[] getListQOfStudent(string student, string folder)
        {          
            string[] fileArray = Directory.GetDirectories(folder+@"\"+student);
            for (int i = 0; i < fileArray.Length; i++)
            {

                fileArray[i] = fileArray[i].Substring(fileArray[i].Length - 2, 2);
            }
            return fileArray;
        }
        private bool checkFolder()
        {
            if (txtClass.Text.Length == 0 || txtTest.Text.Length == 0)
            {
                MessageBox.Show("Must not empty");
                txtClass.Focus();
                return false;
            }           
            if (!txtTest.Text.ToLower().Contains("test_case"))
            {
                MessageBox.Show("Wrong file test case!");
                return false;
            }
            if (!Regex.IsMatch(txtClass.Text.Substring(txtClass.Text.Length - 6, 6), @"^SE\d{4}$"))
            {
                MessageBox.Show("Invalid folder class");
                return false;
            }
            return true;
        }

        private void btnAutoMark_Click(object sender, EventArgs e)
        {
            if(!checkFolder())
            {
                return;
            }
            List<string> listStudent = Database.listStudentByClass(txtClass.Text.Substring(txtClass.Text.Length - 6, 6));
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listStudent.Count;
            foreach (string st in listStudent)
            {
                string[] listQ = getListQOfStudent(st, txtClass.Text);
                string scoreDetail = "";
                float totalMark = 0;
                for (int i = 0; i < listQ.Length; i++)
                {
                    string paperNo = st.Substring(st.Length - 1, 1);
                    string[] lissTest = getListTest(listQ[i], paperNo) ;
                    float markQ = 0;
                    for (int j = 0; j < lissTest.Length; j++)
                    {
                        string[] listAll = File.ReadAllLines(txtTest.Text + @"\Paper_No"+ paperNo + @"\" + listQ[i] + @"\" + lissTest[j]);
                        string[] listInput = new string[listAll.Length];
                        int indexOutput = 0;
                        for (int k = 0; k < listAll.Length; k++)
                        {
                            if (listAll[k].Contains("OUTPUT"))
                            {
                                indexOutput = k;
                            }
                        }
                        for (int k = 0; k < indexOutput; k++)
                        {
                            listInput[k] = listAll[k];
                        }
                        string output = "";
                        for (int k = indexOutput + 1; k < listAll.Length-1; k++)
                        {
                            output += listAll[k];
                        }
                        string markTest = listAll[listAll.Length - 1];
                        float mark = float.Parse(markTest.Substring(markTest.Length-3,3));
                        cmd.Start();
                        cmd.StandardInput.WriteLine(@"cd \");
                        cmd.StandardInput.WriteLine(@"cd /d " + txtClass.Text);
                        cmd.StandardInput.WriteLine("cd " + st);
                        cmd.StandardInput.WriteLine(@"cd " + listQ[i]);
                        cmd.StandardInput.WriteLine(@"cd run");
                        cmd.StandardInput.WriteLine("java -jar "+ listQ[i] + "1"+".jar");
                        foreach (string o in listInput)
                        {
                            cmd.StandardInput.WriteLine(o);
                        }
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.Close();
                        cmd.WaitForExit();
                        string txt = cmd.StandardOutput.ReadToEnd();
                        cmd.Close();
                        string[] listop = txt.Split('\n');
                        int a = 0;
                        int b = 0;
                        for (int k = 0; k < listop.Length; k++)
                        {
                            if (listop[k].Contains("OUTPUT"))
                            {
                                a = k;
                            }
                            if (listop[k].EndsWith(">"))
                            {
                                b = k;
                            }
                        }
                        string outputOfSv = "";
                        for (int k = a + 1; k < b; k++)
                        {
                            outputOfSv += listop[k];
                        }
                        string[] listlast = outputOfSv.Split('\r');
                        outputOfSv = "";
                        for (int k = 0; k < listlast.Length; k++)
                        {
                            outputOfSv += listlast[k];
                        }
                        if (outputOfSv == output)
                        {
                            markQ += mark;
                            totalMark += mark;
                        }                       
                    }
                    scoreDetail += "["+listQ[i] + ":" + markQ + "];";
                }
                if (scoreDetail.Equals(""))
                {
                    scoreDetail = "Exam file not found";
                }
                Database.updateScore(totalMark, scoreDetail, st.Substring(0,8));
                progressBar1.Value += 1;
            }
            MessageBox.Show("Complete grading class "+ txtClass.Text.Substring(txtClass.Text.Length - 6, 6));
            progressBar1.Value = 0;
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frmAutoMark_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            frmManage frmManage = new frmManage(aId);
            frmManage.ShowDialog();
        }
    }
}
