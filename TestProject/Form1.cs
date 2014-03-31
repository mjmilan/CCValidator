using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form1 : Form
    {

        protected string[] mCards = new string[] { "4024007180170515", "5468371994547528", "376057119365993", "30391482917603", "4913755948394389" };
        
        public Form1()
        {
            InitializeComponent();
        }

        protected string ValidationResultToString(CCValidator.CCValidationResult result)
        {
            string str = string.Format("{0} {1} luhn check, and seems to be a {2}", result.CardNumber, result.InValid ? "failed" : "passed", result.CardType.ToString());
            return str;
        }

        private void butSimpleCall_Click(object sender, EventArgs e)
        {
            CCValidator.CCValidator validator = new CCValidator.CCValidator();
            CCValidator.CCValidationResult result = validator.ValidateCard(mCards[0]);
            lstResults.Items.Clear();
            lstResults.Items.Add(ValidationResultToString(result));
        }

        private void butBulkCall_Click(object sender, EventArgs e)
        {
            CCValidator.CCValidator validator = new CCValidator.CCValidator();
            lstResults.Items.Clear();

            List<CCValidator.CCValidationResult> results = validator.BulkValidate(mCards);
            foreach (CCValidator.CCValidationResult result in results)
            {
                lstResults.Items.Add(ValidationResultToString(result));
            }
        }

        private void butAsyncBulkCall_Click(object sender, EventArgs e)
        {
            CCValidator.CCValidator validator = new CCValidator.CCValidator();
            lstResults.Items.Clear();



            List<CCValidator.CCValidationResult> results = new List<CCValidator.CCValidationResult>();
            validator.BulkValidateByAsync(mCards, results, ShowResults);
            
        }

        private void ShowResults(List<CCValidator.CCValidationResult> results)
        {
            // This takes place on the async thread! We need to invoke GUI functionality back on the GUI thread...
            Action<List<CCValidator.CCValidationResult>> del = UpdateDisplay;

            this.Invoke(del, new object[] {results});
        }

        private void UpdateDisplay(List<CCValidator.CCValidationResult> results)
        {
            lstResults.Items.Clear();
            foreach (CCValidator.CCValidationResult result in results)
            {
                lstResults.Items.Add(ValidationResultToString(result));
            }
        }

    }
}
