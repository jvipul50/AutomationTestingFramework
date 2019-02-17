using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moodle
{
    public static class Controls
    {
        public static void _takeScreenshot(this IWebDriver driver, string saveLocation)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(saveLocation, ScreenshotImageFormat.Png);
        }

        /// <summary>
        /// Wait for element to be present on page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="when"></param>
        /// <param name="how"></param>
        public static void waitForElement(this IWebDriver driver, string when, string how)
        {
            for (int i = 0; i < 10; i++)
            {
                //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                Thread.Sleep(1000);

                if (driver.isElementPresent(when, how))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Check if the element is present or not
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="when"></param>
        /// <param name="how"></param>
        /// <returns></returns>
        public static bool isElementPresent(this IWebDriver driver, string when, string how)
        {
            bool isDisplayed = false;
            try
            {
                switch (when.ToLower())
                {
                    case "id":
                        isDisplayed = driver.FindElement(By.Id(how)).Displayed;
                        break;

                    case "xpath":
                        isDisplayed = driver.FindElement(By.XPath(how)).Displayed;
                        break;

                    default:
                        break;
                }
            }
            catch (NoSuchElementException)
            {
                isDisplayed = false;
            }
            catch (StaleElementReferenceException)
            {
                isDisplayed = false;
            }

            return isDisplayed;
        }

        /// <summary>
        /// Find the element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="When"></param>
        /// <param name="How"></param>
        /// <returns></returns>
        public static IWebElement findElement(this IWebDriver driver, string When, string How)
        {
            IWebElement element = null;
            for (int i = 0; i <= 10; i++) //instead of 30 written 10 as in 1point project control.cs _findelement()
            {
                try
                {
                    switch (When.ToLower())
                    {
                        case "id":
                            driver.waitForElement(When, How);
                            element = driver.FindElement(By.Id(How));
                            break;

                        case "xpath":
                            driver.waitForElement(When, How);
                            element = driver.FindElement(By.XPath(How));
                            break;
                        
                        default:
                            //element = null;
                            break;
                    }
                }
                catch (NoSuchElementException)
                {
                    //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    Thread.Sleep(2000);
                    continue;
                }
                catch (ElementNotVisibleException)
                {
                    //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    Thread.Sleep(2000);
                    continue;
                }

                if (element.Displayed)
                {
                    break;
                }
                else
                {
                    //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    Thread.Sleep(2000);
                }

            }
            return element;

        }

        /// <summary>
        /// To type in a input field
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="When"></param>
        /// <param name="How"></param>
        /// <param name="TextToInput"></param>
        public static void type(this IWebDriver driver, string When, string How, String TextToInput)
        {
            driver.waitForElement(When, How);
            IWebElement ele = driver.findElement(When, How);
            ele.Clear();
            ele.SendKeys(TextToInput);
        }

        /// <summary>
        /// To click a button
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="When"></param>
        /// <param name="How"></param>
        public static void click(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver.findElement(When, How);
            ele.Click();
        }
    }
}
