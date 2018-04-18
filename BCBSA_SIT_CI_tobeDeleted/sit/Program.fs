module Program

open canopy
open reporters
open Common
open System

[<EntryPoint>]
let main argv =
  ////Parse all the args into the types that we use in the rest of the code
    let args = Args.parse argv
    configuration.chromeDir <- executingDir()
    //configuration.elementTimeout <- 45.0

    //Console.SetWindowSize(60, 20);

    let executionTime =  DateTime.Now
    let dtString = executionTime.ToString "yyyy-MM-dd-HHmm"

    let chromeOptions = OpenQA.Selenium.Chrome.ChromeOptions()
    chromeOptions.AddAdditionalCapability("useAutomationExtension", false);


    //reporter <- new LiveHtmlReporter(Chrome, configuration.chromeDir) :> IReporter
    reporter <- new LiveHtmlReporter((ChromeWithOptions chromeOptions), configuration.chromeDir) :> IReporter
    reporter.setEnvironment "stage.fepblue.org"

    let liveHtmlReporter  = reporter :?> LiveHtmlReporter
    liveHtmlReporter.reportPath <- Some("logs\TestSummary-{dt}".Replace("{dt}", dtString))

    //Start the browser supplied in args
    //start args.Browser
    start <| ChromeWithOptions chromeOptions
    browser.Manage().Window.Maximize()
    //Register the tests that you want to run (under development, a specific page, all tests, etc)
    Tests.register args.Tag args.TestType
    //Run tests
    run()

    //System.Console.ReadLine() |> ignore
    //System.Console.ReadKey() |> ignore
    //Quit all browsers
    quit ()

    //return code
    canopy.runner.failedCount

    