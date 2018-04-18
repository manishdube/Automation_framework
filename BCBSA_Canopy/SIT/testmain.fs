module testmain
open canopy
open canopy.reporters
open System

//let appName = AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "")

[<EntryPoint>]
let main argv =
    configuration.elementTimeout <- 45.0

    Console.SetWindowSize(60, 20);

    let executionTime =  DateTime.Now
    let dtString = executionTime.ToString "yyyy-MM-dd-HHmm"

    let chromeOptions = OpenQA.Selenium.Chrome.ChromeOptions()
    chromeOptions.AddAdditionalCapability("useAutomationExtension", false);

    reporter <- new LiveHtmlReporter((ChromeWithOptions chromeOptions), AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport") :> IReporter
    reporter.setEnvironment "stage.fepblue.org"

    let liveHtmlReporter  = reporter :?> LiveHtmlReporter
    liveHtmlReporter.reportPath <- Some("logs\TestSummary-{dt}".Replace("{dt}", dtString))

    chromeDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
    ieDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
    

    MyBlue_DMIP_Notification_feature.all ()
    MyBlue_SecureMessageCenter_feature.all ()

    let chromeOptions = OpenQA.Selenium.Chrome.ChromeOptions()
    chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
    start <| ChromeWithOptions chromeOptions
    browser.Manage().Window.Maximize()
    run()
    quit()
    canopy.runner.failedCount

   
