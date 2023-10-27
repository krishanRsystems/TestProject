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

        private IWebElement headerCheckbox => driver.FindElement(By.XPath("//h5[contains (text(), 'Checkbox')]"));
        private IWebElement checkboxTable => driver.FindElement(By.XPath("//h5[contains (text(), 'Checkbox')]/following-sibling::div[@class='p-datatable p-component']/div/table"));
        private IList<IWebElement> columns => CheckboxTable.FindElements(By.XPath("thead/tr/th"));
        private IList<IWebElement> tRows(int col) => CheckboxTable.FindElements(By.XPath($"tbody/tr/td[{col}]"));
        private IList<IWebElement> itemCheckBox(int row) => CheckboxTable.FindElements(By.XPath($"tbody/tr[{row}]/td[1]"));
        private IList<IWebElement> itemCheckBoxValue(int row) => CheckboxTable.FindElements(By.XPath($"tbody/tr[{row}]/td[1]//input"));

        public void CheckItem(string header, string item)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(headerCheckbox);
            actions.Perform();
            int row = GetTableItem(header, item);
            CheckAndVerifyTableItem(row, "Update");
            CheckAndVerifyTableItem(row, "Verify");
            Console.WriteLine($"Checkbox for {header} - {item} checked and verify successfully..!");
        }

        public int GetTableItem(string header, string item)
        {
            int colId = 0;
            int rowId = 0;
            Actions actions = new Actions(driver);
            if (headerCheckbox.Enabled)
            {
                actions.MoveToElement(checkboxTable);
                actions.Perform();
                if (columns != null)
                {
                    int colCount = columns.Count;
                    for (int c = 0; c < colCount; c++)
                    {
                        if (!string.IsNullOrEmpty(columns[c].Text) && Header.Equals(columns[c].Text))
                        {
                            colId = c + 1;
                            break;
                        }
                    }
                    if (colId > 0)
                    {
                        int rowCount = tRows(colId).Count;
                        for (int r = 0; r < rowCount; r++)
                        {
                            string row = tRows(colId)[r].Text;
                            if (!string.IsNullOrEmpty(tRows(colId)[r].Text) && Item.Equals(tRows(colId)[r].Text))
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

        public void CheckAndVerifyTableItem(int rowId, string action)
        {
            int itemCount;
            if (rowId > 0)
            {
                if (action == "Update")
                {
                    itemCount = itemCheckBox(rowId).Count;
                    if (itemCount > 0)
                    {
                        itemCheckBox(rowId)[0].Click();
                        Console.WriteLine("Clicked on checkbox..!");
                    }
                }
                else
                {
                    itemCount = itemCheckBoxValue(rowId).Count;
                    if (itemCount > 0)
                    {
                        string isChecked = itemCheckBoxValue(rowId)[0].GetAttribute("aria-checked");
                        Assert.True(isChecked.Equals("true"));
                        Console.WriteLine("Checkbox is checked successfully..!");
                    }
                }
            }
        }
    }
}
