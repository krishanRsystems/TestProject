using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TestPrimeReact
{
    internal class LandingPage
    {
        private IWebDriver driver;

        public LandingPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement HeaderCheckbox => driver.FindElement(By.XPath("//h5[contains (text(), 'Checkbox')]"));
        private IWebElement CheckboxTable => driver.FindElement(By.XPath("//h5[contains (text(), 'Checkbox')]/following-sibling::div[@class='p-datatable p-component']/div/table"));
        private IList<IWebElement> Columns => CheckboxTable.FindElements(By.XPath("thead/tr/th"));
        private IList<IWebElement> TRows(int col) => CheckboxTable.FindElements(By.XPath($"tbody/tr/td[{col}]"));
        private IList<IWebElement> ItemCheckBox(int row) => CheckboxTable.FindElements(By.XPath($"tbody/tr[{row}]/td[1]"));
        private IList<IWebElement> ItemCheckBoxValue(int row) => CheckboxTable.FindElements(By.XPath($"tbody/tr[{row}]/td[1]//input"));

        public void CheckItem(string Header, string Item)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(HeaderCheckbox);
            actions.Perform();
            int row = GetTableItem(Header, Item);
            CheckAndVerifyTableItem(row, "Update");
            CheckAndVerifyTableItem(row, "Verify");
            Console.WriteLine($"Checkbox for {Header} - {Item} checked and verify successfully..!");
        }

        public int GetTableItem(string Header, string Item)
        {
            int colId = 0;
            int rowId = 0;
            Actions actions = new Actions(driver);
            if (HeaderCheckbox.Enabled)
            {
                actions.MoveToElement(CheckboxTable);
                actions.Perform();
                if (Columns != null)
                {
                    int colCount = Columns.Count;
                    for (int c = 0; c < Columns.Count; c++)
                    {
                        if (!string.IsNullOrEmpty(Columns[c].Text) && Header.Equals(Columns[c].Text))
                        {
                            colId = c + 1;
                            break;
                        }
                    }
                    if (colId > 0)
                    {
                        int rowCount = TRows(colId).Count;
                        for (int r = 0; r < TRows(colId).Count; r++)
                        {
                            string row = TRows(colId)[r].Text;
                            if (!string.IsNullOrEmpty(TRows(colId)[r].Text) && Item.Equals(TRows(colId)[r].Text))
                            {
                                rowId = r + 1;
                                break;
                            }
                        }
                    }
                }
            }
            return rowId;
        }

        public void CheckAndVerifyTableItem(int rowId, string Action)
        {
            int itemCount;
            if (rowId > 0)
            {
                if (Action == "Update")
                {
                    itemCount = ItemCheckBox(rowId).Count;
                    if (itemCount > 0)
                    {
                        ItemCheckBox(rowId)[0].Click();
                        Console.WriteLine("Clicked on checkbox..!");
                    }
                }
                else
                {
                    itemCount = ItemCheckBoxValue(rowId).Count;
                    if (itemCount > 0)
                    {
                        string IsChecked = ItemCheckBoxValue(rowId)[0].GetAttribute("aria-checked");
                        Assert.True(IsChecked.Equals("true"));
                        Console.WriteLine("Checkbox is checked successfully..!");
                    }
                }
            }
        }
    }
}
