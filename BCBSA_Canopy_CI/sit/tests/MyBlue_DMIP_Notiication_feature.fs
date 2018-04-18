module MyBlue_DMIP_Notification_feature
open canopy
open FEPOCEventConsumptionWS

let all _ = 
    context "MyBlue_DMIP_Notification_feature"

    "Given I opened the main page" &&&& fun _ ->
        url ("https://stage.fepblue.org:8443/pilot/login") 
        on ("https://stage.fepblue.org:8443/pilot/login")
    "When I trigger the FEPOC Notification as 'A1C_2ND_HALF_TEST_NOT_COMPLTD' 'R60378908' '14046128'" &&&& fun _ ->
        let statusCode = eventsRequestStatusCode config tokenRequest eventsRequest
        describe (statusCode.ToString())
    "And The 'MyBlue' page is successfully loaded" &&&& fun _ ->
        displayed "Welcome to MyBlue"
    "Given I input a username 'SEAN3'" &&&& fun _ ->
        "#LoginUsername" << "SEAN3"
    "Given I input a password 'Sittest1!'" &&&& fun _ ->
        "#LoginPassword" << "Sittest1!"
    " When I click submit button" &&&& fun _ ->
        click "#login"
    "When I click the link for a different verification way" &&&& fun _ ->
        click "Continue"
        click "Verify your device another way"
    "And I choose use my PIN" &&&& fun _ ->
        click "Use my PIN"
    "And I input the MyBlue PIN code '6128'" &&&& fun _ ->
        ".csr-input" << "6128"
    "And I click the verify button" &&&& fun _ ->
        click "Verify"
    "And I click continue button in the for 4 months dialogue" &&&& fun _ ->
        click "Continue"
        displayed "body > div.modal.fade.ng-scope.ng-isolate-scope.in > div > div > sms-collection-modal > div > div.sms-collection-footer > a.btn.btn-default.add-or-confirm-number"
        click "Not Now"
        notDisplayed "body > div.modal.fade.ng-scope.ng-isolate-scope.in > div > div > sms-collection-modal > div > div.sms-collection-footer > a.btn.btn-default.add-or-confirm-number"
    "When I Click on Notification Icon" &&&& fun _ ->
        hover "#Notification > a > span"
    "And I verify the DMIP Notification Verbiage" &&&& fun _ ->
        displayed "It's time to submit another A1c test result"
        //displayed "iIt's time to submit another A1c test result"

    "And I search for DMIP Notification" &&&& fun _ ->
        click "It's time to submit another A1c test result"
    "Then DMIP Journey Page should be displayed" &&&& fun _ ->
         displayed "Diabetes Management Incentive Program"
    "And I sign off from MyBlue" &&&& fun _ ->
        click "#Username"
        click "Sign Out"
        displayed "Welcome to MyBlue"
